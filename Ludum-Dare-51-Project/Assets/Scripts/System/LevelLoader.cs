using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;

    [SerializeField] private float transitionTime = 2;
    public void LoadGameLevel()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        transition.SetTrigger("Start");
        Time.timeScale = 1f;
        yield return new WaitForSeconds(transitionTime);
        Debug.Log("Game");
        
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMenuLevel()
    {
        StartCoroutine(LoadMenu());
    }

    IEnumerator LoadMenu()
    {
        transition.SetTrigger("Start");
        Time.timeScale = 1f;
        yield return new WaitForSeconds(transitionTime);
        Debug.Log("Menu");
        
        SceneManager.LoadScene("MenuScene");
    }
}
