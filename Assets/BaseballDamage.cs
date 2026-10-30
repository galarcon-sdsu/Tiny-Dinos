using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseballDamage : MonoBehaviour
{
    [Header("Baseball Damage Settings")]
    public int damageAmount = 8; //how much damage each baseball object will do
    public string[] enemyTag = {"Dino", "TankDino", "SpeedDino"}; //tags of valid enemy types
    public GoldCounter goldCounter; //reference to gold counter for updating gold
    public AudioClip launchEffect; //supporting assets for audio
    public AudioClip hitEffect;

    void Start() {
        AudioSource.PlayClipAtPoint(launchEffect, transform.position);
        GameObject goldGO = GameObject.Find("Gold"); //getting necessary references to objects that have interactions with baseballs
        goldCounter = goldGO.GetComponent<GoldCounter>();
    }

    private void OnTriggerEnter(Collider other) {
        if (isValidTag(other)) {
            DinoHealth enemy = other.GetComponent<DinoHealth>(); //call to get the dinosaur health script reference
            if (enemy != null) {
                AudioSource.PlayClipAtPoint(hitEffect, transform.position); //play hit sound
                enemy.TakeDamage(damageAmount); //reduce hit enemy health by amt of damage done
                goldCounter.gold += 8; //add gold to total gold
                Destroy(gameObject); //delete the baseball that hit 
            }
        }
    }

    private bool isValidTag(Collider coll) { //supporting function that checks if collided
        for (int i = 0; i < enemyTag.Length; i++) { //object is part of the valid enemies
            if (!string.IsNullOrEmpty(enemyTag[i]) && coll.CompareTag(enemyTag[i])) {
                return true;
            } //simply returns boolean if it is valid
        }
        return false;
    }
}
