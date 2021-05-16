using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using ZSave;
using SaveType = ZSave.SaveType;

// [Persistent(SaveType.GameObject, ExecutionCycle.None)]
public class PersistentGameObject : MonoBehaviour
{
    public static Type[] ComponentSerializableTypes => AppDomain.CurrentDomain.GetAssemblies().SelectMany(a =>
        a.GetTypes().Where(t =>
            t.IsSubclassOf(typeof(Component)) && !t.IsSubclassOf(typeof(MonoBehaviour)) &&
            t != typeof(Transform) &&
            t.GetCustomAttribute<ObsoleteAttribute>() == null && t.IsVisible)).ToArray();


    private void Start()
    {
        // Debug.Log(Type.GetType("MeshRendererZSaver"));

        // SaveAllPersistentGameObjects();
        // GenerateUnityComponentClasses();
        // LoadAllPersistentGameObjects();
        // GenerateUnityComponentClasses();


        // var objects = FindObjectsOfType<PersistentGameObject>();
        //
        // foreach (var persistentGameObject in objects)
        // {
        //     var components = persistentGameObject.GetComponents<Component>().Where(c =>
        //         c.GetType() != typeof(Transform) && !c.GetType().IsSubclassOf(typeof(MonoBehaviour)));
        //     
        //     foreach (var component in components)
        //     {
        //         var ZSaverType = Type.GetType(component.GetType().Name + "ZSaver");
        //         if (ZSaverType == null) break;
        //         var ZSaverArrayType = ZSaverType.MakeArrayType();
        //
        //         var zSaversArray = Activator.CreateInstance(ZSaverArrayType, new object[] {components.Count()});
        //         
        //         object[] zSavers = (object[]) zSaversArray;
        //         
        //         for (var i = 0; i < zSavers.Length; i++)
        //         {
        //             zSavers[i] = Activator.CreateInstance(ZSaverType, new object[] {component});
        //         }
        //     }
        // }
    }

    public static void SaveAllPersistentGameObjects()
    {
        List<Type> componentTypes = new List<Type>();

        var objects = FindObjectsOfType<PersistentGameObject>();

        foreach (var persistentGameObject in objects)
        {
            foreach (var component in persistentGameObject.GetComponents<Component>()
                .Where(c => !c.GetType().IsSubclassOf(typeof(MonoBehaviour))))
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


            var zSaversArray = Activator.CreateInstance(ZSaverArrayType, new object[] {componentsOfGivenType.Length});

            object[] zSavers = (object[]) zSaversArray;

            for (var i = 0; i < zSavers.Length; i++)
            {
                zSavers[i] = Activator.CreateInstance(ZSaverType, new object[] {componentsOfGivenType[i]});
            }

            var saveMethodInfo = typeof(PersistanceManager).GetMethod(nameof(PersistanceManager.Save));
            var genericSaveMethodInfo = saveMethodInfo.MakeGenericMethod(ZSaverType);
            genericSaveMethodInfo.Invoke(null, new object[] {zSavers, componentType.Name + "GameObject.save"});
        }
    }

    public static void LoadAllPersistentGameObjects()
    {
        var types = ComponentSerializableTypes;

        List<Component> alreadySeenComponents = new List<Component>();

        foreach (var type in types)
        {
            if (!File.Exists(Application.persistentDataPath + "/" + type.Name + "GameObject.save")) continue;
            var ZSaverType = Type.GetType(type.Name + "ZSaver");
            if (ZSaverType == null) continue;
            MethodInfo fromJsonMethod = typeof(JsonHelper).GetMethod(nameof(JsonHelper.FromJson))
                .MakeGenericMethod(ZSaverType);


            object[] FromJSONdObjects = (object[]) fromJsonMethod.Invoke(null,
                new object[] {PersistanceManager.ReadFromFile(type.Name + "GameObject.save")});


            for (var i = 0; i < FromJSONdObjects.Length; i++)
            {
                GameObject gameObject =
                    (GameObject) ZSaverType.GetField("_componentParent").GetValue(FromJSONdObjects[i]);

                Component componentInGameObject =
                    (Component) ZSaverType.GetField("_component").GetValue(FromJSONdObjects[i]);

                int componentInstanceID =
                    (int) ZSaverType.GetField("componentInstanceID").GetValue(FromJSONdObjects[i]);
                int gameObjectInstanceID =
                    (int) ZSaverType.GetField("gameObjectInstanceID").GetValue(FromJSONdObjects[i]);

                if (componentInGameObject == null)
                {
                    Debug.Log("componentInGameObject Null");
                    string prevCOMPInstanceID = componentInstanceID.ToString();
                    string COMPInstanceIDToReplaceString = $"\"componentInstanceID\":{prevCOMPInstanceID}";
                    string COMPInstanceIDToReplace = "\"_component\":{\"instanceID\":" + prevCOMPInstanceID + "}";

                    if (gameObject == null)
                    {
                        string prevGOInstanceID = gameObjectInstanceID.ToString();
                        string GOInstanceIDToReplaceString = "\"gameObjectInstanceID\":" + prevGOInstanceID;
                        string GOInstanceIDToReplace = "\"_componentParent\":{\"instanceID\":" + prevGOInstanceID + "}";

                        GameObjectData gameObjectData =
                            (GameObjectData) ZSaverType.GetField("gameObjectData").GetValue(FromJSONdObjects[i]);

                        gameObject = gameObjectData.MakePerfectlyValidGameObject();
                        gameObject.AddComponent<PersistentGameObject>();
                        gameObjectInstanceID = gameObject.GetInstanceID();

                        ZSaverType.GetField("gameObjectInstanceID")
                            .SetValue(FromJSONdObjects[i], componentInGameObject.GetInstanceID());
                        componentInstanceID = componentInGameObject.GetInstanceID();

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

                    componentInGameObject = gameObject.AddComponent(type);
                    CopyFieldsToProperties(type, alreadySeenComponents, componentInGameObject, ZSaverType,
                        FromJSONdObjects[i], gameObject);
                    alreadySeenComponents.Add(componentInGameObject);

                    Debug.Log(FromJSONdObjects[i] + " " + componentInGameObject);

                    componentInstanceID = componentInGameObject.GetInstanceID();
                    ZSaverType.GetField("componentInstanceID")
                        .SetValue(FromJSONdObjects[i], componentInGameObject.GetInstanceID());

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
                else
                {
                    CopyFieldsToProperties(type, alreadySeenComponents, componentInGameObject, ZSaverType,
                        FromJSONdObjects[i], gameObject);
                    alreadySeenComponents.Add(componentInGameObject);
                }
            }
        }
    }

    static void CopyFieldsToProperties(Type type, List<Component> alreadySeenComponents, Component c,
        Type ZSaverType, object FromJSONdObject, GameObject gameObject)
    {
        foreach (var propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(
                c =>
                    c.GetCustomAttribute<ObsoleteAttribute>() == null &&
                    c.CanRead &&
                    c.CanWrite))
        {
            propertyInfo.SetValue(c,
                ZSaverType.GetFields().First(f => f.Name == propertyInfo.Name)
                    .GetValue(FromJSONdObject));
        }
    }

    void GenerateUnityComponentClasses()
    {
        string longScript = "";

        //use ComponentSerializableTypes next time.

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Component)) && !t.IsSubclassOf(typeof(MonoBehaviour)) &&
                            t.GetCustomAttribute<ObsoleteAttribute>() == null && t.IsVisible))
            {
                longScript +=
                    "[System.Serializable]\npublic class " + type.Name + "ZSaver : ZSave.ZSaver<" + type.FullName +
                    "> {\n";


                foreach (var propertyInfo in type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(c =>
                        c.GetCustomAttribute<ObsoleteAttribute>() == null &&
                        c.CanRead &&
                        c.CanWrite))
                {
                    longScript +=
                        $"    public {propertyInfo.PropertyType.ToString().Replace('+', '.')} " + propertyInfo.Name +
                        ";\n";
                }

                longScript += "    public " + type.Name + "ZSaver (" + type.FullName + " " + type.Name + ") : base(" +
                              type.Name + ".gameObject, " + type.Name + ") {\n";

                foreach (var propertyInfo in type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(c =>
                        c.GetCustomAttribute<ObsoleteAttribute>() == null &&
                        c.CanRead &&
                        c.CanWrite))
                {
                    longScript +=
                        $"        " + propertyInfo.Name + " = " + type.Name + "." + propertyInfo.Name + ";\n";
                }

                longScript += "    }\n";
                longScript += "}\n";
            }
        }

        FileStream fs = new FileStream(Application.dataPath + "/ZSaver/Runtime/Extra/AllComponentDatas.cs",
            FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);

        sw.Write(longScript);
        sw.Close();
    }
}

[CustomEditor(typeof(PersistentGameObject))]
public class PersistentGameObjectEditor : Editor
{
    private PersistentGameObject manager;

    private void OnEnable()
    {
        manager = target as PersistentGameObject;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        // GUILayout.Space(60);
        //
        // MeshRenderer renderer = manager.Mrenderer;
        //
        // SerializedObject obj = new SerializedObject(renderer);
        //
        // foreach (var propertyInfo in typeof(MeshRenderer).GetProperties(BindingFlags.Public | BindingFlags.Instance)
        //     .Where(r =>
        //         r.GetCustomAttributes<ObsoleteAttribute>() == null && r.CanRead && r.CanWrite))
        // {
        //     EditorGUILayout.PropertyField(obj.FindProperty(propertyInfo.Name));
        // }
    }
}