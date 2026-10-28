using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseballDamage : MonoBehaviour
{   
    public GoldCounter goldCounter;
    [Header("Baseball Damage Settings")]
    public int damageAmount = 15;
    public string[] enemyTag = {"Dino", "TankDino", "SpeedDino"};

    


    void Start() {
        GameObject goldGO = GameObject.Find("Gold");

        goldCounter = goldGO.GetComponent<GoldCounter>();
    }

    private void OnTriggerEnter(Collider other) {
        if (isValidTag(other)) {
            
            DinoHealth enemy = other.GetComponent<DinoHealth>();

            if (enemy != null) {
                enemy.TakeDamage(damageAmount);
                goldCounter.gold += 20;
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
