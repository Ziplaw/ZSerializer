using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;
using UnityEditor;
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
        public int loadingOrder;
        public HideFlags hideFlags;
        public string name;
        public bool active;
        public bool isStatic;
        public int layer;
        public string tag;

        public Vector3 position;
        public Quaternion rotation;
        public Vector3 size;

        public GameObject parent;

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
            Debug.LogWarning(parent + " " + o.name);
            Debug.Log(parent);
            o.transform.SetParent(parent != null ? parent.transform : null);

            return o;
        }
    }

    public abstract class ZSaver<T> where T : Component
    {
        [OmitSerializableCheck] public int gameObjectInstanceID;
        [OmitSerializableCheck] public int componentinstanceID;
        [OmitSerializableCheck] public GameObject _componentParent;
        [OmitSerializableCheck] public T _component;
        [OmitSerializableCheck] public GameObjectData gameObjectData;
        public SaveType _saveType => PersistentAttribute.GetAttributeFromType<PersistentAttribute>(typeof(T)).saveType;

        public ZSaver(GameObject componentParent, T component)
        {
            _componentParent = componentParent;
            _component = component;
            gameObjectInstanceID = componentParent.GetInstanceID();
            componentinstanceID = component.GetInstanceID();
            gameObjectData = new GameObjectData()
            {
                loadingOrder = CountParents(componentParent.transform),
                active = _componentParent.activeSelf,
                hideFlags = _componentParent.hideFlags,
                isStatic = _componentParent.isStatic,
                layer = componentParent.layer,
                name = _componentParent.name,
                position = _componentParent.transform.position,
                rotation = _componentParent.transform.rotation,
                size = _componentParent.transform.localScale,
                tag = componentParent.tag,
                parent = componentParent.transform.parent != null ? componentParent.transform.parent.gameObject : null
            };
        }

        int CountParents(Transform transform)
        {
            int totalParents = 1;
            if (transform.parent != null)
            {
                totalParents += CountParents(transform.parent);
            }

            return totalParents;
        }


        public void Load(Type zSaverType, SaveType saveType)
        {
            if (_component == null)
            {
                string prevCOMPInstanceID = componentinstanceID.ToString();
                string COMPInstanceIDToReplaceString = $"instanceID\":{prevCOMPInstanceID}";

                if (_componentParent == null)
                {
                    string prevGOInstanceID = gameObjectInstanceID.ToString();

                    if (saveType == SaveType.Component)
                    {
                        if (Resources.Load<ZSaverSettings>("ZSaverSettings").debugMode)
                            Debug.LogWarning(
                                $"GameObject holding {typeof(T)} was destroyed, change the saving type to \"SaveType.GameObject\" to ensure persistance of this Component if destroying is necessary");
                        return;
                    }


                    string GOInstanceIDToReplaceString = "\"gameObjectInstanceID\":" + prevGOInstanceID;
                    string GOInstanceIDToReplace = "\"_componentParent\":{\"instanceID\":" + prevGOInstanceID + "}";
                    string GOInstanceIDToReplaceParent = "\"parent\":{\"instanceID\":" + prevGOInstanceID + "}";


                    _componentParent = gameObjectData.MakePerfectlyValidGameObject();
                    gameObjectInstanceID = _componentParent.GetInstanceID();

                    string newGOInstanceIDToReplaceString = "\"gameObjectInstanceID\":" + gameObjectInstanceID;
                    string newGOInstanceIDToReplace = "\"_componentParent\":{\"instanceID\":" + gameObjectInstanceID + "}";
                    string newGOInstanceIDToReplaceParent = "\"parent\":{\"instanceID\":" + gameObjectInstanceID + "}";

                    PersistanceManager.UpdateAllJSONFiles(
                        new[]
                        {
                            GOInstanceIDToReplaceString, GOInstanceIDToReplace, GOInstanceIDToReplaceParent
                        },
                        new[]
                        {
                            newGOInstanceIDToReplaceString, newGOInstanceIDToReplace, newGOInstanceIDToReplaceParent
                        });
                }

                _component = (T) _componentParent.AddComponent(typeof(T));
                componentinstanceID = _component.GetInstanceID();
                string newCOMPInstanceIDToReplaceString = "instanceID\":" + componentinstanceID;
                Debug.LogWarning("Updating " + _component);
                PersistanceManager.UpdateAllJSONFiles(
                    new[]
                    {
                        COMPInstanceIDToReplaceString
                    },
                    new[]
                    {
                        newCOMPInstanceIDToReplaceString
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


    public class OmitSerializableCheck : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PersistentAttribute : Attribute
    {
        public readonly SaveType saveType;
        private readonly ExecutionCycle _executionCycle;

        public PersistentAttribute(ExecutionCycle dataRecovery)
        {
            saveType = SaveType.Component;
            _executionCycle = dataRecovery;
        }

        public static void SaveAllObjects(int groupID)
        {
            var types = GetTypesWithPersistentAttribute(AppDomain.CurrentDomain.GetAssemblies());

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

        public static void LoadAllObjects(int groupID)
        {
            var types = GetTypesWithPersistentAttribute(AppDomain.CurrentDomain.GetAssemblies());

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
        public static void CopyValuesFromTo<T>(T from, ref T to)
        {
            if (to.GetType() == from.GetType())
            {
                var toFields = to.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
                var fromFields = from.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

                for (var i = 0; i < toFields.Length; i++)
                {
                    toFields[i].SetValue(to, fromFields[i].GetValue(from));
                }

                var toProperties = to.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.GetCustomAttribute<ObsoleteAttribute>() == null && p.CanRead && p.CanWrite).ToArray();
                var fromProperties = from.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.GetCustomAttribute<ObsoleteAttribute>() == null && p.CanRead && p.CanWrite).ToArray();

                for (var i = 0; i < toProperties.Length; i++)
                {
                    if ((to.GetType() == typeof(MeshRenderer) || to.GetType() == typeof(MeshRenderer)) &&
                        fromProperties[i].Name != "mesh" && fromProperties[i].Name != "material" &&
                        fromProperties[i].Name != "materials" &&
                        fromProperties[i].Name != "sharedMaterials")
                    {
                        if (fromProperties[i].Name == "sharedMaterial")
                            Debug.Log(from.GetType() + " " + fromProperties[i].Name + " " +
                                      fromProperties[i].GetValue(from) + " " + toProperties[i].GetValue(to));
                        toProperties[i].SetValue(to, fromProperties[i].GetValue(from));
                        if (fromProperties[i].Name == "sharedMaterial")
                            Debug.Log(from.GetType() + " " + fromProperties[i].Name + " " +
                                      fromProperties[i].GetValue(from) + " " + toProperties[i].GetValue(to));
                    }
                }
            }
            else
            {
                Debug.LogWarning("What the fuck? Objects are not of the same type!");
            }
        }
#if UNITY_EDITOR
        public static void CreateZSaver(Type type, string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fileStream);

            string script =
                "using ZSave;\n" +
                "\n" +
                "[System.Serializable]\n" +
                $"public class {type.Name}ZSaver : ZSaver<{type.Name}>\n" +
                "{\n";

            foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Where(f => f.GetCustomAttribute(typeof(OmitSerializableCheck)) == null))
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

            Debug.Log("ZSaver script being created at " + path);

            script += "    }\n}";

            sw.Write(script);

            sw.Close();
        }

        public static void CreateEditorScript(Type type, string path)
        {
            string editorScript =
                @"using UnityEditor;
using ZSave.Editor;
using UnityEditor.Callbacks;

[CustomEditor(typeof(" + type.Name + @"))]
public class " + type.Name + @"Editor : Editor
{
    private " + type.Name + @" manager;
    private bool editMode;
    private static ZSaverStyler styler;

    private void OnEnable()
    {
        manager = target as " + type.Name + @";
        styler = new ZSaverStyler();
    }

    [DidReloadScripts]
    static void OnDatabaseReload()
    {
        styler = new ZSaverStyler();
    }

    public override void OnInspectorGUI()
    {
        ZSaverEditor.BuildPersistentComponentEditor(manager, ref editMode, styler);
        base.OnInspectorGUI();
    }
}";


            string newPath = new string((new string(path.Reverse().ToArray())).Substring(path.Split('/').Last().Length)
                .Reverse().ToArray());
            Debug.Log("Editor script being created at " + newPath + "Editor");
            string relativePath = "Assets" + newPath.Substring(Application.dataPath.Length);


            if (!AssetDatabase.IsValidFolder(relativePath + "Editor"))
            {
                Directory.CreateDirectory(newPath + "Editor");
            }


            string newNewPath = newPath + "Editor/" + type.Name + "Editor.cs";
            FileStream fileStream = new FileStream(newNewPath, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fileStream);
            sw.Write(editorScript);
            sw.Close();

            AssetDatabase.Refresh();
        }

#endif

        public static void SaveAllObjectsAndComponents()
        {
            string[] files = Directory.GetFiles(Application.persistentDataPath);
            foreach (string file in files)
            {
                File.Delete(file);
            }
            
            PersistentGameObject.SaveAllPersistentGameObjects();
            PersistentAttribute.SaveAllObjects(0);
            
        }
        
        public static void LoadAllObjectsAndComponents()
        {
            PersistentGameObject.LoadAllPersistentGameObjects();
            PersistentAttribute.LoadAllObjects(0);
            PersistentAttribute.LoadAllObjects(0);
            
            
            SaveAllObjectsAndComponents();
        }

        public static void UpdateAllJSONFiles(string[] previousFields, string[] newFields)
        {
            Debug.Log("-------------------------------------------------------------");
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
                
                Debug.Log(fileName + " " +newJson);
            }
        }

        public static void Save<T>(T[] objectsToPersist, string fileName)
        {
            string json = JsonHelper.ToJson(objectsToPersist, false);
            Debug.Log(typeof(T) + " " + json);
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