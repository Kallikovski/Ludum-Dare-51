using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    private float angleToRotate = 360f;
    private float timeToRotate = 10000f;
    private float currentTime;
    private float stepAngle;

    public static event Action IntervalUp;
    private void Start()
    {
        currentTime = timeToRotate / 1000f;
        stepAngle = angleToRotate / (timeToRotate / 1000f);
    }
    private void FixedUpdate()
    {
        transform.RotateAround(transform.position, Vector3.up, stepAngle * Time.fixedDeltaTime);
    }
    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            currentTime = timeToRotate / 1000f;
            IntervalUp?.Invoke();
        }
    }
}
