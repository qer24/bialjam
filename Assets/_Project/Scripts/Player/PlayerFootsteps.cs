using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerFootsteps : MonoBehaviour
{
    [FormerlySerializedAs("controller")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private FirstPersonController fpsController;
    [SerializeField] private float timeToStep = 0.5f;
    
    [Space]
    [SerializeField] private EventReference footstepEvent;
    [SerializeField] private EventReference jumpEvent;
    [SerializeField] private EventReference landEvent;

    private float footstepTimer;

    private void Awake()
    {
        if (!jumpEvent.IsNull) fpsController.OnJump += () => jumpEvent.Play();
        if (!landEvent.IsNull) fpsController.OnLand += () => landEvent.Play();
    }

    private void Update()
    {
        if (!fpsController.gameObject.activeSelf) return;
        
        if (characterController.velocity.magnitude > 0.1f && characterController.isGrounded)
        {
            footstepTimer += Time.deltaTime;
        }
        else
        {
            footstepTimer = 0;
        }

        if (footstepTimer >= timeToStep)
        {
            footstepTimer = 0;
            footstepEvent.Play();
        }
    }
}