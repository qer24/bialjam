using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTween : MonoBehaviour
{
    [SerializeField] private Vector3 axis = Vector3.forward;
    [SerializeField] private AutoTween tween;
    [SerializeField] private bool repeat = true, tryKeepStartPos = false;

    private Quaternion startRot;
    private Vector3 startPos;
    
    private void Awake()
    {
        startRot = transform.localRotation;
        startPos = transform.localPosition;
    }

    private void OnEnable()
    {
        WaitUtils.Wait(Time.deltaTime, false,
            () => {
                transform.localRotation = startRot;
                if (tryKeepStartPos) transform.localPosition = startPos;
        
                var rotationTween = tween.Play(transform.LeanRotateAroundLocal(axis, -360, tween.time));
                if (repeat) rotationTween.setLoopClamp();
            });
    }

    private void OnDisable()
    {
        tween.Cancel();
    }
}