using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class AutoTween
{
    public float time;
    public LeanTweenType type;

    [SerializeField] private bool hasDelay = false;
    [SerializeField, ShowIf("hasDelay")] private float delay;

    private int? uniqueId;

    [NonSerialized] public bool autoCancel = true;
    
    public LTDescr Play(LTDescr tween)
    {
        if (autoCancel) Cancel();
        var newTween = tween.setEase(type);
        if (hasDelay) newTween.setDelay(delay);
        uniqueId = newTween.uniqueId;
        return newTween;
    }

    public void Cancel()
    {
        if (uniqueId != null) LeanTween.cancel(uniqueId.Value);
    }
    
    public AutoTween Clone()
    {
        return (AutoTween)MemberwiseClone();
    }
}