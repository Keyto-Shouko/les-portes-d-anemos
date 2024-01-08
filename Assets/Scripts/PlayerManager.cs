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
    //we need a list of discovered teleporters
    public List<Teleporter> discoveredTeleporterList = new List<Teleporter>();
    public event Action<Teleporter> OnAddTeleporterToUIList;

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
        return playerGameObject.transform.position;
    }

    public void SetCurrentPosition(Vector3 newPosition)
    {
        // Set the current position of the GameObject
        playerGameObject.transform.position = newPosition;
    }

    private void HandleTeleporterDiscover(Teleporter discoveredTeleporter)
    {
        // Do something when the teleporter is discovered
        // add the discovered teleporter to the list only if it's not already in the list
        if(!discoveredTeleporterList.Contains(discoveredTeleporter)){
            Debug.Log("A new teleporter has been discovered!");
            //create a new teleporter with the arguments
            discoveredTeleporterList.Add(discoveredTeleporter);
            OnAddTeleporterToUIList?.Invoke(discoveredTeleporter);
        }
    }

    //We need a function to get and set the list of discovered teleporters so we can send it to the GameManager to save it
    public List<Teleporter> GetDiscoveredTeleporterList(){
        return discoveredTeleporterList;
    }

    public void SetDiscoveredTeleporterList(List<Teleporter> discoveredTeleporterList){
        this.discoveredTeleporterList = discoveredTeleporterList;
    }

    public void TeleportToTeleporter(string teleporterName){
        //we need to find the teleporter in the list of discovered teleporters
        //we need to get the position of the teleporter
        //we need to set the player's position to the teleporter's position
        Teleporter teleporterToTeleportTo = discoveredTeleporterList.Find(teleporter => teleporter.name == teleporterName);
        Vector3 teleporterPosition = teleporterToTeleportTo.GetPosition();
        SetCurrentPosition(teleporterPosition);
    }
}