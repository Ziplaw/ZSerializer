using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codice.Client.Common.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using ZSerializer;

[AddComponentMenu("ZSerializer/SceneGroupLoader")]
public class SceneGroupLoader : MonoBehaviour
{
    async void Start()
    {
        await CanvasFadeIn();
        
        var operation = SceneManager.UnloadSceneAsync(FindObjectOfType<Camera>().gameObject.scene);

        while (!operation.isDone)
        {
            await Task.Yield();
        }
        
        operation = ZSerialize.LoadSceneGroup("Scene Group 1", LoadSceneMode.Additive);
        
        while (!operation.isDone)
        {
            await Task.Yield();
        }

        SceneManager.SetActiveScene(
            SceneManager.GetSceneByPath(ZSerialize.GetLastSavedScenePath("Scene Group 1").ToAssetPath()));

        ZSerialize.UpdateCurrentScene();
        await ZSerialize.LoadScene();
        
        await CanvasFadeOut();

        SceneManager.UnloadSceneAsync(1);
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
