using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

[assembly: InternalsVisibleTo("com.Ziplaw.ZSaver.Editor")]

namespace ZSerializer
{
    //MonoBehaviour used to manage the Saving Coroutines
    public class ZMono : MonoBehaviour
    {
        private static ZMono _instance;

        public static ZMono Instance
        {
            get
            {
                if (_instance) return _instance;
                _instance = new GameObject("ZMono").AddComponent<ZMono>();
                DontDestroyOnLoad(_instance.gameObject);
                return _instance;
            }
        }
    }

    public class ZSave
    {
        #region Big boys 

        //Cached methods to be invoked dynamically during serialization
        private static MethodInfo castMethod = typeof(Enumerable).GetMethod("Cast");
        private static MethodInfo toArrayMethod = typeof(Enumerable).GetMethod("ToArray");
        private static MethodInfo saveMethod = typeof(ZSave).GetMethod(nameof(Save), BindingFlags.NonPublic | BindingFlags.Static);
        private static MethodInfo fromJsonMethod = typeof(JsonHelper).GetMethod(nameof(JsonHelper.FromJson));
        
        internal const string mainAssembly = "Assembly-CSharp";
        
        //IDs to be stored for InstanceID manipulation when loading destroyed GameObjects
        static Dictionary<int, int> idStorage = new Dictionary<int, int>();
        //Every type marked with [Persistent]
        private static Type[] persistentTypes;
        internal static IEnumerable<Type> GetPersistentTypes()
        {
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies();

            foreach (var assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(PersistentMonoBehaviour)))
                    {
                        yield return type;
                    }
                }
            }
        }
        //Assemblies in which Unity Components are located
        private static List<string> unityComponentAssemblies = new List<string>();
        //All fields allowed to be added to the Serializable Unity Components list
        private static IEnumerable<Type> serializableComponentTypes;
        internal static IEnumerable<Type> ComponentSerializableTypes
        {
            get
            {
                return serializableComponentTypes ?? AppDomain.CurrentDomain.GetAssemblies(
                ).SelectMany(a =>
                    a.GetTypes().Where(t => t == typeof(PersistentGameObject) ||
                                            t.IsSubclassOf(typeof(Component)) &&
                                            !t.IsSubclassOf(typeof(MonoBehaviour)) &&
                                            t != typeof(Transform) &&
                                            t != typeof(MonoBehaviour) &&
                                            t.GetCustomAttribute<ObsoleteAttribute>() == null && t.IsVisible)
                );
            }
        }
        
        //Returns the current save file's stored components.
        static IEnumerable<Type> GetSerializedComponentsInCurrentSaveFile()
        {
            foreach (var fileName in Directory.GetFiles(GetFilePath("")))
            {
                string typeName = fileName.Split('\\').Last().Replace(".zsave", "");

                Assembly assembly = unityComponentAssemblies.Select(Assembly.Load)
                    .FirstOrDefault(a => a.GetType(typeName) != null);
                if (assembly == null) continue;
                yield return Type.GetType($"{typeName}, {assembly}");
            }
        }

        //returns if the current property is to be assigned when loading
        internal static bool PropertyIsSuitableForAssignment(PropertyInfo fieldInfo)
        {
            SerializableComponentBlackList blackList =
                ZSaverSettings.Instance.componentBlackList.FirstOrDefault(c => c.Type == fieldInfo.DeclaringType);
            bool isInBlackList = blackList != null;

            return fieldInfo.GetCustomAttribute<ObsoleteAttribute>() == null &&
                   fieldInfo.GetCustomAttribute<NonZSerialized>() == null &&
                   fieldInfo.CanRead &&
                   fieldInfo.CanWrite &&
                   (
                       (!isInBlackList) || (
                           isInBlackList &&
                           !blackList.componentNames.Contains(fieldInfo.Name))
                   ) &&
                   fieldInfo.Name != "material" &&
                   fieldInfo.Name != "materials" &&
                   fieldInfo.Name != "sharedMaterial" &&
                   fieldInfo.Name != "mesh" &&
                   fieldInfo.Name != "tag" &&
                   fieldInfo.Name != "name";
        }
        #endregion

        #region HelperFunctions

        [RuntimeInitializeOnLoadMethod]
        internal static void Init() //This runs when the game starts, it sets up Instance ID restoration for scene loading

        {
            RecordAllPersistentIDs();

            SceneManager.sceneUnloaded += scene => { RestoreTempIDs(); };

            SceneManager.sceneLoaded += (scene, mode) => { RecordAllPersistentIDs(); };

            Application.wantsToQuit += () =>
            {
                RestoreTempIDs();
                return true;
            };

            persistentTypes = GetPersistentTypes().ToArray();
            serializableComponentTypes = ComponentSerializableTypes;
        }

        //internal functions to Log stuff for Debug Mode
        internal static void Log(object obj)
        {
            if (ZSaverSettings.Instance.debugMode) Debug.Log(obj);
        }

        internal static void LogWarning(object obj)
        {
            if (ZSaverSettings.Instance.debugMode) Debug.LogWarning(obj);
        }

        internal static void LogError(object obj)
        {
            if (ZSaverSettings.Instance.debugMode) Debug.LogError(obj);
        }
    
        static int GetCurrentScene()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        static Object FindObjectFromInstanceID(int instanceID)
        {
            return (Object) typeof(Object)
                .GetMethod("FindObjectFromInstanceID",
                    BindingFlags.NonPublic | BindingFlags.Static)
                ?.Invoke(null, new object[] {instanceID});
        }

        internal static Type FindTypeInsideAssemblies(Assembly[] assemblies, string typeName)
        {
            var assembly = assemblies.First(a => a.GetType(typeName) != null);
            return assembly.GetType(typeName);
        }
        
        //Gets all the types from a persistentGameObject that are not monobehaviours
        static List<Type> GetAllPersistentComponents(PersistentGameObject[] objects)
        {
            var componentTypes = new List<Type>();

            foreach (var persistentGameObject in objects)
            {
                foreach (var component in persistentGameObject.GetComponents<Component>()
                    .Where(c =>
                        c.GetType() == typeof(PersistentGameObject) ||
                        !c.GetType().IsSubclassOf(typeof(MonoBehaviour)) &&
                        c.GetType() != typeof(Transform)
                    ))
                {
                    if (!componentTypes.Contains(component.GetType()))
                    {
                        componentTypes.Add(component.GetType());
                    }
                }
            }

            return componentTypes;
        }

        //Dynamically create array of zsavers based on component
        static object[] CreateArrayOfZSavers(Component[] components, Type componentType)
        {
            var ZSaverType = componentType;
            if (ZSaverType == null) return null;
            var ZSaverArrayType = ZSaverType.MakeArrayType();


            var zSaversArray =
                Activator.CreateInstance(ZSaverArrayType, new object[] {components.Length});

            object[] zSavers = (object[]) zSaversArray;

            for (var i = 0; i < zSavers.Length; i++)
            {
                zSavers[i] = Activator.CreateInstance(ZSaverType, new object[] {components[i]});
            }

            return (object[]) zSaversArray;
        }

        static object[] OrderPersistentGameObjectsByLoadingOrder(object[] zSavers)
        {
            Type ZSaverType = zSavers.GetType().GetElementType();

            zSavers = zSavers.OrderBy(x =>
                ((GameObjectData) x.GetType().GetField("gameObjectData").GetValue(x)).loadingOrder).ToArray();

            MethodInfo cast = castMethod.MakeGenericMethod(new Type[] {ZSaverType});

            MethodInfo toArray = toArrayMethod.MakeGenericMethod(new Type[] {ZSaverType});

            object result = cast.Invoke(zSavers, new object[] {zSavers});

            return (object[]) toArray.Invoke(result, new object[] {result});
        }

        //Save using Reflection
        static IEnumerator ReflectedSave(object[] zsavers, string fileName)
        {
            Type ZSaverType = zsavers.GetType().GetElementType();
            var genericSaveMethodInfo = saveMethod.MakeGenericMethod(ZSaverType);
            genericSaveMethodInfo.Invoke(null, new object[] {zsavers, fileName});
            yield return new WaitForEndOfFrame();
        }

        //Copies the fields of a ZSerializer to the fields of a component
        static void CopyFieldsToFields(Type zSaverType, Type componentType, Component _component, object zSaver)
        {
            FieldInfo[] zSaverFields = zSaverType.GetFields();
            FieldInfo[] componentFields = componentType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            for (var i = 0; i < zSaverFields.Length; i++)
            {
                var fieldInfo = componentFields.FirstOrDefault(f => f.Name == zSaverFields[i].Name);
                fieldInfo?.SetValue(_component, zSaverFields[i].GetValue(zSaver));
            }
        }
        
        //Copies the fields of a ZSerializer to the properties of a component
        static void CopyFieldsToProperties(Type componentType, Component c, object FromJSONdObject)
        {
            IEnumerable<PropertyInfo> propertyInfos = componentType.GetProperties()
                .Where(PropertyIsSuitableForAssignment);

            FieldInfo[] fieldInfos = FromJSONdObject.GetType().GetFields();

            for (var i = 0; i < fieldInfos.Length; i++)
            {
                var propertyInfo = propertyInfos.FirstOrDefault(p => p.Name == fieldInfos[i].Name);
                propertyInfo?.SetValue(c, fieldInfos[i].GetValue(FromJSONdObject));
            }
        }

        //Updates json files changing a specific string for another
        static void UpdateAllJSONFiles(string[] previousFields, string[] newFields, bool isRestoring = false)
        {
            foreach (var file in Directory.GetFiles(GetFilePath(""), "*.zsave",
                SearchOption.AllDirectories))
            {
                string fileName = file.Split('\\').Last();

                string json = ReadFromFile(fileName);
                string newJson = json;
                for (int i = 0; i < previousFields.Length; i++)
                {
                    newJson = newJson.Replace(previousFields[i], newFields[i]);
                }

                WriteToFile(fileName, newJson);
            }
        }
        //Updates json files for ID manipulation
        static void UpdateAllInstanceIDs(int prevInstanceID, int newInstanceID,
            bool isRestoring = false)
        {
            if (!isRestoring)
            {
                RecordTempID(prevInstanceID, newInstanceID);
            }
            UpdateAllJSONFiles(new[] {prevInstanceID.ToString()}, new[] {newInstanceID.ToString()}, isRestoring);
        }
        #endregion

        #region Save
        //Saves all Persistent Components
        static IEnumerator SaveAllObjects()
        {
            var types = persistentTypes.Where(t => Object.FindObjectOfType(t) != null);

            foreach (var type in types)
            {
                var objects = Object.FindObjectsOfType(type);
                yield return SerializeComponents((Component[]) objects,
                    type.Assembly.GetType(type.Name + "ZSerializer"),
                    type.FullName + ".zsave");
            }
        }
        //Gets all the components of a given type from an array of persistent gameobjects
        static Component[] GetComponentsOfGivenType(PersistentGameObject[] objects, Type componentType)
        {
            List<Component> serializedComponentsOfGivenType = new List<Component>();

            var componentsOfGivenType = objects.SelectMany(o => o.GetComponents(componentType));

            foreach (var c in componentsOfGivenType)
            {
                serializedComponentsOfGivenType.Add(c);
            }

            return serializedComponentsOfGivenType.ToArray();
        }
        //Saves all persistent GameObjects and all of its attached unity components
        static IEnumerator SaveAllPersistentGameObjects()
        {
            var objects = Object.FindObjectsOfType<PersistentGameObject>();
            var componentTypes = GetAllPersistentComponents(objects);

            foreach (var componentType in componentTypes)
            {
                yield return SerializeComponents(GetComponentsOfGivenType(objects, componentType),
                    Assembly.Load(componentType == typeof(PersistentGameObject)
                        ? "com.Ziplaw.ZSaver.Runtime"
                        : mainAssembly).GetType(componentType.Name + "ZSerializer"),
                    componentType.FullName + ".zsave");
            }
        }
        //Dynamically serialize a given list of components 
        static IEnumerator SerializeComponents(Component[] components, Type zSaverType, string fileName)
        {
            if (zSaverType == null) yield return new WaitForEndOfFrame();

            object[] zSavers = CreateArrayOfZSavers(components, zSaverType);

            if (zSaverType == typeof(PersistentGameObjectZSerializer))
            {
                zSavers = OrderPersistentGameObjectsByLoadingOrder(zSavers);
            }

            unityComponentAssemblies.Add(components[0].GetType().Assembly.FullName);

            yield return ReflectedSave(zSavers, fileName);
        }

        #endregion

        #region Load
        
        //Loads a new GameObject with the exact same properties as the one which was destroyed
        static void LoadDestroyedGameObject(int gameObjectInstanceID, out GameObject gameObject, Type ZSaverType,
            object FromJSONdObject)
        {
            int prevGOInstanceID = gameObjectInstanceID;

            GameObjectData gameObjectData =
                (GameObjectData) ZSaverType.GetField("gameObjectData").GetValue(FromJSONdObject);

            gameObject = gameObjectData.MakePerfectlyValidGameObject();
            gameObject.AddComponent<PersistentGameObject>();
            gameObjectInstanceID = gameObject.GetInstanceID();

            UpdateAllInstanceIDs(prevGOInstanceID, gameObjectInstanceID);
        }

        //Loads a component no matter the type
        static void LoadObjectsDynamically(Type ZSaverType, Type componentType, object FromJSONdObject)
        {
            GameObject gameObject =
                (GameObject) ZSaverType.GetField("_componentParent").GetValue(FromJSONdObject);

            Component componentInGameObject =
                (Component) ZSaverType.GetField("_component").GetValue(FromJSONdObject);

            int componentInstanceID =
                (int) ZSaverType.GetField("componentinstanceID").GetValue(FromJSONdObject);
            int gameObjectInstanceID =
                (int) ZSaverType.GetField("gameObjectInstanceID").GetValue(FromJSONdObject);

            if (componentType != typeof(PersistentGameObject) && gameObject == null)
            {
                gameObject = (GameObject) FindObjectFromInstanceID(gameObjectInstanceID);
            }


            if (componentInGameObject == null)
            {
                int prevCOMPInstanceID = componentInstanceID;

                if (gameObject == null)
                {
                    if (componentType != typeof(PersistentGameObject))
                    {
                        LogWarning(
                            $"GameObject holding {componentType} was destroyed, add the Persistent GameObject component to said GameObject if persistence was intended");
                        return;
                    }

                    LoadDestroyedGameObject(gameObjectInstanceID, out gameObject, ZSaverType, FromJSONdObject);
                }

                if (componentType == typeof(PersistentGameObject))
                    componentInGameObject = gameObject.GetComponent<PersistentGameObject>();
                else componentInGameObject = gameObject.AddComponent(componentType);
                if (componentInGameObject == null) return;
                componentInstanceID = componentInGameObject.GetInstanceID();
                UpdateAllInstanceIDs(prevCOMPInstanceID, componentInstanceID);
            }

            if (componentType == typeof(PersistentGameObject))
            {
                GameObjectData gameObjectData =
                    (GameObjectData) ZSaverType.GetField("gameObjectData").GetValue(FromJSONdObject);

                gameObject.transform.position = gameObjectData.position;
                gameObject.transform.rotation = gameObjectData.rotation;
                gameObject.transform.localScale = gameObjectData.size;
            }
        }
        //Loads all components marked with [Persistent]
        static void LoadAllObjects()
        {
            var types = persistentTypes;

            foreach (var type in types)
            {
                var ZSaverType = type.Assembly.GetType(type.Name + "ZSerializer");
                if (ZSaverType == null)
                {
                    LogWarning($"Couldn't find ZSerializer for {type}");
                    continue;
                }

                var fromJson = fromJsonMethod.MakeGenericMethod(ZSaverType);

                if (!File.Exists(GetFilePath(type.FullName + ".zsave"))) continue;

                object[] FromJSONdObjects = (object[]) fromJson.Invoke(null,
                    new object[] {ReadFromFile(type.FullName + ".zsave")});

                for (var i = 0; i < FromJSONdObjects.Length; i++)
                {
                    FromJSONdObjects[i] = ((object[]) fromJson.Invoke(null,
                        new object[] {ReadFromFile(type.FullName + ".zsave")}))[i];
                    LoadObjectsDynamically(ZSaverType, type, FromJSONdObjects[i]);
                }
            }
        }
        //Loads all Persistent GameObjects and its respective components
        static void LoadAllPersistentGameObjects()
        {
            var types = GetSerializedComponentsInCurrentSaveFile()
                .OrderByDescending(x => x == typeof(PersistentGameObject));

            foreach (var type in types)
            {
                if (!File.Exists(GetFilePath(type.FullName + ".zsave"))) continue;
                var ZSaverType = Assembly
                    .Load(type == typeof(PersistentGameObject) ? "com.Ziplaw.ZSaver.Runtime" : mainAssembly)
                    .GetType(type.Name + "ZSerializer");
                if (ZSaverType == null) continue;

                var fromJson = fromJsonMethod.MakeGenericMethod(ZSaverType);


                object[] FromJSONdObjects = (object[]) fromJson.Invoke(null,
                    new object[]
                        {ReadFromFile(type.FullName + ".zsave")});

                for (var i = 0; i < FromJSONdObjects.Length; i++)
                {
                    FromJSONdObjects[i] = ((object[]) fromJson.Invoke(null,
                        new object[] {ReadFromFile(type.FullName + ".zsave")}))[i];
                    LoadObjectsDynamically(ZSaverType, type, FromJSONdObjects[i]);
                }
            }
        }
        //Loads all references and fields from already loaded objects, this is done like this to avoid data loss
        static void LoadReferences()
        {
            IEnumerable<Type> types = GetSerializedComponentsInCurrentSaveFile()
                .OrderByDescending(x => x == typeof(PersistentGameObject));

            foreach (var type in types)
            {
                if (!File.Exists(GetFilePath(type.FullName + ".zsave"))) continue;

                var ZSaverType = type.AssemblyQualifiedName.Contains("UnityEngine.")
                    ? Assembly.Load(mainAssembly).GetType(type.Name + "ZSerializer")
                    : type.Assembly.GetType(type.Name + "ZSerializer");


                if (ZSaverType == null) continue;
                var fromJson = fromJsonMethod.MakeGenericMethod(ZSaverType);


                object[] FromJSONdObjects = (object[]) fromJson.Invoke(null,
                    new object[]
                        {ReadFromFile(type.FullName + ".zsave")});

                for (var i = 0; i < FromJSONdObjects.Length; i++)
                {
                    Component componentInGameObject =
                        (Component) ZSaverType.GetField("_component").GetValue(FromJSONdObjects[i]);

                    CopyFieldsToProperties(type, componentInGameObject, FromJSONdObjects[i]);
                    CopyFieldsToFields(ZSaverType, type, componentInGameObject, FromJSONdObjects[i]);
                }
            }

            types = persistentTypes;

            foreach (var type in types)
            {
                var ZSaverType = Type.GetType(type.Name + "ZSerializer, " + mainAssembly);
                if (ZSaverType == null) continue;
                var fromJson = fromJsonMethod.MakeGenericMethod(ZSaverType);

                if (!File.Exists(GetFilePath(type.FullName + ".zsave"))) continue;

                object[] FromJSONdObjects = (object[]) fromJson.Invoke(null,
                    new object[] {ReadFromFile(type.FullName + ".zsave")});

                for (var i = 0; i < FromJSONdObjects.Length; i++)
                {
                    Component componentInGameObject =
                        (Component) ZSaverType.GetField("_component").GetValue(FromJSONdObjects[i]);

                    CopyFieldsToProperties(type, componentInGameObject, FromJSONdObjects[i]);
                    CopyFieldsToFields(ZSaverType, type, componentInGameObject, FromJSONdObjects[i]);
                }
            }
        }

        #endregion
        //This gets all persistent gameObject IDS at the beginning of the game, to be stored and manipulated upon scene loads and other events 
        static void RecordAllPersistentIDs()
        {
            var objs = Object.FindObjectsOfType<PersistentGameObject>();


            idStorage = objs.SelectMany(o => o.GetComponents(typeof(Component))
                    .Where(c =>
                        c.GetType() == typeof(PersistentGameObject) ||
                        c.GetType() is PersistentMonoBehaviour ||
                        GetComponentsOfGivenType(objs, c.GetType()).Contains(c)
                    ))
                .ToDictionary(component => component.GetInstanceID(), component => component.GetInstanceID());

            idStorage.Append(
                objs.Select(o => o.gameObject).ToDictionary(o => o.GetInstanceID(), o => o.GetInstanceID()));
        }
        //Records a specific ID to be tracked for later restoration
        static void RecordTempID(int prevID, int newID)
        {
            for (var i = 0; i < idStorage.Count; i++)
            {
                if (idStorage[idStorage.Keys.ElementAt(i)] == prevID)
                {
                    idStorage[idStorage.Keys.ElementAt(i)] = newID;
                }
            }
        }
        //Restores all termporary IDs to its original value
        static void RestoreTempIDs()
        {
            Log("Restoring Temporary IDs");

            for (var i = 0; i < idStorage.Count; i++)
            {
                UpdateAllInstanceIDs(idStorage[idStorage.Keys.ElementAt(i)], idStorage.Keys.ElementAt(i), true);
            }
        }

        /// <summary>
        /// Serialize all Persistent components and GameObjects on the current scene to the selected save file
        /// </summary>
        public static void SaveAll()
        {
            var persistentMonoBehavioursInScene = Object.FindObjectsOfType<PersistentMonoBehaviour>();
            
            foreach (var persistentMonoBehaviour in persistentMonoBehavioursInScene)
            {
                persistentMonoBehaviour.OnPreSave();
            }

            if (ZSaverSettings.Instance.stableSave)
            {
                var e = SaveAllCoroutine();
                while (e.MoveNext()){ }
            }
            else
            {
                ZMono.Instance.StartCoroutine(SaveAllCoroutine());
            }
            
            foreach (var persistentMonoBehaviour in persistentMonoBehavioursInScene)
            {
                persistentMonoBehaviour.OnPostSave();
            }
        }

        //SaveAll() but its actually a coroutine so the game doesnt lag
        static IEnumerator SaveAllCoroutine()
        {
            float startingTime = Time.realtimeSinceStartup;
            float frameCount = Time.frameCount;

            yield return null;

            string[] files = Directory.GetFiles(GetFilePath(""));
            foreach (string file in files)
            {
                File.Delete(file);
            }

            yield return ZMono.Instance.StartCoroutine(SaveAllPersistentGameObjects());

            yield return ZMono.Instance.StartCoroutine(SaveAllObjects());

            yield return null;

            Save(unityComponentAssemblies.Distinct().ToArray(), "assemblies.zsave");

            Log("Serialization ended in: " + (Time.realtimeSinceStartup - startingTime) + " seconds or " +
                (Time.frameCount - frameCount) + " frames");
        }

        /// <summary>
        /// Load all Persistent components and GameObjects from the current scene that have been previously serialized in the current save file
        /// </summary>
        public static void LoadAll()
        {
            var persistentMonoBehavioursInScene = Object.FindObjectsOfType<PersistentMonoBehaviour>();
            
            foreach (var persistentMonoBehaviour in persistentMonoBehavioursInScene)
            {
                persistentMonoBehaviour.OnPreLoad();
            }
            
            float startingTime = Time.realtimeSinceStartup;
            float frameCount = Time.frameCount;

            unityComponentAssemblies =
                JsonHelper.FromJson<string>(ReadFromFile(GetFilePath("assemblies.zsave"))).ToList();

            LoadAllPersistentGameObjects();
            LoadAllObjects();
            LoadReferences();

            Log("Deserialization ended in: " + (Time.realtimeSinceStartup - startingTime) + " seconds or " +
                (Time.frameCount - frameCount) + " frames");
            
            foreach (var persistentMonoBehaviour in persistentMonoBehavioursInScene)
            {
                persistentMonoBehaviour.OnPostLoad();
            }
        }


        #region JSON Formatting

        //Saves an array of objects to a file
        static void Save<T>(T[] objectsToPersist, string fileName)
        {
            string json = JsonHelper.ToJson(objectsToPersist, false);
            Log(typeof(T) + " " + json);
            WriteToFile(fileName, json);
        }
        
        //Writes json into file
        static void WriteToFile(string fileName, string json)
        {
            if (ZSaverSettings.Instance.encryptData)
            {
                byte[] key =
                    {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F};

                File.WriteAllBytes(GetFilePath(fileName), EncryptStringToBytes(json, key, key));
            }
            else
            {
                File.WriteAllText(GetFilePath(fileName), json);
            }
        }
        //Reads json from file
        static string ReadFromFile(string fileName)
        {
            if (!File.Exists(GetFilePath(fileName)))
            {
                Debug.LogWarning(
                    "You attempted to load a file that didn't exist, this may be caused by trying to load a save file without having it saved first");
                return null;
            }

            if (ZSaverSettings.Instance.encryptData)
            {
                byte[] key =
                    {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F};

                return DecryptStringFromBytes(File.ReadAllBytes(GetFilePath(fileName)), key, key);
            }

            return File.ReadAllText(GetFilePath(fileName));
        }
        
        //Gets complete filepath for a specific filename
        internal static string GetFilePath(string fileName)
        {
            int currentScene = GetCurrentScene();
            if (currentScene == SceneManager.sceneCountInBuildSettings)
            {
                LogWarning(
                    "Be careful! You're trying to save data in an unbuilt Scene, and any data saved in other unbuilt Scenes will overwrite this one, and vice-versa.\n" +
                    "If you want your data to persist properly, add this scene to the list of Scenes In Build in your Build Settings");
            }

            string path = Path.Combine(Application.persistentDataPath,
                ZSaverSettings.Instance.selectedSaveFile.ToString(), currentScene.ToString());
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            return Path.Combine(path, fileName);
        }

        #endregion

        #region Encrypting
        
        //Encripts json to bytes
        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }
        //Decrypts json from bytes

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        #endregion
    }
    //Class to help with saving arrays with jsonutility
    static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array, bool prettyPrint = false)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

    public static class LINQExtensions
    {
        public static void Append<K, V>(this Dictionary<K, V> first, Dictionary<K, V> second)
        {
            List<KeyValuePair<K, V>> pairs = second.ToList();
            pairs.ForEach(pair => first.Add(pair.Key, pair.Value));
        }
    }
}