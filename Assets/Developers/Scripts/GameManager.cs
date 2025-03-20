using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    // volume Profile object
    [SerializeField] private VolumeProfile volumeProfile;
    // post-processing VFX
    private Vignette vignette;

    public Player playerScript;
    public GameObject playerObject;

    [SerializeField] private GameObject EnemyPrefab1;
    [SerializeField] private GameObject EnemyPrefab2;
    void Awake()
    {
        // initialization of vignette
        volumeProfile.TryGet(out vignette);
        vignette.active = false;

        // initialization of objects & scripts
        playerScript = FindFirstObjectByType<Player>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        EnemyPrefab1 = Resources.Load<GameObject>("Prefabs/Enemy (var1)");
        EnemyPrefab2 = Resources.Load<GameObject>("Prefabs/Enemy (var2)");

    }
    void Start()
    {
        SpawnEnemy();
    }

    void Update()
    {
        
    }

    // making screen's borders red 
    public void MakeScreenRed()
    {
        vignette.active = true;
    }
    public void SpawnEnemy()
    {
       Vector3 playerPos = playerObject.transform.position;
        Instantiate(EnemyPrefab2, playerPos += new Vector3(40, 0, 0), Quaternion.identity);
    }
}
