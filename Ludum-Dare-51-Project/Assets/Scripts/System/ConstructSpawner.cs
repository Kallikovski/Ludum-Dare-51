using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructSpawner : MonoBehaviour
{
    [SerializeField] private GameObject startConstructPrefabs;
    [SerializeField] private GameObject warnConstructPrefabs;
    [SerializeField] private GameObject[] constructPrefabs;
    [SerializeField] private Transform[] constructSpawns;
    private GameObject[] constructs;
    private int nextSpawnIndex = 0;
    private void Start()
    {
        constructs = new GameObject[4];
        InitializeArena(startConstructPrefabs, constructSpawns);
        Clock.IntervalUp += SpawnConstruct;
        Clock.WarnIntervalUp += SpawnWarnConstruct;
    }

    private void SpawnConstruct()
    {
        GameObject constructPrefab = constructPrefabs[Random.Range(0, constructPrefabs.Length)];
        while(constructPrefab == constructs[nextSpawnIndex])
        {
            constructPrefab = constructPrefabs[Random.Range(0, constructPrefabs.Length)];
        }
        Debug.Log(constructPrefab);
        Destroy(constructs[nextSpawnIndex]);
        GameObject construct = Instantiate(constructPrefab, constructSpawns[nextSpawnIndex]);
        constructs[nextSpawnIndex] = construct;
        UpdateSpawnIndex();
    }

    private void SpawnWarnConstruct()
    {
        GameObject warnConstruct = Instantiate(warnConstructPrefabs, constructSpawns[nextSpawnIndex].position + new Vector3(0f,1f,0f), constructSpawns[nextSpawnIndex].rotation);
    }

    private void UpdateSpawnIndex()
    {
        nextSpawnIndex++;
        if(nextSpawnIndex >= constructSpawns.Length)
        {
            nextSpawnIndex = 0;
        }
    }

    private void InitializeArena(GameObject prefab, Transform[] spawns)
    {
        for (int i = 0; i < constructSpawns.Length; i++)
        {
            GameObject construct = Instantiate(prefab, spawns[i]);
            constructs[i] = construct;
        }
    }

    private void OnDestroy()
    {
        Clock.IntervalUp -= SpawnConstruct;
        Clock.WarnIntervalUp -= SpawnWarnConstruct;
    }
}
