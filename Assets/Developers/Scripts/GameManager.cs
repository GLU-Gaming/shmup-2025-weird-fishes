using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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

    public float stunDuration;
    private bool isStunned;
    private AudioManager audioManager;
    private string currentScene;

    // wave's variants
    List<List<int>> enemies = new List<List<int>>();
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] private int killedWaves = 0;

    private float waveTimer = 20f;
    void Awake()
    {
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

        //foreach (var row in enemies)
        //{
        //    Debug.LogError(string.Join("", row));
        //}
        
        isStunned = false;
        stunDuration = 2.5f;
    }

    void Update()
    {
        // stun timer
        stunDuration -= Time.deltaTime;
        waveTimer -= Time.deltaTime;
        if(stunDuration <= 0 && isStunned)
        {
            chrome.active = false;
            depthOfField.active = false;
            audioManager.ChangeVolumeSound();
            stunDuration = 2.5f;
            isStunned= false;
        }
        if(waveTimer <= 0)
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
                if(currentScene == "Level1") {SceneManager.LoadScene("Level2");}
                else if(currentScene == "Level2") {SceneManager.LoadScene("Level3");}
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
    public void SpawnEnemy(int enemyNum)
    {
       Vector3 playerPos = playerObject.transform.position;
        int posY = Random.Range(-15, 21);
        GameObject enemyInstance = null;
        if (enemyNum == 1)
        {
            enemyInstance = Instantiate(enemyPrefab1, playerPos += new Vector3(40, posY, 0), Quaternion.identity);
        }
        else if (enemyNum == 2)
        {
            enemyInstance = Instantiate(enemyPrefab2, playerPos += new Vector3(40, posY, 0), Quaternion.identity);
        }
        else if (enemyNum == 3)
        {
             
        }
        if (enemyInstance != null)
        {
            spawnedEnemies.Add(enemyInstance);
        }

    }

    private void CreateWave(int level)
    {
        switch (level)
        {
            case 1:
                enemies.Add(new List<int> {1,1});
                enemies.Add(new List<int> { 1, 2, 2, 2 });
                enemies.Add(new List<int> { 2, 1, 1 });
                break;
                case 2:

                break;
                case 3:

                break;

        }
    }
    public void SpawnWave()
    {
        int wave = Random.Range(0,3);
        for (int i = 0; i < enemies[wave].Count; i++)
        {
            SpawnEnemy(enemies[wave][i]);
        }
    }
}
