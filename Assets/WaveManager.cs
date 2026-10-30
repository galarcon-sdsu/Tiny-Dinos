using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
/*
AI use: this is the most convoluded implementation in this game project.
I know little to nothing about procedural implementation and even less about
round based systems such as this one.
This script defines the ability to make custom rounds for the beginning before
optionally going into procedurally generated waves for an infinite mode that 
slowly gets harder and harder depending on difficulty multipliers and the 
prefabs that are given to it in the form of [easy, medium, hard] which works
with my implementation of my 3 dino prefabs
*/

public class WaveManager : MonoBehaviour
{
    [System.Serializable] //this is shown for each of the customizable waves that is specified by the user
    public class EnemyGroup {
        [Tooltip("Select one of 3 enemy prefabs")]
        public GameObject enemyPrefab;
        [Tooltip("How many of this enemy to spawn this wave")]
        public int count;
        [Tooltip("Delay between each spawn of this enemy type")]
        public float spawnRate = 0.5f;
    }

    [System.Serializable] //more customization paramteres
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

    [Header("Wave Counter")] //similar to the updating of the health, gold, and any counter, just counts up the waves
    [SerializeField]private TextMeshProUGUI waveCounter;

    private int currentWaveIndex = 0; //used for counting the waves
    public bool isSpawning = false;


    void Start() {
        if (waves.Count > 0) {
            StartCoroutine(StartNextWave());
        }
    }

    IEnumerator StartNextWave() {
        isSpawning = true;
        Wave currentWave = waves[currentWaveIndex]; //parameters for user designed wave
        waveCounter.text = $"Wave: {currentWave.waveName}";
        Debug.Log($"Starting {currentWave.waveName}");

        foreach (EnemyGroup group in currentWave.enemyGroups) { //gets the available group count to attach which number of what
            for (int i = 0; i < group.count; i++) {  //dino prefabs to be used for spawning in the wave
                Instantiate(group.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                yield return new WaitForSeconds(group.spawnRate);
            }
        }

        isSpawning = false;
        currentWaveIndex++;

        if (currentWaveIndex < waves.Count) { //start the new round based off user defined delay
            yield return new WaitForSeconds(currentWave.timeBeforeNextWave);
            StartCoroutine(StartNextWave());
        }
        else if (endlessMode) { //if out of user defined waves, use delay defined in procedural method
            yield return new WaitForSeconds(5f);
            GenerateProceduralWave();
        }
    }

    void GenerateProceduralWave() {
        Wave newWave = new Wave(); //start of a new wave
        newWave.waveName = $"{currentWaveIndex + 1}"; //counts up from previous wave
        newWave.enemyGroups = new List<EnemyGroup>(); //includes all parameters

        int waveMultiplier = currentWaveIndex + 1; //difficulty multiplier scaling

        if (allEnemyPrefabs.Length >= 3) { //when all prefabs are present
            newWave.enemyGroups.Add(new EnemyGroup {enemyPrefab = allEnemyPrefabs[0],  //spawns only the first prefab tll the defined threshold round
            count = 5 + (waveMultiplier * 2), spawnRate = 0.4f}); //multipliers define the amount of each prefab that appears
            if (waveMultiplier >= 4) { //threshold round is 4 to spawn the next defined prefab
                newWave.enemyGroups.Add(new EnemyGroup {enemyPrefab = allEnemyPrefabs[1],  //weights adjusted for more enemies for how many of each type
                count = waveMultiplier, spawnRate = 0.6f});
            }
            if (waveMultiplier >= 7) { //threshold round is 7 for the last prefab defined for the procedural method
                newWave.enemyGroups.Add(new EnemyGroup {enemyPrefab = allEnemyPrefabs[2],  //more difficulty adjustment
                count = waveMultiplier / 2, spawnRate = 1.0f});
            }
        }
        waves.Add(newWave); 
        StartCoroutine(StartNextWave()); //recursively calls itself and will continue to make increadingly difficult waves until the game is lost
    }
}