using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        // get the gameManager
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

    public void AddTeleporterToUIList(Teleporter discoveredTeleporter){
        // Instantiate the teleporter UI item
        var newTeleporterToAdd = Instantiate(teleporterScrollViewItemPrefab);
        Debug.Log("newTeleporterToAdd: " + discoveredTeleporter.GetPosition());
        // Set the instantiated item's parent to the content container
        newTeleporterToAdd.transform.SetParent(teleporterContainer);
        TextMeshProUGUI[] textFields = newTeleporterToAdd.GetComponentsInChildren<TextMeshProUGUI>();
        // Assuming the order of TextMeshProUGUI components in the array corresponds to the order in your prefab
        // Update the content of the TextMeshPro fields with the information from the discovered teleporter
        textFields[0].text = discoveredTeleporter.name;  // First TextMeshProUGUI
        textFields[1].text = discoveredTeleporter.description;  // Second TextMeshProUGUI

        Button teleporterButton = newTeleporterToAdd.GetComponentInChildren<Button>();
        if (teleporterButton != null)
        {
            teleporterButton.onClick.AddListener(() => OnTeleporterItemClick(discoveredTeleporter));
        }
        else
        {
            Debug.LogError("Button component not found on teleporter item.");
        }

    }

    private void OnTeleporterItemClick(Teleporter clickedTeleporter)
    {
        // Handle teleporter item click
        Debug.Log("Teleporter clicked: " + clickedTeleporter.name);

        // Send an event to the GameManager to set the player's position
        if (_gameManager != null)
        {
            _gameManager.playerManager.SetCurrentPosition(clickedTeleporter.GetPosition());
        }
        else
        {
            Debug.LogError("GameManager not found.");
        }
    }

    public void ToggleTeleporterList(){
        // Open the teleporter list
        teleporterListPanel.SetActive(!teleporterListPanel.activeSelf);
    }

    public void CloseTeleporterList(){
        // Close the teleporter list
        teleporterListPanel.SetActive(false);
    }
}