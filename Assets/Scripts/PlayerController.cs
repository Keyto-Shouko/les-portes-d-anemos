using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 _movement;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    [Range(5f, 20f)]
    public float speed = 10f;
    private Vector2 _lastMoveDirection;
    private SpriteRenderer _playerRenderer;
    //Reference to the player manager

    private bool _isInTPArea = false;

    private bool _canCollect = false;
    private PlayerManager _playerManager;

    [SerializeField]
    private InventorySO inventoryData;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        
        // Debug log to check if _playerManager is assigned
    }
    // Start is called before the first frame update
    void Start()
    {
        //reference the PlayerManager, it's present in the gameManager
        _playerManager = GameManager.Instance.playerManager;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Animate();
        // Get the player's current position and send it to _playerManager
        //Vector3 currentPosition = transform.position;
        //_playerManager.SetCurrentPosition(currentPosition);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _movement * speed;
        

    }

    void ProcessInputs()
    {
        // Get the input from the keyboard
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        // Normalize the input vector
        _movement = new Vector2(horizontal, vertical).normalized;
        //if we are not moving anymore, we need to update the last move direction
        if (_movement != Vector2.zero)
        {
            _lastMoveDirection = _movement;
        }

        //if the player is in the teleporter area and press F, we need to send an event to the teleporter event manager
        if (_isInTPArea && Input.GetKeyDown(KeyCode.F))
        {
            // Make the UIManager open the teleporter list
            UIManager.Instance.ToggleTeleporterList();
        }

    }

    void Animate(){
        // Update the animator parameters
        _animator.SetFloat("Horizontal", _lastMoveDirection.x);
        _animator.SetFloat("Vertical", _lastMoveDirection.y);
        _animator.SetFloat("Speed", _movement.sqrMagnitude);
        _animator.SetFloat("LastMoveHorizontal", _lastMoveDirection.x);
        _animator.SetFloat("LastMoveVertical", _lastMoveDirection.y);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LayerChanger"))
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
        //When the player enters the teleporter area, if he presses the F key,
        else if(other.gameObject.CompareTag("Teleporters")){
            _isInTPArea = true;
        }

        else if(other.gameObject.CompareTag("Collectibles")){
           Item item = other.GetComponent<Item>();
           if(item != null){
                int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if(reminder == 0){
                    item.DestroyItem();
                }
                else{
                    item.Quantity = reminder;
                }
           }
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Teleporters"))
        {
            _isInTPArea = false;
            UIManager.Instance.CloseTeleporterList();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        /*if(other.gameObject.CompareTag("Collectible") && KeyDown(KeyCode.F)){
            //_playerManager.CollectItem(other.gameObject);
        }*/
    }
    void ChangePlayerLayer(GameObject player, string layerName, string sortingLayerName = "Default")
    {
        player.layer = LayerMask.NameToLayer(layerName);
        // Update the sorting layer of the sprite renderer (assuming your player has a SpriteRenderer component)
        if (_playerRenderer != null){
            _playerRenderer.sortingLayerName = sortingLayerName;
        }
    }

}
