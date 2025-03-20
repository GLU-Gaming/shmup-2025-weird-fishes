using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    // volume Profile object
    [SerializeField] private VolumeProfile volumeProfile;
    // post-processing VFX
    private Vignette vignette;
    private ChromaticAberration chrome;

    //player
    public Player playerScript;
    public GameObject playerObject;

    //enemies
    [SerializeField] private GameObject EnemyPrefab1;
    [SerializeField] private GameObject EnemyPrefab2;

    public float stunDuration;
    void Awake()
    {
        // initialization of vignette
        volumeProfile.TryGet(out vignette);
        vignette.active = false;
        
        volumeProfile.TryGet(out chrome);
        chrome.active = false;

        // initialization of objects & scripts
        playerScript = FindFirstObjectByType<Player>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        EnemyPrefab1 = Resources.Load<GameObject>("Prefabs/Enemy (var1)");
        EnemyPrefab2 = Resources.Load<GameObject>("Prefabs/Enemy (var2)");

    }
    void Start()
    {
        SpawnEnemy();
        stunDuration = 2.5f;
    }

    void Update()
    {
        // stun timer
        stunDuration -= Time.deltaTime;
        if(stunDuration <= 0)
        {
            chrome.active = false;
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
        stunDuration = 2.5f;
        chrome.active = true;
    }
    public void SpawnEnemy()
    {
       Vector3 playerPos = playerObject.transform.position;
        Instantiate(EnemyPrefab2, playerPos += new Vector3(40, 0, 0), Quaternion.identity);
    }
}
