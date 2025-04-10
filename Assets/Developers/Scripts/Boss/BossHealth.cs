using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    //[SerializeField] private SpriteRenderer healthBarRenderer;
    //[SerializeField] private Sprite[] healthSprites; // Array van custom health sprites
    [SerializeField] private Image bossHPBar;
    private GameManager gameManager;
    private AudioManager audioManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        audioManager = FindFirstObjectByType<AudioManager>();
        maxHealth = 100f;
        currentHealth = 100f;
        bossHPBar.fillAmount = 1f;
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    //boss taking damage

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
        bossHPBar.fillAmount = currentHealth / maxHealth;
        //int spriteIndex = Mathf.Clamp(currentHealth, 0, healthSprites.Length - 1);
        //healthBarRenderer.sprite = healthSprites[spriteIndex];
    }

    //boss is dead
    void Die()
    {
        gameManager.ScoreUp(1000);
        gameManager.WinGame(); 
    }

    // trigger to take damage
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player Bullet"))
        {
            audioManager.PlaySound(4);
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}
