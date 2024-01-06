using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    //public ScoreManager scoreManager { get; private set; }
    //public RupeeManager rupeeManager { get; private set; }

    public TimeManager timeManager { get; private set; }
    //public LightManager lightManager { get; private set; }

    public UIManager UIManagerComponent { get; private set; }

    //public AudioManager audioManager { get; private set; }

    // Reference to the player manager
    public PlayerManager playerManager { get; private set; }
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
        UIManagerComponent = GetComponent<UIManager>();
        //audioManager = GetComponent<AudioManager>();
        playerManager = GetComponent<PlayerManager>();
        StartGame();
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
            UIManagerComponent.StartGame();
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
        UIManagerComponent.StopGame();
        //audioManager.StopMusic();
        Time.timeScale = 0;
    }

    public void PauseGame(){
        _isPaused = true;
        Time.timeScale = 0;
        //rupeeManager.StopSpawning();
        timeManager.StopGame();
        UIManagerComponent.StopGame();
        //audioManager.PauseMusic();

    }

    public void ResumeGame(){
        _isPaused = false;
        Time.timeScale = 1;
        //rupeeManager.StartSpawning();
        timeManager.ResumeGame();
        UIManagerComponent.StartGame();
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

        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L)){
            LoadGame();
        }
    }

    public float GetTimeElapsed()
    {
        // Retrieve timeElapsed from TimeManager
        return timeManager.GetTimeElapsed();
    }

    public void SaveGame(){

        // Get the player's current position from PlayerManager
        Vector3 playerPosition = playerManager.GetCurrentPosition();

        PlayerPrefs.SetFloat("PlayerXCoordinates", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerYCoordinates", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerZCoordinates", playerPosition.z);

        PlayerPrefs.Save();

        Debug.Log("Player data saved!");
    }

    public void LoadGame()
    {
        // Load player data from PlayerPrefs or any other preferred method
        float playerX = PlayerPrefs.GetFloat("PlayerXCoordinates");
        float playerY = PlayerPrefs.GetFloat("PlayerYCoordinates");
        float playerZ = PlayerPrefs.GetFloat("PlayerZCoordinates");

        Vector3 savedPosition = new Vector3(playerX, playerY, playerZ);
        // Set the player's position using SetCurrentPosition method
        playerManager.SetCurrentPosition(savedPosition);

        Debug.Log("Player data loaded!");
    }
}
