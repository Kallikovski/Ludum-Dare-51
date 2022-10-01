using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Health health = null;
    [SerializeField] private Image healthBarImage = null;
    // Start is called before the first frame update

    private void Update()
    {
        int currentHealth = health.GetCurrentHealth();
        int maxHealth = health.GetMaxHealth();
        healthBarImage.fillAmount = (float)currentHealth / maxHealth;
    }

}
