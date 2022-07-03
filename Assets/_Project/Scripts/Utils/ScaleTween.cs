using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class ScaleTween : MonoBehaviour
{
    public float time;
    public float delay;
    
    [Space]
    [SerializeField] private LeanTweenType type;
    [SerializeField] private Vector3 tweenStartScale = Vector3.zero;
    
    [Space]
    public bool scaleOnEnable = true;
    [SerializeField] private bool ignoreTimeScale = false;

    private Vector3 startScale;
    private int tweenId;

    private ScaleTween[] children;

    [HideInEditorMode] public bool scaling = false;
    
    private void Awake()
    {
        startScale = transform.localScale;
        children = GetComponentsInChildren<ScaleTween>();

        scaling = false;
    }

    private void OnEnable()
    {
        if (scaleOnEnable) Scale();
    }

    public void Scale(bool withChildren = false)
    {
        transform.localScale = tweenStartScale;
        LeanTween.cancel(tweenId);
        
        scaling = true;
        tweenId = transform.LeanScale(startScale, time).setIgnoreTimeScale(ignoreTimeScale).setDelay(delay).setEase(type).setOnComplete(() => scaling = false).uniqueId;

        if (!withChildren) return;
        foreach (var child in children)
        {
            child.Scale();
        }
    }
    
    public void Scale(float delayOverrite, bool withChildren = false)
    {
        transform.localScale = tweenStartScale;
        LeanTween.cancel(tweenId);

        scaling = true;
        tweenId = transform.LeanScale(startScale, time).setIgnoreTimeScale(ignoreTimeScale).setDelay(delayOverrite).setEase(type).setOnComplete(() => scaling = false).uniqueId;

        if (!withChildren) return;
        foreach (var child in children)
        {
            child.Scale();
        }
    }
}