using UnityEngine;

public class DinoSpawner : MonoBehaviour
{
    [Tooltip("The prefab you want to spawn.")]
    public GameObject prefabToSpawn;

    [Tooltip("Time in seconds between each spawn.")]
    public float spawnInterval = 3f;

    private float timer;

    void Update() {
        timer += Time.deltaTime;
        if (timer >= spawnInterval) {
            SpawnPrefab(); //when the timer reaches the delay time it spawns the prefab
            timer = 0f; //reset the timer for the interval reset
        }
        
    }

    void SpawnPrefab() {
        if (prefabToSpawn != null) {
            Instantiate(prefabToSpawn, transform.position, transform.rotation); //instantiates the prefab at the gameobject this script is attached to
        }
    }
}
