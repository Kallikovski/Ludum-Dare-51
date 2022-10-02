using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTranslate : MonoBehaviour
{

    [SerializeField] private GameObject gameObstacle;
    [SerializeField] private float speed;
    [SerializeField] private Transform[] position;
    [SerializeField] private int startPositionIndex = 0;
    private int currentPositionIndex = 0;

    private void Awake()
    {
        gameObject.transform.position = position[startPositionIndex].position;
    }

    private void Update()
    {
        travelPositions();
    }

    private void travelPositions()
    {
        gameObstacle.transform.localPosition = Vector3.MoveTowards(gameObstacle.transform.position, position[currentPositionIndex].position, speed * Time.deltaTime);
        float distanceToNextPosition = (position[currentPositionIndex].position - gameObstacle.transform.position).magnitude;
        if (distanceToNextPosition <= 0.2)
        {
            currentPositionIndex++;
            if (currentPositionIndex >= position.Length)
            {
                currentPositionIndex = 0;
            }
        }
    }
}
