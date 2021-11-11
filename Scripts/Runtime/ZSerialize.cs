using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZSerializer.Internal;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[assembly: InternalsVisibleTo("com.Ziplaw.ZSaver.Editor")]
// [assembly: InternalsVisibleTo("Assembly-CSharp")]

namespace ZSerializer
{
    public sealed class ZSerialize
    {
        #region Variables

        internal static int currentGroupID = -1;
        private static string currentScene;
        private static string persistentDataPath;
        private static string _currentLevelName;
        private static Transform _currentParent;
        private static string jsonToSave;
        public static Dictionary<string, Object> idMap = new Dictionary<string, Object>();

        internal static (Type, string)[][] tempTuples;

        private static List<string> saveFiles;

        //Assemblies in which Unity Components are located
        private static List<string> unityComponentAssemblies = new List<string>();

        //All fields allowed to be added to the Serializable Unity Components list
        private static List<Type> unitySerializableTypes;
        internal static List<Type> UnitySerializableTypes => unitySerializableTypes ??= GetUnitySerializableTypes();

        private static MethodInfo saveMethod =
            typeof(ZSerialize).GetMethod(nameof(CompileJson), BindingFlags.NonPublic | BindingFlags.Static);

        private static MethodInfo fromJsonMethod = typeof(JsonHelper).GetMethod(nameof(JsonHelper.FromJson));

        private const string mainAssembly = "Assembly-CSharp";


        //IDs to be stored for InstanceID manipulation when loading destroyed GameObjects
        // internal static Dictionary<int, int> idStorage = new Dictionary<int, int>();

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

        #endregion

        #region Helper Functions

        internal static string GetRuntimeSafeZUID()
        {
            return (Random.value * 100000000).ToString();
        }

        static int[] GetIDList()
        {
            return saveFiles.Select(f =>
            {
                var split = f.Replace('\\', '/').Split('/');
                return ZSerializerSettings.Instance.saveGroups.IndexOf(split[split.Length - 2]);
            }).ToArray();
        }

        static List<Type> GetUnitySerializableTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies(
            ).SelectMany(a =>
                a.GetTypes().Where(t =>
                    t == typeof(PersistentGameObject) ||
                    t.IsSubclassOf(typeof(Component)) &&
                    !t.IsSubclassOf(typeof(MonoBehaviour)) &&
                    t != typeof(Transform) &&
                    t != typeof(MonoBehaviour) &&
                    t.GetCustomAttribute<ObsoleteAttribute>() == null &&
                    t.IsVisible)
            ).ToList();
        }

        internal static bool PropertyIsSuitableForZSerializer(PropertyInfo propertyInfo)
        {
            return propertyInfo.CanRead &&
                   propertyInfo.CanWrite &&
                   propertyInfo.Name != "material" &&
                   propertyInfo.Name != "materials" &&
                   propertyInfo.Name != "sharedMaterial" &&
                   propertyInfo.Name != "mesh" &&
                   propertyInfo.Name != "tag" &&
                   propertyInfo.Name != "name" &&
                   propertyInfo.GetCustomAttribute<ObsoleteAttribute>() == null &&
                   propertyInfo.GetCustomAttribute<NonZSerialized>() == null &&
                   propertyInfo.GetSetMethod() != null &&
                   propertyInfo.GetSetMethod().IsPublic &&
                   !ZSerializerSettings.Instance.componentBlackList.IsInBlackList(propertyInfo.ReflectedType,
                       propertyInfo.Name);
        }

        [RuntimeInitializeOnLoadMethod]
        internal static void Init()
        {
            persistentDataPath = Application.persistentDataPath;
            unitySerializableTypes = GetUnitySerializableTypes();
            OnSceneLoad();

            SceneManager.sceneUnloaded += scene => { OnSceneUnload(); };

            SceneManager.sceneLoaded += (scene, mode) => { OnSceneLoad(); };

            Application.wantsToQuit += () =>
            {
                OnSceneUnload();
                return true;
            };
        }

        #region Scene Loading

        private static void OnSceneLoad()
        {
            currentScene = UpdateCurrentScene();
        }

        private static void OnSceneUnload()
        {
            //Scene unloading stuff
        }

        #endregion

        #region Exposed Utilities

        /// <summary>
        /// Gets a Scene's saved level names 
        /// </summary>
        /// <returns>Save Group's ID</returns>
        public static List<string> GetLevelNames()
        {
            List<string> levels = new List<string>();
            string[] dPaths = Directory.GetDirectories(GetFilePath("", true), "*levels");
            if (dPaths.Length > 0)
            {
                foreach (var s1 in Directory.GetFiles(dPaths[0]).Where(s => !s.Contains("assemblies-")))
                {
                    levels.Add(s1.Split('\\').Last().Split('/').Last().Split('.')[0]);
                }
            }

            return levels;
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

        #endregion

        #region Logging

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

        #endregion

        public static void SetCurrentScene(string sceneName)
        {
            currentScene = sceneName;
        }

        public static string GetLastScenePath(string sceneGroupName)
        {
            //TODO : Implement this
            return null;
        }

        static string UpdateCurrentScene()
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
        static async Task<List<Type>> GetAllPersistentComponents(IEnumerable<PersistentGameObject> objects)
        {
            return await RunTask(() =>
            {
                return objects.SelectMany(o => o.serializedComponents)
                    .Where(sc =>
                        sc.persistenceType == PersistentType.Everything ||
                        !ZSerializerSettings.Instance.advancedSerialization).Select(sc => sc.Type).Distinct()
                    .ToList();
            });
        }

        //Dynamically create array of zsavers based on component
        static async Task<object[]> CreateArrayOfZSerializers(List<Component> components, Type componentType)
        {
            var ZSerializerType = componentType;
            if (ZSerializerType == null)
            {
                Debug.LogError($"Couldn't find ZSerializer for {componentType.Name}.");
                return null;
            }

            var ZSerializerArrayType = ZSerializerType.MakeArrayType();

            var ZSerializersArray =
                Activator.CreateInstance(ZSerializerArrayType, components.Count);

            object[] zSavers = (object[])ZSerializersArray;

            int currentComponentCount = 0;

            for (var i = 0; i < zSavers.Length; i++)
            {
                zSavers[i] = Activator.CreateInstance(ZSerializerType, components[i].GetZUID(),
                    components[i].gameObject.GetZUID());
                currentComponentCount++;
                if (ZSerializerSettings.Instance.serializationType == SerializationType.Async &&
                    currentComponentCount >= ZSerializerSettings.Instance.maxBatchCount)
                {
                    currentComponentCount = 0;
                    await Task.Yield();
                }
            }

            return (object[])ZSerializersArray;
        }

        static async Task<object[]> OrderPersistentGameObjectsByLoadingOrder(object[] zSavers)
        {
            return await RunTask(() =>
            {
                return zSavers.OrderBy(x =>
                        (x as PersistentGameObjectZSerializer).gameObjectData.loadingOrder.x).ThenBy(x =>
                        (x as PersistentGameObjectZSerializer).gameObjectData.loadingOrder.y)
                    .Select(x => x as PersistentGameObjectZSerializer).ToArray();
            });
        }

        //Save using Reflection
        static async Task ReflectedSave(object[] zsavers)
        {
            Type ZSaverType = zsavers.GetType().GetElementType();
            var genericSaveMethodInfo = saveMethod.MakeGenericMethod(ZSaverType);
            genericSaveMethodInfo.Invoke(null, new object[] { zsavers });
            if (ZSerializerSettings.Instance.serializationType == SerializationType.Async) await Task.Yield();
        }

        //Restore the values of a given component from a given ZSerializer
        private static void RestoreValues(Component _component, object ZSerializer)
        {
            (ZSerializer as ZSerializer.Internal.ZSerializer).RestoreValues(_component);
        }
        
        static async Task OnPreSave(List<PersistentMonoBehaviour> persistentMonoBehavioursInScene)
        {
            await UpdateIDMap(persistentMonoBehavioursInScene);
            foreach (var persistentMonoBehaviour in persistentMonoBehavioursInScene)
            {
                persistentMonoBehaviour.OnPreSave();
                persistentMonoBehaviour.isSaving = true;
            }
        }

        static void OnPostSave(List<PersistentMonoBehaviour> persistentMonoBehavioursInScene)
        {
            foreach (var persistentMonoBehaviour in persistentMonoBehavioursInScene)
            {
                persistentMonoBehaviour.isSaving = false;
                persistentMonoBehaviour.OnPostSave();
            }
        }

        static async Task UpdateIDMap([NotNull] List<PersistentMonoBehaviour> persistentMonoBehavioursInScene)
        {
            int currentComponentCount = 0;

            foreach (var persistentMonoBehaviour in persistentMonoBehavioursInScene)
            {
                if (string.IsNullOrEmpty(persistentMonoBehaviour.ZUID) ||
                    string.IsNullOrEmpty(persistentMonoBehaviour.GOZUID))
                {
                    persistentMonoBehaviour.GenerateRuntimeZUIDs();
                }

                idMap.TryAdd(persistentMonoBehaviour.ZUID, persistentMonoBehaviour);
                idMap.TryAdd(persistentMonoBehaviour.GOZUID, persistentMonoBehaviour.gameObject);

                currentComponentCount++;
                if (ZSerializerSettings.Instance.serializationType == SerializationType.Async &&
                    currentComponentCount >= ZSerializerSettings.Instance.maxBatchCount)
                {
                    currentComponentCount = 0;
                    await Task.Yield();
                }
            }
        }

        #endregion

        #region Save

        static bool ShouldBeSerialized(IZSerialize serializable)
        {
            return serializable != null && (serializable.GroupID == currentGroupID || currentGroupID == -1) &&
                   serializable.IsOn;
        }

        //Saves all Persistent Components
        static async Task SavePersistentMonoBehaviours(List<PersistentMonoBehaviour> persistentMonoBehaviours)
        {
            if (persistentMonoBehaviours == null) return;
            
            Dictionary<Type, List<Component>> componentMap = new Dictionary<Type, List<Component>>();

            foreach (var persistentMonoBehaviour in persistentMonoBehaviours)
            {
                var type = persistentMonoBehaviour.GetType();
                if (!componentMap.ContainsKey(type))
                    componentMap.Add(type, new List<Component> { persistentMonoBehaviour });
                else componentMap[type].Add(persistentMonoBehaviour);
            }

            foreach (var pair in componentMap)
            {
                await SerializeComponents(componentMap[pair.Key],
                    pair.Key.Assembly.GetType((pair.Key.FullName ?? pair.Key.Name) + "ZSerializer"));
            }
        }

        //Gets all the components of a given type from an array of persistent gameObjects
        static List<Component> GetComponentsOfGivenType(List<PersistentGameObject> objects,
            Type componentType)
        {
            if (componentType == typeof(PersistentGameObject)) return objects.Select(o => o as Component).ToList();
            return objects.SelectMany(o =>
                    o.serializedComponents.Where(sc =>
                        sc.Type == componentType && (sc.persistenceType == PersistentType.Everything ||
                                                     !ZSerializerSettings.Instance.advancedSerialization)))
                .Select(sc => sc.component).ToList();
        }


        //Saves all persistent GameObjects and all of its attached unity components
        static async Task SavePersistentGameObjects(List<PersistentGameObject> persistentGameObjectsToSerialize)
        {
            var componentTypes = await GetAllPersistentComponents(persistentGameObjectsToSerialize);

            if (persistentGameObjectsToSerialize.Any()) componentTypes.Insert(0, typeof(PersistentGameObject));

            foreach (var componentType in componentTypes)
            {
                await SerializeComponents(
                    GetComponentsOfGivenType(persistentGameObjectsToSerialize, componentType),
                    Assembly.Load(componentType == typeof(PersistentGameObject)
                        ? "com.Ziplaw.ZSaver.Runtime"
                        : mainAssembly).GetType("ZSerializer." + componentType.Name + "ZSerializer"));
            }
        }

        //Dynamically serialize a given list of components 
        static async Task SerializeComponents(List<Component> components, Type zSaverType)
        {
            if (zSaverType == null)
            {
                LogError($"No ZSerializer found for {components[0].GetType()}");
                return;
            }

            object[] zSavers = await CreateArrayOfZSerializers(components, zSaverType);

            if (zSaverType == typeof(PersistentGameObjectZSerializer))
            {
                zSavers = await OrderPersistentGameObjectsByLoadingOrder(zSavers);
            }

            if (zSavers.Length > 0) unityComponentAssemblies.Add(components[0].GetType().Assembly.FullName);


            await ReflectedSave(zSavers);
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
        }

        //Loads a component no matter the type
        static async Task<object[]> LoadObjectsDynamically(Type ZSaverType, Type componentType, int objectIndex,
            object[] zSerializerObjects, int tupleIndex, JsonFillType jsonFillType)
        {
            var zSerializerObject = zSerializerObjects[objectIndex];
            string zuid = (zSerializerObject as Internal.ZSerializer)!.ZUID;
            string gozuid = (zSerializerObject as Internal.ZSerializer)!.GOZUID;

            bool componentPresentInGameObject = idMap.TryGetValue(zuid, out var componentObj) && idMap[zuid] != null;
            bool gameObjectPresent = idMap.TryGetValue(gozuid, out var gameObjectObj) && idMap[gozuid] != null;
            GameObject gameObject = gameObjectObj as GameObject;
            Component component = componentObj as Component;

            if (!gameObjectPresent)
            {
                if (componentType != typeof(PersistentGameObject))
                {
                    Debug.LogWarning(
                        $"GameObject holding {componentType} was destroyed, add the Persistent GameObject component to said GameObject if persistence was intended");
                    return zSerializerObjects;
                }

                LoadDestroyedGameObject(out gameObject, ZSaverType, zSerializerObject);
                idMap[gozuid] = gameObject;
            }

            if (!componentPresentInGameObject)
            {
                if (typeof(IZSerialize).IsAssignableFrom(componentType))
                {
                    component = gameObject.AddComponent(componentType);
                    IZSerialize serializer = component as IZSerialize;
                    idMap[zuid] = component;
                    serializer.ZUID = zuid;
                    serializer.GOZUID = gozuid;
                }
            }

            if (componentType == typeof(PersistentGameObject))
            {
                RestoreValues(component, zSerializerObject);

                PersistentGameObject pg = component as PersistentGameObject;
                foreach (var pgSerializedComponent in new List<SerializedComponent>(pg.serializedComponents))
                {
                    if (pgSerializedComponent.component == null &&
                        (pgSerializedComponent.persistenceType != PersistentType.None ||
                         !ZSerializerSettings.Instance.advancedSerialization))
                    {
                        var addedComponent = pg.gameObject.AddComponent(pgSerializedComponent.Type);
                        pg.serializedComponents[pg.serializedComponents.IndexOf(pgSerializedComponent)].component =
                            addedComponent;
                        idMap[pgSerializedComponent.zuid] = addedComponent;
                    }
                }
            }

            if (component is PersistentMonoBehaviour persistentMonoBehaviour)
                persistentMonoBehaviour.IsOn = true;

            if (!gameObjectPresent)
            {
                await FillTemporaryJsonTuples(jsonFillType);
                var fromJson = fromJsonMethod.MakeGenericMethod(tempTuples[currentGroupID][tupleIndex].Item1);
                zSerializerObjects =
                    (object[])await fromJson.InvokeAsync(null, tempTuples[currentGroupID][tupleIndex].Item2);
            }

            return zSerializerObjects;
        }


        static Type GetTypeFromZSerializerType(Type ZSerializerType)
        {
            if (ZSerializerType == typeof(PersistentGameObjectZSerializer)) return typeof(PersistentGameObject);
            return ZSerializerType.Assembly.GetType( (ZSerializerType.FullName ?? ZSerializerType.Name).Replace("ZSerializer", "")) ??
                   unityComponentAssemblies
                       .Select(s =>
                           Assembly.Load(s).GetType("UnityEngine." + ZSerializerType.Name.Replace("ZSerializer", "")))
                       .First(t => t != null);
        }

        static async Task LoadComponents(JsonFillType jsonFillType)
        {
            for (var tupleIndex = 0; tupleIndex < tempTuples[currentGroupID].Length; tupleIndex++)
            {
                var currentTuple = tempTuples[currentGroupID][tupleIndex];
                Type realType = GetTypeFromZSerializerType(currentTuple.Item1);
                if (realType == null)
                    Debug.LogError(
                        "ZSerializer type not found, probably because you added ZSerializer somewhere in the name of the class");
                Log("Deserializing " + realType + "s");

                var fromJson = fromJsonMethod.MakeGenericMethod(currentTuple.Item1);
                object[] zSerializerObjects = (object[])await fromJson.InvokeAsync(null, currentTuple.Item2);

                int currentComponentCount = 0;

                for (var i = 0; i < zSerializerObjects.Length; i++)
                {
                    zSerializerObjects = await LoadObjectsDynamically(currentTuple.Item1, realType, i,
                        zSerializerObjects, tupleIndex, jsonFillType);
                    currentComponentCount++;
                    if (ZSerializerSettings.Instance.serializationType == SerializationType.Async &&
                        currentComponentCount >= ZSerializerSettings.Instance.maxBatchCount)
                    {
                        currentComponentCount = 0;
                        await Task.Yield();
                    }
                }
            }
        }

//Loads all references and fields from already loaded objects, this is done like this to avoid data loss
        static async Task LoadReferences()
        {
            foreach (var tuple in tempTuples[currentGroupID]
                .Where(t => t.Item1 != typeof(PersistentGameObjectZSerializer)))
            {
                Type zSerializerType = tuple.Item1;
                // Type realType = GetTypeFromZSerializerType(zSerializerType);
                string json = tuple.Item2;

                var fromJson = fromJsonMethod.MakeGenericMethod(zSerializerType);

                object[] jsonObjects = (object[])await fromJson.InvokeAsync(null, json);

                int componentCount = 0;
                for (var i = 0; i < jsonObjects.Length; i++)
                {
                    var componentInGameObject =
                        idMap[(jsonObjects[i] as Internal.ZSerializer).ZUID] as Component;

                    RestoreValues(componentInGameObject, jsonObjects[i]);

                    componentCount++;
                    if (ZSerializerSettings.Instance.serializationType == SerializationType.Async &&
                        componentCount >= ZSerializerSettings.Instance.maxBatchCount)
                    {
                        componentCount = 0;
                        await Task.Yield();
                    }
                }
            }
        }

        #endregion

        static List<int> GetIDListFromGroupID(int groupID)
        {
            if (groupID == -1)
                return Object.FindObjectsOfType<MonoBehaviour>().Where(o => o is IZSerialize)
                    .Select(o => ((IZSerialize)o).GroupID).Distinct().ToList();
            return new List<int>() { groupID };
        }

        /// <summary>
        /// Serialize all Persistent components and Persistent GameObjects that are children of the given transform, onto a specified save file.
        /// </summary>
        /// <param name="fileName">The name of the file that will be saved</param>
        /// <param name="parent">The parent of the objects you want to save</param>
        public static async Task SaveObjects(string fileName, Transform parent, int groupID = -1)
        {
            try
            {
                _currentLevelName = fileName;
                _currentParent = parent;

                if (groupID == -1)
                {
                    string[] files;

                    files = Directory.GetFiles(GetFilePath("", true), "*", SearchOption.AllDirectories);

                    foreach (string directory in files)
                    {
                        File.Delete(directory);
                    }
                }

                #region Async

                jsonToSave = "";

                float startingTime = Time.realtimeSinceStartup;
                float frameCount = Time.frameCount;

                var idList = GetIDListFromGroupID(groupID);
                
                for (int i = 0; i < idList.Count; i++)
                {
                    currentGroupID = idList[i];
                    
                    var persistentMonoBehavioursInScene = _currentParent.GetComponentsInChildren<PersistentMonoBehaviour>().Where(ShouldBeSerialized).ToList();

                    await OnPreSave(persistentMonoBehavioursInScene);

                    LogWarning($"Saving {_currentLevelName}/{ZSerializerSettings.Instance.saveGroups[currentGroupID]}");
                    unityComponentAssemblies.Clear();

                    var persistentGameObjectsInScene = _currentParent.GetComponentsInChildren<PersistentGameObject>();
                    if(persistentGameObjectsInScene != null)
                        await SavePersistentGameObjects(persistentGameObjectsInScene.Where(ShouldBeSerialized).ToList()
                        .ToList());
                    
                    if(persistentGameObjectsInScene != null)
                        await SavePersistentMonoBehaviours(_currentParent.GetComponentsInChildren<PersistentMonoBehaviour>()?
                        .Where(ShouldBeSerialized).ToList());


                    await SaveJsonData($"{_currentLevelName}.zsave");
                    CompileJson(unityComponentAssemblies.Distinct().ToArray());
                    await SaveJsonData($"assemblies-{_currentLevelName}.zsave");


                    OnPostSave(persistentMonoBehavioursInScene);
                }
                

                Debug.Log("Serialization ended in: " + (Time.realtimeSinceStartup - startingTime) + " seconds or " +
                          (Time.frameCount - frameCount) + " frames.");
                currentGroupID = -1;
                _currentLevelName = null;
                _currentParent = null;

                // await FillTemporaryJsonTuples();

                #endregion
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Serialize all Persistent components and GameObjects on the current scene to the selected save file
        /// </summary>
        /// <param name="groupID">The ID for the objects you want to save</param>
        public static async Task SaveAll(int groupID = -1)
        {
            try
            {

                if (groupID == -1)
                {
                    string[] files;

                    files = Directory.GetFiles(GetFilePath("", true), "*", SearchOption.AllDirectories);

                    foreach (string directory in files)
                    {
                        File.Delete(directory);
                    }
                }

                #region Async


                List<int> idList = GetIDListFromGroupID(groupID);
                jsonToSave = "";

                float startingTime = Time.realtimeSinceStartup;
                float frameCount = Time.frameCount;

                for (int i = 0; i < idList.Count; i++)
                {
                    currentGroupID = idList[i];

                    List<PersistentMonoBehaviour> persistentMonoBehavioursInScene = Object.FindObjectsOfType<PersistentMonoBehaviour>()
                        .Where(ShouldBeSerialized).ToList();


                    await OnPreSave(persistentMonoBehavioursInScene);

                    LogWarning("Saving data on Group: " + ZSerializerSettings.Instance.saveGroups[currentGroupID]);
                    unityComponentAssemblies.Clear();

                    string[] files = Directory.GetFiles(GetFilePath(""));
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }

                    await SavePersistentGameObjects(idMap
                        .Where(kvp =>
                            kvp.Value is PersistentGameObject && ShouldBeSerialized(kvp.Value as IZSerialize))
                        .Select(kvp => kvp.Value as PersistentGameObject).ToList());
                    await SavePersistentMonoBehaviours(idMap
                        .Where(kvp =>
                            kvp.Value is PersistentMonoBehaviour && ShouldBeSerialized(kvp.Value as IZSerialize))
                        .Select(kvp => kvp.Value as PersistentMonoBehaviour).ToList());


                    await SaveJsonData("components.zsave");
                    CompileJson(unityComponentAssemblies.Distinct().ToArray());
                    await SaveJsonData("assemblies.zsave");

                    Log(
                        $"<color=cyan>{ZSerializerSettings.Instance.saveGroups[currentGroupID]}: {new FileInfo(GetFilePath("components.zsave")).Length * .001f} KB</color>");
                    // if (idList.Length > 1 && i != idList.Length - 1) fileSize += ", ";

                    OnPostSave(persistentMonoBehavioursInScene);
                }

                Debug.Log("Serialization ended in: " + (Time.realtimeSinceStartup - startingTime) + " seconds or " +
                          (Time.frameCount - frameCount) + " frames.");

                currentGroupID = -1;
                // await FillTemporaryJsonTuples(idList);

                #endregion
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        internal static List<int> restorationIDList = new List<int>();

        enum JsonFillType
        {
            All,
            Level
        };

        static async Task FillTemporaryJsonTuples(JsonFillType jsonFillType)
        {
            await RunTask(() =>
            {
                switch (jsonFillType)
                {
                    case JsonFillType.All:
                        if (tempTuples == null || tempTuples.Length == 0)
                            tempTuples = new (Type, string)[ZSerializerSettings.Instance.saveGroups
                                .Where(s => !string.IsNullOrEmpty(s))
                                .Max(sg => ZSerializerSettings.Instance.saveGroups.IndexOf(sg)) + 1][];

                        tempTuples[currentGroupID] = ReadFromFile("components.zsave");
                        break;
                    case JsonFillType.Level:
                        if (tempTuples == null || tempTuples.Length == 0)
                            tempTuples = new (Type, string)[1][];

                        tempTuples[0] = ReadFromFile($"{_currentLevelName}.zsave");
                        break;
                }
            });
        }

        /// <summary>
        /// Load all Persistent components and GameObjects from the current scene that have been previously serialized in the given level save file
        /// </summary>
        public static async Task LoadObjects(string levelName, Transform parent, bool destroyChildren = false)
        {
            try
            {
                if (destroyChildren)
                    foreach (var child in parent.GetComponentsInChildren<Transform>())
                    {
                        if (child != parent)
                            Object.Destroy(child.gameObject);
                    }

                await Task.Yield();

                _currentLevelName = levelName;
                _currentParent = parent;

                if (saveFiles == null || saveFiles.Count == 0)
                    saveFiles = Directory.GetFiles(GetFilePath(""), $"{levelName}.zsave").ToList();


                LogWarning("Loading Level: " + levelName);

                var persistentMonoBehavioursInScene = parent.GetComponentsInChildren<PersistentMonoBehaviour>();

                await UpdateIDMap(Object.FindObjectsOfType<PersistentMonoBehaviour>().ToList());
                foreach (var persistentMonoBehaviour in persistentMonoBehavioursInScene)
                {
                    persistentMonoBehaviour.OnPreLoad();
                    persistentMonoBehaviour.isLoading = true;
                }

                float startingTime = Time.realtimeSinceStartup;
                float frameCount = Time.frameCount;

                unityComponentAssemblies =
                    (await JsonHelper.FromJson<string>(ReadFromFile($"assemblies-{levelName}.zsave")?[0].Item2))
                    .ToList();

                currentGroupID = 0;

                await FillTemporaryJsonTuples(JsonFillType.Level);
                await LoadComponents(JsonFillType.Level);
                await LoadReferences();

                Debug.Log(
                    $"Deserialization of level \"{levelName}\" ended in: " +
                    (Time.realtimeSinceStartup - startingTime) + " seconds or " +
                    (Time.frameCount - frameCount) + " frames");

                foreach (var persistentMonoBehaviour in parent.GetComponentsInChildren<PersistentMonoBehaviour>())
                {
                    persistentMonoBehaviour.isLoading = false;
                    persistentMonoBehaviour.OnPostLoad();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Load all Persistent components and GameObjects from the current scene that have been previously serialized in the current save file
        /// </summary>
        public static async Task LoadAll(int groupID = -1)
        {
            try
            {
                if (saveFiles == null || saveFiles.Count == 0)
                    saveFiles = Directory.GetFiles(GetFilePath("", true), "components.zsave",
                        SearchOption.AllDirectories).ToList();

                currentGroupID = groupID;
                bool isLoadingAll = currentGroupID == -1;
                Log(isLoadingAll ? "Loading All Data" : "Loading Group " + currentGroupID);


                var idList = isLoadingAll ? GetIDList() : new[] { currentGroupID };

                restorationIDList.AddRange(idList);
                restorationIDList = restorationIDList.Distinct().ToList();

                currentGroupID = groupID;

                for (int i = 0; i < idList.Length; i++)
                {
                    currentGroupID = idList[i];
                    LogWarning("Loading Group in disk: " + ZSerializerSettings.Instance.saveGroups[currentGroupID]);

                    List<PersistentMonoBehaviour> persistentMonoBehavioursInScene = Object.FindObjectsOfType<PersistentMonoBehaviour>()
                        .Where(ShouldBeSerialized).ToList();

                    await UpdateIDMap(persistentMonoBehavioursInScene);
                    foreach (var persistentMonoBehaviour in persistentMonoBehavioursInScene)
                    {
                        persistentMonoBehaviour.OnPreLoad();
                        persistentMonoBehaviour.isLoading = true;
                    }

                    float startingTime = Time.realtimeSinceStartup;
                    float frameCount = Time.frameCount;

                    unityComponentAssemblies =
                        (await JsonHelper.FromJson<string>(ReadFromFile("assemblies.zsave")?[0].Item2)).ToList();


                    await FillTemporaryJsonTuples(JsonFillType.All);
                    await LoadComponents(JsonFillType.All);
                    await LoadReferences();

                    Debug.Log(
                        $"Deserialization of group \"{ZSerializerSettings.Instance.saveGroups[currentGroupID]}\" ended in: " +
                        (Time.realtimeSinceStartup - startingTime) + " seconds or " +
                        (Time.frameCount - frameCount) + " frames");

                    foreach (var persistentMonoBehaviour in persistentMonoBehavioursInScene)
                    {
                        persistentMonoBehaviour.isLoading = false;
                        persistentMonoBehaviour.OnPostLoad();
                    }
                }


                currentGroupID = -1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #region JSON Formatting

//Saves an array of objects to a file
        static void CompileJson<T>(T[] objectsToPersist)
        {
            string json = "{" + typeof(T).AssemblyQualifiedName + "}" + JsonHelper.ToJson(objectsToPersist);
            Log("Serializing: " + typeof(T) + " " + json);
            jsonToSave += json + "\n";
            // WriteToFile(fileName, json, useGlobalID);
        }

        internal static async Task<string> ReplaceZUIDs(string json)
        {
            return await RunTask(() =>
            {
#if UNITY_EDITOR
                return Regex.Replace(json, "\"zuid\":\\w+",
                    match =>
                    {
                        if (idMap.TryGetValue(match.Value.Split(':')[1], out var obj))
                            return "\"instanceID\":" + obj.GetHashCode();
                        return "\"instanceID\":0";
                    });
#else
            return Regex.Replace(json, "\"zuid\":\\w+",
                match =>
                {
                    if (idMap.TryGetValue(match.Value.Split(':')[1], out var obj))
                        return "\"m_FileID\":" + obj.GetHashCode() + ", \"m_PathID\":0";
                    return "\"m_FileID\":0, \"m_PathID\":0";
                });
#endif
            });
        }

        static string ReplaceInstanceIDs(string json, Dictionary<string, string> tempIDMap)
        {
            string pattern;
            ;
#if UNITY_EDITOR
            pattern = "\"instanceID\":\\D?[0-9]{2,}";
#else
            pattern = "\"m_FileID:-?[0-9]{2,},\"m_PathID\":0";
#endif

            return Regex.Replace(json, pattern, match =>
            {
                if (tempIDMap.TryGetValue(match.Value, out string zuid)) return "\"zuid\":" + zuid;
                return match.Value;
            });
        }

        static async Task SaveJsonData(string fileName, bool useGlobalID = false)
        {
            await WriteToFile(fileName, jsonToSave, useGlobalID);
            jsonToSave = "";
        }

//Writes json into file
        static async Task WriteToFile(string fileName, string json, bool useGlobalID = false)
        {
            await RunTask(() =>
            {
                var tempIDMap = idMap.ToDictionary(kvp =>
                {
#if UNITY_EDITOR
                    return "\"instanceID\":" + kvp.Value.GetHashCode();
#else
                        return "\"m_FileID:\":" + kvp.Value.GetHashCode() + ", \"m_PathID\":0";
#endif
                }, kvp => kvp.Key);
                json = ReplaceInstanceIDs(json, tempIDMap);
                if (ZSerializerSettings.Instance.encryptData)
                {
                    byte[] key =
                    {
                        0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
                    };

                    File.WriteAllBytes(GetFilePath(fileName, useGlobalID),
                        EncryptStringToBytes(json, key, key));
                }
                else
                {
                    File.WriteAllText(GetFilePath(fileName, useGlobalID), json);
                }
            });
        }

        static async Task RunTask(Action action)
        {
            if (ZSerializerSettings.Instance.serializationType == SerializationType.Sync) action();
            else await Task.Run(action);
        }

        static async Task<T> RunTask<T>(Func<T> action)
        {
            if (ZSerializerSettings.Instance.serializationType == SerializationType.Sync) return action();
            return await Task.Run(action);
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
            string path = useGlobalID
                ? Path.Combine(
                    persistentDataPath,
                    "SaveFile-" + ZSerializerSettings.Instance.selectedSaveFile,
                    currentScene)
                : Path.Combine(
                    persistentDataPath,
                    "SaveFile-" + ZSerializerSettings.Instance.selectedSaveFile,
                    currentScene,
                    _currentLevelName != null ? 
                        "levels/" + $"{_currentLevelName}/" + ZSerializerSettings.Instance.saveGroups[currentGroupID] : 
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
        public static async Task<T[]> FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(await ZSerialize.ReplaceZUIDs(json));
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

        internal static string GetZUID(this Object obj)
        {
            switch (obj)
            {
                case IZSerialize serializable: return serializable.ZUID;
                case GameObject gameObject:
                    return (gameObject.GetComponents<MonoBehaviour>().First(m => m is IZSerialize) as IZSerialize)
                        .GOZUID;
                case Component component:
                    return component.GetComponent<PersistentGameObject>()?.ComponentZuidMap[component];
                default: return null;
            }
        }

        internal static void TryAdd<TK, TV>(this Dictionary<TK, TV> dictionary, TK key, TV value)
        {
            if (!dictionary.TryGetValue(key, out _)) dictionary[key] = value;
        }

        public static async Task<object> InvokeAsync(this MethodInfo @this, object obj, params object[] parameters)
        {
            var task = (Task)@this.Invoke(obj, parameters);
            await task.ConfigureAwait(false);
            var resultProperty = task.GetType().GetProperty("Result");
            return resultProperty.GetValue(task);
        }
    }
}