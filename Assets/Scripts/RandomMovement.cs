using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{

    private Rigidbody2D _rigidbody2D;
    public float speed = 9f;

    private void Awake(){
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
        
    }
}
