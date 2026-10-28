using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyGroup {
        [Tooltip("Select one of 3 enemy prefabs")]
        public GameObject enemyPrefab;
        [Tooltip("How many of this enemy to spawn this wave")]
        public int count;
        [Tooltip("Delay between each spawn of this enemy type")]
        public float spawnRate = 0.5f;
    }

    [System.Serializable]
    public class Wave {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public float timeBeforeNextWave = 5f;
    }

    [Header("Wave Setup")]
    public List<Wave> waves;
    public Transform spawnPoint;

    [Header("Difficulty Scaling")]
    [Tooltip("infinite waves after manual ones are complete")]
    public bool endlessMode = true;
    public GameObject[] allEnemyPrefabs;

    [Header("Wave Counter")]
    [SerializeField]private TextMeshProUGUI waveCounter;

    private int currentWaveIndex = 0;
    public bool isSpawning = false;


    void Start() {
        if (waves.Count > 0) {
            StartCoroutine(StartNextWave());
        }
    }

    IEnumerator StartNextWave() {
        isSpawning = true;
        Wave currentWave = waves[currentWaveIndex];
        waveCounter.text = $"Wave: {currentWave.waveName}";
        Debug.Log($"Starting {currentWave.waveName}");

        foreach (EnemyGroup group in currentWave.enemyGroups) {
            for (int i = 0; i < group.count; i++) {
                Instantiate(group.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                yield return new WaitForSeconds(group.spawnRate);
            }
        }

        isSpawning = false;
        currentWaveIndex++;

        if (currentWaveIndex < waves.Count) {
            yield return new WaitForSeconds(currentWave.timeBeforeNextWave);
            StartCoroutine(StartNextWave());
        }
        else if (endlessMode) {
            yield return new WaitForSeconds(5f);
            GenerateProceduralWave();
        }
        else {
            Debug.Log("All designed waves completed! you win!");
        }
    }

    void GenerateProceduralWave() {
        Wave newWave = new Wave();
        newWave.waveName = $"{currentWaveIndex + 1}";
        newWave.enemyGroups = new List<EnemyGroup>();

        int waveMultiplier = currentWaveIndex + 1;

        if (allEnemyPrefabs.Length >= 3) {
            newWave.enemyGroups.Add(new EnemyGroup {enemyPrefab = allEnemyPrefabs[0], 
            count = 5 + (waveMultiplier * 2), spawnRate = 0.4f});
            if (waveMultiplier >= 4) {
                newWave.enemyGroups.Add(new EnemyGroup {enemyPrefab = allEnemyPrefabs[1], 
                count = waveMultiplier, spawnRate = 0.6f});
            }

            if (waveMultiplier >= 7) {
                newWave.enemyGroups.Add(new EnemyGroup {enemyPrefab = allEnemyPrefabs[2], 
                count = waveMultiplier / 2, spawnRate = 1.0f});
            }
        }
        waves.Add(newWave);
        StartCoroutine(StartNextWave());
    }
}