using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    void Update(){
        transform.Rotate(new Vector3(0f, 100, 100f) * Time.deltaTime);
    }
}
