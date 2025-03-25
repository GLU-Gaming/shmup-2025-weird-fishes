using UnityEngine;

public class Enemy2 : EnemyBase
{
    private int HP = 3;
    [SerializeField] private Transform firePoint;
    public float speed;
    [SerializeField] private GameObject bulletPrefab;
    public float fireCooldown;
    void Start()
    {
        fireCooldown = 2.5f;
        speed = 5;
        audioManager = FindFirstObjectByType<AudioManager>();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
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
