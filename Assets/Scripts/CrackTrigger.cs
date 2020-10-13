using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackTrigger : MonoBehaviour
{
    public Transform target;
    public GameObject player;

    void OnTriggerEnter(Collider other){
        if (Player.level != 0){
            Debug.Log ("went in the crack");
            player.transform.position = target.transform.position;
            Player.stop_movement = true;
            RaceStatus.onstart_point = true;
        }
    }
}
