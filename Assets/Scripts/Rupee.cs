using System;
using UnityEngine;

public class Rupee : MonoBehaviour
{
    public event Action<Rupee> OnCollected;
    public int score = 1;
    private RupeeData _data;
    private SpriteRenderer _spriteRenderer;
    private AudioClip _collectedClip;

    //add a light field to the rupee
    public Light light;
    //add a public particle system
    public ParticleSystem particleSystem;

    public RupeeData Data{
        get => _data;
        set{
            _data = value;
            score = _data.score;
            _spriteRenderer.color = _data.color;
            _collectedClip = _data.collectClip;
            //set the light color to the rupee color
            light.color = _data.color;
            //set the particle system color to the rupee color
            var main = particleSystem.main;
            main.startColor = _data.color;
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            //Destroy(gameObject);
            OnCollected?.Invoke(this);
        }
    }


    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
