using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructSpawner : MonoBehaviour
{
    [SerializeField] private GameObject startConstructPrefabs;
    [SerializeField] private GameObject[] constructPrefabs;
    [SerializeField] private Transform[] constructSpawns;
    private GameObject[] constructs;
    private int nextSpawnIndex = 0;
    private void Start()
    {
        constructs = new GameObject[4];
        InitializeArena(startConstructPrefabs, constructSpawns);
        Clock.IntervalUp += SpawnConstruct;
    }

    private void SpawnConstruct()
    {
        GameObject constructPrefab = constructPrefabs[Random.Range(0, constructPrefabs.Length)];
        Debug.Log(constructPrefab);
        Destroy(constructs[nextSpawnIndex]);
        GameObject construct = Instantiate(constructPrefab, constructSpawns[nextSpawnIndex]);
        constructs[nextSpawnIndex] = construct;
        UpdateSpawnIndex();
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
}
