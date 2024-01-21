using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    private HealthSystem healthSystem;

    private Transform bar;

    private void Awake() {
        bar = transform.Find("Bar");
        if(bar == null) Debug.LogError("No bar found !");
    }

    public void Setup(HealthSystem healthSystem) {
        this.healthSystem = healthSystem;
        if(healthSystem == null) Debug.LogError("HealthSystem is null");
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e) {
        Debug.Log("Health changed !");
        bar.localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }
}