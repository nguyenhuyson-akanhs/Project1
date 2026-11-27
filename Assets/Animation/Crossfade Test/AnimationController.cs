using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public bool Tick = false;
    public Animator animator;

    private void Update()
    {
        if (Tick)
        {
            animator.CrossFade("2", 0.9f, 0);
            Tick = false;
        }
    }
}
