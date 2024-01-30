using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    // detect if the player is in the trigger
    // then setActive to true the boss (it's the child of this object)

    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject == _player){
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

}
