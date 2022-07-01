using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth = 0;

    public event Action<int> OnHealthChanged;
    public UnityEvent OnDeath;
    
    public bool isDead;

    public virtual void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);

        isDead = false;
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
    }
}
