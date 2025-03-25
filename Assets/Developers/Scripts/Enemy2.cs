using UnityEngine;

public class Enemy2 : EnemyBase
{
    private int HP = 1;
    [SerializeField] private Transform firePoint;
    public float speed;
    [SerializeField] private GameObject bulletPrefab;
    public float fireCooldown;
    public float rotationSpeed = 30f; // snelheid van de cirkelbeweging

    void Start()
    {
        fireCooldown = 2.5f;
        speed = 5;
        audioManager = FindFirstObjectByType<AudioManager>();
        gameManager = FindFirstObjectByType<GameManager>();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        // Beweeg richting het firePoint
        transform.position = Vector3.MoveTowards(transform.position, firePoint.position, step);

   

        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0)
        {
            Shoot();
        }
    }
     

    public override void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 90, 0));
        fireCooldown = 2.5f;
    }

    public override void Destroyed()
    {
        //deleting enemy
        gameManager.ScoreUp(25);
        gameManager.spawnedEnemies.Remove(gameObject);
        Destroy(gameObject);
    }

    public override void Spawn()
    {
    }

    public override void Damaged()
    {
        // Vijand krijgt schade
        HP--;
        if (HP <= 0)
        {
            Destroyed();
        }
    }

    // Vijand is geraakt
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player Bullet") || other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy hitted");
            Damaged();
        }
    }
}
