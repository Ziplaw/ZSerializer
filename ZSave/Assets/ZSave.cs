using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ZSave
{
    [Flags]
    public enum ExecutionCycle
    {
        OnStart = 0,
        OnAwake = 1,
        OnApplicationQuit = 2,
        None = 3
    }

    public enum SaveType
    {
        Component,
        GameObject
    }

    [Serializable]
    public struct GameObjectData
    {
        public HideFlags hideFlags;
        public string name;
        public bool active;
        public bool isStatic;
        public int layer;
        public string tag;

        public Vector3 position;
        public Quaternion rotation;
        public Vector3 size;

        public GameObject MakePerfectlyValidGameObject()
        {
            GameObject o = new GameObject();
            o.hideFlags = hideFlags;
            o.name = name;
            o.SetActive(active);
            o.isStatic = isStatic;
            o.layer = layer;
            o.tag = tag;

            o.transform.position = position;
            o.transform.rotation = rotation;
            o.transform.localScale = size;

            return o;
        }
    }

    public struct ComponentData
    {
        public string typeName;
        public int numOfProperties;
        public object[] propertyValues;
    }

    public class ZSaver<T> where T : Component
    {
        [NonPersistent] public int gameObjectInstanceID;
        [NonPersistent] public int componentInstanceID;
        [NonPersistent] public GameObject _componentParent;
        [NonPersistent] public T _component;
        [NonPersistent] public GameObjectData gameObjectData;
        public SaveType _saveType => PersistentAttribute.GetAttributeFromType<PersistentAttribute>(typeof(T)).saveType;

        public ZSaver(GameObject componentParent, T component)
        {
            _componentParent = componentParent;
            _component = component;
            gameObjectInstanceID = componentParent.GetInstanceID();
            componentInstanceID = component.GetInstanceID();
            gameObjectData = new GameObjectData()
            {
                active = _componentParent.activeSelf,
                hideFlags = _componentParent.hideFlags,
                isStatic = _componentParent.isStatic,
                layer = componentParent.layer,
                name = _componentParent.name,
                position = _componentParent.transform.position,
                rotation = _componentParent.transform.rotation,
                size = _componentParent.transform.localScale,
                tag = componentParent.tag
            };
        }


        public void Load(Type zSaverType, SaveType saveType)
        {
            if (_component == null)
            {
                string prevCOMPInstanceID = componentInstanceID.ToString();
                string COMPInstanceIDToReplaceString = $"\"componentInstanceID\":{prevCOMPInstanceID}";
                string COMPInstanceIDToReplace = "\"_component\":{\"instanceID\":" + prevCOMPInstanceID + "}";

                if (_componentParent == null)
                {
                    string prevGOInstanceID = gameObjectInstanceID.ToString();

                    if (saveType == SaveType.Component)
                    {
                        Debug.LogWarning(
                            $"GameObject holding {typeof(T)} was destroyed, change the saving type to \"SaveType.GameObject\" to ensure persistance of this Component if destroying is necessary");
                        return;
                    }


                    string GOInstanceIDToReplaceString = "\"gameObjectInstanceID\":" + prevGOInstanceID;
                    string GOInstanceIDToReplace = "\"_componentParent\":{\"instanceID\":" + prevGOInstanceID + "}";


                    // _componentParent = PersistanceManager.LoadGOfromXML(typeof(T).Name + ".xml", gameObjectInstanceID);
                    _componentParent = gameObjectData.MakePerfectlyValidGameObject();
                    gameObjectInstanceID = _componentParent.GetInstanceID();

                    string newGOInstanceIDToReplaceString = "\"gameObjectInstanceID\":" + gameObjectInstanceID;
                    string newGOInstanceIDToReplace =
                        "\"_componentParent\":{\"instanceID\":" + gameObjectInstanceID + "}";

                    PersistanceManager.UpdateAllJSONFiles(
                        new[]
                        {
                            GOInstanceIDToReplaceString, GOInstanceIDToReplace
                        },
                        new[]
                        {
                            newGOInstanceIDToReplaceString, newGOInstanceIDToReplace
                        });
                }

                _component = (T) _componentParent.AddComponent(typeof(T));
                componentInstanceID = _component.GetInstanceID();
                string newCOMPInstanceIDToReplaceString = "\"componentInstanceID\":" + componentInstanceID;
                string newCOMPInstanceIDToReplace = "\"_component\":{\"instanceID\":" + componentInstanceID + "}";

                PersistanceManager.UpdateAllJSONFiles(
                    new[]
                    {
                        COMPInstanceIDToReplaceString, COMPInstanceIDToReplace
                    },
                    new[]
                    {
                        newCOMPInstanceIDToReplaceString, newCOMPInstanceIDToReplace
                    });
            }


            FieldInfo[] zSaverFields = zSaverType.GetFields();
            FieldInfo[] componentFields = typeof(T).GetFields();

            for (var i = 0; i < componentFields.Length; i++)
            {
                for (var j = 0; j < zSaverFields.Length; j++)
                {
                    if (zSaverFields[j].Name == componentFields[i].Name)
                    {
                        componentFields[i].SetValue(_component, zSaverFields[j].GetValue(this));
                    }
                }
            }
        }
    }

    public class NonPersistent : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PersistentAttribute : Attribute
    {
        internal readonly SaveType saveType;
        private readonly ExecutionCycle _executionCycle;

        public PersistentAttribute(SaveType saveType, ExecutionCycle dataRecovery)
        {
            this.saveType = saveType;
            _executionCycle = dataRecovery;
        }

        public static void SaveAllObjects(int groupID)
        {
            var types = GetTypesWithPersistentAttribute(AppDomain.CurrentDomain.GetAssemblies());

            foreach (var type in types)
            {
                var objects = Object.FindObjectsOfType(type);

                var ZSaverType = Type.GetType(type.Name + "ZSaver");
                if (ZSaverType == null) break;
                var ZSaverArrayType = ZSaverType.MakeArrayType();

                var zsaversArray = Activator.CreateInstance(ZSaverArrayType, new object[] {objects.Length});

                object[] zsavers = (object[]) zsaversArray;

                SaveType saveType = GetAttributeFromType<PersistentAttribute>(type).saveType;
                Transform[] transformsToSave = new Transform[zsavers.Length];


                for (var i = 0; i < zsavers.Length; i++)
                {
                    zsavers[i] = Activator.CreateInstance(ZSaverType, new object[] {objects[i]});

                    transformsToSave[i] = ((GameObject) ZSaverType.GetField("_componentParent").GetValue(zsavers[i]))
                        .transform;
                }


                var saveMethodInfo = typeof(PersistanceManager).GetMethod(nameof(PersistanceManager.Save));
                var genericSaveMethodInfo = saveMethodInfo.MakeGenericMethod(ZSaverType);
                genericSaveMethodInfo.Invoke(null, new object[] {zsavers, type.Name + ".save"});
                // if (saveType == SaveType.GameObject || saveType == SaveType.GameObjectFull)
                // {
                //     PersistanceManager.SaveGOtoXML(transformsToSave, type.Name + ".xml", saveType);
                // }
                // {
                //     PersistanceManager.SaveGOtoXML(transformsToSave, type.Name + ".xml", saveType);
                // }
            }
        }

        public static void LoadAllObjects(int groupID)
        {
            var types = GetTypesWithPersistentAttribute(AppDomain.CurrentDomain.GetAssemblies());

            foreach (var type in types)
            {
                var ZSaverType = Type.GetType(type.Name + "ZSaver");
                if (ZSaverType == null) break;
                MethodInfo fromJsonMethod = typeof(JsonHelper).GetMethod(nameof(JsonHelper.FromJson))
                    .MakeGenericMethod(ZSaverType);

                object[] FromJSONdObjects = (object[]) fromJsonMethod.Invoke(null,
                    new object[] {PersistanceManager.ReadFromFile(type.Name + ".save")});

                for (var i = 0; i < FromJSONdObjects.Length; i++)
                {
                    FromJSONdObjects[i] = ((object[]) fromJsonMethod.Invoke(null,
                        new object[] {PersistanceManager.ReadFromFile(type.Name + ".save")}))[i];
                    MethodInfo loadMethod = ZSaverType.GetMethod("Load");
                    loadMethod.Invoke(FromJSONdObjects[i],
                        new object[] {ZSaverType, ZSaverType.GetProperty("_saveType").GetValue(FromJSONdObjects[i])});
                }
            }
        }

        static T GetAttributeFromObject<T>(object obj) where T : Attribute
        {
            return (T) obj.GetType().GetField("_component").GetValue(obj).GetType().GetCustomAttributes(typeof(T))
                .FirstOrDefault();
        }

        internal static T GetAttributeFromType<T>(Type type) where T : Attribute
        {
            return (T) type.GetCustomAttributes(typeof(T))
                .FirstOrDefault();
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
    }

    public class PersistanceManager
    {
        public static void CreateZSaver(Type type, string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fileStream);

            string script =
                "using ZSave;\n" +
                "\n" +
                "[System.Serializable]\n" +
                $"public class {type.Name}ZSaver : ZSaver<{type.Name}>\n" +
                "{\n";

            foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Where(f => f.GetCustomAttribute(typeof(NonPersistent)) == null))
            {
                script +=
                    $"    public {fieldInfo.FieldType} {fieldInfo.Name};\n";
            }

            string className = type.Name + "Instance";

            script +=
                $"\n    public {type.Name}ZSaver({type.Name} {className}) : base({className}.gameObject, {className})\n" +
                "    {\n";

            foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                //num1 = (System.Single)typeof(Testing).GetField("num1").GetValue(TestingInstance);
                script +=
                    $"         {fieldInfo.Name} = ({fieldInfo.FieldType})typeof({type.Name}).GetField(\"{fieldInfo.Name}\").GetValue({className});\n";
            }

            script += "    }\n}";

            sw.Write(script);

            sw.Close();
        }

        public static void UpdateAllJSONFiles(string[] previousFields, string[] newFields)
        {
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
            }
        }

        public static void Save<T>(T[] objectsToPersist, string fileName)
        {
            string json = JsonHelper.ToJson(objectsToPersist, false);
            Debug.Log(json);
            WriteToFile(fileName, json);
        }

        // public static void SaveGOtoXML(Transform[] transforms, string fileName, SaveType saveType)
        // {
        //     if (transforms.Length > 0)
        //         foreach (var propertyInfo in typeof(Transform).GetProperties()
        //             .Where(p => p.GetCustomAttribute<ObsoleteAttribute>() == null && p.CanWrite))
        //         {
        //             propertyInfo.SetValue(transforms[0], propertyInfo.GetValue(transforms[0]));
        //         }
        //
        //
        //     GameObjectData[] datas = new GameObjectData[transforms.Length];
        //
        //     for (var i = 0; i < datas.Length; i++)
        //     {
        //         Component[] components = transforms[i].gameObject
        //             .GetComponents(saveType == SaveType.GameObject ? typeof(Transform) : typeof(Component));
        //
        //         // foreach (var component in components)
        //         // {
        //         //     Debug.Log(component.GetType());
        //         // }
        //
        //         ComponentData[] componentDatas = new ComponentData[components.Length];
        //
        //         for (int j = 0; j < componentDatas.Length; j++)
        //         {
        //             object[] values =
        //                 (from p in components[j].GetType().GetProperties()
        //                     where p.GetCustomAttribute<ObsoleteAttribute>() == null && p.CanWrite && p.CanRead
        //                     select p.GetValue(components[j])).ToArray();
        //
        //             componentDatas[j] = new ComponentData()
        //             {
        //                 propertyValues = values,
        //                 numOfProperties = values.Length,
        //                 typeName = components[j].GetType().Name
        //             };
        //
        //             try
        //             {
        //                 FileStream fileStream = File.Create("test.xml");
        //                 DataContractSerializer dataContractSerializer =
        //                     new DataContractSerializer(typeof(ComponentData));
        //                 MemoryStream stream = new MemoryStream();
        //
        //                 dataContractSerializer.WriteObject(stream, componentDatas[j]);
        //
        //                 stream.Seek(0, SeekOrigin.Begin);
        //
        //                 fileStream.Write(stream.GetBuffer(), 0, stream.GetBuffer().Length);
        //                 
        //                 fileStream.Dispose();
        //
        //             }
        //             catch (InvalidDataContractException e)
        //             {
        //                 var list = componentDatas.ToList();
        //                 list.RemoveAt(j);
        //                 componentDatas = list.ToArray();
        //                 
        //                 List<Component> componentList = components.ToList();
        //                 componentList.RemoveAt(j);
        //                 components = componentList.ToArray();
        //                 j--;
        //                 Debug.Log(components[j] + " " + e);
        //             }
        //             catch (IOException e)
        //             {
        //                 Debug.Log(components[j] + " " + e);
        //             }
        //         }
        //
        //         foreach (var componentData in componentDatas)
        //         {
        //             Debug.Log(componentData.typeName);
        //         }
        //
        //
        //         datas[i] = new GameObjectData()
        //         {
        //             instanceID = transforms[i].gameObject.GetInstanceID(),
        //
        //             hideFlags = transforms[i].gameObject.hideFlags,
        //             name = transforms[i].gameObject.name,
        //             active = transforms[i].gameObject.activeSelf,
        //             isStatic = transforms[i].gameObject.isStatic,
        //             layer = transforms[i].gameObject.layer,
        //             tag = transforms[i].gameObject.tag,
        //
        //             componentDatas = componentDatas
        //         };
        //     }
        //
        //
        //     FileStream file = File.Create(GetFilePath(fileName));
        //
        //     DataContractSerializer bf = new DataContractSerializer(typeof(GameObjectData[]));
        //     MemoryStream streamer = new MemoryStream();
        //
        //     bf.WriteObject(streamer, datas);
        //
        //     streamer.Seek(0, SeekOrigin.Begin);
        //
        //     file.Write(streamer.GetBuffer(), 0, streamer.GetBuffer().Length);
        //
        //     file.Close();
        // }
        // {
        //     if (transforms.Length > 0)
        //         foreach (var propertyInfo in typeof(Transform).GetProperties()
        //             .Where(p => p.GetCustomAttribute<ObsoleteAttribute>() == null && p.CanWrite))
        //         {
        //             propertyInfo.SetValue(transforms[0], propertyInfo.GetValue(transforms[0]));
        //         }
        //
        //
        //     GameObjectData[] datas = new GameObjectData[transforms.Length];
        //
        //     for (var i = 0; i < datas.Length; i++)
        //     {
        //         Component[] components = transforms[i].gameObject
        //             .GetComponents(saveType == SaveType.GameObject ? typeof(Transform) : typeof(Component));
        //
        //         // foreach (var component in components)
        //         // {
        //         //     Debug.Log(component.GetType());
        //         // }
        //
        //         ComponentData[] componentDatas = new ComponentData[components.Length];
        //
        //         for (int j = 0; j < componentDatas.Length; j++)
        //         {
        //             object[] values =
        //                 (from p in components[j].GetType().GetProperties()
        //                     where p.GetCustomAttribute<ObsoleteAttribute>() == null && p.CanWrite && p.CanRead
        //                     select p.GetValue(components[j])).ToArray();
        //
        //             componentDatas[j] = new ComponentData()
        //             {
        //                 propertyValues = values,
        //                 numOfProperties = values.Length,
        //                 typeName = components[j].GetType().Name
        //             };
        //
        //             try
        //             {
        //                 FileStream fileStream = File.Create("test.xml");
        //                 DataContractSerializer dataContractSerializer =
        //                     new DataContractSerializer(typeof(ComponentData));
        //                 MemoryStream stream = new MemoryStream();
        //
        //                 dataContractSerializer.WriteObject(stream, componentDatas[j]);
        //
        //                 stream.Seek(0, SeekOrigin.Begin);
        //
        //                 fileStream.Write(stream.GetBuffer(), 0, stream.GetBuffer().Length);
        //                 
        //                 fileStream.Dispose();
        //
        //             }
        //             catch (InvalidDataContractException e)
        //             {
        //                 var list = componentDatas.ToList();
        //                 list.RemoveAt(j);
        //                 componentDatas = list.ToArray();
        //                 
        //                 List<Component> componentList = components.ToList();
        //                 componentList.RemoveAt(j);
        //                 components = componentList.ToArray();
        //                 j--;
        //                 Debug.Log(components[j] + " " + e);
        //             }
        //             catch (IOException e)
        //             {
        //                 Debug.Log(components[j] + " " + e);
        //             }
        //         }
        //
        //         foreach (var componentData in componentDatas)
        //         {
        //             Debug.Log(componentData.typeName);
        //         }
        //
        //
        //         datas[i] = new GameObjectData()
        //         {
        //             instanceID = transforms[i].gameObject.GetInstanceID(),
        //
        //             hideFlags = transforms[i].gameObject.hideFlags,
        //             name = transforms[i].gameObject.name,
        //             active = transforms[i].gameObject.activeSelf,
        //             isStatic = transforms[i].gameObject.isStatic,
        //             layer = transforms[i].gameObject.layer,
        //             tag = transforms[i].gameObject.tag,
        //
        //             componentDatas = componentDatas
        //         };
        //     }
        //
        //
        //     FileStream file = File.Create(GetFilePath(fileName));
        //
        //     DataContractSerializer bf = new DataContractSerializer(typeof(GameObjectData[]));
        //     MemoryStream streamer = new MemoryStream();
        //
        //     bf.WriteObject(streamer, datas);
        //
        //     streamer.Seek(0, SeekOrigin.Begin);
        //
        //     file.Write(streamer.GetBuffer(), 0, streamer.GetBuffer().Length);
        //
        //     file.Close();
        // }

        // public static GameObject LoadGOfromXML(string fileName, int instanceID)
        // {
        //     List<Type> knownTypes = typeof(Vector3).Assembly.GetExportedTypes()
        //         .Where(t => t.IsValueType && !t.IsGenericType).ToList();
        //     knownTypes.Add(typeof(Mesh));
        //     
        //     DataContractSerializer bf = new DataContractSerializer(typeof(GameObjectData[]),
        //         knownTypes);
        //     XmlReader reader = new XmlTextReader(GetFilePath(fileName));
        //     GameObjectData[] datas = (GameObjectData[]) bf.ReadObject(reader);
        //     GameObjectData dataOfObjectToReturn = datas.First(d => d.instanceID == instanceID);
        //
        //     GameObject objToReturn = new GameObject();
        //
        //     objToReturn.hideFlags = dataOfObjectToReturn.hideFlags;
        //
        //     objToReturn.hideFlags = dataOfObjectToReturn.hideFlags;
        //     objToReturn.name = dataOfObjectToReturn.name;
        //     objToReturn.SetActive(dataOfObjectToReturn.active);
        //     objToReturn.isStatic = dataOfObjectToReturn.isStatic;
        //     objToReturn.layer = dataOfObjectToReturn.layer;
        //     objToReturn.tag = dataOfObjectToReturn.tag;
        //
        //     int propertyIndex = 0;
        //
        //     foreach (var componentData in dataOfObjectToReturn.componentDatas)
        //     {
        //         Type currentComponentType = AppDomain.CurrentDomain
        //             .GetAssemblies()
        //             .SelectMany(assembly => assembly.GetTypes()).FirstOrDefault(type =>
        //                 type.IsSubclassOf(typeof(Component)) && type.Name == componentData.typeName);
        //
        //         Debug.Log(componentData.typeName);
        //
        //         var component = objToReturn.GetComponent(currentComponentType) ??
        //                         objToReturn.AddComponent(currentComponentType);
        //
        //         var propertyInfos = (from p in currentComponentType.GetProperties()
        //             where p.GetCustomAttribute<ObsoleteAttribute>() == null && p.CanWrite && p.CanRead
        //             select p).ToArray();
        //
        //         for (var i = 0; i < propertyInfos.Length; i++)
        //         {
        //             Debug.Log("setting value: " + propertyInfos[i].Name + " to " + componentData.propertyValues[propertyIndex]);
        //             
        //             propertyInfos[i].SetValue(component, componentData.propertyValues[propertyIndex]);
        //             propertyIndex++;
        //         }
        //     }
        //
        //     // objToReturn.transform.position = transform.position;
        //     // objToReturn.transform.rotation = transform.rotation;
        //     // objToReturn.transform.localScale = transform.localScale;
        //     //
        //     // foreach (var component in (Transform[])dataOfObjectToReturn.components)
        //     // {
        //     //     objToReturn.AddComponent(component.GetType());
        //     //
        //     //     foreach (var fieldInfo in objToReturn.GetComponent(component.GetType()).GetType().GetFields())
        //     //     {
        //     //         foreach (var field in component.GetType().GetFields())
        //     //         {
        //     //             fieldInfo.SetValue(component, field.GetValue(component));
        //     //         }
        //     //     }
        //     // }
        //
        //     return objToReturn;
        // }

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