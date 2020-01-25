﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject focalObj;
    public float camSpeed = 1;

    void Update()
    {
        //Stick to focal point
        transform.position = focalObj.transform.position;
        //transform.rotation = Quaternion.Lerp(transform.rotation, focalObj.transform.rotation, Time.deltaTime);
        transform.rotation = focalObj.transform.rotation;
    }
}
