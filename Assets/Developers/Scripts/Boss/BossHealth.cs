using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    [SerializeField] private SpriteRenderer healthBarRenderer;
    [SerializeField] private Sprite[] healthSprites; // Array van custom health sprites

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        int spriteIndex = Mathf.Clamp(currentHealth, 0, healthSprites.Length - 1);
        healthBarRenderer.sprite = healthSprites[spriteIndex];
    }

    void Die()
    {
        Debug.Log("Boss Defeated!");
        gameObject.SetActive(false);
    }
}
