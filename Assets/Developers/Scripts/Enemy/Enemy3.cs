using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy3 : EnemyBase
{
    private int HP = 3;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform[] firePoints;
    public float speedEnemy;
    public float speedBullet;
    [SerializeField] private GameObject bulletPrefab;
    public float fireRate = 0.3f;
    public float rotateTime = 2.5f;
    private MeshCollider enemyCollider;

    private float rotationSpeed = 500f;
    private float targetSpeed;
    private bool isSlowingDown = false;
    [SerializeField] private bool isRotating = true;

    [SerializeField] private Transform[] fireEndPosition;
    private float stepEnemy;
    private float stepBullet;
    private GameObject[] particles;

    private Renderer enRenderer;
    private Color originalColor;
    public Color flashColor = Color.white;
    public float flashDuration = 0.1f;

    void Start()
    {
        enRenderer = GetComponent<Renderer>();
        originalColor = enRenderer.material.color;
        GameObject leftBorder = GameObject.FindGameObjectWithTag("Border");
        leftPoint = leftBorder.transform;
        particles = new GameObject[2];
        particles[0] = Resources.Load<GameObject>("Prefabs/Particles/EnemyExplosion");
        particles[1] = Resources.Load<GameObject>("Prefabs/Particles/EnemyDamage");
        isRotating = true;
        gameManager = FindFirstObjectByType<GameManager>();
        speedEnemy = 2.5f;
        speedBullet = 10;
        audioManager = FindFirstObjectByType<AudioManager>();
        enemyCollider = GetComponent<MeshCollider>();
        targetSpeed = rotationSpeed;
        StartCoroutine(ControlRotation());
    }

    void Update()
    {
        fireRate -= Time.deltaTime;
        if (isRotating && fireRate <= 0)
        {
            Shoot();
        }
        stepEnemy = speedEnemy * Time.deltaTime;
        stepBullet = speedEnemy * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, leftPoint.position, stepEnemy);
        //transform.position += transform.right * speedEnemy * Time.deltaTime;
        //transform.Translate(Vector3.left * speedEnemy * Time.deltaTime);

        if (!isSlowingDown)
        {
            transform.Rotate(0, 0, targetSpeed * Time.deltaTime);
        }
    }

    IEnumerator ControlRotation()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.5f);
            yield return StartCoroutine(SlowDownRotation());
            yield return new WaitForSeconds(3.5f - 1f);
            isRotating = true;
            targetSpeed = rotationSpeed;
        }
    }

    IEnumerator SlowDownRotation()
    {
        isSlowingDown = true;
        isRotating = false;
        float startSpeed = targetSpeed;
        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            targetSpeed = Mathf.Lerp(startSpeed, 0, elapsedTime / duration);
            transform.Rotate(0, 0, targetSpeed * Time.deltaTime);
            yield return null;
        }

        targetSpeed = 0;
        isSlowingDown = false;
        rotateTime = 2.5f;
    }

    public override void Shoot()
    {
        //for (int i = 0; i < firePoints.Length; i++)
        //{
        //  Instantiate(starBullets[i], firePoints[i].position, firePoints[i].rotation);

        //}
        int pos = Random.Range(0, 5);
        Instantiate(bulletPrefab, firePoints[pos].position, firePoints[pos].rotation);
        fireRate = 0.15f;
    }

    public override void Destroyed()
    {
        Instantiate(particles[0], transform.position, Quaternion.identity);
        gameManager.ScoreUp(40);
        gameManager.spawnedEnemies.Remove(gameObject);
        Destroy(gameObject);
    }

    public override void Spawn()
    {
    }

    public override void Damaged()
    {
        HP--;
        StartCoroutine(FlashEffect());
        if (HP <= 0)
        {
            Destroyed();
        } else
        {
            Instantiate(particles[1], transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player Bullet"))
        {
            Debug.Log("Enemy hitted");
            Destroy(other.gameObject);
            Damaged();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Damaged();
        }
    }
    private System.Collections.IEnumerator FlashEffect()
    {
        enRenderer.material.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        enRenderer.material.color = originalColor;
    }
}
