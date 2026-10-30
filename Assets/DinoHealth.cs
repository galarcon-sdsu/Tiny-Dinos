using UnityEngine;

public class DinoHealth : MonoBehaviour
{
    [Header("Health Settings")]
    private int maxHealth;
    public int currentHealth; //necessary health parameters
    public AudioClip deathFX; //sound fx asset
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch(gameObject.tag) {
            case "TankDino":
            maxHealth = 200;
            break;

            case "Dino":
            maxHealth = 50; //check through tags to see which type
            break; //of enemy has spawned and assign health accordingly

            case "SpeedDino":
            maxHealth = 25;
            break;

            default:
            Debug.LogWarning($"Tag {gameObject.tag} not recognized.");
            maxHealth = 100;
            break;
        }

        currentHealth = maxHealth; //set the health of the enemy to the determined max health
    }

    public void TakeDamage(int amount) { //function that updates the health
        currentHealth -= amount;//called by outside functions and is passed corresponding damage values to update
        if (currentHealth <= 0) {  //if no health or less than 0
            Die();  //die 
            
        }
    }

    private void Die() { 
        AudioSource.PlayClipAtPoint(deathFX, transform.position); //plays corresponding death fx from gameobject prefab element
        Destroy(gameObject); //destroys the object
    }
}
