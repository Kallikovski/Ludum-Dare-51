using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float destructTime;
    [SerializeField] private Color endColor;
    [SerializeField] private GameObject obj;

    private Color startColor;
    private float timeleft;
    private void Start()
    {
        startColor = obj.GetComponent<Renderer>().material.color;
        timeleft = destructTime;
    }

    private void Update()
    {
        timeleft -= Time.deltaTime;
        obj.GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, timeleft);
        if(timeleft <= 0)
        {
            Destroy(gameObject);
        }
    }
}
