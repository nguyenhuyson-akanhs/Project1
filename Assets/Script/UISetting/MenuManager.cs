using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public GameObject Menu;
    private Animator _Animator;
    private int DisplayAnima;
    private int ConcealAnima;

    private void Awake()
    {
        _Animator = Menu.GetComponent<Animator>();
    }
    private void Start()
    {
        Menu.SetActive(false);
        DisplayAnima = Animator.StringToHash("Display");
        ConcealAnima = Animator.StringToHash("Conceal");
    }
    public void OpenMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!Menu.activeInHierarchy) DisplayMethod();
            else _= ConcealMethod();
        }
    }
    private void DisplayMethod() 
    {
        Menu.SetActive(true);
        _Animator.CrossFade(DisplayAnima, 0.1f);
    }
    private async Awaitable ConcealMethod()
    {
        _Animator.CrossFade(ConcealAnima, 0.1f);
        await AwaitableExtension.WaitUntil(() =>
        {
            AnimatorStateInfo state = _Animator.GetCurrentAnimatorStateInfo(0);
            return state.shortNameHash == ConcealAnima && state.normalizedTime >= 1f;
        });
        Menu.SetActive(false);
    }
}
