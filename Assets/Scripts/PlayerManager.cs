//this file is used to manage the player's health, position etc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    // Singleton instance
    public static PlayerManager instance;
    public GameObject playerGameObject; // Reference to the player GameObject
    //we need to listen to the teleporter event
    // Player data
    //public Inventory playerInventory;
    //public SkillTree playerSkillTree;

    private void Awake()
    {
        // Ensure only one instance of PlayerManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
         // Listen to the TeleporterEventManager
        TeleporterEventManager.instance.onTeleporterDiscovered.AddListener(HandleTeleporterDiscover);    
    }

    public Vector3 GetCurrentPosition()
    {
        // Get the current position of the GameObject
        Debug.Log("current position saved : " + playerGameObject.transform.position);
        return playerGameObject.transform.position;
    }

    public void SetCurrentPosition(Vector3 newPosition)
    {
        // Set the current position of the GameObject
        Debug.Log("current position loaded : " + newPosition); 
        playerGameObject.transform.position = newPosition;
    }

    private void HandleTeleporterDiscover(Teleporter discoveredTeleporter)
    {
        // Do something when the teleporter is discovered
        Debug.Log("Teleporter discovered: " + discoveredTeleporter.name);    
    }
}