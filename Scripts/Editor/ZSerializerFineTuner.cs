using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using UnityEditor;
using UnityEngine;
using ZSerializer;

namespace ZSerializer.Editor
{


    public class ZSerializerFineTuner : EditorWindow
    {
        [MenuItem("Tools/ZSerializer/ZSerializer Configurator")]
        internal static void ShowWindow()
        {
            var window = GetWindow<ZSerializerFineTuner>();
            window.titleContent = new GUIContent("ZSerializer Configurator");
            window.Show();
        }

        private List<Type> componentTypes;
        private Type selectedType;
        private List<PropertyInfo> propertyInfoList;
        private string searchTypes;
        private string searchComponents;
        Vector2 scrollPosComponentTypes;
        Vector2 scrollPosComponentProps;


        private void OnEnable()
        {
            componentTypes = ZSerialize.UnitySerializableTypes.OrderBy(a => a.Name).ToList();
            componentTypes.Remove(typeof(PersistentGameObject));
            searchTypes = "";
            searchComponents = "";
        }

        Dictionary<string, string> aliases = new Dictionary<string, string>()
        {
            { "Object", "object" },
            { "String", "string" },
            { "Boolean", "bool" },
            { "Byte", "byte" },
            { "SByte", "sbyte" },
            { "Int16", "short" },
            { "UInt16", "ushort" },
            { "Int32", "int" },
            { "UInt32", "uint" },
            { "Int64", "long" },
            { "UInt64", "ulong" },
            { "Single", "float" },
            { "Double", "double" },
            { "Decimal", "decimal" },
            { "Char", "char" }
        };

        private void OnGUI()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUILayout.VerticalScope("helpbox", GUILayout.MaxWidth(200)))
                {
                    using var scrollView = new EditorGUILayout.ScrollViewScope(scrollPosComponentTypes);
                    scrollPosComponentTypes = scrollView.scrollPosition;

                    searchTypes = GUILayout.TextField(searchTypes, GUI.skin.FindStyle("ToolbarSeachTextField"));

                    foreach (var componentType in componentTypes.Where(c =>
                        c.Name.ToLower().Contains(searchTypes.ToLower())))
                    {
                        if (GUILayout.Button(componentType.Name))
                        {
                            selectedType = componentType;
                            propertyInfoList = selectedType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .Where(PropertyIsSuitableForAssignmentNoBlackList).ToList();
                        }
                    }
                }


                using (new EditorGUILayout.VerticalScope(GUILayout.Height(position.height - 5)))
                {
                    using (new EditorGUILayout.VerticalScope("helpbox", GUILayout.Height(position.height - 5)))
                    {
                        if (selectedType != null)
                        {
                            searchComponents = GUILayout.TextField(searchComponents,
                                GUI.skin.FindStyle("ToolbarSeachTextField"));
                            using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPosComponentProps))
                            {
                                scrollPosComponentProps = scrollView.scrollPosition;

                                int longestPropertyName = selectedType
                                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                    .Where(PropertyIsSuitableForAssignmentNoBlackList)
                                    .Max(p => p.PropertyType.Name.Length);



                                foreach (var propertyInfo in propertyInfoList.Where(c =>
                                    c.Name.ToLower().Contains(searchComponents.ToLower())))
                                {
                                    using (new EditorGUILayout.HorizontalScope())
                                    {
                                        bool isWhiteListed =
                                            !ZSerializerSettings.Instance.componentBlackList.IsInBlackList(selectedType,
                                                propertyInfo.Name);

                                        var prev = isWhiteListed;

                                        Color classOrStruct = new Color(0f, 0.79f, 0.69f);
                                        Color nativeTypes = new Color(0f, 0.55f, 0.85f);

                                        string propertyName = aliases.ContainsKey(propertyInfo.PropertyType.Name)
                                            ? aliases[propertyInfo.PropertyType.Name]
                                            : propertyInfo.PropertyType.Name;

                                        isWhiteListed = GUILayout.Toggle(isWhiteListed, GUIContent.none,
                                            GUILayout.Width(15));
                                        GUILayout.Label(propertyName,
                                            new GUIStyle("label")
                                            {
                                                normal = new GUIStyleState()
                                                {
                                                    textColor = aliases.ContainsKey(propertyInfo.PropertyType.Name)
                                                        ? nativeTypes
                                                        : classOrStruct
                                                }
                                            }, GUILayout.Width(longestPropertyName * 7));
                                        GUILayout.Label(propertyInfo.Name);

                                        if (isWhiteListed != prev)
                                        {
                                            Undo.RecordObject(ZSerializerSettings.Instance,
                                                "Change Component Blacklist");
                                            if (isWhiteListed)
                                            {
                                                ZSerializerSettings.Instance.componentBlackList.SafeRemove(selectedType,
                                                    propertyInfo.Name);
                                            }
                                            else
                                            {
                                                ZSerializerSettings.Instance.componentBlackList.SafeAdd(selectedType,
                                                    propertyInfo.Name);
                                            }
                                        }
                                    }
                                }
                            }

                            if (GUILayout.Button("Remove All"))
                            {
                                foreach (var propertyInfo in propertyInfoList.Where(c =>
                                    c.Name.ToLower().Contains(searchComponents.ToLower())))
                                {
                                    ZSerializerSettings.Instance.componentBlackList.SafeAdd(selectedType,
                                        propertyInfo.Name);
                                }
                            }

                            if (GUILayout.Button("Select All"))
                            {
                                foreach (var propertyInfo in propertyInfoList.Where(c =>
                                    c.Name.ToLower().Contains(searchComponents.ToLower())))
                                {
                                    ZSerializerSettings.Instance.componentBlackList.SafeRemove(selectedType,
                                        propertyInfo.Name);
                                }

                            }

                            if (GUILayout.Button("Save & Apply"))
                            {
                                ZSerializerEditor.GenerateUnityComponentClasses();
                                EditorUtility.SetDirty(ZSerializerSettings.Instance);
                                AssetDatabase.SaveAssets();
                            }

                        }
                        else
                        {
                            GUILayout.Label("Select a Component");
                        }


                    }
                }
            }
        }

        internal static bool PropertyIsSuitableForAssignmentNoBlackList(PropertyInfo fieldInfo)
        {
            // SerializableComponentBlackList blackList =
            //     ZSerializerSettings.Instance.componentBlackList.FirstOrDefault(c => c.Type == fieldInfo.DeclaringType);
            // bool isInBlackList = blackList != null;

            return fieldInfo.GetCustomAttribute<ObsoleteAttribute>() == null &&
                   fieldInfo.GetCustomAttribute<NonZSerialized>() == null &&
                   fieldInfo.CanRead &&
                   fieldInfo.CanWrite &&
                   // (
                   //     (!isInBlackList) || (
                   //         isInBlackList &&
                   //         !blackList.componentNames.Contains(fieldInfo.Name))
                   // ) &&
                   fieldInfo.Name != "material" &&
                   fieldInfo.Name != "materials" &&
                   fieldInfo.Name != "sharedMaterial" &&
                   fieldInfo.Name != "mesh" &&
                   fieldInfo.Name != "tag" &&
                   fieldInfo.Name != "name";
        }
    }
}