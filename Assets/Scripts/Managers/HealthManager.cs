using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public Transform pfHealthBar;
    public Vector2 offset;

    // Public field for the health so i can adjust it in editor
    public int health = 100;
    private HealthSystem _healthSystem;
    private HealthBar _healthBar; // Store a reference to the health bar

    // getter for _healthSystem
    public HealthSystem GetHealthSystem()
    {
        return _healthSystem;
    }


    // Start is called before the first frame update
    void Start()
    {
        _healthSystem = new HealthSystem(health, gameObject);

        // Add each component separately
        Vector2 finalPosition = new Vector2(gameObject.transform.position.x + offset.x, gameObject.transform.position.y + offset.y);

        Transform healthBarTransform = Instantiate(pfHealthBar, finalPosition, Quaternion.identity, transform);
        _healthBar = healthBarTransform.GetComponent<HealthBar>();

        if (_healthBar == null) Debug.LogError("No health bar found!");

        // Pass the existing health system to the health bar setup
        _healthBar.Setup(_healthSystem);
    }
}
