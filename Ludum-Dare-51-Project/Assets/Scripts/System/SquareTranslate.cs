using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTranslate : MonoBehaviour
{

    [SerializeField] private GameObject gameObstacle;
    [SerializeField] private float speed;
    [SerializeField] private Transform[] positions;
    [SerializeField] private int startPositionIndex = 0;
    private int currentPositionIndex = 0;

    private void Awake()
    {
        gameObstacle.transform.position = positions[startPositionIndex].position;
    }

    private void Update()
    {
        travelPositions();
    }

    private void travelPositions()
    {
        gameObstacle.transform.position = Vector3.MoveTowards(gameObstacle.transform.position, positions[currentPositionIndex].position, speed * Time.deltaTime);
        float distanceToNextPosition = (positions[currentPositionIndex].position - gameObstacle.transform.position).magnitude;
        if (distanceToNextPosition <= 0.2)
        {
            currentPositionIndex++;
            if (currentPositionIndex >= positions.Length)
            {
                currentPositionIndex = 0;
            }
        }
    }
}
