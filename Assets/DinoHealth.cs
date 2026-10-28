using UnityEngine;

public class DinoHealth : MonoBehaviour
{
    [Header("Health Settings")]
    private int maxHealth;
    public int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch(gameObject.tag) {
            case "TankDino":
            maxHealth = 100;
            break;

            case "Dino":
            maxHealth = 50;
            break;

            case "SpeedDino":
            maxHealth = 25;
            break;

            default:
            Debug.LogWarning($"Tag {gameObject.tag} not recognized.");
            maxHealth = 100;
            break;
        }

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(int amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) {
            Die();
            
        }
    }

    private void Die() {
        Destroy(gameObject);
    }
}
