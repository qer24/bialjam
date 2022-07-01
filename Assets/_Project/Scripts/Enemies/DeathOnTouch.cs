using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Lean.Pool;
using UnityEngine;

public class DeathOnTouch : MonoBehaviour
{
    [SerializeField] private Health enemyHealth;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private float autoDeathTime = 1f;
    [SerializeField] private EventReference particlesPop;

    private float deathTimer;
    
    private void Update()
    {
        if (!enemyHealth.isDead) return;

        deathTimer += Time.deltaTime;

        if (deathTimer > autoDeathTime)
        {
            OnDeath();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!enemyHealth.isDead) return;

        OnDeath();
    }

    private void OnDeath()
    {
        var particles = LeanPool.Spawn(deathParticles, transform.position, Quaternion.identity);
        LeanPool.Despawn(particles, 3f);

        particlesPop.Play(transform.position);
        
        Destroy(transform.parent.gameObject);
    }
}