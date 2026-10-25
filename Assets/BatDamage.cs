using UnityEngine;

public class BatDamage : MonoBehaviour
{
    [Header("Bat Damage Settings")]
    public float damageAmount = 25f;
    public string enemyTag = "Dino";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag(enemyTag)) {
            DinoHealth enemy = other.GetComponent<DinoHealth>();

            if (enemy != null) {
                enemy.TakeDamage(damageAmount);
            }
        }
        
    }
}
