using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossBattle : MonoBehaviour
{
    [Header("Boss Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI Elements")]
    public Slider healthBar;

    [Header("Boss Attacks")]
    public GameObject attackPrefab; // Aanvallen prefab (bijv. vuurballen)
    public Transform attackSpawnPoint; // Waar de aanval spawnt
    public float attackCooldown = 2f;

    [Header("Boss Attack Script")]
    [SerializeField] private BossAttack bossAttackScript;

    void Start()
    {
        //currentHealth = maxHealth;
        //healthBar.maxValue = maxHealth;
        //healthBar.value = currentHealth;
    }

    public void StartBoss()
    {
        Debug.Log("Boss Battle Started!");
        gameObject.SetActive(true); // Zorg dat de boss actief wordt
        currentHealth = maxHealth;
        healthBar.value = currentHealth;

        StartCoroutine(BossAttackLoop());
        if (bossAttackScript != null)
        {
            bossAttackScript.StartAttacking();
        }
        else
        {
            Debug.LogWarning("BossAttack script is not assigned!");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        Debug.Log("Boss took damage: " + damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss defeated!");
        gameObject.SetActive(false);
        if (bossAttackScript != null)
        {
            bossAttackScript.StopAttacking();
        }
    }

    IEnumerator BossAttackLoop()
    {
        while (currentHealth > 0)
        {
            yield return new WaitForSeconds(attackCooldown);
            BossAttack();
        }
    }

    public void BossAttack()
    {
        if (attackPrefab != null && attackSpawnPoint != null)
        {
            Instantiate(attackPrefab, attackSpawnPoint.position, Quaternion.identity);
            Debug.Log("Boss attacked!");
        }
    }
}
