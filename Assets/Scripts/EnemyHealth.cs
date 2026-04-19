using UnityEngine;
using UnityEngine.UI; 

public class EnemyHealth : MonoBehaviour
{
    // MOVED THESE INSIDE THE CLASS!
    [Header("Loot Drops")]
    public GameObject healthDropPrefab;
    public float dropChance = 0.3f; 

    [Header("Health Settings")]
    public float maxHealth = 20f;
    private float currentHealth;

    [Header("UI")]
    public Slider healthSlider; 

    [Header("Effects")]
    public GameObject deathEffect; 

    void Start()
    {
        currentHealth = maxHealth;
        
        if(healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        
        if(healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.instance.AddScore(1);
        AudioManager.instance.PlaySound(AudioManager.instance.enemyDead);
        if (deathEffect != null)
        {
            GameObject explosion = Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(explosion, 2f); 
        }

        // --- NEW LOOT DROP LOGIC ---
        if (healthDropPrefab != null && Random.value <= dropChance)
        {
            Instantiate(healthDropPrefab, transform.position + Vector3.up, Quaternion.identity);
        }

        Destroy(gameObject); 
    }
}