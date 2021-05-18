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
    public static int CountParents(Transform transform)
    {
        int totalParents = 1;
        if (transform.parent != null)
        {
            totalParents += CountParents(transform.parent);
        }

        return totalParents;
    }
    
    #if UNITY_EDITOR

        [ContextMenu("Generate Class Thingy")]
        public void GenerateUnityComponentClasses()
        {
            string longScript = "";

            Type[] types = PersistanceManager.ComponentSerializableTypes;
            foreach (var type in types)
            {
                string[] blackListForThisComponent = {" "};

                if (PersistanceManager.ComponentBlackList.ContainsKey(type))
                    PersistanceManager.ComponentBlackList.TryGetValue(type, out blackListForThisComponent);


                longScript +=
                    "[System.Serializable]\npublic class " + type.Name + "ZSaver : ZSave.ZSaver<" + type.FullName +
                    "> {\n";


                foreach (var propertyInfo in type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(PersistanceManager.FieldIsSuitableForAssignment))
                {
                    if (blackListForThisComponent.Contains(propertyInfo.Name)) continue;

                    longScript +=
                        $"    public {propertyInfo.PropertyType.ToString().Replace('+', '.')} " + propertyInfo.Name +
                        ";\n";
                }
                if(type == typeof(PersistentGameObject)) 
                    longScript +=
                    $"    public ZSave.GameObjectData gameObjectData;\n";

                longScript += "    public " + type.Name + "ZSaver (" + type.FullName + " " + type.Name +"Instance) : base(" +
                              type.Name + "Instance.gameObject, " + type.Name +"Instance ) {\n";

                foreach (var propertyInfo in type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(PersistanceManager.FieldIsSuitableForAssignment))
                {
                    if (blackListForThisComponent.Contains(propertyInfo.Name)) continue;

                    longScript +=
                        $"        " + propertyInfo.Name + " = " + type.Name + "Instance." + propertyInfo.Name + ";\n";
                }

                if (type == typeof(PersistentGameObject))
                    longScript +=
                        @"        gameObjectData =new ZSave.GameObjectData()
        {
            loadingOrder = PersistentGameObject.CountParents(PersistentGameObjectInstance.transform),
            active = _componentParent.activeSelf,
            hideFlags = _componentParent.hideFlags,
            isStatic = _componentParent.isStatic,
            layer = PersistentGameObjectInstance.gameObject.layer,
            name = _componentParent.name,
            position = _componentParent.transform.position,
            rotation = _componentParent.transform.rotation,
            size = _componentParent.transform.localScale,
            tag = PersistentGameObjectInstance.gameObject.tag,
            parent = PersistentGameObjectInstance.transform.parent ? PersistentGameObjectInstance.transform.parent.gameObject : null
        };";

                longScript += "\n    }\n";
                longScript += "}\n";
            }

            FileStream fs = new FileStream(Application.dataPath + "/com.Ziplaw.ZSaver/Scripts/Runtime/Generated/AllComponentDatas.cs",
                FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            sw.Write(longScript);
            sw.Close();

            AssetDatabase.Refresh();
        }

#endif
}