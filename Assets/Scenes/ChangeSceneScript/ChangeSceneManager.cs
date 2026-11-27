using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneManager : MonoBehaviour
{
    public static ChangeSceneManager Instance { get; private set; }

    public List<GameObject> _ListBlock = new();

    protected virtual void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }


    public virtual async Awaitable IntitaliazeObject(Action<float> Progress)
    {
        int Count = 0;

        for (int i = 0;i < _ListBlock.Count;i++)
        {
            GameObject TypeBlock = _ListBlock[i];
            foreach(Transform child in TypeBlock.transform)
            {
                Count++;
                child.gameObject.SetActive(true);

                if(Count == 10)
                {
                    Count = 0;
                    await Awaitable.NextFrameAsync();
                }
            }
            Progress((i+1)/_ListBlock.Count);
        }
    }
}
