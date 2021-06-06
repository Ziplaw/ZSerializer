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
[assembly: InternalsVisibleTo("Assembly-CSharp")]

namespace ZSaver
{
    public class ZMono : MonoBehaviour
    {
        private static ZMono _instance;

        public static ZMono Instance
        {
            get
            {
                if (_instance) return _instance;
                _instance = new GameObject().AddComponent<ZMono>();
                DontDestroyOnLoad(_instance.gameObject);
                return _instance;
            }
        }
    }

    public class ZSave
    {
        #region Big boys

        static Action OnBeforeSave;
        static Action OnAfterSave;

        private static MethodInfo castMethod = typeof(Enumerable).GetMethod("Cast");
        private static MethodInfo toArrayMethod = typeof(Enumerable).GetMethod("ToArray");

        private static MethodInfo saveMethod =
            typeof(ZSave).GetMethod(nameof(Save), BindingFlags.NonPublic | BindingFlags.Static);

        private static MethodInfo fromJsonMethod = typeof(JsonHelper).GetMethod(nameof(JsonHelper.FromJson));

        internal static string mainAssembly = "Assembly-CSharp";
        static Dictionary<int, int> idStorage = new Dictionary<int, int>();
        private static IEnumerable<Type> persistentTypes;

        private static IEnumerable<Type> serializableComponentTypes;
        // internal static IEnumerable<Type> PersistentTypes
        // {
        //     get
        //     {
        //         Debug.Log(persistentTypes != null);
        //         if (persistentTypes != null) return persistentTypes;
        //         persistentTypes = GetTypesWithPersistentAttribute();
        //         return persistentTypes;
        //     }
        // }

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
                                            t.GetCustomAttribute<ObsoleteAttribute>() == null && t.IsVisible /*&&
                                            !ManualComponentBlacklist.Contains(t)*/)
                );
            }
        }

        static IEnumerable<Type> GetSerializedComponentsInCurrentSaveFile()
        {
            foreach (var fileName in Directory.GetFiles(GetFilePath("")))
            {
                string typeName = fileName.Split('\\').Last().Replace(".save", "");
                Assembly assembly = ZSaverSettings.Instance.AddedAssemblies.First(a => a.GetType(typeName) != null);
                yield return Type.GetType($"{typeName}, {assembly}");
            }
        }


        // static readonly Type[] ManualComponentBlacklist =
        //         {typeof(MeshRenderer), typeof(SkinnedMeshRenderer), typeof(SpriteRenderer)};


        internal static readonly Dictionary<Type, string[]> ComponentBlackList = new Dictionary<Type, string[]>()
        {
            {typeof(LightProbeGroup), new[] {"dering"}},
            {typeof(Light), new[] {"shadowRadius", "shadowAngle", "areaSize", "lightmapBakeType"}},
            {typeof(MeshRenderer), new[] {"scaleInLightmap", "receiveGI", "stitchLightmapSeams"}},
            {typeof(Terrain), new[] {"bakeLightProbesForTrees", "deringLightProbesForTrees"}},
            {typeof(MonoBehaviour), new[] {"runInEditMode", "useGUILayout"}},
        };

        internal static bool FieldIsSuitableForAssignment(PropertyInfo fieldInfo)
        {
            return fieldInfo.GetCustomAttribute<ObsoleteAttribute>() == null &&
                   fieldInfo.GetCustomAttribute<OmitSerializableCheck>() == null &&
                   fieldInfo.CanRead &&
                   fieldInfo.CanWrite &&
                   (
                       !ComponentBlackList.ContainsKey(fieldInfo.DeclaringType) || (
                           ComponentBlackList.ContainsKey(fieldInfo.DeclaringType) &&
                           !ComponentBlackList[fieldInfo.DeclaringType].Contains(fieldInfo.Name))
                   ) &&
                   fieldInfo.Name != "material" &&
                   fieldInfo.Name != "materials" &&
                   fieldInfo.Name != "sharedMaterial" &&
                   fieldInfo.Name != "mesh" &&
                   fieldInfo.Name != "tag" &&
                   fieldInfo.Name != "name";
        }

        internal static IEnumerable<Type> GetTypesWithPersistentAttribute()
        {
            var assemblies = ZSaverSettings.Instance.AddedAssemblies; /*AppDomain.CurrentDomain
                .GetAssemblies();*/

            foreach (var assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.GetCustomAttributes(typeof(PersistentAttribute), true).Length > 0)
                    {
                        yield return type;
                    }
                }
            }
        }

        #endregion

        #region HelperFunctions

        [RuntimeInitializeOnLoadMethod]
        internal static void Init()
        {
            RecordAllPersistentIDs();

            SceneManager.sceneUnloaded += scene => { RestoreTempIDs(); };

            SceneManager.sceneLoaded += (scene, mode) => { RecordAllPersistentIDs(); };

            Application.wantsToQuit += () =>
            {
                RestoreTempIDs();
                return true;
            };

            persistentTypes = GetTypesWithPersistentAttribute();
            serializableComponentTypes = ComponentSerializableTypes;
        }

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
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.Invoke(null, new object[] {instanceID});
        }

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
                        if (component.GetType() == typeof(PersistentGameObject))
                            componentTypes.Add(component.GetType());
                        else
                        {
                            var datas = persistentGameObject._componentDatas;
                            bool componentIsSerialized = false;
                            foreach (var serializableComponentData in datas)
                            {
                                if (Type.GetType(serializableComponentData.typeName) == component.GetType() &&
                                    serializableComponentData.serialize) componentIsSerialized = true;
                            }

                            if (componentIsSerialized)
                                componentTypes.Add(component.GetType());
                        }
                    }
                }
            }

            return componentTypes;
        }

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

            // Debug.Log(zSavers);
            // Debug.Log(ZSaverType);
            // Debug.Log(zSavers[0].GetType().GetField("gameObjectData"));
            // Debug.Log(zSavers[0].GetType().GetField("gameObjectData").GetValue(zSavers[0]));
            zSavers = zSavers.OrderBy(x =>
                ((GameObjectData) x.GetType().GetField("gameObjectData").GetValue(x)).loadingOrder).ToArray();

            MethodInfo cast = castMethod.MakeGenericMethod(new Type[] {ZSaverType});

            MethodInfo toArray = toArrayMethod.MakeGenericMethod(new Type[] {ZSaverType});

            object result = cast.Invoke(zSavers, new object[] {zSavers});

            return (object[]) toArray.Invoke(result, new object[] {result});
        }

        static IEnumerator ReflectedSave(object[] zsavers, string fileName)
        {
            Type ZSaverType = zsavers.GetType().GetElementType();
            var genericSaveMethodInfo = saveMethod.MakeGenericMethod(ZSaverType);
            genericSaveMethodInfo.Invoke(null, new object[] {zsavers, fileName});
            yield return new WaitForEndOfFrame();
        }

        static void CopyFieldsToFields(Type zSaverType, Type componentType, Component _component, object zSaver)
        {
            FieldInfo[] zSaverFields = zSaverType.GetFields();
            FieldInfo[] componentFields = componentType.GetFields();

            for (var i = 0; i < componentFields.Length; i++)
            {
                for (var j = 0; j < zSaverFields.Length; j++)
                {
                    if (zSaverFields[j].Name == componentFields[i].Name && _component != null)
                    {
                        componentFields[i].SetValue(_component, zSaverFields[j].GetValue(zSaver));
                    }
                }
            }
        }

        static void CopyFieldsToProperties(Type componentType, Component c, object FromJSONdObject)
        {
            IEnumerable<PropertyInfo> propertyInfos = componentType.GetProperties()
                .Where(FieldIsSuitableForAssignment);

            FieldInfo[] fieldInfos = FromJSONdObject.GetType().GetFields();

            for (var i = 0; i < fieldInfos.Length; i++)
            {
                for (var j = 0; j < propertyInfos.Count(); j++)
                {
                    if (propertyInfos.ElementAt(j).Name == fieldInfos[i].Name)
                    {
                        if (propertyInfos.ElementAt(j) != null)
                            propertyInfos.ElementAt(j).SetValue(c, fieldInfos[i].GetValue(FromJSONdObject));
                    }
                }
            }
        }

        static void UpdateAllJSONFiles(string[] previousFields, string[] newFields, bool isRestoring = false)
        {
            // /*if(!isRestoring)*/ Log("-------------------------------------------------------------");
            foreach (var file in Directory.GetFiles(GetFilePath(""), "*.save",
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

                // /*if(!isRestoring)*/ Log(previousFields.First() + " " + fileName + "Prev " + json);
                // /*if(!isRestoring)*/ Log(newFields.First() + " " + fileName + "New " + newJson);
            }
        }

        static void UpdateAllInstanceIDs(int prevInstanceID, int newInstanceID,
            bool isRestoring = false)
        {
            // string COMPInstanceIDToReplaceString = $"instanceID\":{prevInstanceID}";
            // string newCOMPInstanceIDToReplaceString = "instanceID\":" + newInstanceID;
            //
            // string COMPFileIDToReplaceString = $"m_FileID\":{prevInstanceID}";
            // string newCOMPFileIDToReplaceString = "m_FileID\":" + newInstanceID;
            //
            // string GOInstanceIDToReplaceString = "\"gameObjectInstanceID\":" + prevInstanceID;
            // string GOInstanceIDToReplace =
            //     "\"_componentParent\":{\"instanceID\":" + prevInstanceID + "}";
            // string GOInstanceIDToReplaceParent = "\"parent\":{\"instanceID\":" + prevInstanceID + "}";
            // string oldParentFileID = "\"parent\":{\"m_FileID\":" + prevInstanceID;
            // string oldGOFileID = "\"_componentParent\":{\"m_FileID\":" + prevInstanceID;
            // //"parent":{"instanceID":-15442}
            //
            // string newGOInstanceIDToReplaceString = "\"gameObjectInstanceID\":" + newInstanceID;
            // string newGOInstanceIDToReplace =
            //     "\"_componentParent\":{\"instanceID\":" + newInstanceID + "}";
            // string newGOInstanceIDToReplaceParent =
            //     "\"parent\":{\"instanceID\":" + newInstanceID + "}";
            // string newGOFileID = "\"_componentParent\":{\"m_FileID\":" + newInstanceID;
            // string newParentFileID = "\"parent\":{\"m_FileID\":" + prevInstanceID;

            if (!isRestoring)
            {
                RecordTempID(prevInstanceID, newInstanceID);
            }

            // UpdateAllJSONFiles(
            //     new[]
            //     {
            //         COMPInstanceIDToReplaceString, COMPFileIDToReplaceString, GOInstanceIDToReplaceString,
            //         GOInstanceIDToReplace, GOInstanceIDToReplaceParent, oldParentFileID, oldGOFileID
            //     }, new[]
            //     {
            //         newCOMPInstanceIDToReplaceString, newCOMPFileIDToReplaceString, newGOInstanceIDToReplaceString,
            //         newGOInstanceIDToReplace, newGOInstanceIDToReplaceParent, newParentFileID, newGOFileID
            //     });
            UpdateAllJSONFiles(new[] {prevInstanceID.ToString()}, new[] {newInstanceID.ToString()}, isRestoring);
        }

        static void UpdateComponentInstanceIDs(int prevComponentInstanceID, int newComponentInstanceID,
            bool isRestoring = false)
        {
            string COMPInstanceIDToReplaceString = $"instanceID\":{prevComponentInstanceID}";
            string newCOMPInstanceIDToReplaceString = "instanceID\":" + newComponentInstanceID;

            string COMPFileIDToReplaceString = $"m_FileID\":{prevComponentInstanceID}";
            string newCOMPFileIDToReplaceString = "m_FileID\":" + newComponentInstanceID;

            if (!isRestoring)
            {
                RecordTempID(prevComponentInstanceID, newComponentInstanceID);
            }

            UpdateAllJSONFiles(
                new[]
                {
                    COMPInstanceIDToReplaceString, COMPFileIDToReplaceString
                },
                new[]
                {
                    newCOMPInstanceIDToReplaceString, newCOMPFileIDToReplaceString
                });
        }

        static void UpdateGameObjectInstanceIDs(int prevGameObjectInstanceID, int newGameObjectInstanceID,
            bool isRestoring = false)
        {
            string GOInstanceIDToReplaceString = "\"gameObjectInstanceID\":" + prevGameObjectInstanceID;
            string GOInstanceIDToReplace =
                "\"_componentParent\":{\"instanceID\":" + prevGameObjectInstanceID + "}";
            string GOInstanceIDToReplaceParent = "\"parent\":{\"instanceID\":" + prevGameObjectInstanceID + "}";
            string oldParentFileID = "\"parent\":{\"m_FileID\":" + prevGameObjectInstanceID;
            string oldGOFileID = "\"_componentParent\":{\"m_FileID\":" + prevGameObjectInstanceID;
            //"parent":{"instanceID":-15442}

            string newGOInstanceIDToReplaceString = "\"gameObjectInstanceID\":" + newGameObjectInstanceID;
            string newGOInstanceIDToReplace =
                "\"_componentParent\":{\"instanceID\":" + newGameObjectInstanceID + "}";
            string newGOInstanceIDToReplaceParent =
                "\"parent\":{\"instanceID\":" + newGameObjectInstanceID + "}";
            string newFileID = "\"_componentParent\":{\"m_FileID\":" + newGameObjectInstanceID;
            string newParentFileID = "\"parent\":{\"m_FileID\":" + prevGameObjectInstanceID;

            if (!isRestoring)
            {
                RecordTempID(prevGameObjectInstanceID, newGameObjectInstanceID);
            }

            UpdateAllJSONFiles(
                new[]
                {
                    GOInstanceIDToReplaceString, GOInstanceIDToReplace, GOInstanceIDToReplaceParent, oldGOFileID,
                    oldParentFileID
                },
                new[]
                {
                    newGOInstanceIDToReplaceString, newGOInstanceIDToReplace, newGOInstanceIDToReplaceParent, newFileID,
                    newParentFileID
                });
        }

        #endregion

        #region Save

        static IEnumerator SaveAllObjects()
        {
            var types = persistentTypes.Where(t => Object.FindObjectOfType(t) != null);

            foreach (var type in types)
            {
                var objects = Object.FindObjectsOfType(type);
                yield return SerializeComponents((Component[]) objects,
                    Type.GetType(type.Name + "ZSerializer, " + mainAssembly),
                    type.FullName + ".save");
            }
        }

        static Component[] GetSerializedComponentsOfGivenType(PersistentGameObject[] objects, Type componentType)
        {
            List<Component> serializedComponentsOfGivenType = new List<Component>();

            var componentsOfGivenType = objects.SelectMany(o => o.GetComponents(componentType));

            foreach (var c in componentsOfGivenType)
            {
                var persistentGameObject = c.GetComponent<PersistentGameObject>();

                var datas = persistentGameObject._componentDatas;
                bool componentIsSerialized = false;
                foreach (var serializableComponentData in datas)
                {
                    if (Type.GetType(serializableComponentData.typeName) == componentType &&
                        serializableComponentData.serialize) componentIsSerialized = true;
                }

                if (componentIsSerialized || componentType == typeof(PersistentGameObject))
                    serializedComponentsOfGivenType.Add(c);
            }

            return serializedComponentsOfGivenType.ToArray();
        }

        static IEnumerator SaveAllPersistentGameObjects()
        {
            var objects = Object.FindObjectsOfType<PersistentGameObject>();
            var componentTypes = GetAllPersistentComponents(objects);

            foreach (var componentType in componentTypes)
            {
                yield return SerializeComponents(GetSerializedComponentsOfGivenType(objects, componentType),
                    Type.GetType(componentType.Name + "ZSerializer"),
                    componentType.FullName + ".save");
            }
        }

        static IEnumerator SerializeComponents(Component[] components, Type zSaverType, string fileName)
        {
            if (zSaverType == null) yield return new WaitForEndOfFrame();

            object[] zSavers = CreateArrayOfZSavers(components, zSaverType);

            if (zSaverType == typeof(PersistentGameObjectZSerializer))
            {
                zSavers = OrderPersistentGameObjectsByLoadingOrder(zSavers);
            }

            yield return ReflectedSave(zSavers, fileName);
        }

        #endregion

        #region Load

        static void LoadDestroyedGameObject(int gameObjectInstanceID, out GameObject gameObject, Type ZSaverType,
            object FromJSONdObject)
        {
            int prevGOInstanceID = gameObjectInstanceID;

            //ONLY PRESENT IN PERSISTENT GAMEOBJECT
            GameObjectData gameObjectData =
                (GameObjectData) ZSaverType.GetField("gameObjectData").GetValue(FromJSONdObject);

            gameObject = gameObjectData.MakePerfectlyValidGameObject();
            gameObject.AddComponent<PersistentGameObject>();
            gameObjectInstanceID = gameObject.GetInstanceID();

            // UpdateGameObjectInstanceIDs(prevGOInstanceID, gameObjectInstanceID);
            UpdateAllInstanceIDs(prevGOInstanceID, gameObjectInstanceID);
        }

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

                Debug.LogWarning("updating " + componentType);
                UpdateAllInstanceIDs(prevCOMPInstanceID, componentInstanceID);
                // UpdateComponentInstanceIDs(prevCOMPInstanceID, componentInstanceID);
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

        static void LoadAllObjects()
        {
            var types = persistentTypes;

            foreach (var type in types)
            {
                var ZSaverType = Type.GetType(type.Name + "ZSerializer, " + mainAssembly);
                if (ZSaverType == null) continue;

                var fromJson = fromJsonMethod.MakeGenericMethod(ZSaverType);

                if (!File.Exists(GetFilePath(type.FullName + ".save"))) continue;

                object[] FromJSONdObjects = (object[]) fromJson.Invoke(null,
                    new object[] {ReadFromFile(type.FullName + ".save")});

                for (var i = 0; i < FromJSONdObjects.Length; i++)
                {
                    FromJSONdObjects[i] = ((object[]) fromJson.Invoke(null,
                        new object[] {ReadFromFile(type.FullName + ".save")}))[i];
                    LoadObjectsDynamically(ZSaverType, type, FromJSONdObjects[i]);
                }
            }
        }

        static void LoadAllPersistentGameObjects()
        {
            var types = GetSerializedComponentsInCurrentSaveFile()
                .OrderByDescending(x => x == typeof(PersistentGameObject));

            foreach (var type in types)
            {
                if (!File.Exists(GetFilePath(type.FullName + ".save"))) continue;
                var ZSaverType = Type.GetType(type.Name + "ZSerializer");
                if (ZSaverType == null) continue;

                var fromJson = fromJsonMethod.MakeGenericMethod(ZSaverType);


                object[] FromJSONdObjects = (object[]) fromJson.Invoke(null,
                    new object[]
                        {ReadFromFile(type.FullName + ".save")});

                for (var i = 0; i < FromJSONdObjects.Length; i++)
                {
                    FromJSONdObjects[i] = ((object[]) fromJson.Invoke(null,
                        new object[] {ReadFromFile(type.FullName + ".save")}))[i];
                    LoadObjectsDynamically(ZSaverType, type, FromJSONdObjects[i]);
                }
            }
        }

        static void LoadReferences()
        {
            IEnumerable<Type> types = GetSerializedComponentsInCurrentSaveFile()
                .OrderByDescending(x => x == typeof(PersistentGameObject))
                .ToArray();

            foreach (var type in types)
            {
                if (!File.Exists(GetFilePath(type.FullName + ".save"))) continue;
                var ZSaverType = Type.GetType(type.Name + "ZSerializer");
                if (ZSaverType == null) continue;
                var fromJson = fromJsonMethod.MakeGenericMethod(ZSaverType);


                object[] FromJSONdObjects = (object[]) fromJson.Invoke(null,
                    new object[]
                        {ReadFromFile(type.FullName + ".save")});

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

                if (!File.Exists(GetFilePath(type.FullName + ".save"))) continue;

                object[] FromJSONdObjects = (object[]) fromJson.Invoke(null,
                    new object[] {ReadFromFile(type.FullName + ".save")});

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

        static void RecordAllPersistentIDs()
        {
            var objs = Object.FindObjectsOfType<PersistentGameObject>();


            idStorage = objs.SelectMany(o => o.GetComponents(typeof(Component))
                    .Where(c =>
                        c.GetType() == typeof(PersistentGameObject) ||
                        c.GetType().GetCustomAttribute<PersistentAttribute>() != null ||
                        GetSerializedComponentsOfGivenType(objs, c.GetType()).Contains(c)
                    ))
                .ToDictionary(component => component.GetInstanceID(), component => component.GetInstanceID());

            idStorage.Append(
                objs.Select(o => o.gameObject).ToDictionary(o => o.GetInstanceID(), o => o.GetInstanceID()));


            // foreach (var persistentGameObject in objs)
            // {
            //     int id = persistentGameObject.gameObject.GetInstanceID();
            //
            //     idStorage.Add(id, id);
            // }

            // var componentTypes = GetAllPersistentComponents(objs);
            // foreach (var componentType in componentTypes)
            // {
            //     var components = GetSerializedComponentsOfGivenType(objs, componentType);
            //
            //     int[] ids = components.Select(c => c.GetInstanceID()).ToArray();
            //     foreach (var id in ids)
            //     {
            //         idStorage.Add(id, id);
            //     }
            // }

            // foreach (var keyValuePair in idStorage)
            // {
            //     Debug.Log( "id: " + keyValuePair.Key + " " + FindObjectFromInstanceID(keyValuePair.Value).GetType());
            // }
        }

        static void RecordTempID(int prevID, int newID)
        {
            Debug.LogWarning("got called!");
            for (var i = 0; i < idStorage.Count; i++)
            {
                if (idStorage[idStorage.Keys.ToArray()[i]] == prevID)
                {
                    idStorage[idStorage.Keys.ToArray()[i]] = newID;
                    Debug.LogWarning(prevID + " is now " + newID);
                }
            }
        }

        static void RestoreTempIDs()
        {
            LogWarning("Restoring Temporary IDs");

            for (var i = 0; i < idStorage.Count; i++)
            {
                // UpdateGameObjectInstanceIDs(idStorage[idStorage.Keys.ToArray()[i]], idStorage.Keys.ToArray()[i], true);
                // UpdateComponentInstanceIDs(idStorage[idStorage.Keys.ToArray()[i]], idStorage.Keys.ToArray()[i], true);
                UpdateAllInstanceIDs(idStorage[idStorage.Keys.ToArray()[i]], idStorage.Keys.ToArray()[i], true);
            }
        }

        public static void SaveAll()
        {
            ZMono.Instance.StartCoroutine(SaveAllCoroutine());
        }

        static IEnumerator SaveAllCoroutine()
        {
            OnBeforeSave?.Invoke();
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

            Log("Serialization ended in: " + (Time.realtimeSinceStartup - startingTime) + " seconds or " +
                (Time.frameCount - frameCount) + " frames");
            OnAfterSave?.Invoke();
        }

        public static void LoadAll()
        {
            float startingTime = Time.realtimeSinceStartup;
            float frameCount = Time.frameCount;

            LoadAllPersistentGameObjects();
            LoadAllObjects();
            LoadReferences();

            Log("Deserialization ended in: " + (Time.realtimeSinceStartup - startingTime) + " seconds or " +
                (Time.frameCount - frameCount) + " frames");


            // SaveAll(); //Temporary fix for objects duping after loading destroyed GOs

            // string[]
            //     files = Directory.GetFiles(
            //         GetFilePath("")); //Temporary fix for objects duping after loading destroyed GOs
            // foreach (string file in files)
            // {
            //     File.Delete(file);
            // }
            //
            // SaveAllPersistentGameObjects();
            // SaveAllObjects();
        }


        #region JSON Formatting

        static void Save<T>(T[] objectsToPersist, string fileName)
        {
            string json = JsonHelper.ToJson(objectsToPersist, false);
            Log(typeof(T) + " " + json);
            WriteToFile(fileName, json);
        }

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

        static string ReadFromFile(string fileName)
        {
            if (ZSaverSettings.Instance.encryptData)
            {
                byte[] key =
                    {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F};

                return DecryptStringFromBytes(File.ReadAllBytes(GetFilePath(fileName)), key, key);
            }

            return File.ReadAllText(GetFilePath(fileName));
        }

        internal static string GetFilePath(string fileName)
        {
            int currentScene = GetCurrentScene();
            if (currentScene == SceneManager.sceneCount)
            {
                Debug.LogWarning(
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