using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Vector3 offset;
    [SerializeField] public Transform target;
    [SerializeField] public float translateSpeed;
    [SerializeField] public float rotationSpeed;

    public void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }
   
    public void HandleTranslation()
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Slerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }
    public void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}