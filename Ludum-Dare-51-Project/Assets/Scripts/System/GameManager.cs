using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawn;

    private int score = 0;

    //public static event Action GameStart;
    //public static event Action GamePause;
    public static event Action<int> GameEnd;

    private void Start()
    {
        Time.timeScale = 0;
        Health.UnitOnDeath += HandleUnitDeath;
        //GameEnd += HandleGameEnd;
        UIManager.GameStart += HandleGameStart;
    }
    private void HandleUnitDeath(GameObject gameObject)
    {
        if(gameObject.tag == "Player")
        {
            GameEnd?.Invoke(score);
        }
        else
        {
            score += 10;
        }

    }

    private void InitalizePlayer()
    {
        Instantiate(player, playerSpawn);
    }

    //private void HandleGameEnd()
    //{
    //    Time.timeScale = 0;
    //}
    private void HandleGameStart()
    {
        Time.timeScale = 1;
        InitalizePlayer();
    }

    private void OnDestroy()
    {
        //GameEnd -= HandleGameEnd;
        UIManager.GameStart -= HandleGameStart;
    }
}
