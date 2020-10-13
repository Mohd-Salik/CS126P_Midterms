using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTrigger : MonoBehaviour
{
    public static bool food_eaten = false;

    //Rotation animation
    void Update(){
        transform.Rotate(new Vector3(0f, 0f, 100f) * Time.deltaTime);
    }

    //If something collided with food, then the food is eaten
    void OnTriggerEnter(Collider other){
        food_eaten = true;
    }
}
