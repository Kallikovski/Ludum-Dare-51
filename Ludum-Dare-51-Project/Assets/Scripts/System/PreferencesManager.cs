using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferencesManager : MonoBehaviour
{
    private const string volumeKey = "Volume";
    private const float volumeDefault = 1.0f;
    [Range(0.0f, 9.0f)]
    public float volume = 1.0f;

    private void Start()
    {
        LoadPrefs();
    }

    private void OnApplicationQuit()
    {
        SavePrefs();
    }

    public void LoadPrefs()
    {
        volume = PlayerPrefs.GetFloat(volumeKey, volumeDefault);
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetFloat(volumeKey, volume);
        PlayerPrefs.Save();
    }
}
