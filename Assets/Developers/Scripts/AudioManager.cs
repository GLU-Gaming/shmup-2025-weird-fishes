﻿using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEngine.Rendering.DebugUI;

public class AudioManager : MonoBehaviour
{
    //Sounds
    [SerializeField] private AudioSource audioSFX;
    [SerializeField] private AudioClip[] SFX;

    //Music
    [SerializeField] private AudioSource audioMusic;
    [SerializeField] private AudioClip[] soundtracks;

    // logic variabels for Lerp (smooth change)
    private float targetValueUp = 1f;
    private float targetValueDown = 0.2f;
    private float targetValueMid = 0.5f;
    public float timeDuration = 1.5f;
    private float elapsedTime = 0f;
    private float currentVolume;
    [SerializeField] private string changeConfig;
    private bool isChanging;

    void Start()
    {
        isChanging = false;
        //getting Scene name
        string currentScene = SceneManager.GetActiveScene().name;

        //Setting soundtrack depending on a scene
        if (currentScene == "Menu")
        {
            audioMusic.clip = soundtracks[0];

        } 
        else if (currentScene == "Level1")
        {
            audioMusic.clip = soundtracks[1];
        } 
        else if (currentScene == "Level2")
        {
            audioMusic.clip = soundtracks[2];
        } 
        else if (currentScene == "Level3")
        {
            audioMusic.clip = soundtracks[3];
        } 
        else if (currentScene == "GameOver")
        {
            audioMusic.clip = soundtracks[4];
        }
        else if (currentScene == "winYAY")
        {
            audioMusic.clip = soundtracks[5];
        }
        // "𝄞 Music sounds better with you 𝄞"
        audioMusic.Play();
    }

    // play sound from the array
    public void PlaySound(int soundIndex)
    {
     

        switch (soundIndex)
        {
            case 0:
                audioSFX.PlayOneShot(SFX[0]);
                break;
            case 1:
                audioSFX.PlayOneShot(SFX[1]);
                break;
            case 2:
                audioSFX.PlayOneShot(SFX[2]);
                break;
            case 3:
                audioSFX.PlayOneShot(SFX[3]);
                break;
            case 4:
                audioSFX.PlayOneShot(SFX[4]);
                break;
            case 5:
                audioSFX.PlayOneShot(SFX[5]);
                break;
            case 6:
                audioSFX.PlayOneShot(SFX[6]);
                break;

        }
    }
     void Update()
    {


        // smooth change volume
        if (elapsedTime < timeDuration && isChanging)
        {
            elapsedTime += Time.deltaTime;
            float changedVolume = Mathf.Lerp(currentVolume, GetTargetValue(), elapsedTime / timeDuration);
            audioMusic.volume = changedVolume;
        }

        // reset
        if (elapsedTime >= timeDuration)
        {
            isChanging = false;
            changeConfig = "null";
            elapsedTime = 0;
        }
    }

    // changing volume of audio source
    public void ChangeVolumeSound(string config = "mid")
    {
        isChanging = true;
        currentVolume = audioMusic.volume;
        Debug.Log(currentVolume);
        if (config == "up")
        {
            changeConfig = "upper";
        }
        else if (config == "down")
        {
            changeConfig = "downer";
        } else
        {
            changeConfig = "mideum";
        }
    }

    // return string for operation
    private float GetTargetValue()
    {
        
        switch (changeConfig)
        {
            case "upper":
                return targetValueUp;
            case "downer":
                return targetValueDown;
            case "mideum":
                return targetValueMid;
        }
        return 1f;

    }
}
