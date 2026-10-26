using UnityEngine;

public class BaseballDamage : MonoBehaviour
{
    [Header("Baseball Damage Settings")]
    public float damageAmount = 15f;
    public string[] enemyTag = {"Dino", "TankDino", "SpeedDino"};

    private void OnTriggerEnter(Collider other) {
        if (isValidTag(other)) {
            
            DinoHealth enemy = other.GetComponent<DinoHealth>();

            if (enemy != null) {
                enemy.TakeDamage(damageAmount);
                Destroy(gameObject);
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
