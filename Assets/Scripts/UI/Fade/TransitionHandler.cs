using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionHandler : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public event Action fadeOutFinished;
    public event Action fadeInFinished;

    public void StartFadeIn()
    {
        animator.SetTrigger("FadeIn");
    }

    public void StartFadeOut()
    {
        animator.SetTrigger("FadeOut");
    }

    private void FadeInEnded()
    {
        fadeInFinished?.Invoke();
    }

    private void FadeOutEnded()
    {
        fadeOutFinished?.Invoke();
    }
}
