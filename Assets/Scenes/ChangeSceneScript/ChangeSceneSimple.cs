using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneSimple : MonoBehaviour
{
    public void Onclick()
    {
        _ = ChangeScene();
    }
    protected virtual async Awaitable ChangeScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("ChangeScene 2",LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log($"progress: {progress * 100}%");

            if(progress >= 1)
            {
                asyncLoad.allowSceneActivation = true;
                break;
            }

            await Awaitable.NextFrameAsync();
        }
    }
}
