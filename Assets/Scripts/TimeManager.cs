using UnityEngine;
using System;
public class TimeManager : MonoBehaviour
{
    //duration of 1 game
    public int duration = 20;

    public float RemainingTime { get; private set; }
    private bool _isRunning = false;
    public event Action OnTimeUp;
    public void Reset(){
        RemainingTime = duration;
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

    public void Update(){
        if(!_isRunning){
            return;
        }
        RemainingTime -= Time.deltaTime;
        if(RemainingTime <= 0){
            RemainingTime = 0;
            StopGame();
            OnTimeUp?.Invoke();
            //GameManager.Instance.StopGame();
        }
    }
}
