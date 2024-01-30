using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthSystem {
    

    public event EventHandler OnHealthChanged;
    private int _health;
    private int _maxHealth;

    private GameObject gameObject; // Reference to the GameObject

    public HealthSystem(int _maxHealth, GameObject gameObject) {
        this._maxHealth = _maxHealth;
        _health = _maxHealth;
        this.gameObject = gameObject;
    }

    public int GetHealth() {
        return _health;
    }

    public float GetHealthPercent() {
        return (float)_health / _maxHealth;
    }

    public void Damage(int damageAmount) {
        _health -= damageAmount;
        if (_health <= 0) {
            _health = 0;
            Die();
        }
        if(OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Heal(int healAmount) {
        _health += healAmount;
        if (_health > _maxHealth) {
            _health = _maxHealth;
        }
        if(OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    void Die() {
        // check if the object has a lootbag
        LootBag lootBag = gameObject.GetComponent<LootBag>();
        if (lootBag != null){
            lootBag.GetComponent<LootBag>().InstantiateLoot(gameObject.transform.position);
        }
        GameObject.Destroy(gameObject);
    }
}
