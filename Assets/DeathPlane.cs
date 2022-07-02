using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathPlane : MonoBehaviour
{
    private FirstPersonController player;

    private Vector3 startPos;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponentInChildren<FirstPersonController>();
        startPos = player.transform.position;
    }

    private void Update()
    {
        if (!EnemyManager.instance.levelFinished && Keyboard.current.rKey.wasPressedThisFrame)
        {
            Reset();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player.transform)
        {
            Reset();
        }
    }

    private void Reset()
    {
        RestartManager.instance.Restart();
    }
}