using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAnimation : MonoBehaviour
{   
    int counter = 50;
    bool clockwise = true;

    void move(){
        if (clockwise == true){
            if (counter == 0){
                clockwise = false;
                counter = 500;
            }else{
                transform.position = new Vector3((transform.position.x + .1f), transform.position.y, transform.position.z);
                counter --;
            }
        }else{
            if (counter == 0){
                clockwise = true;
                counter = 500;
            }else{
                transform.position = new Vector3((transform.position.x - .1f), transform.position.y, transform.position.z);
                counter --;
            } 
        }
        
    }
    void Update()
    {   
        
        Invoke("move", .2f);
    }
}
