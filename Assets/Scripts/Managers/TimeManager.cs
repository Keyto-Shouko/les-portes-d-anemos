using UnityEngine;
using System;


public class TimeManager : MonoBehaviour
{
    private GameManager _gameManager;
    public float TimeElapsed { get; private set; }
    private float _startTime;
    public float CurrentTimeInDay { get; private set; }
    
    private bool _isRunning = false;
    public event Action OnTimeUp;
    public void Reset(){
        //CurrentTimeInDay = duration;
    }

    public void StartGame(){
        Reset();
        _isRunning = true;
    }

    public void ResumeGame(){
        _isRunning = true;
    }
    public void StopGame(){
        _isRunning = false;
    }
    public float GetTimeElapsed(){
        return TimeElapsed;
    }
    public void Awake(){
        _startTime = Time.time;
        _gameManager = GameManager.Instance;
    }
    public void Update(){
        /*if(!_isRunning){
            return;
        }*/
       TimeElapsed = Time.time - _startTime;
        /*CurrentTimeInDay -= Time.deltaTime;
        if(CurrentTimeInDay <= 0){
            CurrentTimeInDay = 0;
            StopGame();
            //OnTimeUp?.Invoke();
            //GameManager.Instance.StopGame();
        }*/
    }
}
