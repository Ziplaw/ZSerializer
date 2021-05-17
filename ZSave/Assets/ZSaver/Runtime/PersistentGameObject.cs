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

[AddComponentMenu("ZSaver/Persistent GameObject")]
public class PersistentGameObject : MonoBehaviour
{
    public static Type[] ComponentSerializableTypes => AppDomain.CurrentDomain.GetAssemblies().SelectMany(a =>
        a.GetTypes().Where(t =>
            (t == typeof(PersistentGameObject)) || (
            t.IsSubclassOf(typeof(Component)) &&
            !t.IsSubclassOf(typeof(MonoBehaviour)) &&
            t != typeof(Transform) &&
            t != typeof(MonoBehaviour) &&
            t.GetCustomAttribute<ObsoleteAttribute>() == null && t.IsVisible))).ToArray();

    public static readonly Dictionary<Type,string[]> ComponentBlackList = new Dictionary<Type, string[]>()
    {
        {typeof(LightProbeGroup),new []{"dering"}},
        {typeof(Light),new []{"shadowRadius","shadowAngle","areaSize","lightmapBakeType"}},
        {typeof(MeshRenderer),new []{"scaleInLightmap","receiveGI","stitchLightmapSeams"}},
        {typeof(Terrain),new []{"bakeLightProbesForTrees","deringLightProbesForTrees"}},
        {typeof(PersistentGameObject),new []{"runInEditMode"}},
    };

    int CountParents(Transform transform)
    {
        int totalParents = 1;
        if (transform.parent != null)
        {
            totalParents += CountParents(transform.parent);
        }

        return totalParents;
    }
    private void Start()
    {
        name = gameObject.GetInstanceID().ToString();

        // LightProbeGroup lpb = gameObject.AddComponent<LightProbeGroup>();
        // lpb.dering = true;

        // name = CountParents(transform).ToString();
        // object[] obj = {new PersistentGameObjectZSaver(this)};
        // obj = obj.OrderBy(x => ((PersistentGameObjectZSaver)x).name).ToArray();
        //
        // Debug.Log( obj.GetType());
        //
        // MethodInfo cast = typeof(Enumerable).GetMethod("Cast")
        //     .MakeGenericMethod(new Type[] {typeof(PersistentGameObjectZSaver)});
        //
        // MethodInfo toArrayMethod = typeof(Enumerable).GetMethod("ToArray")
        //     .MakeGenericMethod(new Type[] {typeof(PersistentGameObjectZSaver)});
        //
        //
        // object result = cast.Invoke(obj, new object[] {obj});
        //
        // obj = (object[]) toArrayMethod.Invoke(result, new object[] {result});
        //
        //
        //
        //
        //
        //
        // Debug.Log(toArrayMethod);
        //
        // // obj = result;
        //
        // Debug.Log(obj.GetType());
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
                .Where(c => (c.GetType() == typeof(PersistentGameObject)) || (!c.GetType().IsSubclassOf(typeof(MonoBehaviour)) && c.GetType() != typeof(Transform))))
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

            zSavers = zSavers.OrderBy(x =>
                ((GameObjectData) x.GetType().GetField("gameObjectData").GetValue(x)).loadingOrder).ToArray();
            
            MethodInfo cast = typeof(Enumerable).GetMethod("Cast")
                .MakeGenericMethod(new Type[] {ZSaverType});
            
            MethodInfo toArrayMethod = typeof(Enumerable).GetMethod("ToArray")
                .MakeGenericMethod(new Type[] {ZSaverType});
            
            object result = cast.Invoke(zSavers, new object[] {zSavers});
            
            zSavers = (object[]) toArrayMethod.Invoke(result, new object[] {result});

            var saveMethodInfo = typeof(PersistanceManager).GetMethod(nameof(PersistanceManager.Save));
            var genericSaveMethodInfo = saveMethodInfo.MakeGenericMethod(ZSaverType);
            genericSaveMethodInfo.Invoke(null, new object[] {zSavers, componentType.Name + "GameObject.save"});
        }
    }

    public static void LoadAllPersistentGameObjects()
    {
        var types = ComponentSerializableTypes.OrderByDescending(x => x.Name.Contains("PersistentGameObject"))
            .ToArray();

        List<Component> alreadySeenComponents = new List<Component>();

        foreach (var type in types)
        {
            if (!File.Exists(Application.persistentDataPath + "/" + type.Name + "GameObject.save")) continue;
            var ZSaverType = Type.GetType(type.Name + "ZSaver");
            if (ZSaverType == null) continue;
            MethodInfo fromJsonMethod = typeof(JsonHelper).GetMethod(nameof(JsonHelper.FromJson))
                .MakeGenericMethod(ZSaverType);


            object[] FromJSONdObjects = ((object[]) fromJsonMethod.Invoke(null,
                new object[] {PersistanceManager.ReadFromFile(type.Name + "GameObject.save")}))/*.OrderByDescending(x =>
                ((GameObjectData) x.GetType().GetField("gameObjectData").GetValue(x)).loadingOrder).ToArray()*/;


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
                
                GameObjectData gameObjectData =
                    (GameObjectData) ZSaverType.GetField("gameObjectData").GetValue(FromJSONdObjects[i]);

                if (componentInGameObject == null)
                {
                    string prevCOMPInstanceID = componentInstanceID.ToString();
                    string COMPInstanceIDToReplaceString = $"instanceID\":{prevCOMPInstanceID}";
                    


                    if (gameObject == null)
                    {
                        string prevGOInstanceID = gameObjectInstanceID.ToString();
                        string GOInstanceIDToReplaceString = "\"gameObjectInstanceID\":" + prevGOInstanceID;
                        string GOInstanceIDToReplace = "\"_componentParent\":{\"instanceID\":" + prevGOInstanceID + "}";
                        string GOInstanceIDToReplaceParent = "\"parent\":{\"instanceID\":" + prevGOInstanceID + "}";

                        

                        gameObject = gameObjectData.MakePerfectlyValidGameObject();
                        gameObject.AddComponent<PersistentGameObject>();
                        gameObjectInstanceID = gameObject.GetInstanceID();
                        

                        ZSaverType.GetField("gameObjectInstanceID")
                            .SetValue(FromJSONdObjects[i], gameObject.GetInstanceID());

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

                    if (type == typeof(PersistentGameObject)) componentInGameObject = gameObject.GetComponent<PersistentGameObject>();
                    else componentInGameObject = gameObject.AddComponent(type);
                    
                    CopyFieldsToProperties(type, alreadySeenComponents, componentInGameObject, ZSaverType,
                        FromJSONdObjects[i], gameObject);
                    alreadySeenComponents.Add(componentInGameObject);

                    componentInstanceID = componentInGameObject.GetInstanceID();
                    ZSaverType.GetField("componentinstanceID")
                        .SetValue(FromJSONdObjects[i], componentInGameObject.GetInstanceID());

                    string newCOMPInstanceIDToReplaceString = "instanceID\":" + componentInstanceID;
                    Debug.LogWarning("Updating " + componentInGameObject);

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
                else
                {
                    CopyFieldsToProperties(type, alreadySeenComponents, componentInGameObject, ZSaverType,
                        FromJSONdObjects[i], gameObject);
                    alreadySeenComponents.Add(componentInGameObject);
                    
                    gameObject.transform.position = gameObjectData.position;
                    gameObject.transform.rotation = gameObjectData.rotation;
                    gameObject.transform.localScale = gameObjectData.size;
                }
            }
        }
    }

    static void CopyFieldsToProperties(Type type, List<Component> alreadySeenComponents, Component c,
        Type ZSaverType, object FromJSONdObject, GameObject gameObject)
    {
        string[] blackListForThisComponent = {" "};

        if(ComponentBlackList.ContainsKey(type)) ComponentBlackList.TryGetValue(type, out blackListForThisComponent);
        
        foreach (var propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(
                c =>
                    c.GetCustomAttribute<ObsoleteAttribute>() == null &&
                    c.CanRead &&
                    c.CanWrite))
        {
            
            if (blackListForThisComponent.Contains(propertyInfo.Name)) continue;
            propertyInfo.SetValue(c,
                ZSaverType.GetFields().First(f => f.Name == propertyInfo.Name)
                    .GetValue(FromJSONdObject));
        }
    }
    
    #if UNITY_EDITOR

    [ContextMenu("Generate Class Thingy")]
    public void GenerateUnityComponentClasses()
    {
        string longScript = "";

        Type[] types = ComponentSerializableTypes;
        foreach (var type in types)
        {
            string[] blackListForThisComponent = {" "};

            if(ComponentBlackList.ContainsKey(type)) ComponentBlackList.TryGetValue(type, out blackListForThisComponent);
            
            
            longScript +=
                "[System.Serializable]\npublic class " + type.Name + "ZSaver : ZSave.ZSaver<" + type.FullName +
                "> {\n";


            foreach (var propertyInfo in type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(c =>
                    c.GetCustomAttribute<ObsoleteAttribute>() == null &&
                    c.CanRead &&
                    c.CanWrite))
            {
                if (blackListForThisComponent.Contains(propertyInfo.Name)) continue;
                
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
                if (blackListForThisComponent.Contains(propertyInfo.Name)) continue;
                
                longScript +=
                    $"        " + propertyInfo.Name + " = " + type.Name + "." + propertyInfo.Name + ";\n";
            }

            longScript += "    }\n";
            longScript += "}\n";
        }

        FileStream fs = new FileStream(Application.dataPath + "/ZSaver/Runtime/Extra/AllComponentDatas.cs",
            FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);

        sw.Write(longScript);
        sw.Close();
        
        AssetDatabase.Refresh();
    }
    
    #endif
}