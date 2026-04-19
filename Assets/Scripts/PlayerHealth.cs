using UnityEngine;
using UnityEngine.UI; // REQUIRED FOR UI!

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    public Slider healthSlider; // The slot for your cyan bar

    void Start()
    {
        currentHealth = maxHealth;
        
        // Set up the slider's starting values
        if(healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        
        // Update the visual bar
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
        GameManager.instance.GameOver();

        // Hide the health bar from the screen!
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(false);
        }

        Destroy(gameObject); 
    }

    public void Heal(float amount)
    {
        // Add the health
        currentHealth += amount;

        // Prevent the player from healing past their max HP!
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Update the visual cyan slider
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }
}