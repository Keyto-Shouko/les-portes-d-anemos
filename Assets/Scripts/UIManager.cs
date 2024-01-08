using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI score;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI time;
    public GameObject menuPanel;
    public GameObject controlsButton;
    public GameObject audioButton;
    
    public GameObject saveButton;
    public GameObject quitButton;
    public GameObject backButton;

    public GameObject teleporterListPanel;

    [SerializeField] 
    private Transform teleporterContainer;

    [SerializeField]
    private GameObject teleporterScrollViewItemPrefab;

    private int teleporterCount;
    private GameManager _gameManager;

    // listen to the event that comes from the playerManager

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
    }

    private void Start()
    {
        
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

    public void AddTeleporterToUIList(Teleporter discoveredTeleporter){
        // Instantiate the teleporter UI item
        var newTeleporterToAdd = Instantiate(teleporterScrollViewItemPrefab);
        // Set the instantiated item's parent to the content container
        newTeleporterToAdd.transform.SetParent(teleporterContainer);
        TextMeshProUGUI nameTextField = newTeleporterToAdd.GetComponentInChildren<TextMeshProUGUI>(); // Replace with the correct reference
        TextMeshProUGUI descriptionTextField = newTeleporterToAdd.GetComponentInChildren<TextMeshProUGUI>(); // Replace with the correct reference

        // Update the content of the TextMeshPro fields with the information from the discovered teleporter
        nameTextField.text = discoveredTeleporter.name;
        descriptionTextField.text = discoveredTeleporter.name;

    }

    public void ToggleTeleporterList(){
        // Open the teleporter list
        teleporterListPanel.SetActive(!teleporterListPanel.activeSelf);
    }
}