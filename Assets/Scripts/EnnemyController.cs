using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyController : MonoBehaviour {

    private Animator _animator;

    private GameObject _player;

    private AudioSource _audioSource;
    [SerializeField]
    private List<AttackEntry> _attackEntries;

    private Dictionary<string, Attack> _attackDictionary;

    [System.Serializable]
    public class AttackEntry {
        public string attackName;
        public Attack attackData;
    }

    [System.Serializable]
    public class Attack {
        public string name;
        public bool projectile;
        public GameObject projectilePrefab;
        public AudioClip audioClip;
        public float projectileSpeed;
        public bool AoE;
        public int damage;
        public bool selfCast;
        public int heal;
        public float windup;
    }

    void Start() {
        _attackDictionary = new Dictionary<string, Attack>();
        foreach (var entry in _attackEntries) {
            _attackDictionary[entry.attackName] = entry.attackData;
        }

        _animator = GetComponent<Animator>();
        // audiosource
        _audioSource = GetComponent<AudioSource>();
    }

    // Example function to trigger attacks
    public void TriggerAttack(string attackName) {
        if (_attackDictionary.ContainsKey(attackName)) {
            Attack attack = _attackDictionary[attackName];
            // Implement your logic to trigger the attack based on its properties
            Debug.Log("Triggering Attack: " + attack.name);
            Debug.Log("Projectile: " + attack.projectile);
            Debug.Log("AoE: " + attack.AoE);
            Debug.Log("Damage: " + attack.damage);
            Debug.Log("Self Cast: " + attack.selfCast);
            Debug.Log("Heal: " + attack.heal);
            Debug.Log("Windup: " + attack.windup);
        } else {
            Debug.LogError("Attack with name '" + attackName + "' not found!");
        }
    }

    public void Judgement() {
        Debug.Log("Judgement !");
    }

    IEnumerator LifeSentence() {
        Debug.Log("Life Sentence !");
        // play the sound on the audio source of the ennemy
        AudioClip clip = _attackDictionary["Life Sentence"].audioClip;
        _audioSource.clip = clip;
        _audioSource.Play();
        //wait for the windup then instantiate the bullet
        yield return new WaitForSeconds(_attackDictionary["Life Sentence"].windup);
        var bullet = Instantiate(_attackDictionary["Life Sentence"].projectilePrefab, transform.position, Quaternion.identity);
        // call the SetupSpeed function on the bullet
        bullet.GetComponent<BulletController>().SetupSpeed(_attackDictionary["Life Sentence"].projectileSpeed);
        bullet.GetComponent<BulletController>().SetupDamage(_attackDictionary["Life Sentence"].damage);
        bullet.GetComponent<BulletController>().SetupShooter(gameObject);

        
    }
}