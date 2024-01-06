using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI time;
    public GameObject menuPanel;
    public GameObject controlsButton;
    public GameObject audioButton;
    
    public GameObject saveButton;
    public GameObject quitButton;
    public GameObject backButton;
    private GameManager _gameManager;

    //create an event to save the game
    public event EventHandler OnSave;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        /*score.text = $"Score: {_gameManager.scoreManager.Score}";
        bestScore.text = $"Best Score: {_gameManager.scoreManager.BestScore}";
        time.text = $"Time: {Mathf.Ceil(_gameManager.timeManager.RemainingTime)}";*/
    }

    public void StartGame(){
        menuPanel.SetActive(false);
    }

    public void StopGame(){
        menuPanel.SetActive(true);
    }

    public void closeMenu(){
        menuPanel.SetActive(false);
    }
}