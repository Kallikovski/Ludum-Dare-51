using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private void Awake()
    {
        GameManager.GameEnd += HandleGameEnd;
    }

    private void HandleGameEnd(int score)
    {
        Time.timeScale = 0;
        Debug.Log("Score: " + score.ToString());
        scoreText.text = "Score: " + score.ToString();
    }

    private void OnDestroy()
    {
        GameManager.GameEnd -= HandleGameEnd;
    }
}
