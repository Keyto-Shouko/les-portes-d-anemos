using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Teleporter : MonoBehaviour
{

    public event Action<Teleporter> OnDiscover;

    public AudioClip activationClip;
    public string name;
    public string description;
    private Vector2 position;
    
    public Teleporter(string name, string description, float positionX, float positionY)
    {
        this.name = name;
        this.description = description;
        this.position = new Vector2(positionX, positionY);
    }
    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SerializableTeleporter ToSerializableTeleporter()
    {
        return new SerializableTeleporter(name, description, position.x, position.y);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            TeleporterEventManager.instance.DiscoverTeleporter(name, description, position.x, position.y);
        }
    }

    //acces the position of the teleporter
    public Vector2 GetPosition(){
        return position;
    }
}
