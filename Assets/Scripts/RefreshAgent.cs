using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class RefreshAgent : MonoBehaviour
{
    NavMeshAgent agent;

    public static bool refresh = false;
    void Update()
    {
        agent = GetComponent<NavMeshAgent>();
        if (refresh == true){
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
        }
    }
}
