using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private FirstPersonController player;

    private Vector3 startPos;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponentInChildren<FirstPersonController>();
        startPos = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player.transform)
        {
            player.TeleportPlayer(startPos, Quaternion.identity);
            player.GetComponentInParent<PlayerSizeManager>().Reset();
        }
    }
}
