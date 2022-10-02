using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] enemySpawns;
    [SerializeField] private float spawnFrequency;

    private float adjustedSpawnFrequency;
    private float timeSinceLastSpawn = 0;

    private int difficultyLevel = 0;
    // Start is called before the first frame update
    private void Start()
    {
        Clock.IntervalUp += updateDifficultyLevel;
        adjustedSpawnFrequency = spawnFrequency;
    }

    // Update is called once per frame
    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= adjustedSpawnFrequency)
        {
            SpawnEnemy(enemyPrefabs[0]);
            chooseSpawnCase();
            timeSinceLastSpawn = 0;
        }
    }

    private void updateDifficultyLevel()
    {
        difficultyLevel++;
        adjustedSpawnFrequency -= adjustedSpawnFrequency / 20;
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Transform spawn = enemySpawns[Random.Range(0, enemySpawns.Length)];
        Instantiate(enemyPrefab, spawn);
    }

    private void chooseSpawnCase()
    {
        switch (difficultyLevel)
        {
            case 0:
                SpawnEnemy(enemyPrefabs[0]);
                break;
            case 1:
                SpawnEnemy(enemyPrefabs[0]);
                SpawnEnemy(enemyPrefabs[0]);
                break;
            case 2:
                SpawnEnemy(enemyPrefabs[0]);
                SpawnEnemy(enemyPrefabs[0]);
                SpawnEnemy(enemyPrefabs[1]);
                break;
            case 3:
                SpawnEnemy(enemyPrefabs[0]);
                SpawnEnemy(enemyPrefabs[1]);
                SpawnEnemy(enemyPrefabs[1]);
                break;
            case 4:
                SpawnEnemy(enemyPrefabs[0]);
                SpawnEnemy(enemyPrefabs[0]);
                SpawnEnemy(enemyPrefabs[1]);
                SpawnEnemy(enemyPrefabs[1]);
                break;
            case 5:
                SpawnEnemy(enemyPrefabs[0]);
                SpawnEnemy(enemyPrefabs[1]);
                SpawnEnemy(enemyPrefabs[2]);
                break;
            case 6:
                SpawnEnemy(enemyPrefabs[0]);
                SpawnEnemy(enemyPrefabs[1]);
                SpawnEnemy(enemyPrefabs[1]);
                SpawnEnemy(enemyPrefabs[2]);
                break;
            default:
                SpawnEnemy(enemyPrefabs[0]);
                SpawnEnemy(enemyPrefabs[1]);
                SpawnEnemy(enemyPrefabs[2]);
                SpawnEnemy(enemyPrefabs[2]);
                break;
        }
    }

    private void OnDestroyt()
    {
        Clock.IntervalUp -= updateDifficultyLevel;
    }
}
