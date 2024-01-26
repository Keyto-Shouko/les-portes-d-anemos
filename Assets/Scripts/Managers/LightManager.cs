/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    // we will need to call the game manager to access the time manager as we need to pass it to the worldLight script 
    private GameManager _gameManager;
    private WorldLight _worldLight;
    // we need to set timeElapsed so the update function can refresh it
    public float _timeElapsed;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _worldLight = GetComponent<WorldLight>();
        _timeElapsed = _gameManager.timeManager.TimeElapsed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //refresh the _timeElapsed that comes from the time manager
        _timeElapsed = _gameManager.GetTimeElapsed();
        //send the _timeElapsed to the worldLight script
        _worldLight.SetTimeElapsed(_timeElapsed);
    }
}
*/