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
    public class ZSerializerConfigurator : EditorWindow
    {
        [MenuItem("Tools/ZSerializer/ZSerializer Configurator", priority = 1)]
        public static void ShowWindow()
        {
            var window = GetWindow<ZSerializerConfigurator>();
            window.titleContent = new GUIContent("ZSerializer Configurator");
            window.Show();
        }

        private List<Type> componentTypes;
        private Type selectedType;
        private List<PropertyInfo> propertyInfoList;
        private List<UnityComponentData.CustomVariableEntry> _customVariableEntries;
        private int customVariableEntryEditIndex = -1;

        readonly Color classOrStruct = new Color(0f, 0.79f, 0.69f);
        readonly Color nativeTypes = new Color(0f, 0.55f, 0.85f);


        private string variableType;
        private string variableName;
        private string onVariableSerialize;
        private string onVariableDeserialize;

        private string searchTypes;
        private string searchComponents;
        Vector2 scrollPosComponentTypes;
        Vector2 scrollPosComponentProps;
        Vector2 scrollPosCustomVariableEntry;


        private void OnEnable()
        {
            componentTypes = ZSerialize.UnitySerializableTypes.Intersect(ZSerializerSettings.Instance.unityComponentTypes.Select(s => Type.GetType(s))).OrderBy(a => a.Name).ToList();
            searchTypes = "";
            searchComponents = "";
        }

        readonly Dictionary<string, string> aliases = new Dictionary<string, string>()
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
                    using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPosComponentTypes))
                    {
                        scrollPosComponentTypes = scrollView.scrollPosition;

                        searchTypes = GUILayout.TextField(searchTypes, GUI.skin.FindStyle("ToolbarSeachTextField"));

                        foreach (var componentType in componentTypes.Where(c =>
                            c.Name.ToLower().Contains(searchTypes.ToLower())))
                        {
                            if (GUILayout.Button(componentType.Name))
                            {
                                selectedType = componentType;
                                propertyInfoList = selectedType
                                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                    .Where(PropertyIsSuitableForAssignmentNoBlackList).ToList();
                                _customVariableEntries = ZSerializerSettings.Instance.unityComponentDataList
                                    .FirstOrDefault(s => s.Type == selectedType)?.customVariableEntries;
                                customVariableEntryEditIndex = -1;
                            }
                        }
                    }
                }


                using (new EditorGUILayout.VerticalScope(GUILayout.Height(position.height - 5)))
                {
                    if (selectedType != null)
                    {
                        using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPosComponentProps, "helpbox"))
                        {
                            scrollPosComponentProps = scrollView.scrollPosition;
                            searchComponents = GUILayout.TextField(searchComponents,
                                GUI.skin.FindStyle("ToolbarSeachTextField"));

                            GUILayout.Label("Unity Variables",
                                new GUIStyle("box") { alignment = TextAnchor.MiddleCenter, stretchWidth = true });


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
                                        !ZSerializerSettings.Instance.unityComponentDataList.IsInBlackList(
                                            selectedType,
                                            propertyInfo.Name);

                                    var prev = isWhiteListed;


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
                                            ZSerializerSettings.Instance.unityComponentDataList.SafeRemove(
                                                selectedType,
                                                propertyInfo.Name);
                                        }
                                        else
                                        {
                                            ZSerializerSettings.Instance.unityComponentDataList.SafeAdd(
                                                selectedType,
                                                propertyInfo.Name);
                                        }
                                    }
                                }
                            }

                            GUILayout.Label("Custom Variable Entries",
                                new GUIStyle("box") { alignment = TextAnchor.MiddleCenter, stretchWidth = true });

                            if (_customVariableEntries != null)
                                for (var i = 0; i < _customVariableEntries.Count; i++)
                                {
                                    var customVariableEntry = _customVariableEntries[i];
                                    using (new GUILayout.HorizontalScope())
                                    {
                                        if (i == customVariableEntryEditIndex)
                                        {
                                            using (new GUILayout.VerticalScope())
                                            {
                                                using (new GUILayout.HorizontalScope())
                                                {
                                                    variableType = GUILayout.TextField(variableType);
                                                    variableName = GUILayout.TextField(variableName);
                                                }
                                            }

                                            if (GUILayout.Button("Save", GUILayout.Width(50)))
                                            {
                                                _customVariableEntries[i] =
                                                    new UnityComponentData.CustomVariableEntry(variableType,
                                                        variableName);

                                                ZSerializerSettings.Instance.unityComponentDataList.First(data =>
                                                        data.Type == selectedType).customVariableEntries[i] =
                                                    _customVariableEntries[i];

                                                EditorUtility.SetDirty(ZSerializerSettings.Instance);
                                                AssetDatabase.SaveAssets();
                                                customVariableEntryEditIndex = -1;
                                            }

                                            if (GUILayout.Button("-", GUILayout.Width(20)))
                                            {
                                                customVariableEntryEditIndex = -1;

                                                ZSerializerSettings.Instance.unityComponentDataList.SafeRemove(
                                                    selectedType, i);
                                                EditorUtility.SetDirty(ZSerializerSettings.Instance);
                                                AssetDatabase.SaveAssets();
                                            }
                                        }
                                        else
                                        {
                                            using (new EditorGUILayout.VerticalScope())
                                            {
                                                using (new EditorGUILayout.HorizontalScope())
                                                {
                                                    using (new EditorGUI.DisabledScope(true))
                                                    {
                                                        GUILayout.Toggle(true, GUIContent.none, GUILayout.Width(15));
                                                    }


                                                    GUILayout.Label(customVariableEntry.variableType,
                                                        new GUIStyle("label")
                                                        {
                                                            normal = new GUIStyleState
                                                            {
                                                                textColor = aliases.ContainsValue(customVariableEntry
                                                                    .variableType)
                                                                    ? nativeTypes
                                                                    : classOrStruct
                                                            }
                                                        }, GUILayout.Width(longestPropertyName * 7));

                                                    GUILayout.Label(customVariableEntry.variableName);
                                                }
                                            }

                                            if (GUILayout.Button("Edit", GUILayout.Width(50)))
                                            {
                                                variableType = customVariableEntry.variableType;
                                                variableName = customVariableEntry.variableName;

                                                customVariableEntryEditIndex = i;
                                            }
                                        }
                                    }
                                }

                            if (GUILayout.Button("+"))
                            {
                                ZSerializerSettings.Instance.unityComponentDataList.SafeAdd(selectedType,
                                    new UnityComponentData.CustomVariableEntry("type", "name"));
                                _customVariableEntries = ZSerializerSettings.Instance.unityComponentDataList
                                    .FirstOrDefault(s => s.Type == selectedType)?.customVariableEntries;

                                if (_customVariableEntries != null)
                                    customVariableEntryEditIndex = _customVariableEntries.Count - 1;
                                variableType = "type";
                                variableName = "name";
                                EditorUtility.SetDirty(ZSerializerSettings.Instance);
                                AssetDatabase.SaveAssets();
                            }
                        }

                        using (new EditorGUILayout.HorizontalScope("helpbox"))
                        {
                            var serializedObject = new SerializedObject(ZSerializerSettings.Instance);

                            serializedObject.Update();
                            for (var i = 0; i < ZSerializerSettings.Instance.unityComponentDataList.Count; i++)
                            {
                                if (ZSerializerSettings.Instance.unityComponentDataList[i].Type == selectedType)
                                {
                                    EditorGUILayout.PropertyField(serializedObject
                                        .FindProperty("unityComponentDataList").GetArrayElementAtIndex(i)
                                        .FindPropertyRelative("OnSerialize"));
                                    EditorGUILayout.PropertyField(serializedObject
                                        .FindProperty("unityComponentDataList").GetArrayElementAtIndex(i)
                                        .FindPropertyRelative("OnDeserialize"));
                                }
                            }

                            serializedObject.ApplyModifiedProperties();
                        }
                    }

                    else
                    {
                        GUILayout.Label("Select a Component");
                    }

                    if (selectedType != null)
                    {
                        if (GUILayout.Button("Remove All"))
                        {
                            foreach (var propertyInfo in propertyInfoList.Where(c =>
                                c.Name.ToLower().Contains(searchComponents.ToLower())))
                            {
                                ZSerializerSettings.Instance.unityComponentDataList.SafeAdd(selectedType,
                                    propertyInfo.Name);
                            }
                        }

                        if (GUILayout.Button("Select All"))
                        {
                            foreach (var propertyInfo in propertyInfoList.Where(c =>
                                c.Name.ToLower().Contains(searchComponents.ToLower())))
                            {
                                ZSerializerSettings.Instance.unityComponentDataList.SafeRemove(selectedType,
                                    propertyInfo.Name);
                            }
                        }


                        if (GUILayout.Button("Save & Apply"))
                        {
                            ZSerializerEditorRuntime.GenerateUnityComponentClasses();
                            EditorUtility.SetDirty(ZSerializerSettings.Instance);
                            AssetDatabase.SaveAssets();
                        }
                    }
                }
            }
        }

        internal static bool PropertyIsSuitableForAssignmentNoBlackList(PropertyInfo fieldInfo)
        {
            // UnityComponentData blackList =
            //     ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(c => c.Type == fieldInfo.DeclaringType);
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