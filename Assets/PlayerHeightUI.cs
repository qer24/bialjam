using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeightUI : MonoBehaviour
{
    [SerializeField] private PlayerSizeManager sizeManager;
    [SerializeField] private Transform heightTransform;
    [SerializeField] private AutoTween scaleTween;
    
    private void OnEnable()
    {
        sizeManager.OnSizeUpdated += ScaleUI;
    }

    private void OnDisable()
    {
        sizeManager.OnSizeUpdated -= ScaleUI;
    }

    private void ScaleUI(float currentSize)
    {
        scaleTween.Play(heightTransform.LeanScale(heightTransform.transform.localScale.WithY(currentSize), scaleTween.time));
    }
}
