using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using ZSaver;

class CustomBuildPipeline : IPreprocessBuildWithReport, IPostprocessBuildWithReport
{
    public int callbackOrder => 0;
    private bool errorCs1061;

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
            errorCs1061 = true;
            var split = condition.Split('\'');
            string typeName = split[1];
            string propertyName = split[3];

            var componentType =
                ZSave.FindTypeInsideAssemblies(AppDomain.CurrentDomain.GetAssemblies(),
                    "UnityEngine." + typeName); //THIS MIGHT BREAK IN FUTURE VERSIONS

            var s = ZSaverSettings.Instance.componentBlackList.FirstOrDefault(c => c.Type == componentType);

            if (s != null)
            {
                s.componentNames.Add(propertyName);
            }
            else
            {
                ZSaverSettings.Instance.componentBlackList.Add(
                    new SerializableComponentBlackList(componentType, propertyName));
            }
        }

        if (condition.Contains("'Failed'") && errorCs1061)
        {
            Debug.LogWarning(
                "Some of your build errors had to do with Editor Only Properties being Serialized, rebuilding Unity Component Serializer");
            ZSaverEditor.GenerateUnityComponentClasses();
            
        }
    }
    
    

    // CALLED AFTER THE BUILD
    public void OnPostprocessBuild(BuildReport report)
    {
        // IF BUILD FINISHED AND SUCCEEDED, STOP LOOKING FOR ERRORS
        Application.logMessageReceived -= OnBuildError;
            
    }
}