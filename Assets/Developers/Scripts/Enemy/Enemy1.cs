using UnityEngine;

public class Enemy1 : EnemyBase
{
    [SerializeField] private Transform target; //player
    [SerializeField] private GameObject player; //player
    public float speed;
    private GameObject particle;
    void Start()
    {
        particle = Resources.Load<GameObject>("Prefabs/Particles/EnemyExplosion");
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        speed = 9;
        audioManager = FindFirstObjectByType<AudioManager>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

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
        Instantiate(particle, transform.position, Quaternion.identity);
        // deleting object
        gameManager.spawnedEnemies.Remove(gameObject);
        Destroy(gameObject);
    }
    public override void Spawn()
    {
        //gameManager.SpawnEnemy(1, true);
        //gameManager.spawnedEnemies.Remove(gameObject);
        //Destroy(gameObject);

        gameObject.transform.position = gameManager.GetRandomPos();
    }
    public override void Damaged()
    {
        gameManager.ScoreUp(20);
        audioManager.PlaySound(2);
        Destroyed();
    }
}
