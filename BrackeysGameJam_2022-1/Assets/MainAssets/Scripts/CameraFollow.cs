using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform target;

    [SerializeField] private float followSpeed;

    private Vector3 offset;


    private void Start()
    {
        offset = transform.position - target.position;
    }


    private void LateUpdate()
    {
        var targetPos = target.position + offset;
        
        // transform.position = targetPos;

        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);

    }
}
