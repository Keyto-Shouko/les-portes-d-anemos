using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    //public ScoreManager scoreManager { get; private set; }
    //public RupeeManager rupeeManager { get; private set; }

    public TimeManager timeManager { get; private set; }
    //public LightManager lightManager { get; private set; }

    //public UIManager uIManager { get; private set; }

    //public AudioManager audioManager { get; private set; }

    private bool _isPaused = false;
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //scoreManager = GetComponent<ScoreManager>();
        //rupeeManager = GetComponent<RupeeManager>();
        timeManager = GetComponent<TimeManager>();
        //GetTimeElapsed();
        //lightManager = GetComponent<LightManager>();
        //uIManager = GetComponent<UIManager>();
        //audioManager = GetComponent<AudioManager>();
    }

    private void Start()
    {
        //timeManager.OnTimeUp += TimeUpHandler;
    }

    private void TimeUpHandler(){
        //StopGame();
    }

    public void StartGame(){
        if(!_isPaused){
            //rupeeManager.StartSpawning();
            //scoreManager.Reset();
            timeManager.StartGame();
            //uIManager.StartGame();
            //audioManager.StartMusic();
            Time.timeScale = 1;
        }
        if(_isPaused){
            ResumeGame();
        }
    }

    public void StopGame(){
        //rupeeManager.StopSpawning();
        //rupeeManager.ClearRuppees();
        timeManager.StopGame();
        //uIManager.StopGame();
        //audioManager.StopMusic();
        Time.timeScale = 0;
    }

    public void PauseGame(){
        _isPaused = true;
        Time.timeScale = 0;
        //rupeeManager.StopSpawning();
        timeManager.StopGame();
        //uIManager.StopGame();
        //audioManager.PauseMusic();

    }

    public void ResumeGame(){
        _isPaused = false;
        Time.timeScale = 1;
        //rupeeManager.StartSpawning();
        timeManager.ResumeGame();
        //uIManager.StartGame();
        //audioManager.ResumeMusic();
    }

    //when the player press escape, pause the game
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !_isPaused){
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPaused){
            ResumeGame();
        }


    }

    public float GetTimeElapsed()
    {
        // Retrieve timeElapsed from TimeManager
        return timeManager.GetTimeElapsed();
    }
}
