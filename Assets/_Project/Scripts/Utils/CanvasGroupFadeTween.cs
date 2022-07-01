using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupFadeTween : MonoBehaviour
{
    [SerializeField] private AutoTween tween;
    public float Time => tween.time;

    private CanvasGroup canvasGroup;
    
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        FadeIn(1f);
    }

    public void FadeIn(float to)
    {
        canvasGroup.alpha = 0;
        tween.Play(canvasGroup.LeanAlpha(to, tween.time));
    }
    
    public void FadeOut(float to, float? delayOverwrite = null)
    {
        canvasGroup.alpha = 1;
        var playedTween = tween.Play(canvasGroup.LeanAlpha(to, tween.time));
        if (delayOverwrite != null) playedTween.setDelay(delayOverwrite.Value);
    }
}