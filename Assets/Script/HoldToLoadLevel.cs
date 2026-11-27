using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HoldToLoadLevel : MonoBehaviour
{
    public float HoldTimeNeed = 1f;
    public float HoldTimeCurrent = 0f;
    public float CurrentAmount = 0f;
    public bool IsHolding = false;
    public Image LoadBar;

    public static event Action OnHold;
    private void Start()
    {
        LoadBar.fillAmount = 0f;
        HoldTimeCurrent = 0f;
        CurrentAmount = 0f;
    }
    private void Update()
    {
        if (IsHolding && GameControllerScript.FullProcess)
        {
            HoldTimeCurrent += Time.deltaTime;
            CurrentAmount = HoldTimeCurrent / HoldTimeNeed;
            LoadBar.fillAmount = CurrentAmount;
        }
    }

    public void OnHoldSmt(InputAction.CallbackContext context)
    {
        if (context.performed) IsHolding = true;

        if(LoadBar.fillAmount == 1)
        {
            OnHold?.Invoke();
        }

        if (context.canceled)
        {
            IsHolding = false;
            HoldTimeCurrent = 0;
            CurrentAmount = HoldTimeCurrent / HoldTimeNeed;
            LoadBar.fillAmount = CurrentAmount;
        }

    }
}
