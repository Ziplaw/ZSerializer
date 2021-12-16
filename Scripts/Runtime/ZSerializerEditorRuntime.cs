using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using ZSerializer;

namespace ZSerializer
{
    public static class ZSerializerEditorRuntime
    {
#if UNITY_EDITOR
        // [MenuItem("Tools/ZSerializer/Generate Unity Component ZSerializers", priority = 20)]
        public static void GenerateUnityComponentClasses()
        {
            string longScript = @"
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
namespace ZSerializer {

";

            List<Type> types = ZSerialize.UnitySerializableTypes
                .Intersect(ZSerializerSettings.Instance.unityComponentTypes.Select(s => Type.GetType(s))).ToList();
            foreach (var type in types)
            {
                EditorUtility.DisplayProgressBar("Generating Unity Component ZSerializers", type.Name,
                    types.IndexOf(type) / (float)types.Count);


                if (type != typeof(PersistentGameObject))
                {
                    longScript +=
                        "[System.Serializable]\npublic sealed class " + type.Name +
                        "ZSerializer : ZSerializer.Internal.ZSerializer {\n";

                    foreach (var propertyInfo in type
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(ZSerialize.PropertyIsSuitableForZSerializer))
                    {
                        longScript +=
                            $"    public {propertyInfo.PropertyType.ToString().Replace('+', '.')} " +
                            propertyInfo.Name +
                            ";\n";
                    }

                    foreach (var fieldInfo in type
                        .GetFields(BindingFlags.Public | BindingFlags.Instance)
                        .Where(f => f.GetCustomAttribute<ObsoleteAttribute>() == null))
                    {
                        var fieldType = fieldInfo.FieldType;

                        if (fieldInfo.FieldType.IsArray)
                        {
                            fieldType = fieldInfo.FieldType.GetElementType();
                        }

                        int genericParameterAmount = fieldType.GenericTypeArguments.Length;

                        longScript +=
                            $"    public {fieldInfo.FieldType.ToString().Replace('+', '.')} " + fieldInfo.Name +
                            ";\n";

                        if (genericParameterAmount > 0)
                        {
                            string oldString = $"`{genericParameterAmount}[";
                            string newString = "<";

                            var genericArguments = fieldType.GenericTypeArguments;

                            for (var i = 0; i < genericArguments.Length; i++)
                            {
                                oldString += genericArguments[i].ToString().Replace('+', '.') +
                                             (i == genericArguments.Length - 1 ? "]" : ",");
                                newString += genericArguments[i].ToString().Replace('+', '.') +
                                             (i == genericArguments.Length - 1 ? ">" : ",");
                            }

                            longScript = longScript.Replace(oldString, newString);
                        }
                    }

                    var data = ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(d =>
                        d.Type == type);

                    if (data != null)
                        foreach (var customVariableEntry in data.customVariableEntries)
                        {
                            longScript +=
                                $"    public {customVariableEntry.variableType} {customVariableEntry.variableName};\n";
                        }


                    longScript += "    public " + type.Name +
                                  "ZSerializer (string ZUID, string GOZUID) : base(ZUID, GOZUID) {\n" +
                                  "        var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID] as " +
                                  type.FullName +
                                  ";\n";

                    foreach (var propertyInfo in type
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(ZSerialize.PropertyIsSuitableForZSerializer))
                    {
                        longScript += $"        " + propertyInfo.Name + " = " + "instance." + propertyInfo.Name +
                                      ";\n";
                    }

                    foreach (var fieldInfo in type
                        .GetFields(BindingFlags.Public | BindingFlags.Instance)
                        .Where(f => f.GetCustomAttribute<ObsoleteAttribute>() == null))
                    {
                        longScript += $"        " + fieldInfo.Name + " = " + "instance." + fieldInfo.Name + ";\n";
                    }

                    longScript +=
                        $"        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof({type.FullName}))?.OnSerialize?.Invoke(this, instance);\n";


                    longScript += "    }\n";


                    longScript +=
                        @"    public override void RestoreValues(UnityEngine.Component component)
    {
        var instance = (" + type.FullName + @")component;
";
                    foreach (var propertyInfo in type
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(ZSerialize.PropertyIsSuitableForZSerializer))
                    {
                        longScript += $"        instance." + propertyInfo.Name + " = " + propertyInfo.Name + ";\n";
                    }

                    longScript +=
                        $"        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof({type.FullName}))?.OnDeserialize?.Invoke(this, instance);\n";


                    longScript += "    }\n";
                    longScript += "}\n";
                }
            }

            EditorUtility.ClearProgressBar();

            longScript += "}";

            if (!Directory.Exists(Application.dataPath + "/ZResources/ZSerializer"))
                Directory.CreateDirectory(Application.dataPath + "/ZResources/ZSerializer");

            FileStream fs = new FileStream(
                Application.dataPath + "/ZResources/ZSerializer/UnityComponentZSerializers.cs",
                FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            sw.Write(longScript);
            sw.Close();

            AssetDatabase.Refresh();
            ZSerialize.Log("<color=cyan>Unity Component ZSerializers rebuilt</color>", DebugMode.Informational);
        }
#endif
    }
}