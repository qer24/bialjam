using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerHeightUI : MonoBehaviour
{
    [SerializeField] private PlayerSizeManager sizeManager;
    [SerializeField] private FirstPersonController controller;
    [SerializeField] private Transform heightTransform;
    [SerializeField] private AutoTween scaleTween;
    [SerializeField] private float rotateLerpSpeed;
    
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

    private void Update()
    {
        var angle = controller.reverseMovement < 0 ? 180 : 0;
        transform.parent.localRotation = Quaternion.Slerp(transform.parent.localRotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotateLerpSpeed);
    }
}