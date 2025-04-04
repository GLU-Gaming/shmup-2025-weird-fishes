﻿using System.Reflection;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public RotateConfig rotateClass;
    //[SerializeField] private Button startBtn;
    //[SerializeField] private Button quitBtn;
    private string sceneName;
    void Start()
    {
        rotateClass = FindFirstObjectByType<RotateConfig>();
        //getting Scene name
        sceneName = SceneManager.GetActiveScene().name;
    }
     void Update()
    {

    }
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadScene()
    {
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene("Level1");
    }

}
