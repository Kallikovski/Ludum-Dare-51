using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject GameOverMenu;

    public static event Action GameStart;

    private void Awake()
    {
        CharacterAction.GamePause += HandleGamePause;
        GameManager.GameEnd += HandleGameEnd;
        GameStart += HandleGameStart;
    }

    private void HandleGameStart()
    {
        Menu.SetActive(false);
        Time.timeScale = 0;
    }

    private void HandleGameEnd(int score)
    {
        GameOverMenu.SetActive(true);
    }

    private void HandleGamePause()
    {
        Debug.Log("Pause");
        if (PauseMenu.activeInHierarchy == true)
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void StartGame()
    {
        GameStart?.Invoke();
    }
}
