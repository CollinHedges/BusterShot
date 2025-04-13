using UnityEngine;
using UnityEngine.UI; // Needed for 'Image'

public class Enemy : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [Header("References")]
    [SerializeField] private Image healthFillImage; // Drag the HealthFill image here

    private void Awake()
    {
        // Initialize current health
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Called when the enemy takes damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        // Check if the enemy should die
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        // If the image is assigned, update its fill
        if (healthFillImage != null)
        {
            float fillValue = currentHealth / maxHealth;
            healthFillImage.fillAmount = fillValue;
        }
    }

    private void Die()
    {
        // For now, just destroy the GameObject. 
        // Later, you can do animations, or pooling, etc.
        Destroy(gameObject);
    }
}
