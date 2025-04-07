using System.Reflection;
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
    [SerializeField] private GameObject[] steams;
    private float steamCooldown;
    void Start()
    {
        steamCooldown = 0.1f;
        //steams = new Image[steams.Length];
        //steams = new GameObject[steams.Length];
        steams[0].SetActive(true);
        steams[1].SetActive(false);
        steams[2].SetActive(false);
        steams[3].SetActive(false);
        rotateClass = FindFirstObjectByType<RotateConfig>();
        //getting Scene name
        sceneName = SceneManager.GetActiveScene().name;
    }
     void Update()
    {
        if (steams != null)
        {
            steamCooldown -= Time.deltaTime;
        }
        if (steams != null && steamCooldown <= 0)
        {
            AnimateSteam();
        }
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
    private void AnimateSteam()
    {
        if (steams[0].activeSelf)
        {
            steams[0].SetActive(false);
            steams[1].SetActive(true);
        } else if (steams[1].activeSelf)
        {
            steams[1].SetActive(false);
            steams[2].SetActive(true);
        }
        else if (steams[2].activeSelf)
        {
            steams[2].SetActive(false);
            steams[3].SetActive(true);
        }
        else if (steams[3].activeSelf)
        {
            steams[3].SetActive(false);
            steams[0].SetActive(true);
        }
        steamCooldown = 0.1f;
    }

}
