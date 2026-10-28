using UnityEngine;

public class BatDamage : MonoBehaviour
{
    public GoldCounter goldCounter;
    [Header("Bat Damage Settings")]
    public int damageAmount = 25;
    public string[] enemyTag = {"Dino", "TankDino", "SpeedDino"};
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start() {
        GameObject goldGO = GameObject.Find("Gold");

        goldCounter = goldGO.GetComponent<GoldCounter>();
    }

    private void OnTriggerEnter(Collider other) {
        if (isValidTag(other)) {
            
            DinoHealth enemy = other.GetComponent<DinoHealth>();

            if (enemy != null) {
                goldCounter.gold += 20;
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
