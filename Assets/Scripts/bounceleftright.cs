using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceleftright : MonoBehaviour { 

    public float ballforce = 200;
    private Rigidbody rb; 

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * ballforce;
    }
     
}
