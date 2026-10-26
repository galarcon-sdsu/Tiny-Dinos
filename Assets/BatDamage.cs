using UnityEngine;

public class BatDamage : MonoBehaviour
{
    [Header("Bat Damage Settings")]
    public float damageAmount = 25f;
    public string[] enemyTag = {"Dino", "TankDino", "SpeedDino"};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other) {
        if (isValidTag(other)) {
            
            DinoHealth enemy = other.GetComponent<DinoHealth>();

            if (enemy != null) {
                enemy.TakeDamage(damageAmount);
            }
        }
    }

    private bool isValidTag(Collider coll) {
        for (int i = 0; i < enemyTag.Length; i++) {
            if (!string.IsNullOrEmpty(enemyTag[i]) && coll.CompareTag(enemyTag[i])) {
                return true;
            }
        }
        return false;
    }
}
