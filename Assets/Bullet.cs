using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;

    private TrailRenderer rend;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        rend.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FirstPersonController>())
        {
            RestartManager.instance.Restart();
        }
    }
}