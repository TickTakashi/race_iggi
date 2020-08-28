using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBallsGenerator : MonoBehaviour
{

    public float minWidth = .15f;
    public float maxWidth = .4f;

    public float minForce = 5f;
    public float maxForce = 15f;

    private float width;
    private float force;

    void Start()
    {
        width = Random.Range(minWidth, maxWidth);
        Transform path = this.transform.GetChild(0);

        path.transform.localScale = new Vector3(width, path.transform.localScale.y, path.transform.localScale.z);
        
        Transform balls = this.transform.GetChild(1);
        foreach (Transform child in balls.transform)
        {
            force = Random.Range(minForce, maxForce);
            child.GetComponent<Rigidbody>().velocity = child.transform.right * force;
        }
               
    }

}
