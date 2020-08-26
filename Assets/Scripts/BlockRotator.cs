﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRotator : MonoBehaviour
{

    public float speed = 150f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Vector3.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
       // transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}