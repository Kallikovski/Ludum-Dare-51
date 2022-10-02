using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISettings : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    [SerializeField] private GameObject settings;

    public void OpenSetting()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }

    public void CloseSetting()
    {
        menu.SetActive(true);
        settings.SetActive(false);
    }
}
