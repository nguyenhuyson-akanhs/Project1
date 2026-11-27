using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    public void Onclick()
    {
        _ = ChangeSceneAsync();
    }
    protected virtual async Awaitable ChangeSceneAsync()
    {
        // ----- Enter -----
        // do animation or smt to face in v.v
        AsyncOperation LoadAsync = SceneManager.LoadSceneAsync("ChangeScene 2",LoadSceneMode.Additive);
        LoadAsync.allowSceneActivation = false;
        Debug.Log("DoneEnter");


        // ----- MainLoad -----
        while (Mathf.Clamp01(LoadAsync.progress) < 0.9f)
        {
            //loadbar there
            //Debug.Log($"Progress: {Mathf.Clamp01(LoadAsync.progress) * 100}%");

            await Awaitable.NextFrameAsync();
        }
        //Debug.Log("Done 90%");

        LoadAsync.allowSceneActivation = true;
        while (!LoadAsync.isDone) await Awaitable.NextFrameAsync();

        await Awaitable.NextFrameAsync();

        await ChangeSceneManager.Instance.IntitaliazeObject((progress) =>
        {
            // loadbar there;
            //Debug.Log($"Progress: {(0.9 + progress/10) * 100}%");

        });

        await Awaitable.NextFrameAsync();
        // ----- EndLoad -----
        // do face out v.v
        //Debug.Log("Done all load");
    }
}
