using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RupeeManager : MonoBehaviour
{

    public Transform container;
    public Transform spawner;
    public Rupee rupeePrefab;
    public AudioSource collectedAudioSource;
    private List<Rupee> _rupees = new List<Rupee>();
    public List<RupeeData> rupeeData;
    public float delay = 0.2f;
    private Coroutine _spawnCoroutine;
    //create an instance of the game manager
    private GameManager _gameManager;


    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void Spawn(){
        var data = rupeeData[Random.Range(0, rupeeData.Count)];
        Rupee rupee = Instantiate(rupeePrefab, spawner.position, Quaternion.identity, container);
        //listen to the event OnCollected from the rupee object
        rupee.OnCollected += RupeeCollectedHandler;
        rupee.Data = data;
        _rupees.Add(rupee);
    }

    private void RupeeCollectedHandler(Rupee rupee){
        collectedAudioSource.clip = rupee.Data.collectClip;
        collectedAudioSource.Play();
        RemoveRupee(rupee);
        //call the instance of the score manager and add the score of the collected rupee
        _gameManager.scoreManager.AddScore(rupee.score);
        
    }

    public void StartSpawning(){
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    public void StopSpawning(){
        if(_spawnCoroutine != null){
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnCoroutine(){
        Spawn();
        yield return new WaitForSeconds(delay);
        StartSpawning();
    }

    private void RemoveRupee(Rupee rupee){
        _rupees.Remove(rupee);
        rupee.OnCollected -= RupeeCollectedHandler;
        Destroy(rupee.gameObject);
    }


    public void ClearRuppees(){
        StopSpawning();
        for (var i = _rupees.Count - 1; i >= 0; i--)
        {
            RemoveRupee(_rupees[i]);
        }
        _rupees.Clear();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
