using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float angleToRotate = 360f;
    [SerializeField] private float timeToRotate = 5000f;
    private float stepAngle;
    private void Start()
    {
        stepAngle = angleToRotate / (timeToRotate / 1000f);
    }
    private void FixedUpdate()
    {
        transform.RotateAround(transform.position, Vector3.up, stepAngle * Time.fixedDeltaTime);
    }
}
