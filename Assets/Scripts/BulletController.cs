using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private GameObject _player;
    private Rigidbody2D _rigidbody2D;

    //get a reference to the GameObject that instantiated the bullet
    private GameObject _shooter;
    private float _speed;
    private int _damage;
    private float timer;
    // Start is called before the first frame update

    void Awake(){
        
    }
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");

        Vector2 direction = _player.transform.position - transform.position;
        _rigidbody2D.velocity = new Vector2(direction.x, direction.y).normalized * _speed;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot + 180);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5f){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //check if the bullet enters a sound area or the shooter hitbox
        if(other.gameObject != _shooter && other.tag != "Area"){
            Destroy(gameObject);
        }
        // damage the entity
        var entityHealthSystem = other.gameObject.GetComponent<HealthManager>().GetHealthSystem();
        if(entityHealthSystem != null && other.gameObject != _shooter){
            other.gameObject.GetComponent<HealthManager>().GetHealthSystem().Damage(_damage);
            Destroy(gameObject);
        }
    }

    public void SetupSpeed(float speed){
        _speed = speed;
    }

    public void SetupDamage(int damage){
        _damage = damage;
    }

    public void SetupShooter(GameObject shooter){
        _shooter = shooter;
    }
}