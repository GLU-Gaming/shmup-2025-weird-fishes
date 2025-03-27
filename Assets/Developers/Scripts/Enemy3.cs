using UnityEngine;

public class Enemy3 : EnemyBase
{
    private int HP = 3;
    [SerializeField] private Transform firePoint;
    public float speed;
    [SerializeField] private GameObject bulletPrefab;
    public float fireCooldown;
    [SerializeField] private Transform target; //player
    [SerializeField] private GameObject player; //player
    private BoxCollider enemyCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        speed = 5;
        audioManager = FindFirstObjectByType<AudioManager>();
      
        enemyCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // moving to the player
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    public override void Shoot()
    {

    }
    public override void Destroyed()
    {
        //deleting enemy
        gameManager.ScoreUp(40);
        gameManager.spawnedEnemies.Remove(gameObject);
        Destroy(gameObject);
    }
    public override void Spawn()
    {

    }
    public override void Damaged()
    {
        //damaging enemy
        HP--;
        if (HP <= 0)
        {
            Destroyed();
        }
    }

    //Enemy has been hitted
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player Bullet") || other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy hitted");
            //audioManager.PlaySound(0);
            Damaged();
        }
    }
}
