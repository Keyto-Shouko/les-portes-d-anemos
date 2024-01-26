using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip BGM;
    public AudioClip rupeeCollectedClip;
    public AudioSource BGMsource;
    public AudioSource rupeeCollectedSource;

    public void StartMusic(){
        BGMsource.clip = BGM;
        BGMsource.Play();
    }

    public void StopMusic(){
        BGMsource.Stop();
    }

    public void PauseMusic(){
        BGMsource.Pause();
    }

    public void ResumeMusic(){
        BGMsource.UnPause();
    }

    public void playCollected(){
        rupeeCollectedSource.Play();
    }
}
