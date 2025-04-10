using UnityEngine;

public class Enemy2 : EnemyBase
{
    private int HP = 1;
    [SerializeField] private Transform firePoint;
    public float speed;
    [SerializeField] private GameObject bulletPrefab;
    public float fireCooldown;
    private GameObject particle;

    void Start()
    {

        particle = Resources.Load<GameObject>("Prefabs/Particles/EnemyExplosion");

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
        //transform.position = Vector3.MoveTowards(transform.position, firePoint.position, step);
        transform.position += transform.right * -speed * Time.deltaTime;



        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0)
        {
            Shoot();
        }
    }
     

    public override void Shoot()
    {
        audioManager.PlaySound(5);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 90, 0));
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        fireCooldown = 2.5f;
    }

    public override void Destroyed()
    {
        //deleting enemy
        audioManager.PlaySound(0);
        Instantiate(particle, transform.position, Quaternion.identity);
        gameManager.ScoreUp(25);
        gameManager.spawnedEnemies.Remove(gameObject);
        Destroy(gameObject);
    }

    public override void Spawn()
    {
        //gameManager.SpawnEnemy(2, true);
        //gameManager.spawnedEnemies.Remove(gameObject);
        //Destroy(gameObject);
        gameObject.transform.position = gameManager.GetRandomPos();

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
        if (other.gameObject.CompareTag("Player Bullet"))
        {
            Destroy(other.gameObject);
            Debug.Log("Enemy hitted");
            Damaged();
        } 
        else if (other.gameObject.CompareTag("Player"))
        {
            Damaged();
        }
    }
}
