using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEController : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D _rigidbody2D;

    //get a reference to the GameObject that instantiated the bullet
    private GameObject _caster;
    private float _speed;
    private int _damage;
    private float _timer;

    private float _windup;
    // Start is called before the first frame update

    void Awake(){
        
    }
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");

        // get the player position at first
        transform.position = _player.transform.position;
        _timer = _windup;
        StartCoroutine(ActivateColliderAfterWindup());
    }

    void Update()
    {
        // Track the player's position
        
        _timer -= Time.deltaTime;
        if(_timer >= 0){
            transform.position = _player.transform.position;
        }
    }

    IEnumerator ActivateColliderAfterWindup()
    {
        // Wait for the windup duration
        yield return new WaitForSeconds(_windup+0.3f);

        // Activate the circle collider after the windup
        GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //check if the bullet GameObject hasn't already been destroyed
        if(gameObject == null){
            return;
        }
        //check if the bullet enters a sound area or the shooter hitbox
        if(_caster != null && other.gameObject != _caster && other.tag != "Area"){
            Destroy(gameObject);
        }
        // Damage the entity
        var entityHealthManager = other.gameObject.GetComponent<HealthManager>();
        if (entityHealthManager != null && other.gameObject != _caster) {
            HealthSystem entityHealthSystem = entityHealthManager.GetHealthSystem();
            if (entityHealthSystem != null) {
                entityHealthSystem.Damage(_damage);
                Destroy(gameObject);
            }
        }
    }
    public void SetupCaster(GameObject caster)
    {
        _caster = caster;
    }

    public void SetupSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetupDamage(int damage)
    {
        _damage = damage;
    }

    public void SetupWindup(float windup)
    {
        _windup = windup;
    }

    public void SetupCollisionLayer(int layer){
        gameObject.layer = layer;
    }

}
