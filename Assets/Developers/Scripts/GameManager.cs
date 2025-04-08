using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using TMPro;

public class GameManager : MonoBehaviour
{
    // volume Profile object
    [SerializeField] private VolumeProfile volumeProfile;
    // post-processing VFX
    private Vignette vignette;
    private ChromaticAberration chrome;
    private DepthOfField depthOfField;

    //player
    public Player playerScript;
    public GameObject playerObject;

    //enemies
    [SerializeField] private GameObject enemyPrefab1;
    [SerializeField] private GameObject enemyPrefab2;
    [SerializeField] private GameObject enemyPrefab3;

    [SerializeField] private BossBattle bossbattle;

    public float stunDuration;
    private bool isStunned;
    private AudioManager audioManager;
    private string currentScene;

    // wave's variants
    List<List<int>> enemies = new List<List<int>>();
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] private int killedWaves = 0;
    [SerializeField] private int score;

    private float waveTimer = 20f;
    [SerializeField] private TextMeshProUGUI scoreView;
    private BossBattleScreenFader bossFader;

    void Awake()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        scoreView.text = "Score: " + score;
        // initialization of profile VFX
        volumeProfile.TryGet(out vignette);
        vignette.active = false;

        volumeProfile.TryGet(out chrome);
        chrome.active = false;

        volumeProfile.TryGet(out depthOfField);
        depthOfField.active = false;

        // initialization of objects & scripts
        playerScript = FindFirstObjectByType<Player>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        enemyPrefab1 = Resources.Load<GameObject>("Prefabs/Enemy (var1)");
        enemyPrefab2 = Resources.Load<GameObject>("Prefabs/Enemy (var2)");
        enemyPrefab3 = Resources.Load<GameObject>("Prefabs/Enemy (var3)");
        audioManager = FindFirstObjectByType<AudioManager>();
        bossFader = FindFirstObjectByType<BossBattleScreenFader>();
    }
    void Start()
    {
        //getting Scene name
        currentScene = SceneManager.GetActiveScene().name;

        //Setting soundtrack depending on a scene
        if (currentScene == "Level1")
        {
            CreateWave(1);
        }
        else if (currentScene == "Level2")
        {
            CreateWave(2);
        }
        else if (currentScene == "Level3")
        {
            CreateWave(3);
        }

        SpawnWave();

        isStunned = false;
        stunDuration = 2.5f;
    }

    void Update()
    {
        // stun timer
        stunDuration -= Time.deltaTime;
        waveTimer -= Time.deltaTime;
        if (stunDuration <= 0 && isStunned)
        {
            chrome.active = false;
            depthOfField.active = false;
            audioManager.ChangeVolumeSound();
            stunDuration = 2.5f;
            isStunned = false;
        }
        if (waveTimer <= 0)
        {
            SpawnWave();
            waveTimer = 20f;
        }
    }
    void FixedUpdate()
    {
        //spawning new wave
        if (spawnedEnemies.Count == 0)
        {
            killedWaves++;
            if (killedWaves > 5)
            {
                if (currentScene == "Level1")
                {
                    PlayerPrefs.SetInt("Score", score);
                    SceneManager.LoadScene("Level2");
                }
                else if (currentScene == "Level2")
                {
                    PlayerPrefs.SetInt("Score", score);
                    SceneManager.LoadScene("Level3");
                }
                else if (currentScene == "Level3")
                {
                    //bossFader.StartBossFade();
                }
            }
            else
            {
                SpawnWave();
                waveTimer = 20f;
            }
        }
    }

    // making screen's borders red 
    public void MakeScreenRed()
    {
        vignette.active = true;
    }

    // making screen chromatic
    public void Stunned()
    {
        isStunned = true;
        stunDuration = 2.5f;
        chrome.active = true;
        depthOfField.active = true;
    }
    public void SpawnEnemy(int enemyNum, bool respawn = false)
    {
        Vector3 playerPos = playerObject.transform.position;
        int posY1 = Random.Range(-10, 10);
        int posY2 = Random.Range(-5, 5);
        GameObject enemyInstance = null;
        if (enemyNum == 1)
        {
            enemyInstance = Instantiate(enemyPrefab1, playerPos += new Vector3(40, posY1, 0), Quaternion.identity);
        }
        else if (enemyNum == 2)
        {
            enemyInstance = Instantiate(enemyPrefab2, playerPos += new Vector3(40, posY1, 0), Quaternion.identity);
        }
        else if (enemyNum == 3)
        {
            enemyInstance = Instantiate(enemyPrefab3, playerPos += new Vector3(40, posY2, 0), Quaternion.identity);
        }
        if (enemyInstance != null && !respawn)
        {
            spawnedEnemies.Add(enemyInstance);
        }
    }

    private void CreateWave(int level)
    {
        switch (level)
        {
            case 1:
                enemies.Add(new List<int> { 1, 1 });
                enemies.Add(new List<int> { 1, 2, 3, 2 });
                enemies.Add(new List<int> { 1, 1, 3, 2 });
                enemies.Add(new List<int> { 3, 3 });
                enemies.Add(new List<int> { 2, 1, 3 });
                break;
            case 2:
                enemies.Add(new List<int> { 1, 1 });
                enemies.Add(new List<int> { 1, 2, 3, 2 });
                enemies.Add(new List<int> { 1, 1, 3, 2 });
                enemies.Add(new List<int> { 3, 3 });
                enemies.Add(new List<int> { 2, 1, 3 });
                break;
            case 3:
                break;
        }
    }
    public void SpawnWave()
    {
        if (currentScene != "Level3")
        {
            int wave = Random.Range(0, 3);
            for (int i = 0; i < enemies[wave].Count; i++)
            {
                SpawnEnemy(enemies[wave][i]);
            }
        }
    }
    public void ScoreUp(int amount)
    {
        score += amount;
        scoreView.text = "Score: " + score;
    }
    public void ScoreDown(int amount)
    {
        score -= amount;
        scoreView.text = "Score: " + score;
    }
    public int GetScore()
    {
        return score;
    }

    public Vector3 GetRandomPos()
    {
        int posY = Random.Range(-5, 5);
        Vector3 transf = new Vector3(40f, posY, -4.8f);
        return transf;
    }
}
