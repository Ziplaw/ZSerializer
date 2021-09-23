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
// [assembly: InternalsVisibleTo("Assembly-CSharp")]

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
                _instance.gameObject.hideFlags = HideFlags.HideAndDontSave;
                DontDestroyOnLoad(_instance.gameObject);
                return _instance;
            }
        }
    }

    public class ZSerialize
    {
        #region Big boys

        internal static int currentGroupID = -1;
        public static bool isSaving;
        internal static (Type, string)[][] tempTuples;
        
        private static List<string> saveFiles;
        //Assemblies in which Unity Components are located
        private static List<string> unityComponentAssemblies = new List<string>();
        //All fields allowed to be added to the Serializable Unity Components list
        private static IEnumerable<Type> serializableComponentTypes;

        
        //Cached methods to be invoked dynamically during serialization
        private static MethodInfo castMethod = typeof(Enumerable).GetMethod("Cast");
        private static MethodInfo toArrayMethod = typeof(Enumerable).GetMethod("ToArray");

        private static MethodInfo saveMethod =
            typeof(ZSerialize).GetMethod(nameof(CompileJson), BindingFlags.NonPublic | BindingFlags.Static);

        private static MethodInfo fromJsonMethod = typeof(JsonHelper).GetMethod(nameof(JsonHelper.FromJson));

        private const string mainAssembly = "Assembly-CSharp";

        //IDs to be stored for InstanceID manipulation when loading destroyed GameObjects
        internal static Dictionary<int, int> idStorage = new Dictionary<int, int>();

        //Every type inheriting from PersistentMonoBehaviour
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
                                            t.GetCustomAttribute<ObsoleteAttribute>() == null &&
                                            t.IsVisible)
                );
            }
        }

        internal static bool PropertyIsSuitableForZSerializer(PropertyInfo fieldInfo)
        {
            return fieldInfo.GetCustomAttribute<ObsoleteAttribute>() == null &&
                   fieldInfo.GetCustomAttribute<NonZSerialized>() == null &&
                   fieldInfo.CanRead &&
                   fieldInfo.CanWrite &&
                   !ZSerializerSettings.Instance.componentBlackList.IsInBlackList(fieldInfo.ReflectedType,
                       fieldInfo.Name) &&
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

            serializableComponentTypes = ComponentSerializableTypes;
        }
        
        /// <summary>
        /// Get a Save Group's ID from its name
        /// </summary>
        /// <param name="name">The name of the SaveGroup</param>
        /// <returns>Save Group's ID</returns>
        public static int NameToSaveGroupID(string name)
        {
            return ZSerializerSettings.Instance.saveGroups.IndexOf(name);
        }
        
        /// <summary>
        /// Get a SaveGroup's name from its ID
        /// </summary>
        /// <param name="id">The ID of the Save Group</param>
        /// <returns>Save Group's Name</returns>
        public static string SaveGroupIDToName(int id)
        {
            return ZSerializerSettings.Instance.saveGroups[id];
        }

        //internal functions to Log stuff for Debug Mode
        internal static void Log(object obj)
        {
            if (ZSerializerSettings.Instance.debugMode) Debug.Log(obj);
        }

        internal static void LogWarning(object obj)
        {
            if (ZSerializerSettings.Instance.debugMode) Debug.LogWarning(obj);
        }

        internal static void LogError(object obj)
        {
            if (ZSerializerSettings.Instance.debugMode) Debug.LogError(obj);
        }

        static string GetCurrentScene()
        {
            return SceneManager.GetActiveScene().name;
        }

        static Object FindObjectFromInstanceID(int instanceID)
        {
            return (Object)typeof(Object)
                .GetMethod("FindObjectFromInstanceID",
                    BindingFlags.NonPublic | BindingFlags.Static)
                ?.Invoke(null, new object[] { instanceID });
        }

        //Gets all the types from a persistentGameObject that are not monobehaviours
        static List<Type> GetAllPersistentComponents(IEnumerable<PersistentGameObject> objects)
        {
            var componentTypes = new List<Type>();

            if (Object.FindObjectOfType<PersistentGameObject>())
                componentTypes.Add(typeof(PersistentGameObject));

            foreach (var persistentGameObject in objects)
            {
                if (ZSerializerSettings.Instance.advancedSerialization)
                {
                    foreach (var serializedComponent in persistentGameObject.serializedComponents)
                    {
                        if (serializedComponent.persistenceType == PersistentType.Everything)
                        {
                            componentTypes.Add(serializedComponent.Type);
                        }
                    }
                }
                else
                {
                    foreach (var component in persistentGameObject.GetComponents<Component>()
                        .Where(c =>
                            c is PersistentGameObject ||
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
            }

            return componentTypes.Distinct().ToList();
        }

        //Dynamically create array of zsavers based on component
        static object[] CreateArrayOfZSerializers(IEnumerable<Component> components, Type componentType)
        {
            var ZSerializerType = componentType;
            if (ZSerializerType == null) return null;
            var ZSerializerArrayType = ZSerializerType.MakeArrayType();


            var ZSerializersArray =
                Activator.CreateInstance(ZSerializerArrayType, new object[] { components.Count() });

            object[] zSavers = (object[])ZSerializersArray;

            var componentsList = components.ToList();

            for (var i = 0; i < zSavers.Length; i++)
            {
                zSavers[i] = Activator.CreateInstance(ZSerializerType, new object[] { componentsList[i] });
            }

            return (object[])ZSerializersArray;
        }

        static object[] OrderPersistentGameObjectsByLoadingOrder(object[] zSavers)
        {
            Type ZSaverType = zSavers.GetType().GetElementType();

            zSavers = zSavers.OrderBy(x =>
                ((GameObjectData)x.GetType().GetField("gameObjectData").GetValue(x)).loadingOrder.x).ThenBy(x =>
                ((GameObjectData)x.GetType().GetField("gameObjectData").GetValue(x)).loadingOrder.y).ToArray();

            MethodInfo cast = castMethod.MakeGenericMethod(new Type[] { ZSaverType });

            MethodInfo toArray = toArrayMethod.MakeGenericMethod(new Type[] { ZSaverType });

            object result = cast.Invoke(zSavers, new object[] { zSavers });

            return (object[])toArray.Invoke(result, new object[] { result });
        }

        //Save using Reflection
        static IEnumerator ReflectedSave(object[] zsavers)
        {
            Type ZSaverType = zsavers.GetType().GetElementType();
            var genericSaveMethodInfo = saveMethod.MakeGenericMethod(ZSaverType);
            genericSaveMethodInfo.Invoke(null, new object[] { zsavers });
            yield return null;
        }

        //Restore the values of a given component from a given ZSerializer
        private static void RestoreValues(Component _component, object ZSerializer)
        {
            ZSerializer.GetType().GetMethod("RestoreValues").Invoke(ZSerializer, new object[] { _component });
        }

        //Updates json files changing a specific string for another
        static void UpdateAllJsonFiles(string[] previousFields, string[] newFields, int[] idList)
        {
            foreach (var i in idList)
            {
                for (int j = 0; j < previousFields.Length; j++)
                {
                    for (var k = 0; k < tempTuples[i].Length; k++)
                    {
                        tempTuples[i][k].Item2 = tempTuples[i][k].Item2
                            .Replace(":" + previousFields[j] + ",", ":" + newFields[j] + ",")
                            .Replace(":" + previousFields[j] + "}", ":" + newFields[j] + "}");
                    }
                }
            }
        }

        //Updates json files for ID manipulation
        static void UpdateAllInstanceIDs(string[] prevInstanceIDs, string[] newInstanceIDs, bool isRestoring = false)
        {
            if (!isRestoring)
            {
                for (int i = 0; i < prevInstanceIDs.Length; i++)
                {
                    RecordTempID(Int32.Parse(prevInstanceIDs[i]), Int32.Parse(newInstanceIDs[i]));
                }
            }

            UpdateAllJsonFiles(prevInstanceIDs, newInstanceIDs, restorationIDList.ToArray());
        }

        #endregion

        #region Save

        static bool PersistentMonoBehaviourShouldBeSerialized(Object persistentMonoBehaviour)
        {
            PersistentMonoBehaviour m = (PersistentMonoBehaviour)persistentMonoBehaviour;
            return (m.GroupID == currentGroupID || currentGroupID == -1) && m.isOn;
        }

        //Saves all Persistent Components
        static IEnumerator SaveAllPersistentMonoBehaviours()
        {
            Dictionary<Type, List<Component>> componentMap = new Dictionary<Type, List<Component>>();

            foreach (var persistentMonoBehaviour in Object.FindObjectsOfType<PersistentMonoBehaviour>()
                .Where(PersistentMonoBehaviourShouldBeSerialized))
            {
                var type = persistentMonoBehaviour.GetType();
                if (!componentMap.ContainsKey(type))
                    componentMap.Add(type, new List<Component>() { persistentMonoBehaviour });
                else componentMap[type].Add(persistentMonoBehaviour);
            }

            foreach (var pair in componentMap)
            {
                yield return ZMono.Instance.StartCoroutine(SerializeComponents(componentMap[pair.Key],
                    pair.Key.Assembly.GetType(pair.Key.Name + "ZSerializer")));
            }
        }

        //Gets all the components of a given type from an array of persistent gameObjects
        static IEnumerable<Component> GetComponentsOfGivenType(IEnumerable<PersistentGameObject> objects,
            Type componentType)
        {
            return objects.SelectMany(o => o.GetComponents(componentType)
                .Where(c =>
                {
                    var selection = o.serializedComponents.FirstOrDefault(sc => sc.instanceID == c.GetInstanceID());

                    return !ZSerializerSettings.Instance.advancedSerialization ||
                           !selection.Equals(default(SerializedComponent)) && selection.persistenceType ==
                           PersistentType.Everything || c is PersistentGameObject;
                }));
        }

        //Saves all persistent GameObjects and all of its attached unity components
        static IEnumerator SaveAllPersistentGameObjects()
        {
            IEnumerable<PersistentGameObject> objects = currentGroupID == -1
                ? Object.FindObjectsOfType<PersistentGameObject>()
                : Object.FindObjectsOfType<PersistentGameObject>().Where(t => t.GroupID == currentGroupID);

            var componentTypes = GetAllPersistentComponents(objects);

            foreach (var componentType in componentTypes)
            {
                yield return ZMono.Instance.StartCoroutine(SerializeComponents(
                    GetComponentsOfGivenType(objects, componentType),
                    Assembly.Load(componentType == typeof(PersistentGameObject)
                        ? "com.Ziplaw.ZSaver.Runtime"
                        : mainAssembly).GetType(componentType.Name + "ZSerializer")));
            }
        }

        //Dynamically serialize a given list of components 
        static IEnumerator SerializeComponents(IEnumerable<Component> components, Type zSaverType)
        {
            if (zSaverType == null) yield return new WaitForEndOfFrame();

            object[] zSavers = CreateArrayOfZSerializers(components, zSaverType);

            if (zSaverType == typeof(PersistentGameObjectZSerializer))
            {
                zSavers = OrderPersistentGameObjectsByLoadingOrder(zSavers);
            }

            if (components.Any()) unityComponentAssemblies.Add(components.ElementAt(0).GetType().Assembly.FullName);

            yield return ZMono.Instance.StartCoroutine(ReflectedSave(zSavers));
        }

        #endregion

        #region Load

        //Loads a new GameObject with the exact same properties as the one which was destroyed
        static void LoadDestroyedGameObject(out GameObject gameObject, Type ZSaverType,
            object zSerializerObject)
        {
            GameObjectData gameObjectData =
                (GameObjectData)ZSaverType.GetField("gameObjectData").GetValue(zSerializerObject);

            gameObject = gameObjectData.MakePerfectlyValidGameObject();
            gameObject.AddComponent<PersistentGameObject>();
        }

        //Loads a component no matter the type
        static void LoadObjectsDynamically(Type ZSaverType, Type componentType, object zSerializerObject)
        {
            GameObject gameObject =
                (GameObject)ZSaverType.GetField("_componentParent").GetValue(zSerializerObject);

            Component componentInGameObject =
                (Component)ZSaverType.GetField("_component").GetValue(zSerializerObject);

            bool isUnityComponent = !typeof(ISaveGroupID).IsAssignableFrom(componentType);

            if (isUnityComponent && (!gameObject ||
                                     gameObject && !gameObject.GetComponent<PersistentGameObject>() ||
                                     gameObject && gameObject.GetComponent<PersistentGameObject>() &&
                                     gameObject.GetComponent<PersistentGameObject>().GroupID != currentGroupID)) return;

            int componentInstanceID =
                (int)ZSaverType.GetField("componentinstanceID").GetValue(zSerializerObject);
            int gameObjectInstanceID =
                (int)ZSaverType.GetField("gameObjectInstanceID").GetValue(zSerializerObject);


            if (componentType != typeof(PersistentGameObject) && gameObject == null)
            {
                gameObject = (GameObject)FindObjectFromInstanceID(gameObjectInstanceID);
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

                    LoadDestroyedGameObject(out gameObject, ZSaverType, zSerializerObject);
                    UpdateAllInstanceIDs(new[] { gameObjectInstanceID.ToString() },
                        new[] { gameObject.GetInstanceID().ToString() });
                }

                if (componentType == typeof(PersistentGameObject))
                {
                    componentInGameObject = gameObject.GetComponent<PersistentGameObject>();
                    var pc = (PersistentGameObject)componentInGameObject;

                    // CopyFieldsToFields(typeof(PersistentGameObjectZSerializer), typeof(PersistentGameObject), pc,
                    //     zSerializerObject);
                    RestoreValues(componentInGameObject, zSerializerObject);

                    if (ZSerializerSettings.Instance.advancedSerialization)
                    {
                        var scList = new List<SerializedComponent>(pc.serializedComponents);
                        pc.serializedComponents.Clear();

                        foreach (var serializedComponent in scList)
                        {
                            if (serializedComponent.persistenceType == PersistentType.Component)
                            {
                                pc.AddComponent(serializedComponent.Type, PersistentType.Component);
                            }
                        }
                    }
                }
                else
                    componentInGameObject = gameObject.GetComponent<PersistentGameObject>()
                        .AddComponent(componentType, PersistentType.Everything);

                if (componentInGameObject is PersistentMonoBehaviour persistentMonoBehaviour)
                    persistentMonoBehaviour.isOn = true;

                if (componentInGameObject == null)
                    Debug.LogError("Achievement Unlocked: No idea how you caused this error");
                componentInstanceID = componentInGameObject.GetInstanceID();
                UpdateAllInstanceIDs(new[] { prevCOMPInstanceID.ToString() }, new[] { componentInstanceID.ToString() });
            }

            if (componentType == typeof(PersistentGameObject))
            {
                GameObjectData gameObjectData =
                    (GameObjectData)ZSaverType.GetField("gameObjectData").GetValue(zSerializerObject);

                gameObject.ApplyValues(gameObjectData);
            }
        }

        static Type GetTypeFromZSerializerType(Type ZSerializerType)
        {
            return ZSerializerType.Assembly.GetType(ZSerializerType.Name.Replace("ZSerializer", "")) ??
                   unityComponentAssemblies
                       .Select(s =>
                           Assembly.Load(s).GetType("UnityEngine." + ZSerializerType.Name.Replace("ZSerializer", "")))
                       .First(t => t != null);
        }

        static void LoadComponents(int tupleID)
        {
            for (var j = 0; j < tempTuples[tupleID].Length; j++)
            {
                var tuple = tempTuples[tupleID][j];
                Type realType = GetTypeFromZSerializerType(tuple.Item1);
                Log("Deserializing " + realType + "s");
                if (realType == null)
                    Debug.LogWarning(
                        "ZSerializer type not found, probably because you added ZSerializer somewhere in the name of the class");

                var fromJson = fromJsonMethod.MakeGenericMethod(tuple.Item1);
                object[] zSerializerObjects = new object[((object[])fromJson.Invoke(null,
                    new object[]
                        { tuple.Item2 })).Length];

                // Debug.LogWarning(tuple.Item1);

                for (var i = 0; i < zSerializerObjects.Length; i++)
                {
                    zSerializerObjects[i] = ((object[])fromJson.Invoke(null,
                        new object[]
                            { tempTuples[tupleID][j].Item2 }))[i];
                    LoadObjectsDynamically(tempTuples[tupleID][j].Item1, realType, zSerializerObjects[i]);
                }

                WriteToFile("components.zsave", GetStringFromTypesAndJson(tempTuples[tupleID]));
            }
        }

        //Loads all references and fields from already loaded objects, this is done like this to avoid data loss
        static void LoadReferences(int tupleID)
        {
            foreach (var tuple in tempTuples[tupleID])
            {
                Type zSerializerType = tuple.Item1;
                // Type realType = GetTypeFromZSerializerType(zSerializerType);
                string json = tuple.Item2;

                var fromJson = fromJsonMethod.MakeGenericMethod(zSerializerType);

                object[] jsonObjects = (object[])fromJson.Invoke(null,
                    new object[]
                        { json });

                for (var i = 0; i < jsonObjects.Length; i++)
                {
                    Component componentInGameObject =
                        (Component)zSerializerType.GetField("_component").GetValue(jsonObjects[i]);

                    RestoreValues(componentInGameObject, jsonObjects[i]);

                    // if (componentInGameObject is PersistentMonoBehaviour ||
                    //     componentInGameObject is PersistentGameObject)
                    //     CopyFieldsToFields(zSerializerType, realType, componentInGameObject, jsonObjects[i]);
                    // else
                    //     CopyFieldsToProperties(realType, componentInGameObject, jsonObjects[i]);
                }
            }
        }

        #endregion

        static int[] GetIDList()
        {
            return saveFiles.Select(f =>
            {
                var split = f.Replace('\\', '/').Split('/');
                return ZSerializerSettings.Instance.saveGroups.IndexOf(split[split.Length - 2]);
            }).ToArray();
        }

        //This gets all persistent gameObject IDS at the beginning of the game, to be stored and manipulated upon scene loads and other events 
        static void RecordAllPersistentIDs()
        {
            var objs = Object.FindObjectsOfType<PersistentGameObject>();

            idStorage = objs.SelectMany(o => o.GetComponents(typeof(Component))
                    .Where(c =>
                        c is PersistentGameObject ||
                        c is PersistentMonoBehaviour ||
                        GetComponentsOfGivenType(objs, c.GetType()).Contains(c)
                    ))
                .ToDictionary(component => component.GetInstanceID(), component => component.GetInstanceID());

            idStorage.Append(
                objs.Select(o => o.gameObject).ToDictionary(o => o.GetInstanceID(), o => o.GetInstanceID()));
        }

        //Records a specific ID to be tracked for later restoration
        static void RecordTempID(int prevID, int newID)
        {
            int key = 0;

            foreach (var pair in idStorage)
            {
                if (pair.Value == prevID)
                {
                    key = pair.Key;
                    break;
                }
            }

            if (key == 0) return;

            idStorage[key] = newID;
        }

        //Restores all temporary IDs to their original value
        static void RestoreTempIDs()
        {
            if (tempTuples != null)
            {
                Log("<color=cyan>Restoring Temporary IDs</color>");

                var originalIDs = idStorage.Select(i => i.Key.ToString()).ToArray();
                var temporaryIDs = idStorage.Select(i => i.Value.ToString()).ToArray();

                UpdateAllJsonFiles(temporaryIDs, originalIDs, restorationIDList.ToArray());

                foreach (var id in restorationIDList)
                {
                    currentGroupID = id;
                    WriteToFile("components.zsave", GetStringFromTypesAndJson(tempTuples[id]));
                }
            }
        }

        

        /// <summary>
        /// Serialize all Persistent components and GameObjects on the current scene to the selected save file
        /// </summary>
        /// <param name="groupID">The ID for the objects you want to save</param>
        public static void SaveAll(int groupID = -1)
        {
            if (isSaving)
            {
                Debug.LogWarning("A save is in progress, use \"isSaving\" to assert if you can save again or not");
                return;
            }

            isSaving = true;
            currentGroupID = groupID;

            if (groupID == -1)
            {
                string[] files = Directory.GetFiles(GetFilePath("", true), "*", SearchOption.AllDirectories);
                foreach (string directory in files)
                {
                    File.Delete(directory);
                }

                string[] directories = Directory.GetDirectories(GetFilePath("", true));
                foreach (string directory in directories)
                {
                    Directory.Delete(directory);
                }
            }

            var persistentMonoBehavioursInScene = Object.FindObjectsOfType<PersistentMonoBehaviour>();

            foreach (var persistentMonoBehaviour in persistentMonoBehavioursInScene)
            {
                persistentMonoBehaviour.OnPreSave();
            }

            if (ZSerializerSettings.Instance.stableSave)
            {
                var e = SaveAllCoroutine(true);
                while (e.MoveNext())
                {
                }
            }
            else
            {
                ZMono.Instance.StartCoroutine(SaveAllCoroutine(false));
            }

            foreach (var persistentMonoBehaviour in persistentMonoBehavioursInScene)
            {
                persistentMonoBehaviour.OnPostSave();
            }
        }

        internal static List<int> restorationIDList = new List<int>();

        //SaveAll() but its actually a coroutine so the game doesn't lag
        static IEnumerator SaveAllCoroutine(bool stableSave)
        {
            bool isSavingAll = currentGroupID == -1;

            int[] idList;
            if (isSavingAll)
                idList = Object.FindObjectsOfType<MonoBehaviour>().Where(o => o is ISaveGroupID)
                    .Select(o => ((ISaveGroupID)o).GroupID).Distinct().ToArray();
            else idList = new[] { currentGroupID };

            jsonToSave = "";

            float startingTime = Time.realtimeSinceStartup;
            float frameCount = Time.frameCount;

            string fileSize = "";

            for (int i = 0; i < idList.Length; i++)
            {
                currentGroupID = idList[i];
                LogWarning("Saving data on Group: " + ZSerializerSettings.Instance.saveGroups[currentGroupID]);


                string[] files = Directory.GetFiles(GetFilePath(""));
                foreach (string file in files)
                {
                    File.Delete(file);
                }

                if (stableSave)
                {
                    var e = SaveAllPersistentGameObjects();
                    while (e.MoveNext())
                    {
                    }

                    e = SaveAllPersistentMonoBehaviours();
                    while (e.MoveNext())
                    {
                    }
                }
                else
                {
                    yield return ZMono.Instance.StartCoroutine(SaveAllPersistentGameObjects());

                    yield return ZMono.Instance.StartCoroutine(SaveAllPersistentMonoBehaviours());
                }

                SaveJsonData("components.zsave");
                CompileJson(unityComponentAssemblies.Distinct().ToArray());
                SaveJsonData("assemblies.zsave");

                fileSize +=
                    $"{ZSerializerSettings.Instance.saveGroups[currentGroupID]}: {new FileInfo(GetFilePath("components.zsave")).Length * .001f} KB";
                if (idList.Length > 1 && i != idList.Length - 1) fileSize += ", ";
            }

            Log("Serialization ended in: " + (Time.realtimeSinceStartup - startingTime) + " seconds or " +
                (Time.frameCount - frameCount) + " frames. (" + fileSize + ")");
            currentGroupID = -1;
            isSaving = false;

            FillTemporaryJsonTuples(idList);
        }

        static void FillTemporaryJsonTuples(int[] idList)
        {
            if (tempTuples == null || tempTuples.Length == 0)
                tempTuples = new (Type, string)[ZSerializerSettings.Instance.saveGroups
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Max(sg => ZSerializerSettings.Instance.saveGroups.IndexOf(sg)) + 1][];

            foreach (var i in idList)
            {
                currentGroupID = i;
                tempTuples[i] = ReadFromFile("components.zsave");
            }
        }

        /// <summary>
        /// Load all Persistent components and GameObjects from the current scene that have been previously serialized in the current save file
        /// </summary>
        public static void LoadAll(int groupID = -1)
        {
            saveFiles ??= Directory.GetFiles(GetFilePath("", true), "components.zsave",
                SearchOption.AllDirectories).ToList();

            currentGroupID = groupID;
            bool isLoadingAll = currentGroupID == -1;
            Log(isLoadingAll ? "Loading All Data" : "Loading Group " + currentGroupID);


            int[] idList;
            if (isLoadingAll)
            {
                idList = GetIDList();
            }
            else
            {
                idList = new[] { currentGroupID };
            }

            restorationIDList.AddRange(idList);
            restorationIDList = restorationIDList.Distinct().ToList();
            FillTemporaryJsonTuples(idList);

            currentGroupID = groupID;

            for (int i = 0; i < idList.Length; i++)
            {
                currentGroupID = idList[i];
                LogWarning("Loading Group in disk: " + ZSerializerSettings.Instance.saveGroups[currentGroupID]);

                var persistentMonoBehavioursInScene = Object.FindObjectsOfType<PersistentMonoBehaviour>();

                foreach (var persistentMonoBehaviour in persistentMonoBehavioursInScene)
                {
                    persistentMonoBehaviour.OnPreLoad();
                }

                float startingTime = Time.realtimeSinceStartup;
                float frameCount = Time.frameCount;


                unityComponentAssemblies =
                    JsonHelper.FromJson<string>(ReadFromFile("assemblies.zsave")[0].Item2).ToList();


                LoadComponents(currentGroupID);
                LoadReferences(currentGroupID);

                Log(
                    $"Deserialization of group \"{ZSerializerSettings.Instance.saveGroups[currentGroupID]}\" ended in: " +
                    (Time.realtimeSinceStartup - startingTime) + " seconds or " +
                    (Time.frameCount - frameCount) + " frames");

                foreach (var persistentMonoBehaviour in persistentMonoBehavioursInScene)
                {
                    persistentMonoBehaviour.OnPostLoad();
                }
            }


            currentGroupID = -1;
        }


        #region JSON Formatting

        private static string jsonToSave;

        //Saves an array of objects to a file
        static void CompileJson<T>(T[] objectsToPersist)
        {
            string json = "{" + typeof(T).AssemblyQualifiedName + "}" + JsonHelper.ToJson(objectsToPersist, false);
            Log("Serializing: " + typeof(T) + " " + json);
            jsonToSave += json + "\n";
            // WriteToFile(fileName, json, useGlobalID);
        }

        static void SaveJsonData(string fileName, bool useGlobalID = false)
        {
            WriteToFile(fileName, jsonToSave, useGlobalID);
            jsonToSave = "";
        }

        //Writes json into file
        static void WriteToFile(string fileName, string json, bool useGlobalID = false)
        {
            if (ZSerializerSettings.Instance.encryptData)
            {
                byte[] key =
                    { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

                File.WriteAllBytes(GetFilePath(fileName, useGlobalID),
                    EncryptStringToBytes(json, key, key)); // this is reverted because of naming shenanigans
            }
            else
            {
                File.WriteAllText(GetFilePath(fileName, useGlobalID), json);
            }
        }

        //Reads json from file
        static (Type, string)[] ReadFromFile(string fileName, bool useGlobalID = false)
        {
            if (!File.Exists(GetFilePath(fileName, useGlobalID)))
            {
                Debug.LogWarning(
                    $"You attempted to load a file that didn't exist ({GetFilePath(fileName, useGlobalID)}), this may be caused by trying to load a save file without having it saved first");

                return null;
            }

            if (ZSerializerSettings.Instance.encryptData)
            {
                byte[] key =
                    { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
                return GetTypesAndJsonFromString(
                    DecryptStringFromBytes(File.ReadAllBytes(GetFilePath(fileName, useGlobalID)), key, key));
            }

            return GetTypesAndJsonFromString(File.ReadAllText(GetFilePath(fileName, useGlobalID)));
        }

        static (Type, string)[] GetTypesAndJsonFromString(string modifiedJson)
        {
            var strings = modifiedJson.Split('\n');
            (Type, string)[] tuples = new (Type, string)[strings.Length - 1];

            for (int i = 0; i < strings.Length - 1; i++)
            {
                string[] parts = strings[i].Split('}');
                var typeName = parts[0].Replace("{", "");
                parts[0] = "";
                var json = String.Join("}", parts).Remove(0, 1);
                tuples[i] = (Type.GetType(typeName), json);
            }

            return tuples;
        }

        static string GetStringFromTypesAndJson(IEnumerable<(Type, string)> tuples)
        {
            return String.Join("",
                tuples.Select(t => "{" + t.Item1.AssemblyQualifiedName + "}" + t.Item2 + "\n"));
        }

        //Gets complete filepath for a specific filename
        static string GetFilePath(string fileName, bool useGlobalID = false)
        {
            string currentScene = GetCurrentScene();
            if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings)
            {
                LogWarning(
                    "Be careful! You're trying to save data in an unbuilt Scene, and any data saved in other unbuilt Scenes will overwrite this one, and vice-versa.\n" +
                    "If you want your data to persist properly, add this scene to the list of Scenes In Build in your Build Settings");
            }

            string path = useGlobalID
                ? Path.Combine(
                    Application.persistentDataPath,
                    "SaveFile-" + ZSerializerSettings.Instance.selectedSaveFile,
                    currentScene)
                : Path.Combine(
                    Application.persistentDataPath,
                    "SaveFile-" + ZSerializerSettings.Instance.selectedSaveFile,
                    currentScene,
                    ZSerializerSettings.Instance.saveGroups[currentGroupID]);


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
        public static void Append<T1, T2>(this Dictionary<T1, T2> first, Dictionary<T1, T2> second)
        {
            List<KeyValuePair<T1, T2>> pairs = second.ToList();
            pairs.ForEach(pair => first.Add(pair.Key, pair.Value));
        }
    }
}