using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemTest : MonoBehaviour
{
    public GameObject goTo;
    public NavMeshAgent agent;

    private void Update()
    {
        agent.SetDestination(goTo.transform.position);
    }
}
