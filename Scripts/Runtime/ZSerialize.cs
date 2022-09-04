using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
#if UNITY_EDITOR
using UnityEditor;
#if UNITY_2021_2_OR_NEWER
using UnityEditor.SceneManagement;
#else
using UnityEditor.Experimental.SceneManagement;
#endif
#endif
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngineInternal;
using ZSerializer.Internal;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[assembly: InternalsVisibleTo("ZSerializer.Editor")]

namespace ZSerializer
{
    public sealed class ZSerialize
    {
        #region Variables

        private static int currentGroupID = -1;
        public static int CurrentGroupID => currentGroupID;

        private static string currentSceneName;
        private static string currentScenePath;
        private static string persistentDataPath;
        private static string _currentLevelName;
        private static Transform _currentParent;
        private static string _currentSceneGroupName;
        private static string jsonToSave;

        private static byte[] key =
        {
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
        };

        public static List<Dictionary<string, Object>> idMap = new List<Dictionary<string, Object>>();
        public static Dictionary<string, SceneGroup> sceneToLoadingSceneMap = new Dictionary<string, SceneGroup>();

        internal static (Type, string)[][] tempTuples;

        private static List<string> saveFiles;

        //Assemblies in which Unity Components are located
        private static List<string> unityComponentAssemblies = new List<string>();
        private static List<string> unityComponentNamespaces = new List<string>();


        //Scene fields allowed to be added to the Serializable Unity Components list
        private static List<Type> unitySerializableTypes;

        internal static List<Type> UnitySerializableTypes
        {
            get
            {
                if (unitySerializableTypes == null) unitySerializableTypes = GetUnitySerializableTypes();
                return unitySerializableTypes;
            }
        }

        public static string PersistentDataPath
        {
            get => persistentDataPath = persistentDataPath ?? Application.persistentDataPath;
        }


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

        static int runtimeIDs = 0;

        internal static string GetRuntimeSafeZUID(Type type)
        {
            var serializables = Object.FindObjectsOfType<MonoBehaviour>().OfType<IZSerializable>();
            string idCandidate = null;

            while (idCandidate == null || serializables.Any(s => s.GetZUIDList().Contains(idCandidate)))
            {
                idCandidate = $"R{runtimeIDs++}{new string(type.Name.Where(c => char.IsUpper(c)).ToArray())}";
            }


            return idCandidate;
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
                   !ZSerializerSettings.Instance.unityComponentDataList.IsInBlackList(propertyInfo.ReflectedType,
                       propertyInfo.Name);
        }

        [RuntimeInitializeOnLoadMethod]
        internal static void Init()
        {
            Log($"Save-file path: {PersistentDataPath}", DebugMode.Developer);
            unitySerializableTypes = GetUnitySerializableTypes();

            foreach (var instanceSceneGroup in ZSerializerSettings.Instance.sceneGroups)
            {
                foreach (var scenePath in instanceSceneGroup.scenePaths)
                {
                    sceneToLoadingSceneMap.Add(scenePath, instanceSceneGroup);
                }
            }

            OnSceneLoad();

            SceneManager.sceneUnloaded += _ => { OnSceneUnload(); };

            SceneManager.sceneLoaded += (arg0, arg1) => { OnSceneLoad(); };

            SceneManager.activeSceneChanged += (arg0, arg1) => { UpdateCurrentScene(); };

            Application.wantsToQuit += () =>
            {
                OnSceneUnload();
                return true;
            };
        }

        #region Scene Loading

        private static void OnSceneLoad()
        {
            idMap.Clear();

            //fill idMap
            // foreach (var monoBehaviour in Object.FindObjectsOfType<MonoBehaviour>(true).Where(m => m is IZSerializable))
            // {
            //     var zs = monoBehaviour as IZSerializable;
            //     zs!.AddZUIDsToIDMap();
            // }

            UpdateCurrentScene();
        }

        private static void OnSceneUnload()
        {
            UpdateCurrentScene();
        }

        #endregion

        #region Exposed Utilities

        /*
        #region Instantiate Overloads

        public static T Instantiate<T>(T original) where T : Object
        {
            return Instantiate(original, Vector3.zero, Quaternion.identity, null);
        }

        public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object
        {
            return Instantiate(original, position, rotation, null);
        }

        public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent)
            where T : Object
        {
            var obj = Object.Instantiate(original, position, rotation, parent);
            ((obj as GameObject).GetComponents<MonoBehaviour>().First(m => m is IZSerializable) as IZSerializable)
                .GenerateRuntimeZUIDs(true);
            return obj;
        }

        #endregion
        
        */

        /// <summary>
        /// Gets a Scene's saved level names 
        /// </summary>
        /// <returns>Save Group's ID</returns>
        public static List<string> GetLevelNames()
        {
            List<string> levels = new List<string>();
            string[] dPaths = Directory.GetDirectories(GetGlobalFilePath(), "levels");
            if (dPaths.Length > 0)
            {
                foreach (var s1 in Directory.GetDirectories(dPaths[0]))
                {
                    levels.Add(s1.Split('/', '\\').Last());
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
        internal static void Log(object obj, DebugMode debugMode = DebugMode.Off)
        {
            if (debugMode <= ZSerializerSettings.Instance.debugMode) Debug.Log($"<b><color=#7cffed><i>ZS</i> →</color></b> {obj}");
        }

        internal static void LogWarning(object obj, DebugMode debugMode = DebugMode.Off)
        {
            if (debugMode <= ZSerializerSettings.Instance.debugMode) Debug.LogWarning($"<b><color=#ffc107><i>ZS</i> →</color></b> {obj}");
        }

        internal static void LogError(object obj, DebugMode debugMode = DebugMode.Off)
        {
            if (debugMode <= ZSerializerSettings.Instance.debugMode) Debug.LogError($"<b><color=#ff625a><i>ZS</i> →</color></b> {obj}");
        }

        #endregion

        private static void UpdateCurrentScene()
        {
            currentSceneName = SetCurrentScene();
            currentScenePath = SetCurrentScenePath();
        }

        static string SetCurrentScene()
        {
            return SceneManager.GetActiveScene().name;
        }

        static string SetCurrentScenePath()
        {
            return SceneManager.GetActiveScene().path.ToEditorBuildSettingsPath();
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
                try
                {
                    zSavers[i] = Activator.CreateInstance(ZSerializerType, components[i].GetZUID(),
                        components[i].gameObject.GetZUID());
                }
                catch (Exception e)
                {
                    LogError(
                        $"An exception was thrown when trying to create a {ZSerializerType} for {components[i]}.", DebugMode.Off);
                    throw e;
                }

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

        static void OnPreSave(List<IZSerializable> serializables)
        {
            foreach (var zs in serializables)
            {
                zs.OnPreSave();
                zs.IsSaving = true;
            }
        }

        static void OnPostSave(List<PersistentMonoBehaviour> persistentMonoBehavioursInScene)
        {
            foreach (var persistentMonoBehaviour in persistentMonoBehavioursInScene)
            {
                persistentMonoBehaviour.IsSaving = false;
                persistentMonoBehaviour.OnPostSave();
            }
        }

        static async Task UpdateIDMap(List<IZSerializable> serializablesInScene)
        {
            int currentComponentCount = 0;

            foreach (var serializable in serializablesInScene)
            {
                if (string.IsNullOrEmpty(serializable.ZUID) ||
                    string.IsNullOrEmpty(serializable.GOZUID))
                {
                    LogWarning(
                        $"{serializable} found with empty ZUID, if this is not an instanced object, please go to Tools/ZSerializer/Reset Project ZUIDs",
                        DebugMode.Informational);
                    serializable.GenerateRuntimeZUIDs(false); //used to be false
                }

                if (!serializable.ZUID.StartsWith("R") && (serializable as Component).GetComponents<IZSerializable>()
                    .Any(c => c.ZUID.StartsWith("R")))
                {
                    LogError($"Found non-matching ZUID types. {serializable}'s ZUID is an editor ZUID when it should be a runtime ZUID. Reset the ZUID in the prefab to solve the issue", DebugMode.Off);
                }

                serializable.AddZUIDsToIDMap();

                currentComponentCount++;
                if (ZSerializerSettings.Instance.serializationType == SerializationType.Async &&
                    currentComponentCount >= ZSerializerSettings.Instance.maxBatchCount)
                {
                    currentComponentCount = 0;
                    await Task.Yield();
                }
            }
        }

        static List<int> GetIDListFromGroupID(int groupID)
        {
            if (groupID == -1)
                // return Object.FindObjectsOfType<MonoBehaviour>().Where(o => o is IZSerializable)
                //     .Select(o => ((IZSerializable)o).GroupID).Distinct().ToList();
                return ZSerializerSettings.Instance.saveGroups.Where(sg => !string.IsNullOrEmpty(sg))
                    .Select(sg => ZSerializerSettings.Instance.saveGroups.IndexOf(sg)).ToList();
            return new List<int>(ZSerializerSettings.Instance.saveGroups.ToIndexList());
        }


        private async static Task SerializeCurrentScenePath()
        {
            await RunTask(() =>
            {
                var path = GetCurrentSceneGroupPath() + "/lastScenePath.zsave";
                if (ZSerializerSettings.Instance.encryptData)
                {
                    File.WriteAllBytes(path,
                        EncryptStringToBytes(currentScenePath, key, key));
                }
                else
                {
                    File.WriteAllText(path, currentScenePath);
                }
            });
        }

#if UNITY_EDITOR
        internal static bool IsPrefab(IZSerializable zs)
        {
            // Debug.LogError(PrefabStageUtility.GetCurrentPrefabStage() != null);
            // Debug.LogError(PrefabUtility.GetPrefabAssetType(zs as Object));
            // Debug.LogError(PrefabUtility.IsPartOfPrefabAsset((zs as Component).gameObject));

            // Debug.LogError(PrefabUtility.GetPrefabParent((zs as Component).gameObject) == null && PrefabUtility.GetPrefabObject((zs as Component).gameObject) != null);
            
            return PrefabStageUtility.GetCurrentPrefabStage() != null || Selection.activeObject is GameObject && Selection.assetGUIDs.Length > 0;
        }
#endif

        public static string GetLastSavedScenePath(string sceneGroupName)
        {
            var path = GetSceneGroupPathFromName(sceneGroupName) + "/lastScenePath.zsave";

            if (!File.Exists(path))
            {
                Debug.LogWarning(
                    $"You attempted to load a file that didn't exist ({path}), Loading first scene in {sceneGroupName} instead");

                return ZSerializerSettings.Instance.sceneGroups.First(sg => sg.name == sceneGroupName)
                    .scenePaths[0];
            }

            if (ZSerializerSettings.Instance.encryptData)
            {
                return DecryptStringFromBytes(File.ReadAllBytes(path), key, key);
            }

            return File.ReadAllText(path);
        }

        #endregion

        #region Save

        static bool ShouldBeSerialized(IZSerializable serializable)
        {
            return serializable != null && (serializable.GroupID == CurrentGroupID || CurrentGroupID == -1) &&
                   serializable.IsOn &&
                   (serializable as MonoBehaviour).enabled;
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

            componentTypes.Insert(0, typeof(PersistentGameObject));

            foreach (var componentType in componentTypes)
            {
                await SerializeComponents(
                    GetComponentsOfGivenType(persistentGameObjectsToSerialize, componentType),
                    Assembly.Load(componentType == typeof(PersistentGameObject)
                        ? "ZSerializer.Runtime"
                        : mainAssembly).GetType("ZSerializer." + componentType.Name + "ZSerializer"));
            }
        }

        //Dynamically serialize a given list of components 
        static async Task SerializeComponents(List<Component> components, Type zSaverType)
        {
            if (zSaverType == null)
            {
                LogError(
                    $"No ZSerializer found for {components[0].GetType()}, this may be caused by a <color=cyan>{components[0].GetType()}</color> not having a <color=cyan>ZSerializer.</color>",
                    DebugMode.Off);
                return;
            }

            object[] zSavers = await CreateArrayOfZSerializers(components, zSaverType);

            if (zSaverType == typeof(PersistentGameObjectZSerializer))
            {
                zSavers = await OrderPersistentGameObjectsByLoadingOrder(zSavers);
            }

            if (zSavers.Length > 0)
            {
                unityComponentAssemblies.Add(components[0].GetType().Assembly.FullName);
                if (components[0].GetType().Namespace != null)
                    unityComponentNamespaces.Add(components[0].GetType().Namespace);
            }


            await ReflectedSave(zSavers);
        }

        #endregion

        #region Load

        //Loads a component no matter the type
        static object[] LoadObjectsDynamically(Type componentType, int objectIndex,
            object[] zSerializerObjects)
        {
            var zSerializerObject = zSerializerObjects[objectIndex];
            string zuid = (zSerializerObject as Internal.ZSerializer).ZUID;
            string gozuid = (zSerializerObject as Internal.ZSerializer).GOZUID;

            bool componentPresentInGameObject = idMap[CurrentGroupID].TryGetValue(zuid, out var componentObj) &&
                                                idMap[CurrentGroupID][zuid];
            bool gameObjectPresent = idMap[CurrentGroupID].TryGetValue(gozuid, out var gameObjectObj) &&
                                     idMap[CurrentGroupID][gozuid];
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

                gameObject = new GameObject(); //LoadDestroyedGameObject(out gameObject, zSerializerObject);
                idMap[CurrentGroupID][gozuid] = gameObject;
            }

            if (!componentPresentInGameObject)
            {
                if (typeof(IZSerializable).IsAssignableFrom(componentType))
                {
                    component = gameObject.AddComponent(componentType);
                    IZSerializable serializer = component as IZSerializable;
                    idMap[CurrentGroupID][zuid] = component;
                    serializer.ZUID = zuid;
                    serializer.GOZUID = gozuid;
                }
            }

            if (zSerializerObjects[objectIndex] is PersistentGameObjectZSerializer)
            {
                var pg = component as PersistentGameObject;
                var zserializer = zSerializerObjects[objectIndex] as PersistentGameObjectZSerializer;
                var realSerializableComponentList = pg.serializedComponents;
                var zserializerComponentList = zserializer.serializedComponents;

                foreach (var zserializedComponent in zserializerComponentList)
                {
                    if (zserializedComponent.component == null &&
                        (zserializedComponent.persistenceType != PersistentType.None ||
                         !ZSerializerSettings.Instance.advancedSerialization))
                    {
                        Component addedComponent;
                        if (zserializedComponent.Type == typeof(Transform)) addedComponent = gameObject.transform;
                        else addedComponent = gameObject.AddComponent(zserializedComponent.Type);

                        realSerializableComponentList.Add(new SerializedComponent(addedComponent,
                            zserializedComponent.zuid, zserializedComponent.persistenceType));
                        idMap[currentGroupID][zserializedComponent.zuid] = addedComponent;
                    }
                }

                // foreach (var pgzserializedComponent in new List<SerializedComponent>(pgzs.serializedComponents))
                // {
                //     if (pgzserializedComponent.component == null &&
                //         (pgzserializedComponent.persistenceType != PersistentType.None ||
                //          !ZSerializerSettings.Instance.advancedSerialization))
                //     {
                //         Component addedComponent;
                //         if (pgzserializedComponent.Type == typeof(Transform)) addedComponent = gameObject.transform;
                //         else addedComponent = gameObject.AddComponent(pgzserializedComponent.Type);
                //
                //         pg.serializedComponents[pgzs.serializedComponents.IndexOf(pgzserializedComponent)].component =
                //             addedComponent;
                //         ZSerialize.idMap[ZSerialize.CurrentGroupID][pgzserializedComponent.zuid] = addedComponent;
                //     }
                // }
            }

            if (component is PersistentMonoBehaviour persistentMonoBehaviour)
                persistentMonoBehaviour.IsOn = true;


            return zSerializerObjects;
        }


        static Type GetTypeFromZSerializerType(Type ZSerializerType)
        {
            if (ZSerializerType == typeof(PersistentGameObjectZSerializer)) return typeof(PersistentGameObject);
            var type = ZSerializerType.Assembly.GetType(
                (ZSerializerType.FullName ?? ZSerializerType.Name).Replace("ZSerializer", ""));
            if (type != null) return type;

            foreach (var unityComponentAssembly in unityComponentAssemblies)
            {
                foreach (var unityComponentNamespace in unityComponentNamespaces)
                {
                    type = Assembly.Load(unityComponentAssembly)
                        .GetType($"{unityComponentNamespace}.{ZSerializerType.Name.Replace("ZSerializer", "")}");
                    if (type != null) return type;
                }
            }

            throw new Exception($"Could not find real type for {ZSerializerType.Name}");
        }

        static async Task LoadComponents(ZSerializationType zSerializationType)
        {
            for (var tupleIndex = 0; tupleIndex < tempTuples[CurrentGroupID].Length; tupleIndex++)
            {
                var currentTuple = tempTuples[CurrentGroupID][tupleIndex];
                Type realType = GetTypeFromZSerializerType(currentTuple.Item1);
                if (realType == null)
                    Debug.LogError(
                        "ZSerializer type not found, probably because you added ZSerializer somewhere in the name of the class");
                Log("Deserializing " + realType + "s", DebugMode.Informational);

                var fromJson = fromJsonMethod.MakeGenericMethod(currentTuple.Item1);
                object[] zSerializerObjects = (object[])fromJson.Invoke(null, new object[] { currentTuple.Item2 });

                int currentComponentCount = 0;

                for (var i = 0; i < zSerializerObjects.Length; i++)
                {
                    zSerializerObjects = LoadObjectsDynamically(realType, i,
                        zSerializerObjects);
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
            foreach (var tuple in tempTuples[CurrentGroupID]
                .Where(t => t.Item1 != Type.GetType("ZSerializer.TransformZSerializer")))
            {
                Type zSerializerType = tuple.Item1;
                string json = tuple.Item2;

                var fromJson = fromJsonMethod.MakeGenericMethod(zSerializerType);

                object[] jsonObjects = (object[])fromJson.Invoke(null, new object[] { json });
                // var persistentGameObjectEventSerializationList

                int componentCount = 0;
                for (var i = 0; i < jsonObjects.Length; i++)
                {
                    var componentInGameObject =
                        idMap[CurrentGroupID][(jsonObjects[i] as Internal.ZSerializer).ZUID] as Component;

                    (jsonObjects[i] as ZSerializer.Internal.ZSerializer).RestoreValues(componentInGameObject);
                    if (componentInGameObject is PersistentGameObject persistentGameObject)

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

        #region Serialization Type Specific Functions

        enum ZSerializationType
        {
            Level,
            Scene,
        };

        private static void DeleteComponentFiles(ZSerializationType zSerializationType)
        {
            string[] files;

            switch (zSerializationType)
            {
                case ZSerializationType.Scene:
                    files = Directory.GetFiles(GetGlobalFilePath(), "*", SearchOption.AllDirectories);
                    break;
                case ZSerializationType.Level:
                    files = Directory.GetFiles(GetSaveGrouplessFilePath(), "*", SearchOption.AllDirectories);
                    break;
                default: throw new SerializationException("Serialization Type not implemented");
            }

            foreach (string file in files)
            {
                File.Delete(file);
            }
        }

        static List<PersistentMonoBehaviour> GetPersistentMonoBehavioursInScene(ZSerializationType zSerializationType)
        {
            switch (zSerializationType)
            {
                case ZSerializationType.Scene:
#if UNITY_2020_3_OR_NEWER
                    return Object.FindObjectsOfType<PersistentMonoBehaviour>(true).Where(ShouldBeSerialized).ToList();
#else
                    return Object.FindObjectsOfType<PersistentMonoBehaviour>().Where(ShouldBeSerialized).ToList();
#endif
                case ZSerializationType.Level:
                    return _currentParent.GetComponentsInChildren<PersistentMonoBehaviour>(true)
                        .Where(ShouldBeSerialized)
                        .ToList();
                default: throw new SerializationException("Serialization Type not implemented");
            }
        }

        static List<PersistentGameObject> GetPersistentGameObjectsInScene(ZSerializationType zSerializationType)
        {
            switch (zSerializationType)
            {
                case ZSerializationType.Scene:
#if UNITY_2020_3_OR_NEWER
                    return Object.FindObjectsOfType<PersistentGameObject>(true).Where(ShouldBeSerialized).ToList();
#else
                    return Object.FindObjectsOfType<PersistentGameObject>().Where(ShouldBeSerialized).ToList();
#endif
                case ZSerializationType.Level:
                    return _currentParent.GetComponentsInChildren<PersistentGameObject>(true).Where(ShouldBeSerialized)
                        .ToList();
                default: throw new SerializationException("Serialization Type not implemented");
            }
        }

        static void LogCurrentSave(ZSerializationType zSerializationType)
        {
            switch (zSerializationType)
            {
                case ZSerializationType.Scene:
                    LogWarning($"Saving {ZSerializerSettings.Instance.saveGroups[CurrentGroupID]}", DebugMode.Off);
                    break;
                case ZSerializationType.Level:
                    LogWarning(
                        $"Saving {_currentLevelName}/{ZSerializerSettings.Instance.saveGroups[CurrentGroupID]}",
                        DebugMode.Off);
                    break;

                default: throw new SerializationException("Serialization Type not implemented");
            }
        }

        static async Task SerializeData(ZSerializationType zSerializationType)
        {
            switch (zSerializationType)
            {
                case ZSerializationType.Scene:
                    await SaveJsonData("components.zsave");
                    CompileJson(unityComponentAssemblies.Distinct().ToArray());
                    CompileJson(unityComponentNamespaces.Distinct().ToArray());
                    await SaveJsonData("assemblies.zsave");
                    break;
                case ZSerializationType.Level:
                    await SaveJsonData($"{_currentLevelName}.zsave");
                    CompileJson(unityComponentAssemblies.Distinct().ToArray());
                    CompileJson(unityComponentNamespaces.Distinct().ToArray());
                    await SaveJsonData($"assemblies-{_currentLevelName}.zsave");
                    break;
                default: throw new SerializationException("Serialization Type not implemented");
            }
        }

        static void LogFileSize(ZSerializationType zSerializationType)
        {
            switch (zSerializationType)
            {
                case ZSerializationType.Scene:
                    Log(
                        $"{ZSerializerSettings.Instance.saveGroups[CurrentGroupID]}: {new FileInfo(GetFilePath("components.zsave")).Length * .001f} KB",
                        DebugMode.Informational);
                    break;
                case ZSerializationType.Level:
                    Log(
                        $"{ZSerializerSettings.Instance.saveGroups[CurrentGroupID]}: {new FileInfo(GetFilePath($"{_currentLevelName}.zsave")).Length * .001f} KB",
                        DebugMode.Informational);
                    break;
                default: throw new SerializationException("Serialization Type not implemented");
            }
        }

        static async Task FillTemporaryJsonTuples(ZSerializationType zSerializationType)
        {
            await RunTask(() =>
            {
                if (tempTuples == null || tempTuples.Length == 0)
                    tempTuples = new (Type, string)[ZSerializerSettings.Instance.saveGroups
                        .Where(s => !string.IsNullOrEmpty(s))
                        .Max(sg => ZSerializerSettings.Instance.saveGroups.IndexOf(sg)) + 1][];

                switch (zSerializationType)
                {
                    case ZSerializationType.Scene:
                        tempTuples[CurrentGroupID] = ReadFromFile("components.zsave");
                        break;
                    case ZSerializationType.Level:
                        tempTuples[CurrentGroupID] = ReadFromFile($"{_currentLevelName}.zsave");
                        break;
                }
            });
        }

        static (Type, string)[] GetAssemblyTuple(ZSerializationType zSerializationType)
        {
            switch (zSerializationType)
            {
                case ZSerializationType.Scene: return ReadFromFile("assemblies.zsave");
                case ZSerializationType.Level: return ReadFromFile($"assemblies-{_currentLevelName}.zsave");
                default: throw new SerializationException("Serialization Type not implemented");
            }
        }

        static void GetSaveFiles(ZSerializationType zSerializationType)
        {
            if (saveFiles == null || saveFiles.Count == 0)
                switch (zSerializationType)
                {
                    case ZSerializationType.Level:
                        saveFiles = Directory.GetFiles(GetGlobalFilePath(), $"{_currentLevelName}.zsave",
                            SearchOption.AllDirectories).ToList();
                        break;
                    case ZSerializationType.Scene:
                        saveFiles = Directory
                            .GetFiles(GetGlobalFilePath(), "components.zsave", SearchOption.AllDirectories).ToList();
                        break;
                }
        }

        static void LogLevelLoading(ZSerializationType zSerializationType)
        {
            switch (zSerializationType)
            {
                case ZSerializationType.Level:
                    LogWarning(
                        $"Loading Level: {_currentLevelName}/{ZSerializerSettings.Instance.saveGroups[CurrentGroupID]}",
                        DebugMode.Off);
                    break;
                case ZSerializationType.Scene:
                    LogWarning($"Loading Group: {ZSerializerSettings.Instance.saveGroups[CurrentGroupID]}",
                        DebugMode.Off);
                    break;
            }
        }

        #endregion

        #region Internals

        private static async Task SaveInternal(ZSerializationType zSerializationType)
        {
            try
            {
                if (CurrentGroupID == -1) DeleteComponentFiles(zSerializationType);

                float startingTime = Time.realtimeSinceStartup;
                float frameCount = Time.frameCount;

                var idList = GetIDListFromGroupID(CurrentGroupID);

                var tempGroupID = CurrentGroupID;

                for (var i = 0; i < idList.Count; i++)
                {
                    idMap.Add(new Dictionary<string, Object>());
                }


                for (int i = 0; i < idList.Count; i++)
                {
                    if (tempGroupID != -1 && idList[i] != tempGroupID) continue;

                    currentGroupID = idList[i];

                    var persistentMonoBehavioursInScene = GetPersistentMonoBehavioursInScene(zSerializationType);
                    var persistentGameObjectsInScene = GetPersistentGameObjectsInScene(zSerializationType);

                    List<IZSerializable> serializables = new List<IZSerializable>(persistentGameObjectsInScene);
                    serializables.AddRange(persistentMonoBehavioursInScene);

                    await UpdateIDMap(serializables);
                    OnPreSave(serializables);

                    LogCurrentSave(zSerializationType);
                    unityComponentAssemblies.Clear();
                    unityComponentNamespaces.Clear();

                    if (persistentGameObjectsInScene != null)
                        await SavePersistentGameObjects(persistentGameObjectsInScene);

                    if (persistentMonoBehavioursInScene != null)
                        await SavePersistentMonoBehaviours(persistentMonoBehavioursInScene);


                    await SerializeData(zSerializationType);

                    OnPostSave(persistentMonoBehavioursInScene);
                    LogFileSize(zSerializationType);
                }

                Log("Serialization ended in: " + (Time.realtimeSinceStartup - startingTime) + " seconds or " +
                          (Time.frameCount - frameCount) + " frames.", DebugMode.Off);

                jsonToSave = "";
                currentGroupID = -1;
                _currentLevelName = null;
                _currentParent = null;
                idMap.Clear();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static async Task LoadInternal(ZSerializationType zSerializationType)
        {
            GetSaveFiles(zSerializationType);
            var idList = CurrentGroupID == -1 ? GetIDList() : ZSerializerSettings.Instance.saveGroups.ToIndexArray();

            for (var i = 0; i < idList.Length; i++)
            {
                idMap.Add(new Dictionary<string, Object>());
            }

            int tempGroupID = CurrentGroupID;

            foreach (var i in idList)
            {
                if (tempGroupID != -1 && idList[i] != tempGroupID) continue;

                currentGroupID = idList[i];
                idMap.Add(new Dictionary<string, Object>());

                LogLevelLoading(zSerializationType);

                var persistentMonoBehavioursInScene = GetPersistentMonoBehavioursInScene(zSerializationType);
                var persistentGameObjectsInScene = GetPersistentGameObjectsInScene(zSerializationType);

                var serializableList = new List<IZSerializable>(persistentMonoBehavioursInScene);
                serializableList.AddRange(persistentGameObjectsInScene);

                await UpdateIDMap(serializableList);

                foreach (var zs in serializableList)
                {
                    zs.OnPreLoad();
                    zs.IsLoading = true;
                }

                float startingTime = Time.realtimeSinceStartup;
                float frameCount = Time.frameCount;


                var assemblyTuple = GetAssemblyTuple(zSerializationType);

                unityComponentAssemblies = JsonHelper.FromJson<string>(assemblyTuple?[0].Item2).ToList();
                unityComponentNamespaces = JsonHelper.FromJson<string>(assemblyTuple?[1].Item2).ToList();

                await FillTemporaryJsonTuples(zSerializationType);
                var persistentGameObjectTuple =
                    tempTuples[idList[i]].FirstOrDefault(t => t.Item1 == typeof(PersistentGameObjectZSerializer));

                var pgList =
                    JsonHelper.FromJson<PersistentGameObjectZSerializer>(persistentGameObjectTuple.Item2);

                var gozuidList = pgList.Select(pg => pg.GOZUID).ToList();
                var zuidToDestroyList = new List<string>();

                foreach (var kvp in idMap[CurrentGroupID])
                {
                    if (kvp.Value is GameObject go && (!gozuidList.Contains(kvp.Key) || kvp.Key.StartsWith("R")))
                    {
                        if (go && go.GetComponent<PersistentGameObject>())
                        {
                            var zss = go.GetComponents<IZSerializable>();
                            foreach (var zs in zss)
                            {
                                if (zs != null && zs.GroupID == CurrentGroupID)
                                {
                                    zuidToDestroyList.Add(zs.GOZUID);
                                    zuidToDestroyList.Add(zs.ZUID);

                                    if (zs is PersistentGameObject pg)
                                        foreach (var sc in pg.serializedComponents)
                                            zuidToDestroyList.Add(sc.zuid);

                                    Object.Destroy(go);
                                }
                            }
                        }
                    }
                }

                foreach (var zuid in zuidToDestroyList)
                {
                    idMap[currentGroupID][zuid] = null;
                }

                await LoadComponents(zSerializationType);
                await FillTemporaryJsonTuples(
                    zSerializationType); // this is here because pg events lose their target if we dont do it
                await LoadReferences();

                Log(
                    $"Deserialization of group \"{ZSerializerSettings.Instance.saveGroups[CurrentGroupID]}\" ended in: " +
                    (Time.realtimeSinceStartup - startingTime) + " seconds or " +
                    (Time.frameCount - frameCount) + " frames", DebugMode.Off);

                persistentMonoBehavioursInScene = GetPersistentMonoBehavioursInScene(zSerializationType);
                persistentGameObjectsInScene = GetPersistentGameObjectsInScene(zSerializationType);

                serializableList = new List<IZSerializable>(persistentMonoBehavioursInScene);
                serializableList.AddRange(persistentGameObjectsInScene);

                foreach (var zs in serializableList)
                {
                    zs.IsLoading = false;
                    zs.OnPostLoad();
                }
            }

            _currentLevelName = null;
            _currentParent = null;
            currentGroupID = -1;
            idMap.Clear();
        }

        #endregion

        #region Externals

        /// <summary>
        /// Serialize all Persistent components and Persistent GameObjects that are children of the given transform, onto a specified save file.
        /// </summary>
        /// <param name="fileName">The name of the file that will be saved</param>
        /// <param name="parent">The parent of the objects you want to save</param>
        /// <param name="groupID">The Save Group of objects that will be saved for this specific level</param>
        public static async Task SaveLevel(string fileName, Transform parent, int groupID = -1)
        {
            try
            {
                _currentLevelName = fileName;
                _currentParent = parent;
                currentGroupID = groupID;

                await SaveInternal(ZSerializationType.Level);
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
        public static async Task SaveScene(int groupID = -1)
        {
            try
            {
                currentGroupID = groupID;
                await SerializeCurrentScenePath();
                await SaveInternal(ZSerializationType.Scene);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task SaveGlobal(GlobalObject globalObject)
        {
            try
            {
                var t = globalObject.GetType();
                var attr = t.GetCustomAttribute<SerializeGlobalDataAttribute>();
                if (attr == null)
                    throw new SerializationException($"Type {t} must implement SerializeGlobalData Attribute");

                var json = JsonUtility.ToJson(globalObject);
                
                Log($"Saving {globalObject.name}", DebugMode.Informational);
                
                var time = Time.realtimeSinceStartup;
                var frameCount = Time.frameCount;
                
                await WriteToFileGlobal(attr.serializationType, $"{t.Name}.zsave", json);
                Log($"{globalObject.name} has been succesfully saved! ({Time.realtimeSinceStartup - time} seconds or ({Time.frameCount - frameCount} frames))", DebugMode.Off);
            }
            catch (Exception e)
            {
                LogError($"{globalObject.name} save was interrupted by the following exception: {e.Message}", DebugMode.Off);
                throw;
            }
        }

        public static async Task LoadGlobal(GlobalObject globalObject)
        {
            try
            {
                Type t = globalObject.GetType();
                var attr = t.GetCustomAttribute<SerializeGlobalDataAttribute>();
                if (attr == null)
                    throw new SerializationException($"Type {t} must implement SerializeGlobalData Attribute");

                Log($"Loading {globalObject.name}", DebugMode.Informational);
                
                var time = Time.realtimeSinceStartup;
                var frameCount = Time.frameCount;
                
                JsonUtility.FromJsonOverwrite(await ReadFromFileGlobal(attr.serializationType, $"{t.Name}.zsave"),
                    globalObject);
                Log($"{globalObject.name} has been succesfully loaded! ({Time.realtimeSinceStartup - time} seconds or ({Time.frameCount - frameCount} frames)", DebugMode.Off);
            }
            catch (Exception e)
            {
                LogError($"{globalObject.name} load was interrupted by the following exception: {e.Message}", DebugMode.Off);
                throw;
            }
        }

        /// <summary>
        /// Load all Persistent components and GameObjects from the current scene that have been previously serialized in the given level save file
        /// </summary>
        public static async Task LoadLevel(string levelName, Transform parent, bool destroyChildren = false,
            int groupID = -1)
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
                currentGroupID = groupID;

                await LoadInternal(ZSerializationType.Level);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Load all Persistent components and GameObjects from the current scene that have been previously serialized in the current save file
        /// </summary>
        public static async Task LoadScene(int groupID = -1)
        {
            try
            {
                currentGroupID = groupID;
                await LoadInternal(ZSerializationType.Scene);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Load the provided Scene Group's last saved scene.
        /// </summary>
        /// <param name="sceneGroupName">The name of the Scene Group you want to load.</param>
        /// <param name="lastSavedScenePath">The asset path to the last saved scene of this scene group</param>
        /// <param name="mode">The LoadSceneMode in which the scene will get loaded.</param>
        /// <returns></returns>
        public static AsyncOperation LoadSceneGroup(string sceneGroupName, out string lastSavedScenePath,
            LoadSceneMode mode = LoadSceneMode.Single)
        {
            lastSavedScenePath = GetLastSavedScenePath(sceneGroupName).ToAssetPath();
            return SceneManager.LoadSceneAsync(lastSavedScenePath, mode);
        }

        #endregion

        #region JSON Formatting

        static async Task RunTask(Action action)
        {
            try
            {
                if (ZSerializerSettings.Instance.serializationType == SerializationType.Sync) action();
                else
                    await Task.Run(action).ContinueWith(t =>
                    {
                        if (t.IsFaulted) throw t.Exception;
                    });
            }
            catch (Exception up)
            {
                throw up;
            }
        }

        static async Task<T> RunTask<T>(Func<T> action)
        {
            if (ZSerializerSettings.Instance.serializationType == SerializationType.Sync) return action();
            return await Task.Run(action);
        }

//Saves an array of objects to a file
        static void CompileJson<T>(T[] objectsToPersist)
        {
            string json = "{" + typeof(T).AssemblyQualifiedName + "}" + JsonHelper.ToJson(objectsToPersist);
            Log("Serializing: " + typeof(T) + " " + json, DebugMode.Informational);
            jsonToSave += json + "\n";
            // WriteToFile(fileName, json, useGlobalID);
        }

        internal static string ReplaceZUIDs(string json)
        {
#if UNITY_EDITOR
            return Regex.Replace(json, "\"zuid\":\\w+",
                match =>
                {
                    if (idMap[CurrentGroupID].TryGetValue(match.Value.Split(':')[1], out var obj) && obj != null)
                        return "\"instanceID\":" + obj.GetInstanceID();
                    return "\"instanceID\":0";
                });
#else
            return Regex.Replace(json, "\"zuid\":\\w+",
                match =>
                {
                    if (idMap[CurrentGroupID].TryGetValue(match.Value.Split(':')[1], out var obj) && obj != null)
                        return "\"m_FileID\":" + obj.GetInstanceID() + ", \"m_PathID\":0";
                    return "\"m_FileID\":0, \"m_PathID\":0";
                });
#endif
        }

        static string ReplaceInstanceIDs(string json, Dictionary<string, string> tempIDMap)
        {
            string pattern;
            ;
#if UNITY_EDITOR
            pattern = "\"instanceID\":\\D?[0-9]{2,}";
#else
            pattern = "\"m_FileID\":-?[0-9]{2,},\"m_PathID\":0";
#endif

            return Regex.Replace(json, pattern, match =>
            {
                if (tempIDMap.TryGetValue(match.Value, out string zuid)) return "\"zuid\":" + zuid;
                return match.Value;
            });
        }

        static async Task SaveJsonData(string fileName)
        {
            await WriteToFile(fileName, jsonToSave);
            jsonToSave = "";
        }

//Writes json into file
        static async Task WriteToFile(string fileName, string json)
        {
            var tempIDMap = idMap[CurrentGroupID].ToDictionary(kvp =>
            {
#if UNITY_EDITOR
                return "\"instanceID\":" + kvp.Value.GetInstanceID();
#else
                return "\"m_FileID\":" + kvp.Value.GetInstanceID() + ",\"m_PathID\":0";
#endif
            }, kvp => kvp.Key);

            await RunTask(() =>
            {
                json = ReplaceInstanceIDs(json, tempIDMap);
                if (ZSerializerSettings.Instance.encryptData)
                {
                    File.WriteAllBytes(GetFilePath(fileName),
                        EncryptStringToBytes(json, key, key));
                }
                else
                {
                    File.WriteAllText(GetFilePath(fileName), json);
                }
            });
        }

        static async Task WriteToFileGlobal(GlobalDataType dataType, string fileName, string json)
        {
            await RunTask(() =>
            {
                if (ZSerializerSettings.Instance.encryptData)
                {
                    File.WriteAllBytes(GetGlobalDataPath(dataType, fileName),
                        EncryptStringToBytes(json, key, key));
                }
                else
                {
                    File.WriteAllText(GetGlobalDataPath(dataType, fileName), json);
                }
            });
        }

        static async Task<string> ReadFromFileGlobal(GlobalDataType dataType, string fileName)
        {
            return await RunTask(() =>
            {
                var path = GetGlobalDataPath(dataType, fileName);

                if (!File.Exists(path))
                {
                    throw new SerializationException(
                        $"You attempted to load a file that didn't exist ({path}), this may be caused by trying to load a save file without having it saved first");
                    
                }

                if (ZSerializerSettings.Instance.encryptData)
                {
                    return DecryptStringFromBytes(File.ReadAllBytes(path), key, key);
                }

                return File.ReadAllText(path);
            });
        }


//Reads json from file
        static (Type, string)[] ReadFromFile(string fileName)
        {
            if (!File.Exists(GetFilePath(fileName)))
            {
                Debug.LogWarning(
                    $"You attempted to load a file that didn't exist ({GetFilePath(fileName)}), this may be caused by trying to load a save file without having it saved first");

                return null;
            }

            if (ZSerializerSettings.Instance.encryptData)
            {
                return GetTypesAndJsonFromString(
                    DecryptStringFromBytes(File.ReadAllBytes(GetFilePath(fileName)), key, key));
            }

            return GetTypesAndJsonFromString(File.ReadAllText(GetFilePath(fileName)));
        }

        public static bool DeleteAllSaveFiles()
        {
            try
            {
                foreach (var dir in Directory.GetDirectories(PersistentDataPath))
                {
                    Directory.Delete(dir, true);
                }
                return true;
            }
            catch (Exception e)
            {
                LogError(e);
                return false;
            }
            
        }

        public static bool DeleteSaveFile(int saveFile)
        {
            try
            {
                Directory.Delete(GetSaveFileDataPath(saveFile),true);
                return true;
            }
            catch (Exception e)
            {
                LogError(e);
                return false;
            } 
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

        static string GetSceneGroupPath(string scenePath)
        {
            return sceneToLoadingSceneMap.TryGetValue(scenePath, out var sceneGroup) ? sceneGroup.name : "no-group";
        }

        internal static string GetSaveFileDataPath(int saveFile)
        {
            var path = Path.Combine(
                PersistentDataPath,
                "SaveFile-" + saveFile);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }

        static string GetGlobalDataPath(GlobalDataType serializationType, string fileName)
        {
            var path = Path.Combine(
                PersistentDataPath,
                serializationType == GlobalDataType.PerSaveFile
                    ? "SaveFile-" + ZSerializerSettings.Instance.selectedSaveFile
                    : "",
                "GlobalData");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return Path.Combine(path, fileName);
        }

        static string GetSceneGroupPathFromName(string sceneGroupName)
        {
            var path = Path.Combine(
                PersistentDataPath,
                "SaveFile-" + ZSerializerSettings.Instance.selectedSaveFile,
                sceneGroupName);

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }

        static string GetCurrentSceneGroupPath()
        {
            string sceneGroupName = GetSceneGroupPath(currentScenePath);

            var path = Path.Combine(
                PersistentDataPath,
                "SaveFile-" + ZSerializerSettings.Instance.selectedSaveFile,
                sceneGroupName);

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }

        static string GetGlobalFilePath()
        {
            string sceneGroupName = GetSceneGroupPath(currentScenePath);

            var path = Path.Combine(
                PersistentDataPath,
                "SaveFile-" + ZSerializerSettings.Instance.selectedSaveFile,
                sceneGroupName,
                currentSceneName);

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }

        static string GetSaveGrouplessFilePath()
        {
            string sceneGroupName = GetSceneGroupPath(currentScenePath);
            string sceneDir = string.IsNullOrEmpty(_currentLevelName)
                ? "data"
                : $"levels/{_currentLevelName}";

            string path = Path.Combine(
                PersistentDataPath,
                "SaveFile-" + ZSerializerSettings.Instance.selectedSaveFile,
                sceneGroupName,
                currentSceneName,
                sceneDir);

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }


//Gets complete filepath for a specific filename
        static string GetFilePath(string fileName)
        {
            string sceneGroupName = GetSceneGroupPath(currentScenePath);
            string sceneDir = string.IsNullOrEmpty(_currentLevelName)
                ? $"data/{ZSerializerSettings.Instance.saveGroups[CurrentGroupID]}"
                : $"levels/{_currentLevelName}/{ZSerializerSettings.Instance.saveGroups[CurrentGroupID]}";

            string path = Path.Combine(
                PersistentDataPath,
                "SaveFile-" + ZSerializerSettings.Instance.selectedSaveFile,
                sceneGroupName,
                currentSceneName,
                sceneDir);

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
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(ZSerialize.ReplaceZUIDs(json));
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

    public static class ZSExtensions
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
                case IZSerializable serializable: return serializable.ZUID;
                case GameObject gameObject:
                    return (gameObject.GetComponents<MonoBehaviour>().First(m => m is IZSerializable) as
                            IZSerializable)
                        .GOZUID;
                case Component component:
                    var pg = component.GetComponent<PersistentGameObject>();
                    return pg ? pg.ComponentZuidMap[component] : "-1";
                default: return null;
            }
        }

        public static string ToEditorBuildSettingsPath(this string path)
        {
            return path.Substring(7, path.Length - 13);
        }

        public static string ToAssetPath(this string path)
        {
            return $"Assets/{path}.unity";
        }

        public static async Task<AsyncOperation> WaitUntilDone(this AsyncOperation op)
        {
            while (!op.isDone)
            {
                await Task.Yield();
            }

            return op;
        }

        internal static void TryAddToDictionary(this Dictionary<string, Object> dictionary,
            string key, Object value)
        {
            while (dictionary.TryGetValue(key, out var _value))
            {
                if (_value == value) break;
                switch (value)
                {
                    case GameObject gameObject:
                        var zs =
                            gameObject.GetComponentsInChildren<MonoBehaviour>()
                                .First(m => m is IZSerializable) as IZSerializable;
                        zs.GenerateRuntimeZUIDs(true);
                        key = zs.GOZUID;
                        break;
                    case IZSerializable serializable:
                        serializable.GenerateRuntimeZUIDs(true);
                        key = serializable.ZUID;
                        break;
                    default:
                        throw new SerializationException(
                            $"{value} and {_value} have the same ZUID ({key}). Reset both PersistentGameObject components to fix it.");
                }
            }

            dictionary[key] = value;
        }

        public static async Task<object> InvokeAsync(this MethodInfo @this, object obj, params object[] parameters)
        {
            var task = (Task)@this.Invoke(obj, parameters);
            await task.ConfigureAwait(false);
            var resultProperty = task.GetType().GetProperty("Result");
            return resultProperty.GetValue(task);
        }

        public static List<int> ToIndexList<T>(this List<T> list)
        {
            var _list = new List<int>();
            for (var i = 0; i < list.Count; i++)
            {
                _list.Add(i);
            }

            return _list;
        }

        public static int[] ToIndexArray<T>(this List<T> list)
        {
            var arr = new int[list.Count];
            for (var i = 0; i < list.Count; i++)
            {
                arr[i] = i;
            }

            return arr;
        }
    }
}