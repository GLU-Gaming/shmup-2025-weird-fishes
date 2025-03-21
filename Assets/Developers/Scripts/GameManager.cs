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
    private DepthOfField depthOfField;

    //player
    public Player playerScript;
    public GameObject playerObject;

    //enemies
    [SerializeField] private GameObject enemyPrefab1;
    [SerializeField] private GameObject enemyPrefab2;

    public float stunDuration;
    private bool isStunned;
    private AudioManager audioManager;
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
        audioManager = FindFirstObjectByType<AudioManager>();

    }
    void Start()
    {
        isStunned = false;
        SpawnEnemy();
        stunDuration = 2.5f;
    }

    void Update()
    {
        // stun timer
        stunDuration -= Time.deltaTime;
        if(stunDuration <= 0 && isStunned)
        {
            chrome.active = false;
            depthOfField.active = false;
            audioManager.ChangeVolumeSound();
            stunDuration = 2.5f;
            isStunned= false;
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
    public void SpawnEnemy()
    {
       Vector3 playerPos = playerObject.transform.position;
        Instantiate(enemyPrefab2, playerPos += new Vector3(40, 0, 0), Quaternion.identity);
    }
}
