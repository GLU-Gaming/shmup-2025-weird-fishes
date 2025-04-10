using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    private int score;
    [SerializeField] private TextMeshProUGUI scoreView;
    void Start()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        scoreView.text = "Your Score: " + score;
    }

}
