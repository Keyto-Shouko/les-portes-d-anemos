using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthSystem {
    

    public event EventHandler OnHealthChanged;
    private int health;
    private int maxHealth;

    public HealthSystem(int maxHealth) {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public int GetHealth() {
        return health;
    }

    public float GetHealthPercent() {
        return (float)health / maxHealth;
    }

    public void Damage(int damageAmount) {
        health -= damageAmount;
        if (health < 0) {
            health = 0;
        }
        if(OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        Debug.Log("Damaged ! Health is " + health);
    }

    public void Heal(int healAmount) {
        health += healAmount;
        if (health > maxHealth) {
            health = maxHealth;
        }
        if(OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        Debug.Log("Healed ! Health is " + health);
    }
}
