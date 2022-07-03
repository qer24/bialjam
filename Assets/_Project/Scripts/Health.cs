using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth = 0;
    [SerializeField] private EventReference deathSfx;

    public event Action<int> OnHealthChanged;
    public UnityEvent OnSpawn;
    public UnityEvent OnDeath;

    public bool isDead;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);

        isDead = false;
        
        OnSpawn?.Invoke();
    }

    public virtual void RemoveHealth(int amount)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);
        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0) Death();
    }

    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        OnHealthChanged?.Invoke(currentHealth);
    }

    private void Death()
    {
        if (isDead) return;

        isDead = true;
        OnDeath?.Invoke();
        
        deathSfx.Play(transform.position);
    }
}