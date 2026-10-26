using UnityEngine;

public class DinoHealth : MonoBehaviour
{
    [Header("Health Settings")]
    private float maxHealth;
    private float currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch(gameObject.tag) {
            case "TankDino":
            maxHealth = 100f;
            break;

            case "Dino":
            maxHealth = 50f;
            break;

            case "SpeedDino":
            maxHealth = 25f;
            break;

            default:
            Debug.LogWarning($"Tag {gameObject.tag} not recognized.");
            maxHealth = 100f;
            break;
        }

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
