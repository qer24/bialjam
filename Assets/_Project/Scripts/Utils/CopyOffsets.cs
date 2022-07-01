using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CopyOffsets : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float scaleX = 3f, scaleY = 3f, smoothing = 0.25f;

    private CinemachineVirtualCamera vcam;
    private Cinemachine3rdPersonFollow follow;
    
    private Vector3 targetStartPos;

    private void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        follow = vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        targetStartPos = target.localPosition;
    }

    private void LateUpdate()
    {
        var offset = target.localPosition - targetStartPos;
        offset.x *= scaleX;
        offset.y *= scaleY;
        follow.ShoulderOffset = Vector3.MoveTowards(follow.ShoulderOffset, offset, Time.deltaTime * smoothing);
    }
}