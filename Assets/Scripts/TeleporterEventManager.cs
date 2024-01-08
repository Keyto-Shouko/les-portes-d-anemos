using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class TeleporterEventManager : MonoBehaviour
{
    public UnityEvent<Teleporter> onTeleporterDiscovered;

    public UnityEvent<List<Teleporter>> onTeleporterListOpen;

    public static TeleporterEventManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Function to call when a teleporter is discovered
    public void DiscoverTeleporter(string name, string description, float x, float y)
    {
        //create a new teleporter with the arguments
        Teleporter discoveredTeleporter = new Teleporter(name, description, x, y);
        onTeleporterDiscovered.Invoke(discoveredTeleporter);
    }

    // Function to call when a player wants to open the teleporter list UI
    public void AddTeleporterToUIList(List<Teleporter> discoveredTeleporterList)
    {
        onTeleporterListOpen.Invoke(discoveredTeleporterList);
    }
}
