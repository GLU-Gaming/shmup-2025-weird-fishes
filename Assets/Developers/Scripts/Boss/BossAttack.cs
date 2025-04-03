using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private GameObject tentacleProjectilePrefab;  // Prefab voor projectiel
    [SerializeField] private GameObject tentacleStrikePrefab;      // Prefab voor tentakel aanval
    [SerializeField] private Transform[] strikeSpawnPoints;        // Posities voor tentakel-strikes
    [SerializeField] private Transform projectileSpawnPoint;       // Positie aan rechterkant scherm

    private bool isAttacking = false;

    public void StartAttacking()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(AttackPattern());
        }
    }

    private IEnumerator AttackPattern()
    {
        while (isAttacking)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f)); // Willekeurige aanvalstijd

            int attackType = Random.Range(0, 2);
            if (attackType == 0)
            {
                FireTentacleProjectile();
            }
            else
            {
                TentacleStrike();
            }
        }
    }

    private void FireTentacleProjectile()
    {
        // Spawnt een projectiel aan de rechterkant van het scherm
        Instantiate(tentacleProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Debug.Log("Tentacle Projectile Fired from Right!");
    }

    private void TentacleStrike()
    {
        // Spawnt een tentakel op x = -13 en beweegt omhoog of omlaag
        Transform spawnPoint = strikeSpawnPoints[Random.Range(0, strikeSpawnPoints.Length)];
        GameObject tentacle = Instantiate(tentacleStrikePrefab, spawnPoint.position, Quaternion.identity);
        //tentacle.GetComponent<TentacleMovement>().StartMoving();
        Debug.Log("Tentacle Strike at x = -13!");
    }

    public void StopAttacking()
    {
        isAttacking = false;
    }
}
