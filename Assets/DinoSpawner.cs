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
            SpawnPrefab();
            timer = 0f;
        }
        
    }

    void SpawnPrefab() {
        if (prefabToSpawn != null) {
            Instantiate(prefabToSpawn, transform.position, transform.rotation);
        }
    }
}
