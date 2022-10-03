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
    private bool wasWarned = false;

    public static event Action IntervalUp;
    public static event Action WarnIntervalUp;
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
            wasWarned = false;
            IntervalUp?.Invoke();
        }
        if (currentTime - 2 < 0 && wasWarned == false)
        {
            wasWarned = true;
            WarnIntervalUp?.Invoke();
        }
    }
}
