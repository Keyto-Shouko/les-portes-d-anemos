using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }
    public int BestScore { get; private set; }

    private void Awake()
    {
        BestScore = PlayerPrefs.GetInt("best-score", 0);
    }

    public void Reset(){
        Score = 0;
    }
    public void AddScore(int score){
        Score += score;
        if(Score > BestScore){
            BestScore = Score;
            PlayerPrefs.SetInt("best-score", BestScore);
        }
        Debug.Log($"Score: {Score}");
    }
}
