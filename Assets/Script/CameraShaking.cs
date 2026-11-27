using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShaking : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin noise;
    private float shakeTimer;

    public static event Action<float, float> OnCameraShakeRequested;

    void Awake()
    {
        noise = GetComponent<CinemachineBasicMultiChannelPerlin>();
        noise.AmplitudeGain = 0;
        shakeTimer = 0;
    }

    void OnEnable()
    {
        OnCameraShakeRequested += TriggerShake;
    }

    void OnDisable()
    {
        OnCameraShakeRequested -= TriggerShake;
    }

    public static void CallShakeCamera()
    {
        CameraShaking.OnCameraShakeRequested?.Invoke(5f, 0.1f);
    }

    private void TriggerShake(float intensity, float duration)
    {
        if (noise != null)
        {
            noise.AmplitudeGain = intensity;
            shakeTimer = duration;
        }
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                noise.AmplitudeGain = 0f;
            }
        }
    }
}
