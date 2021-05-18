using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ZSave
{
    public class PersistanceManager
    {
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
                   fieldInfo.Name != "materials";
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
        
        public static void SaveAllObjects()
        {
            var types = GetTypesWithPersistentAttribute(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => Object.FindObjectOfType(t) != null);

            foreach (var type in types)
            {
                var objects = Object.FindObjectsOfType(type);

                var ZSaverType = Type.GetType(type.Name + "ZSaver");
                if (ZSaverType == null) continue;
                var ZSaverArrayType = ZSaverType.MakeArrayType();

                var zsaversArray = Activator.CreateInstance(ZSaverArrayType, new object[] {objects.Length});

                object[] zsavers = (object[]) zsaversArray;


                for (var i = 0; i < zsavers.Length; i++)
                {
                    zsavers[i] = Activator.CreateInstance(ZSaverType, new object[] {objects[i]});
                }


                var saveMethodInfo = typeof(PersistanceManager).GetMethod(nameof(PersistanceManager.Save));
                var genericSaveMethodInfo = saveMethodInfo.MakeGenericMethod(ZSaverType);
                genericSaveMethodInfo.Invoke(null, new object[] {zsavers, type.Name + ".save"});
            }
        }

        public static void LoadAllObjects()
        {
            var types = GetTypesWithPersistentAttribute(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => Object.FindObjectOfType(t) != null);

            foreach (var type in types)
            {
                var ZSaverType = Type.GetType(type.Name + "ZSaver");
                if (ZSaverType == null) continue;
                MethodInfo fromJsonMethod = typeof(JsonHelper).GetMethod(nameof(JsonHelper.FromJson))
                    .MakeGenericMethod(ZSaverType);

                object[] FromJSONdObjects = (object[]) fromJsonMethod.Invoke(null,
                    new object[] {PersistanceManager.ReadFromFile(type.Name + ".save")});

                for (var i = 0; i < FromJSONdObjects.Length; i++)
                {
                    FromJSONdObjects[i] = ((object[]) fromJsonMethod.Invoke(null,
                        new object[] {PersistanceManager.ReadFromFile(type.Name + ".save")}))[i];
                    MethodInfo loadMethod = ZSaverType.GetMethod("LoadComponent");
                    loadMethod.Invoke(FromJSONdObjects[i],
                        new object[] {ZSaverType});
                }
            }
        }

        private static void SaveAllPersistentGameObjects()
        {
            List<Type> componentTypes = new List<Type>();

            var objects = Object.FindObjectsOfType<PersistentGameObject>();

            foreach (var persistentGameObject in objects)
            {
                foreach (var component in persistentGameObject.GetComponents<Component>()
                    .Where(c => (c.GetType() == typeof(PersistentGameObject)) ||
                                (!c.GetType().IsSubclassOf(typeof(MonoBehaviour)) && c.GetType() != typeof(Transform))))
                {
                    var componentType = component.GetType();
                    bool isRepeated = false;

                    for (var i = 0; i < componentTypes.Count; i++)
                    {
                        if (componentType == componentTypes[i])
                        {
                            isRepeated = true;
                            break;
                        }
                    }

                    if (!isRepeated) componentTypes.Add(componentType);
                }
            }

            foreach (var componentType in componentTypes)
            {
                var componentsOfGivenType = objects.SelectMany(o => o.GetComponents(componentType)).ToArray();

                var ZSaverType = Type.GetType(componentType.Name + "ZSaver");
                if (ZSaverType == null) continue;
                var ZSaverArrayType = ZSaverType.MakeArrayType();


                var zSaversArray =
                    Activator.CreateInstance(ZSaverArrayType, new object[] {componentsOfGivenType.Length});

                object[] zSavers = (object[]) zSaversArray;

                for (var i = 0; i < zSavers.Length; i++)
                {
                    zSavers[i] = Activator.CreateInstance(ZSaverType, new object[] {componentsOfGivenType[i]});
                }

                if (componentType == typeof(PersistentGameObject))
                {

                    zSavers = zSavers.OrderBy(x =>
                        ((GameObjectData) x.GetType().GetField("gameObjectData").GetValue(x)).loadingOrder).ToArray();

                    MethodInfo cast = typeof(Enumerable).GetMethod("Cast")
                        .MakeGenericMethod(new Type[] {ZSaverType});

                    MethodInfo toArrayMethod = typeof(Enumerable).GetMethod("ToArray")
                        .MakeGenericMethod(new Type[] {ZSaverType});

                    object result = cast.Invoke(zSavers, new object[] {zSavers});

                    zSavers = (object[]) toArrayMethod.Invoke(result, new object[] {result});
                }

                var saveMethodInfo = typeof(PersistanceManager).GetMethod(nameof(PersistanceManager.Save));
                var genericSaveMethodInfo = saveMethodInfo.MakeGenericMethod(ZSaverType);
                genericSaveMethodInfo.Invoke(null, new object[] {zSavers, componentType.Name + "GameObject.save"});
            }
        }

        private static void LoadAllPersistentGameObjects()
        {
            var types = ComponentSerializableTypes.OrderByDescending(x => x == typeof(PersistentGameObject))
                .ToArray();

            List<Component> alreadySeenComponents = new List<Component>();

            foreach (var type in types)
            {
                if (!File.Exists(Application.persistentDataPath + "/" + type.Name + "GameObject.save")) continue;
                var ZSaverType = Type.GetType(type.Name + "ZSaver");
                if (ZSaverType == null) continue;
                MethodInfo fromJsonMethod = typeof(JsonHelper).GetMethod(nameof(JsonHelper.FromJson))
                    .MakeGenericMethod(ZSaverType);


                object[] FromJSONdObjects = (object[]) fromJsonMethod.Invoke(null,
                    new object[]
                        {ReadFromFile(type.Name + "GameObject.save")});


                for (var i = 0; i < FromJSONdObjects.Length; i++)
                {
                    FromJSONdObjects[i] = ((object[]) fromJsonMethod.Invoke(null,
                        new object[] {PersistanceManager.ReadFromFile(type.Name + "GameObject.save")}))[i];

                    GameObject gameObject =
                        (GameObject) ZSaverType.GetField("_componentParent").GetValue(FromJSONdObjects[i]);

                    Component componentInGameObject =
                        (Component) ZSaverType.GetField("_component").GetValue(FromJSONdObjects[i]);

                    int componentInstanceID =
                        (int) ZSaverType.GetField("componentinstanceID").GetValue(FromJSONdObjects[i]);
                    int gameObjectInstanceID =
                        (int) ZSaverType.GetField("gameObjectInstanceID").GetValue(FromJSONdObjects[i]);


                    if (componentInGameObject == null)
                    {
                        string prevCOMPInstanceID = componentInstanceID.ToString();
                        string COMPInstanceIDToReplaceString = $"instanceID\":{prevCOMPInstanceID}";


                        if (gameObject == null)
                        {
                            string prevGOInstanceID = gameObjectInstanceID.ToString();
                            string GOInstanceIDToReplaceString = "\"gameObjectInstanceID\":" + prevGOInstanceID;
                            string GOInstanceIDToReplace =
                                "\"_componentParent\":{\"instanceID\":" + prevGOInstanceID + "}";
                            string GOInstanceIDToReplaceParent = "\"parent\":{\"instanceID\":" + prevGOInstanceID + "}";
                            
                            GameObjectData gameObjectData = 
                                (GameObjectData) ZSaverType.GetField("gameObjectData").GetValue(FromJSONdObjects[i]);

                            gameObject = gameObjectData.MakePerfectlyValidGameObject();
                            gameObject.AddComponent<PersistentGameObject>();
                            gameObjectInstanceID = gameObject.GetInstanceID();


                            ZSaverType.GetField("gameObjectInstanceID")
                                .SetValue(FromJSONdObjects[i], gameObject.GetInstanceID());

                            string newGOInstanceIDToReplaceString = "\"gameObjectInstanceID\":" + gameObjectInstanceID;
                            string newGOInstanceIDToReplace =
                                "\"_componentParent\":{\"instanceID\":" + gameObjectInstanceID + "}";
                            string newGOInstanceIDToReplaceParent =
                                "\"parent\":{\"instanceID\":" + gameObjectInstanceID + "}";

                            UpdateAllJSONFiles(
                                new[]
                                {
                                    GOInstanceIDToReplaceString, GOInstanceIDToReplace, GOInstanceIDToReplaceParent
                                },
                                new[]
                                {
                                    newGOInstanceIDToReplaceString, newGOInstanceIDToReplace,
                                    newGOInstanceIDToReplaceParent
                                });
                        }

                        if (type == typeof(PersistentGameObject))
                            componentInGameObject = gameObject.GetComponent<PersistentGameObject>();
                        else componentInGameObject = gameObject.AddComponent(type);
                        
                        if(componentInGameObject == null) break;

                        CopyFieldsToProperties(type, componentInGameObject, ZSaverType,
                            FromJSONdObjects[i]);
                        alreadySeenComponents.Add(componentInGameObject);

                        componentInstanceID = componentInGameObject.GetInstanceID();
                        ZSaverType.GetField("componentinstanceID")
                            .SetValue(FromJSONdObjects[i], componentInGameObject.GetInstanceID());

                        string newCOMPInstanceIDToReplaceString = "instanceID\":" + componentInstanceID;
                        if(ZSaverSettings.instance.debugMode) Debug.LogWarning("Updating " + componentInGameObject);

                        UpdateAllJSONFiles(
                            new[]
                            {
                                COMPInstanceIDToReplaceString
                            },
                            new[]
                            {
                                newCOMPInstanceIDToReplaceString
                            });
                    }
                    else
                    {
                        CopyFieldsToProperties(type, componentInGameObject, ZSaverType,
                            FromJSONdObjects[i]);
                        alreadySeenComponents.Add(componentInGameObject);
                    }

                    if (type == typeof(PersistentGameObject))
                    {
                        GameObjectData gameObjectData = 
                            (GameObjectData) ZSaverType.GetField("gameObjectData").GetValue(FromJSONdObjects[i]);
                        
                        gameObject.transform.position = gameObjectData.position;
                        gameObject.transform.rotation = gameObjectData.rotation;
                        gameObject.transform.localScale = gameObjectData.size;
                    }
                }
            }
        }

        static void CopyFieldsToProperties(Type type, Component c,
            Type ZSaverType, object FromJSONdObject)
        {
            string[] blackListForThisComponent = {" "};

            if (ComponentBlackList.ContainsKey(type))
                ComponentBlackList.TryGetValue(type, out blackListForThisComponent);


            foreach (var propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(FieldIsSuitableForAssignment))
            {
                if (blackListForThisComponent.Contains(propertyInfo.Name)) continue;
                if(c)
                    propertyInfo.SetValue(c,
                        ZSaverType.GetFields().First(f => f.Name == propertyInfo.Name)
                            .GetValue(FromJSONdObject));
            }
        }
        public static void SaveAllObjectsAndComponents()
        {
            string[] files = Directory.GetFiles(Application.persistentDataPath);
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
            LoadAllObjects();


            SaveAllObjectsAndComponents();
        }

        public static void UpdateAllJSONFiles(string[] previousFields, string[] newFields)
        {
            if(ZSaverSettings.instance.debugMode) Debug.Log("-------------------------------------------------------------");
            foreach (var file in Directory.GetFiles(Application.persistentDataPath, "*.save",
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

                if(ZSaverSettings.instance.debugMode) Debug.Log(fileName + " " + newJson);
            }
        }

        public static void Save<T>(T[] objectsToPersist, string fileName)
        {
            string json = JsonHelper.ToJson(objectsToPersist, false);
            if(ZSaverSettings.instance.debugMode) Debug.Log(typeof(T) + " " + json);
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
            return Application.persistentDataPath + "/" + fileName;
        }
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