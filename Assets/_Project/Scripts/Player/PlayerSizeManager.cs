using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class PlayerSizeManager : MonoBehaviour
{
    [SerializeField] private Transform playerCapsule;
    [SerializeField] private float sizeLerpSpeed;

    [Space]
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private Vector2 minMaxSpeed;

    [Space] 
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private Vector2 minMaxFov;

    public float currentSize = 1f;
    private float currentFov;

    private void Update()
    {
        playerCapsule.localScale = Vector3.Lerp(playerCapsule.localScale, currentSize.ToVector3(), sizeLerpSpeed * Time.deltaTime);
        playerController.MoveSpeed = Mathf.Lerp(minMaxSpeed.y, minMaxSpeed.x, currentSize);

        currentFov = Mathf.Lerp(minMaxFov.y, minMaxFov.x, currentSize);
        vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, currentFov, sizeLerpSpeed * Time.deltaTime);
    }
}
