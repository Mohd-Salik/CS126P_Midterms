using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyAI : MonoBehaviour
{
    public Transform goal;
    public Transform body;
    public static bool enemy_win = false;
    float difference_x;
    float difference_y;
    NavMeshAgent agent;

    void Update(){
        agent = GetComponent<NavMeshAgent>();
        rushToGoal();
    }
    
    //Pathfinding AI to food, If player pressed space bar and food is not eaten then pathfind
    void rushToGoal(){
        if ((Player.start == true)&(FoodTrigger.food_eaten != true)){
            agent.SetDestination(goal.position);

            //find the distance between agent and AI
            difference_x = Math.Abs(agent.transform.position.x - goal.position.x);
            difference_y = Math.Abs(agent.transform.position.z - goal.position.z);
            Debug.Log("X: " + difference_x + "y: " + difference_y);
            if ((difference_x < 5) && (difference_y < 5)){
                enemy_win = true;
                agent.velocity = Vector3.zero;
                agent.isStopped = true;
            }
        }
    }
}
