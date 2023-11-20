using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Vector2 _movement;
    private Rigidbody2D _rigidbody2D;
    //private Animator _animator;

    [Range(5f, 20f)]
    public float speed = 10f;
    private SpriteRenderer playerRenderer;


    private void Awake()
    {
        Debug.Log("awake");
        _rigidbody2D = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        //_animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        _movement = new Vector2(horizontal, vertical).normalized;
        //_animator.SetFloat("Horizontal", horizontal);
        //_animator.SetFloat("Vertical", vertical);
        //_animator.SetFloat("Speed", _movement.sqrMagnitude);
        //Debug.Log(_movement);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _movement * speed;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LayerChanger"))
        {
        {
            string currentLayer = LayerMask.LayerToName(gameObject.layer);
            // Change the layer based on the conditions and movement direction
            if (currentLayer == "Ground" && _movement.y > 0)
            {
                ChangePlayerLayer(gameObject, "Upstair-1", "Elevation+1");
            }
            else if (currentLayer == "Upstair-1" && _movement.y > 0)
            {
                ChangePlayerLayer(gameObject, "Upstair-2", "Elevation+2");
            }
            else if (currentLayer == "Upstair-2" && _movement.y < 0)
            {
                ChangePlayerLayer(gameObject, "Upstair-1", "Elevation+1");
            }
            else if (currentLayer == "Upstair-1" && _movement.y < 0)
            {
                ChangePlayerLayer(gameObject, "Ground", "Elevation-0");
            }
            else if (currentLayer == "Ground" && _movement.y < 0)
            {
                ChangePlayerLayer(gameObject, "Cave-1", "Elevation-1");
            }
            else if (currentLayer == "Cave-1" && _movement.y < 0)
            {
                ChangePlayerLayer(gameObject, "Cave-2", "Elevation-2");
            }
            else if (currentLayer == "Cave-2" && _movement.y > 0)
            {
                ChangePlayerLayer(gameObject, "Cave-1", "Elevation-1");
            }
            else if (currentLayer == "Cave-1" && _movement.y > 0)
            {
                ChangePlayerLayer(gameObject, "Ground", "Elevation-0");
            }
            else
            {
                // Default case: Assuming the default case is if the player is on "Upstair-1"
                Debug.Log("Default case");
                ChangePlayerLayer(gameObject, "Ground");
            }
        }
        }
    }
    void ChangePlayerLayer(GameObject player, string layerName, string sortingLayerName = "Default")
    {
        player.layer = LayerMask.NameToLayer(layerName);
        // Update the sorting layer of the sprite renderer (assuming your player has a SpriteRenderer component)
        if (playerRenderer != null){
            playerRenderer.sortingLayerName = sortingLayerName;
        }
    }

}
