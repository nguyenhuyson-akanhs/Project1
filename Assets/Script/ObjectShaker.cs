using UnityEngine;
using System;

public class ObjectShaker : MonoBehaviour
{
    // Đặt tên nhãn cho vật thể này để phân biệt (ví dụ: "ThanhMau", "CaiCua", "HopQua"...)
    [SerializeField] private string objectLabel;

    private Vector3 initialPosition;
    private float shakeTimer;
    private float shakeIntensity;

    private static event Action<string, float, float> OnObjectShakeRequested;

    void Awake()
    {
        initialPosition = transform.localPosition;
    }

    void OnEnable() => OnObjectShakeRequested += CheckAndShake;
    void OnDisable() => OnObjectShakeRequested -= CheckAndShake;

    public static void CallObjectShake(string label, float intensity, float duration)
    {
        OnObjectShakeRequested?.Invoke(label, intensity, duration);
    }

    private void CheckAndShake(string label, float intensity, float duration)
    {
        if (this.objectLabel == label)
        {
            shakeIntensity = intensity;
            shakeTimer = duration;
        }
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * shakeIntensity;

            randomOffset.z = 0;

            transform.localPosition = initialPosition + randomOffset;

            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0f)
            {
                transform.localPosition = initialPosition;
            }
        }
    }
}