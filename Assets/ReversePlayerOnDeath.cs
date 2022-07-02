using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class ReversePlayerOnDeath : MonoBehaviour
{
    private FirstPersonController playerController;
    
    private void Awake()
    {
        playerController = GameObject.FindWithTag("Player").GetComponentInChildren<FirstPersonController>();
    }

    public void AwardReverse() => playerController.reverseMovement *= -1;
}