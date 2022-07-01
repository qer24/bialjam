using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsateScale : MonoBehaviour
{
    [SerializeField] private float pulseAmount = 1.1f;
    [SerializeField] private float pulseTime = 1f;
    [SerializeField] private float waitTime = 0.5f;

    private int tweenId;

    private void Start()
    {
        StartCoroutine(PulseCoroutine());
    }

    private IEnumerator PulseCoroutine()
    {
        yield return null;
        
        while (true)
        {
            yield return new WaitForSecondsRealtime(pulseTime);
            
            LeanTween.cancel(tweenId);
            var tween = transform.LeanScale(transform.localScale * pulseAmount, pulseTime).setEase(LeanTweenType.punch).setIgnoreTimeScale(true);
            tweenId = tween.uniqueId;
            
            yield return new WaitForSecondsRealtime(waitTime);
        }
    }
}