using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovementIA : MonoBehaviour
{

    private Rigidbody2D _rigidbody2D;
    private Vector3 _spawnPoint;
    public float maxDistance = 5f;
    public float intervalOfMovement = 5f;
    public float speed = 4f;
    private GameManager _gameManager;
    private float _timeElapsed;

    private void Awake(){
        _gameManager = GameManager.Instance;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spawnPoint = transform.position;
        if(_gameManager == null){
            Debug.Log("gameManager in RandomMovementIA is null");
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        var movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        _rigidbody2D.velocity = movement * speed;
    }
    
    // Update is called once per frame
    void Update()
    {
        _timeElapsed = _gameManager.timeManager.GetTimeElapsed();
        //cast _timeElapsed to int to make it easier to compare
        //the object will move every time intervalOfMovement seconds
        //the object has a square area of _spawnPoint +- maxDistance on both axis
        //if the object's next position is outside of the square area, make it move to the opposite direction
        //make the object move only ONCE every intervalOfMovement seconds
        if(_timeElapsed % intervalOfMovement <= 0.1f){
            Debug.Log(_timeElapsed);
            var movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            _rigidbody2D.velocity = movement * speed;
        }
    }
}
