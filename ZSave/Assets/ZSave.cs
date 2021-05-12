using System;
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

    public struct GameObjectData
    {
        public int instanceID;

        public HideFlags hideFlags;
        public string name;
        public bool active;
        public bool isStatic;
        public int layer;
        public string tag;

        public Vector3 position;
        public Quaternion rotation;
        public Vector3 localScale;
    }

    public class ZSaver<T> where T : Component
    {
        [NonPersistent]public int gameObjectInstanceID;
        [NonPersistent]public int componentInstanceID;
        [NonPersistent]public GameObject _componentParent;
        [NonPersistent]public T _component;
        public SaveType _saveType => PersistentAttribute.GetAttributeFromType<PersistentAttribute>(typeof(T)).saveType;

        public ZSaver(GameObject componentParent, T component)
        {
            _componentParent = componentParent;
            _component = component;
            gameObjectInstanceID = componentParent.GetInstanceID();
            componentInstanceID = component.GetInstanceID();
        }

        public void Load(Type zSaverType, SaveType saveType)
        {
            Debug.Log(PersistanceManager.ReadFromFile("Testing.save"));
            Debug.Log("loading");
            if (_component == null)
            {
                Debug.Log($"component null for {componentInstanceID}");
                string prevCOMPInstanceID = componentInstanceID.ToString();
                string COMPInstanceIDToReplaceString = $"\"componentInstanceID\":{prevCOMPInstanceID}";
                string COMPInstanceIDToReplace = "\"_component\":{\"instanceID\":" + prevCOMPInstanceID + "}";

                if (_componentParent == null)
                {
                    Debug.Log($"component parent: {gameObjectInstanceID} null for {componentInstanceID}");
                    string prevGOInstanceID = gameObjectInstanceID.ToString();

                    if (saveType != SaveType.GameObject)
                    {
                        Debug.LogWarning(
                            $"GameObject holding {typeof(T)} was destroyed, change the saving type to \"SaveType.GameObject\" to ensure persistance of this Component if destroying is necessary");
                        return;
                    }


                    string GOInstanceIDToReplaceString = "\"gameObjectInstanceID\":"+prevGOInstanceID;
                    string GOInstanceIDToReplace = "\"_componentParent\":{\"instanceID\":" + prevGOInstanceID + "}";


                    _componentParent = PersistanceManager.LoadGOfromXML(typeof(T).Name + ".xml", gameObjectInstanceID);
                    gameObjectInstanceID = _componentParent.GetInstanceID();

                    string newGOInstanceIDToReplaceString = "\"gameObjectInstanceID\":" + gameObjectInstanceID;
                    string newGOInstanceIDToReplace =
                        "\"_componentParent\":{\"instanceID\":" + gameObjectInstanceID + "}";

                    PersistanceManager.UpdateFile(typeof(T).Name + ".save",
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

                PersistanceManager.UpdateFile(typeof(T).Name + ".save",
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
                if (saveType == SaveType.GameObject)
                {
                    PersistanceManager.SaveGOtoXML(transformsToSave, type.Name + ".xml");
                }
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

            foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Instance).Where(f => f.GetCustomAttribute(typeof(NonPersistent)) == null))
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

        public static void UpdateFile(string fileName, string[] previousFields, string[] newFields)
        {
            string json = ReadFromFile(fileName);
            string newJson = json;
            for (int i = 0; i < previousFields.Length; i++)
            {
                newJson = newJson.Replace(previousFields[i], newFields[i]);
            }
            
            WriteToFile(fileName, newJson);
        }

        public static void Save<T>(T[] objectsToPersist, string fileName)
        {
            string json = JsonHelper.ToJson(objectsToPersist, false);
            Debug.Log(json + " " + fileName);
            WriteToFile(fileName, json);
        }

        public static void SaveGOtoXML(Transform[] transforms, string fileName)
        {
            FileStream file = File.Create(GetFilePath(fileName));

            DataContractSerializer bf = new DataContractSerializer(typeof(GameObjectData[]));
            MemoryStream streamer = new MemoryStream();

            GameObjectData[] datas = new GameObjectData[transforms.Length];

            for (var i = 0; i < datas.Length; i++)
            {
                datas[i] = new GameObjectData()
                {
                    instanceID = transforms[i].gameObject.GetInstanceID(),

                    hideFlags = transforms[i].gameObject.hideFlags,
                    name = transforms[i].gameObject.name,
                    active = transforms[i].gameObject.activeSelf,
                    isStatic = transforms[i].gameObject.isStatic,
                    layer = transforms[i].gameObject.layer,
                    tag = transforms[i].gameObject.tag,

                    position = transforms[i].position,
                    rotation = transforms[i].rotation,
                    localScale = transforms[i].localScale
                };
            }


            bf.WriteObject(streamer, datas);

            streamer.Seek(0, SeekOrigin.Begin);

            file.Write(streamer.GetBuffer(), 0, streamer.GetBuffer().Length);

            file.Close();
        }

        public static GameObject LoadGOfromXML(string fileName, int instanceID)
        {
            DataContractSerializer bf = new DataContractSerializer(typeof(GameObjectData[]));
            XmlReader reader = new XmlTextReader(GetFilePath(fileName));
            GameObjectData[] datas = (GameObjectData[]) bf.ReadObject(reader);
            GameObjectData dataOfObjectToReturn = datas.First(d => d.instanceID == instanceID);

            GameObject objToReturn = new GameObject();

            objToReturn.hideFlags = dataOfObjectToReturn.hideFlags;

            objToReturn.hideFlags = dataOfObjectToReturn.hideFlags;
            objToReturn.name = dataOfObjectToReturn.name;
            objToReturn.SetActive(dataOfObjectToReturn.active);
            objToReturn.isStatic = dataOfObjectToReturn.isStatic;
            objToReturn.layer = dataOfObjectToReturn.layer;
            objToReturn.tag = dataOfObjectToReturn.tag;

            objToReturn.transform.position = dataOfObjectToReturn.position;
            objToReturn.transform.rotation = dataOfObjectToReturn.rotation;
            objToReturn.transform.localScale = dataOfObjectToReturn.localScale;

            return objToReturn;
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