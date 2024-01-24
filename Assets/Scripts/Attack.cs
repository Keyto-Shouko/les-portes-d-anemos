using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private int _damage = 5;
    // get the parent gameobject
    private GameObject _parent;

    private void Awake()
    {
        _parent = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<HealthManager>() != null && collider.gameObject != _parent){
            HealthManager healthManager = collider.GetComponent<HealthManager>();
            healthManager.GetHealthSystem().Damage(10);
        }
            
    }
}
