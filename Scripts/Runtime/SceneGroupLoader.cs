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
    
    async void Start()
    {
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

        var operation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        while (!operation.isDone)
        {
            await Task.Yield();
        }
        
        var sceneGroup = ZSerialize.sceneToLoadingSceneMap[SceneManager.GetActiveScene().path.ToEditorBuildSettingsPath()];
        var scenePath = await ZSerialize.GetLastSavedScenePath(sceneGroup.name);
        var sceneLoaderScene = SceneManager.GetSceneByPath(sceneGroup.loadingScenePath.ToAssetPath());
        operation = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            await Task.Yield();
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByPath(scenePath.ToAssetPath()));
        
        if(cam != null) Destroy(cam.gameObject);
        ZSerialize.UpdateCurrentScene();
        await ZSerialize.LoadScene();
        await CanvasFadeOut();

        operation = SceneManager.UnloadSceneAsync(sceneLoaderScene);

        while (!operation.isDone)
        {
            await Task.Yield();
        }
        
        ZSerialize.UpdateCurrentScene();
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
