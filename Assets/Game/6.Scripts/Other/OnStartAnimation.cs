using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] string animationString;
    [SerializeField] bool playOnStart;

    private void Start()
    {
        if (playOnStart)
            PlayAnimation();
    }

    public void PlayAnimation()
    {
        animator.Play(animationString);
    }
}
