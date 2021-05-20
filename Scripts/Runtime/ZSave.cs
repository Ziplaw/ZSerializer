using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace ZSaver
{
    public class ZSave
    {
        #region Big boys

        private static MethodInfo castMethod = typeof(Enumerable).GetMethod("Cast");
        private static MethodInfo toArrayMethod = typeof(Enumerable).GetMethod("ToArray");
        private static MethodInfo saveMethod = typeof(ZSave).GetMethod(nameof(Save));
        private static MethodInfo fromJsonMethod = typeof(JsonHelper).GetMethod(nameof(JsonHelper.FromJson));

        public static string mainAssembly = "Assembly-CSharp";

        public static Type[] ComponentSerializableTypes => AppDomain.CurrentDomain.GetAssemblies().SelectMany(a =>
            a.GetTypes().Where(t =>
                (t == typeof(PersistentGameObject)) || (
                    t.IsSubclassOf(typeof(Component)) &&
                    !t.IsSubclassOf(typeof(MonoBehaviour)) &&
                    t != typeof(Transform) &&
                    t != typeof(MonoBehaviour) &&
                    t.GetCustomAttribute<ObsoleteAttribute>() == null && t.IsVisible))).ToArray();

        public static readonly Dictionary<Type, string[]> ComponentBlackList = new Dictionary<Type, string[]>()
        {
            {typeof(LightProbeGroup), new[] {"dering"}},
            {typeof(Light), new[] {"shadowRadius", "shadowAngle", "areaSize", "lightmapBakeType"}},
            {typeof(MeshRenderer), new[] {"scaleInLightmap", "receiveGI", "stitchLightmapSeams"}},
            {typeof(Terrain), new[] {"bakeLightProbesForTrees", "deringLightProbesForTrees"}},
            {typeof(PersistentGameObject), new[] {"runInEditMode"}},
        };

        public static bool FieldIsSuitableForAssignment(PropertyInfo fieldInfo)
        {
            return fieldInfo.GetCustomAttribute<ObsoleteAttribute>() == null &&
                   fieldInfo.CanRead &&
                   fieldInfo.CanWrite &&
                   fieldInfo.Name != "material" &&
                   fieldInfo.Name != "materials" &&
                   fieldInfo.Name != "mesh" &&
                   fieldInfo.Name != "tag" &&
                   fieldInfo.Name != "name";
        }

        public static IEnumerable<Type> GetTypesWithPersistentAttribute(Assembly[] assemblies)
        {
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

        static int GetCurrentScene()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        public static Object FindObjectFromInstanceID(int instanceID)
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
                    .Where(c => (c.GetType() == typeof(PersistentGameObject)) ||
                                (!c.GetType().IsSubclassOf(typeof(MonoBehaviour)) && c.GetType() != typeof(Transform))))
                {
                    if (!componentTypes.Contains(component.GetType())) componentTypes.Add(component.GetType());
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
            
            Debug.Log(zSavers);
            Debug.Log(ZSaverType);
            Debug.Log(zSavers[0].GetType().GetField("gameObjectData"));
            Debug.Log(zSavers[0].GetType().GetField("gameObjectData").GetValue(zSavers[0]));
            zSavers = zSavers.OrderBy(x =>
                ((GameObjectData) x.GetType().GetField("gameObjectData").GetValue(x)).loadingOrder).ToArray();

            MethodInfo cast = castMethod.MakeGenericMethod(new Type[] {ZSaverType});

            MethodInfo toArray = toArrayMethod.MakeGenericMethod(new Type[] {ZSaverType});

            object result = cast.Invoke(zSavers, new object[] {zSavers});

            return (object[]) toArray.Invoke(result, new object[] {result});
        }

        static void ReflectedSave(object[] zsavers, string fileName)
        {
            Type ZSaverType = zsavers.GetType().GetElementType();
            var genericSaveMethodInfo = saveMethod.MakeGenericMethod(ZSaverType);
            genericSaveMethodInfo.Invoke(null, new object[] {zsavers, fileName});
        }

        static void CopyFieldsToFields(Type zSaverType, Type componentType, Component _component, object zSaver)
        {
            FieldInfo[] zSaverFields = zSaverType.GetFields();
            FieldInfo[] componentFields = componentType.GetFields();

            for (var i = 0; i < componentFields.Length; i++)
            {
                for (var j = 0; j < zSaverFields.Length; j++)
                {
                    if (zSaverFields[j].Name == componentFields[i].Name)
                    {
                        componentFields[i].SetValue(_component, zSaverFields[j].GetValue(zSaver));
                    }
                }
            }
        }

        static void CopyFieldsToProperties(Type componentType, Component c, object FromJSONdObject)
        {
            PropertyInfo[] propertyInfos = componentType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(FieldIsSuitableForAssignment).ToArray();

            FieldInfo[] fieldInfos = FromJSONdObject.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            for (var i = 0; i < fieldInfos.Length; i++)
            {
                for (var j = 0; j < propertyInfos.Length; j++)
                {
                    if (propertyInfos[j].Name == fieldInfos[i].Name)
                    {
                        Debug.Log("Same name " + propertyInfos[j].Name + " " + c + " " + componentType);
                        propertyInfos[j].SetValue(c, fieldInfos[i].GetValue(FromJSONdObject));
                    }
                }
            }
        }

        public static void UpdateAllJSONFiles(string[] previousFields, string[] newFields)
        {
            if (ZSaverSettings.instance.debugMode)
                Debug.Log("-------------------------------------------------------------");
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

                if (ZSaverSettings.instance.debugMode) Debug.Log(fileName + " " + newJson);
            }
        }

        static void UpdateComponentInstanceIDs(int prevComponentInstanceID, int newComponentInstanceID)
        {
            string COMPInstanceIDToReplaceString = $"instanceID\":{prevComponentInstanceID}";
            string newCOMPInstanceIDToReplaceString = "instanceID\":" + newComponentInstanceID;
            
            string COMPFileIDToReplaceString = $"m_FileID\":{prevComponentInstanceID}";
            string newCOMPFileIDToReplaceString = "m_FileID\":" + newComponentInstanceID;


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

        static void UpdateGameObjectInstanceIDs(int prevGameObjectInstanceID, int newGameObjectInstanceID)
        {
            Debug.Log(prevGameObjectInstanceID + " " + newGameObjectInstanceID);
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

            UpdateAllJSONFiles(
                new[]
                {
                    GOInstanceIDToReplaceString, GOInstanceIDToReplace, GOInstanceIDToReplaceParent, oldGOFileID, oldParentFileID
                },
                new[]
                {
                    newGOInstanceIDToReplaceString, newGOInstanceIDToReplace, newGOInstanceIDToReplaceParent, newFileID, newParentFileID
                });
        }

        #endregion

        #region Save

        public static void SaveAllObjects()
        {
            var types = GetTypesWithPersistentAttribute(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => Object.FindObjectOfType(t) != null);

            foreach (var type in types)
            {
                var objects = Object.FindObjectsOfType(type);
                SerializeComponents((Component[]) objects, Type.GetType(type.Name + "ZSaver, " + mainAssembly),
                    type.Name + ".save");
            }
        }

        private static void SaveAllPersistentGameObjects()
        {
            var objects = Object.FindObjectsOfType<PersistentGameObject>();
            var componentTypes = GetAllPersistentComponents(objects);

            foreach (var componentType in componentTypes)
            {
                var componentsOfGivenType = objects.SelectMany(o => o.GetComponents(componentType)).ToArray();
                SerializeComponents(componentsOfGivenType, Type.GetType(componentType.Name + "ZSaver"),
                    componentType.Name + "GameObject.save");
            }
        }

        static void SerializeComponents(Component[] components, Type zSaverType, string fileName)
        {
            if (zSaverType == null) return;

            object[] zSavers = CreateArrayOfZSavers(components, zSaverType);

            if (zSaverType == typeof(PersistentGameObjectZSaver))
            {
                zSavers = OrderPersistentGameObjectsByLoadingOrder(zSavers);
            }

            ReflectedSave(zSavers, fileName);
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

            UpdateGameObjectInstanceIDs(prevGOInstanceID, gameObjectInstanceID);
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
                
                Debug.Log(gameObject);

                if (gameObject == null)
                {
                    if (componentType != typeof(PersistentGameObject))
                    {
                        if (ZSaverSettings.instance.debugMode)
                            Debug.LogWarning(
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


                UpdateComponentInstanceIDs(prevCOMPInstanceID, componentInstanceID);
            }

            // CopyFieldsToFields(ZSaverType, componentType, componentInGameObject, FromJSONdObject);
            // Debug.Log("Beginning to copy everything for " + gameObject + " " + componentInGameObject);
            // CopyFieldsToProperties(componentType, componentInGameObject,
            //     FromJSONdObject);

            if (componentType == typeof(PersistentGameObject))
            {
                GameObjectData gameObjectData =
                    (GameObjectData) ZSaverType.GetField("gameObjectData").GetValue(FromJSONdObject);

                gameObject.transform.position = gameObjectData.position;
                gameObject.transform.rotation = gameObjectData.rotation;
                gameObject.transform.localScale = gameObjectData.size;
            }
        }

        public static void LoadAllObjects()
        {
            var types = GetTypesWithPersistentAttribute(AppDomain.CurrentDomain.GetAssemblies());

            foreach (var type in types)
            {
                Debug.LogWarning("Loading object: " + type);
                var ZSaverType = Type.GetType(type.Name + "ZSaver, " + mainAssembly);
                if (ZSaverType == null) continue;
               
                    var fromJson = fromJsonMethod.MakeGenericMethod(ZSaverType);

                if (!File.Exists(GetFilePath(type.Name + ".save"))) continue;

                object[] FromJSONdObjects = (object[]) fromJson.Invoke(null,
                    new object[] {ReadFromFile(type.Name + ".save")});

                for (var i = 0; i < FromJSONdObjects.Length; i++)
                {
                    FromJSONdObjects[i] = ((object[]) fromJson.Invoke(null,
                        new object[] {ReadFromFile(type.Name + ".save")}))[i];
                    LoadObjectsDynamically(ZSaverType, type, FromJSONdObjects[i]);
                }
            }
        }

        private static void LoadAllPersistentGameObjects()
        {
            var types = ComponentSerializableTypes.OrderByDescending(x => x == typeof(PersistentGameObject))
                .ToArray();

            foreach (var type in types)
            {
                
                if (!File.Exists(GetFilePath(type.Name + "GameObject.save"))) continue;
                var ZSaverType = Type.GetType(type.Name + "ZSaver");
                if (ZSaverType == null) continue;
                
                var fromJson = fromJsonMethod.MakeGenericMethod(ZSaverType);


                object[] FromJSONdObjects = (object[]) fromJson.Invoke(null,
                    new object[]
                        {ReadFromFile(type.Name + "GameObject.save")});

                for (var i = 0; i < FromJSONdObjects.Length; i++)
                {
                    FromJSONdObjects[i] = ((object[]) fromJson.Invoke(null,
                        new object[] {ReadFromFile(type.Name + "GameObject.save")}))[i];
                    Debug.LogWarning(ZSaverType.GetField("_componentParent").GetValue(FromJSONdObjects[i]));
                    LoadObjectsDynamically(ZSaverType, type, FromJSONdObjects[i]);
                }
            }
        }

        private static void LoadReferences()
        {
            var types = ComponentSerializableTypes.OrderByDescending(x => x == typeof(PersistentGameObject))
                .ToArray();

            foreach (var type in types)
            {
                if (!File.Exists(GetFilePath(type.Name + "GameObject.save"))) continue;
                var ZSaverType = Type.GetType(type.Name + "ZSaver");
                if (ZSaverType == null) continue;
                var fromJson = fromJsonMethod.MakeGenericMethod(ZSaverType);


                object[] FromJSONdObjects = (object[]) fromJson.Invoke(null,
                    new object[]
                        {ReadFromFile(type.Name + "GameObject.save")});
                Debug.LogWarning(ReadFromFile(type.Name + "GameObject.save"));

                for (var i = 0; i < FromJSONdObjects.Length; i++)
                {
                    Component componentInGameObject =
                        (Component) ZSaverType.GetField("_component").GetValue(FromJSONdObjects[i]);

                    CopyFieldsToProperties(type, componentInGameObject, FromJSONdObjects[i]);
                    CopyFieldsToFields(ZSaverType, type, componentInGameObject, FromJSONdObjects[i]);
                }
            }

            types = GetTypesWithPersistentAttribute(AppDomain.CurrentDomain.GetAssemblies()).ToArray();

            foreach (var type in types)
            {
                var ZSaverType = Type.GetType(type.Name + "ZSaver, " + mainAssembly);
                if (ZSaverType == null) continue;
                var fromJson = fromJsonMethod.MakeGenericMethod(ZSaverType);
                
                if (!File.Exists(GetFilePath(type.Name + ".save"))) continue;

                object[] FromJSONdObjects = (object[]) fromJson.Invoke(null,
                    new object[] {ReadFromFile(type.Name + ".save")});

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

        public static void SaveAllObjectsAndComponents()
        {
            string[] files = Directory.GetFiles(GetFilePath(""));
            foreach (string file in files)
            {
                File.Delete(file);
            }

            SaveAllPersistentGameObjects();
            SaveAllObjects();
        }

        public static void LoadAllObjectsAndComponents()
        {
            LoadAllPersistentGameObjects();
            LoadAllObjects();
            LoadReferences();


            SaveAllObjectsAndComponents(); //Temporary fix for objects duping after loading destroyed GOs
        }


        #region JSON Formatting

        public static void Save<T>(T[] objectsToPersist, string fileName)
        {
            string json = JsonHelper.ToJson(objectsToPersist, false);
            if (ZSaverSettings.instance.debugMode) Debug.Log(typeof(T) + " " + json);
            WriteToFile(fileName, json);
        }

        static void WriteToFile(string fileName, string json)
        {
            StreamWriter writer = new StreamWriter(GetFilePath(fileName));
            writer.WriteLine(json);
            writer.Close();
        }

        public static string ReadFromFile(string fileName)
        {
            StreamReader reader = new StreamReader(GetFilePath(fileName));
            string json = reader.ReadToEnd();
            reader.Close();
            return json;
        }

        static string GetFilePath(string fileName)
        {
            int currentScene = GetCurrentScene();
            if (currentScene == SceneManager.sceneCount)
            {
                Debug.LogWarning(
                    "Be careful! You're trying to save data in an unbuilt Scene, and any data saved in other unbuilt Scenes will overwrite this one, and vice-versa.\n" +
                    "If you want your data to persist properly, add this scene to the list of Scenes In Build in your Build Settings");
            }
            
            string path = Path.Combine(Application.persistentDataPath,
                ZSaverSettings.instance.selectedSaveFile.ToString(), currentScene.ToString());
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            return Path.Combine(path, fileName);
        }

        #endregion
    }

    public static class JsonHelper
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
}