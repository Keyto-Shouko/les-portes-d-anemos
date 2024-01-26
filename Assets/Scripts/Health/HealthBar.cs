using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem _healthSystem;

    private Transform _bar;

    private void Awake()
    {
        _bar = transform.Find("Bar");
        if (_bar == null) Debug.LogError("No _bar found !");
    }

    public void Setup(HealthSystem _healthSystem)
    {
        this._healthSystem = _healthSystem;
        if (_healthSystem == null) Debug.LogError("HealthSystem is null");
        
        // Only subscribe to the event once in the Setup method
        _healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        _bar.localScale = new Vector3(_healthSystem.GetHealthPercent(), 1);
    }
}
