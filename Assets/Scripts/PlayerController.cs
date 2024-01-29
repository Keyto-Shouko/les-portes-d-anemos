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

    private bool _isSlashing = false;
    private bool _isDashing = false;
    private SpriteRenderer _playerRenderer;
    //Reference to the player manager

    private bool _isInTPArea = false;

    private bool _canCollect = false;
    private bool _canDash = true;
    private float _dashCooldown = 6f;
    private float _dashSpeed = 10f;

    private bool _canAttack = true;
    public float _attackCooldown = 0.6f;
    private PlayerManager _playerManager;

    private AudioSource _audioSource;

    [SerializeField]
    private List<AudioClipEntry> _audioClipEntries;

    private Dictionary<string, AudioClip> _audioClipDictionary;

    [System.Serializable]
    public class AudioClipEntry
    {
        public string clipName;
        public AudioClip clip;
    }

    [SerializeField]
    private InventorySO _inventoryData;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        // Initialize the dictionary from the list
        _audioClipDictionary = new Dictionary<string, AudioClip>();
        foreach (var entry in _audioClipEntries)
        {
            _audioClipDictionary[entry.clipName] = entry.clip;
        }
        
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
        Move();
    }

    void ProcessInputs()
    {
        // Get the input from the keyboard
        if(_isDashing == false && _isSlashing == false){
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            // Normalize the input vector
             _movement = new Vector2(horizontal, vertical).normalized;
        }
        
        //if we are not moving anymore, we need to update the last move direction
        if (_movement != Vector2.zero)
        {
            _lastMoveDirection = _movement;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _canDash && _isSlashing == false)
        {
            Debug.Log("Dash");
            StartCoroutine(Dash(_lastMoveDirection));

        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && _canAttack && _lastMoveDirection != Vector2.zero && _isDashing == false && _isSlashing == false)
        {
            StartCoroutine(Attack());
        }

        //if the player is in the teleporter area and press F, we need to send an event to the teleporter event manager
        if (_isInTPArea && Input.GetKeyDown(KeyCode.F))
        {
            // Make the UIManager open the teleporter list
            UIManager.Instance.ToggleTeleporterList();
        }
        

    }
    void Move()
    {
        _rigidbody2D.velocity = _movement * speed;
    }
    void Animate(){
        // Update the animator parameters
        _animator.SetFloat("Horizontal", _lastMoveDirection.x);
        _animator.SetFloat("Vertical", _lastMoveDirection.y);
        _animator.SetFloat("Speed", _movement.sqrMagnitude);
        _animator.SetFloat("LastMoveHorizontal", _lastMoveDirection.x);
        _animator.SetFloat("LastMoveVertical", _lastMoveDirection.y);
        _animator.SetBool("IsSlashing", _isSlashing);
        _animator.SetBool("IsDashing", _isDashing);
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
                int reminder = _inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if(reminder == 0){
                    item.DestroyItem();
                }
                else{
                    item.Quantity = reminder;
                }
           }
        }
        //detect if player enters the "spawnArea" and play the sound
        else if(other.gameObject.CompareTag("Area")){
            PlayAudioClip(other.gameObject.name);
        }

        //detect if player enters the slime hitbox and damage the player
        else if(other.gameObject.CompareTag("Slime")){
            HealthManager healthManager = GetComponent<HealthManager>();
            if(healthManager != null){
                healthManager.GetHealthSystem().Damage(10);
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

    void PlayAudioClip(string clipName)
    {
        if (_audioSource != null && _audioClipDictionary.ContainsKey(clipName))
        {
            AudioClip clip = _audioClipDictionary[clipName];
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }

    IEnumerator Dash(Vector2 direction)
    {
        _canDash = false;
        _isDashing = true;

        float elapsedTime = 0f;
        float duration = 0.2f; // Adjust the duration of the dash
        float startSpeed = speed;
        float endSpeed = _dashSpeed;

        while (elapsedTime < duration)
        {
            // Interpolate the speed gradually
            speed = Mathf.Lerp(startSpeed, endSpeed, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the speed reaches the final value exactly at the end
        speed = endSpeed;

        yield return new WaitForSeconds(0.40f);

        // Reset velocity after dash
        speed = startSpeed;

        _isDashing = false;

        yield return new WaitForSeconds(_dashCooldown);

        _canDash = true;
    }

    IEnumerator Attack()
    {
        //reduce speed to 0
        var oldSpeed = speed;
        speed = 0f;
        _isSlashing = true;
        // Play the attack animation
        yield return new WaitForSeconds(0.5f);
        _isSlashing = false;
        speed = oldSpeed;
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
        
    }
}
