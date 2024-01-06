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
}