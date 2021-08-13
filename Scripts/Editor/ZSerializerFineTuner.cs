using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ZSerializer.Editor
{
    public class ZSerializerFineTuner : EditorWindow
    {
        [MenuItem("Tools/ZSave/ZSerializer Fine Tuner")]
        internal static void ShowWindow()
        {
            var window = GetWindow<ZSerializerFineTuner>();
            window.titleContent = new GUIContent("ZSerializer Fine Tuner");
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
            componentTypes = ZSave.ComponentSerializableTypes.OrderBy(a => a.Name).ToList();
            componentTypes.Remove(typeof(PersistentGameObject));
            searchTypes = "";
            searchComponents = "";
        }

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


                using (var scope = new EditorGUILayout.VerticalScope(GUILayout.Height(position.height - 5)))
                {
                    using (new EditorGUILayout.VerticalScope("helpbox", GUILayout.Height(position.height - 5)))
                    {
                        if (selectedType != null)
                        {
                            searchComponents = GUILayout.TextField(searchComponents, GUI.skin.FindStyle("ToolbarSeachTextField"));
                            using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPosComponentProps))
                            {
                                scrollPosComponentProps = scrollView.scrollPosition;



                                foreach (var propertyInfo in propertyInfoList.Where(c =>
                                    c.Name.ToLower().Contains(searchComponents.ToLower())))
                                {
                                    using (new EditorGUILayout.HorizontalScope())
                                    {
                                        bool isWhiteListed =
                                            !ZSaverSettings.Instance.componentBlackList.IsInBlackList(selectedType,
                                                propertyInfo.Name);

                                        var prev = isWhiteListed;

                                        isWhiteListed = GUILayout.Toggle(isWhiteListed, propertyInfo.Name);

                                        if (isWhiteListed != prev)
                                        {
                                            if (isWhiteListed)
                                            {
                                                ZSaverSettings.Instance.componentBlackList.SafeRemove(selectedType, propertyInfo.Name);
                                            }
                                            else
                                            {
                                                ZSaverSettings.Instance.componentBlackList.SafeAdd(selectedType, propertyInfo.Name);
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
                                    ZSaverSettings.Instance.componentBlackList.SafeAdd(selectedType, propertyInfo.Name);
                                }
                            }
                        
                            if (GUILayout.Button("Select All"))
                            {
                                foreach (var propertyInfo in propertyInfoList.Where(c =>
                                    c.Name.ToLower().Contains(searchComponents.ToLower())))
                                {
                                    ZSaverSettings.Instance.componentBlackList.SafeRemove(selectedType, propertyInfo.Name);
                                }

                            }
                        
                            if (GUILayout.Button("Save & Apply"))
                            {
                                ZSaverEditor.GenerateUnityComponentClasses();
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
            //     ZSaverSettings.Instance.componentBlackList.FirstOrDefault(c => c.Type == fieldInfo.DeclaringType);
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