using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using ZSerializer;

namespace ZSerializer.Editor
{
    class CustomBuildPipeline : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;
        private bool errorCS1061;
        private bool errorCS0200;

        // CALLED BEFORE THE BUILD
        public void OnPreprocessBuild(BuildReport report)
        {
            Application.logMessageReceived += OnBuildError;
        }

        // CALLED DURING BUILD TO CHECK FOR ERRORS
        private void OnBuildError(string condition, string stacktrace, LogType type)
        {
            if (condition.Contains("CS1061")) // BUILD ERROR FOR WHEN USING EDITOR ONLY PROPERTIES ON BUILD 
            {
                errorCS1061 = true;
                var split = condition.Split('\'');
                string typeName = split[1]; //EW
                string propertyName = split[3]; //EWWWWW

                var componentType =
                    FindTypeInsideAssemblies(AppDomain.CurrentDomain.GetAssemblies(),
                        "UnityEngine." + typeName); //THIS MIGHT BREAK IN FUTURE VERSIONS

                ZSerializerSettings.Instance.unityComponentDataList.SafeAdd(componentType, propertyName);
            }

            if (condition.Contains("CS0200"))
            {
                errorCS0200 = true;
                var split = condition.Split('\'')[1];
                string typeName = split.Split('.')[0];
                string propertyName = split.Split('.')[1];
                
                var componentType =
                    FindTypeInsideAssemblies(AppDomain.CurrentDomain.GetAssemblies(),
                        "UnityEngine." + typeName);
                
                ZSerializerSettings.Instance.unityComponentDataList.SafeAdd(componentType, propertyName);

            }

            if (condition.Contains("'Failed'") && (errorCS1061 || errorCS0200))
            {
                Debug.LogWarning(
                    "<color=cyan>[ZS] Some of your build errors had to do with Editor Only Properties being Serialized, rebuilding Unity Component Serializer</color>");
                EditorUtility.SetDirty(ZSerializerSettings.Instance);
                AssetDatabase.SaveAssets();
                ZSerializerEditorRuntime.GenerateUnityComponentClasses();

            }
        }

        internal static Type FindTypeInsideAssemblies(Assembly[] assemblies, string typeName)
        {
            var assembly = assemblies.First(a => a.GetType(typeName) != null);
            return assembly.GetType(typeName);
        }



        // CALLED AFTER THE BUILD
        public void OnPostprocessBuild(BuildReport report)
        {
            // IF BUILD FINISHED AND SUCCEEDED, STOP LOOKING FOR ERRORS
            Application.logMessageReceived -= OnBuildError;

        }
    }
}