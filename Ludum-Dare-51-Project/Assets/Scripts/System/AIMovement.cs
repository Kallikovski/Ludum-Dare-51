using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    private GameObject target = null;
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log(GameObject.FindGameObjectsWithTag("Player").Length);
        if(GameObject.FindGameObjectsWithTag("Player").Length != 0)
        {
            target = GameObject.FindGameObjectsWithTag("Player")[0];
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }
}
