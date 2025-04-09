using UnityEngine;

public class TentacleShooter : MonoBehaviour
{
    [System.Serializable]
    public class SpawnSettings
    {
        public Transform spawnPoint;
        public float fireRate = 2f;  // Hoe vaak dit punt schiet (in seconden)
        public float initialDelay = 0f; // Vertraging voordat dit punt begint met schieten
        [HideInInspector] public float nextFireTime;
    }

    public GameObject projectilePrefab;
    public SpawnSettings[] spawnSettings;
    public float spawnOffsetDistance = 0.5f; // Hoe ver links van het spawn point het projectiel spawnt

    private void Start()
    {
        foreach (var setting in spawnSettings)
        {
            setting.nextFireTime = Time.time + setting.initialDelay;
        }
    }

    private void Update()
    {
        foreach (var setting in spawnSettings)
        {
            if (Time.time >= setting.nextFireTime)
            {
                FireProjectile(setting.spawnPoint);
                setting.nextFireTime = Time.time + setting.fireRate;
            }
        }
    }

    void FireProjectile(Transform spawnPoint)
    {
        // Spawn de projectile iets naar links vanaf het spawn point
        Vector3 spawnPosition = spawnPoint.position + Vector3.left * spawnOffsetDistance;

        Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
    }
}
