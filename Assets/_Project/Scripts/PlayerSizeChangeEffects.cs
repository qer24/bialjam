using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerSizeChangeEffects : MonoBehaviour
{
    [SerializeField] private PlayerSizeManager sizeManager;
    [SerializeField] private Volume postProcessing;
    [SerializeField] private AutoTween weightTween;

    private void OnEnable()
    {
        postProcessing.weight = 0f;
        sizeManager.OnSizeUpdated += Effects;
    }

    private void OnDisable()
    {
        sizeManager.OnSizeUpdated -= Effects;
    }

    private void Effects(float _)
    {
        postProcessing.weight = 1f;
        weightTween.Play(postProcessing.LeanWeight(0f, weightTween.time));
    }
}