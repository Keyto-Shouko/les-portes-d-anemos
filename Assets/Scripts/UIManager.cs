using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI time;
    public GameObject startButton;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = $"Score: {_gameManager.scoreManager.Score}";
        bestScore.text = $"Best Score: {_gameManager.scoreManager.BestScore}";
        time.text = $"Time: {Mathf.Ceil(_gameManager.timeManager.RemainingTime)}";
    }

    public void StartGame(){
        startButton.SetActive(false);
    }

    public void StopGame(){
        startButton.SetActive(true);
    }
}
