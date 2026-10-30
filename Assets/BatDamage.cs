using UnityEngine;

public class BatDamage : MonoBehaviour
{
    public GoldCounter goldCounter;
    [Header("Bat Damage Settings")]
    public int damageAmount = 10; //similar setup to baseball damage
    public string[] enemyTag = {"Dino", "TankDino", "SpeedDino"}; //list of valid enemy tags

    [Header("Sound Effect")]
    [SerializeField] private AudioSource audioSource; //necessary variables for sound fx
    [SerializeField] private AudioClip soundEffect;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        GameObject goldGO = GameObject.Find("Gold"); //get necessary components for updating
        goldCounter = goldGO.GetComponent<GoldCounter>();
    }

    private void OnTriggerEnter(Collider other) {
        if (isValidTag(other)) { //only enters if collision detected is a valid enemy
            DinoHealth enemy = other.GetComponent<DinoHealth>(); //grab collided enemy health for updating
            if (enemy != null) {
                if (soundEffect != null && audioSource != null) {
                    audioSource.PlayOneShot(soundEffect); //play damage noise
                }
                goldCounter.gold += 12; //add gold
                enemy.TakeDamage(damageAmount); //update enemy's health
            }
        }
    }

    private bool isValidTag(Collider coll) { //same as with baseball, supporting function used to
        for (int i = 0; i < enemyTag.Length; i++) { //check if collided object is valid
            if (!string.IsNullOrEmpty(enemyTag[i]) && coll.CompareTag(enemyTag[i])) {
                return true;
            }
        }
        return false;
    }
}
