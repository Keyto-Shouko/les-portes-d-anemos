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

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            TeleporterEventManager.instance.DiscoverTeleporter(this);
        }
    }

    //acces the position of the teleporter
    public Vector2 GetPosition(){
        return position;
    }
}
