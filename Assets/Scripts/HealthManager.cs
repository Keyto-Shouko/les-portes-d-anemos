using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public Transform pfHealthBar;
    public Vector2 offset;

    private HealthSystem healthSystem;
    private HealthBar healthBar; // Store a reference to the health bar

    // getter for healthSystem
    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }


    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(100);

        // Add each component separately
        Vector2 finalPosition = new Vector2(gameObject.transform.position.x + offset.x, gameObject.transform.position.y + offset.y);

        Transform healthBarTransform = Instantiate(pfHealthBar, finalPosition, Quaternion.identity, transform);
        healthBar = healthBarTransform.GetComponent<HealthBar>();

        if (healthBar == null) Debug.LogError("No health bar found!");

        // Pass the existing health system to the health bar setup
        healthBar.Setup(healthSystem);
    }
}
