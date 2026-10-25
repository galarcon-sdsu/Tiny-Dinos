using UnityEngine;

public class DinoHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 50f;
    private float currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(float amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) {
            Die();
            
        }
    }

    private void Die() {
        Destroy(gameObject);
    }
}
