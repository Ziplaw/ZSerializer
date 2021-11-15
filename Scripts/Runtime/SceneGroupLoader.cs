using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using ZSerializer;

[AddComponentMenu("ZSerializer/SceneGroupLoader")]
public class SceneGroupLoader : MonoBehaviour
{
    public bool loadSceneData = true;
    
    async void Start()
    {
        
        //Create Camera if not present
        var cam = FindObjectOfType<Camera>();
        if (!cam)
        {
            var camGO = new GameObject("Camera");
            camGO.AddComponent<AudioListener>();
            cam = camGO.AddComponent<Camera>();
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = new Color(0.11f, 0.11f, 0.11f);
        }
        
        await CanvasFadeIn();

        //Deactivate previous scene if LoadSceneMode was set to additive.
        AsyncOperation operation;
        var currentActiveScene = SceneManager.GetActiveScene();
        if (currentActiveScene != gameObject.scene)
        {
            operation = SceneManager.UnloadSceneAsync(currentActiveScene);

            while (!operation.isDone)
            {
                await Task.Yield();
            }
        }

        //Loading last saved scene
        var sceneGroup = ZSerialize.sceneToLoadingSceneMap[SceneManager.GetActiveScene().path.ToEditorBuildSettingsPath()];
        var scenePath = await ZSerialize.GetLastSavedScenePath(sceneGroup.name);
        // var sceneLoaderScene = SceneManager.GetSceneByPath(sceneGroup.loadingScenePath.ToAssetPath());
        operation = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            await Task.Yield();
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByPath(scenePath.ToAssetPath()));
        
        //Destroy unneded camera if it isnt unloaded by now.
        if(cam != null) Destroy(cam.gameObject);
        
        
        // Loading data from scene if specified
        if (loadSceneData)
        {
            ZSerialize.UpdateCurrentScene();
            await ZSerialize.LoadScene();
        }

        await CanvasFadeOut();

        //Unloading loading scene
        // operation = SceneManager.UnloadSceneAsync(sceneLoaderScene);

        while (!operation.isDone)
        {
            await Task.Yield();
        }
    }
    
    public async Task CanvasFadeIn()
    {
        var canvas = GameObject.Find("Canvas");
        var canvasGroup = canvas.GetComponent<CanvasGroup>();
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            await Task.Yield();
        }
    }
    
    public async Task CanvasFadeOut()
    {
        var canvas = GameObject.Find("Canvas");
        var canvasGroup = canvas.GetComponent<CanvasGroup>();
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            await Task.Yield();
        }
    }
    
    
}
