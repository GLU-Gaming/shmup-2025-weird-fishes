using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private GameObject tentacleProjectilePrefab;
    [SerializeField] private GameObject tentacleStrikePrefab;
    [SerializeField] private Transform[] projectileSpawnPoints;
    [SerializeField] private Transform[] strikeSpawnPoints;

    public float attackInterval = 3f;
    private float attackTimer = 0f;

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            int attackType = Random.Range(0, 2);
            if (attackType == 0)
                FireTentacleProjectiles();
            else
                TentacleStrike();

            attackTimer = 0f;
        }
    }

    void FireTentacleProjectiles()
    {
        foreach (Transform spawnPoint in projectileSpawnPoints)
        {
            Instantiate(tentacleProjectilePrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    void TentacleStrike()
    {
        Transform strikePoint = strikeSpawnPoints[Random.Range(0, strikeSpawnPoints.Length)];
        Instantiate(tentacleStrikePrefab, strikePoint.position, Quaternion.identity);
    }
}

