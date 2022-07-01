using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardPlayerSizeOnDeath : MonoBehaviour
{
    [SerializeField] private float sizeAward = 0.25f;

    private PlayerSizeManager sizeManager;
    
    private void Awake()
    {
        sizeManager = GameObject.FindWithTag("Player").GetComponent<PlayerSizeManager>();
    }

    public void AwardSize() => sizeManager.UpdateSize(sizeAward);
}