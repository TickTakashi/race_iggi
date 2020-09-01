using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public Transform target; // calls the transform attached to the camera into the script
    public float rotateSpeed;
    public Vector3 offset; // creates a vector3 named offset
    public bool useOffsetValues; // creates a true/false variable that lets us change the offset values in the scene
    

    // Start is called before the first frame update
    void Start()
    {
        if (!useOffsetValues)
        {
            offset = transform.position - target.position; // defines the offset as the difference between the player and camera. 
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed; // defines a float that equals the players mouse movement across x multiplied by the rotateSpeed float
        target.Rotate(0, horizontal, 0);  // rotate only the horizontal access

        float desiredYAngle = target.eulerAngles.y; // defines a float for the desiredYngle as equal to the quanternion translation version of the y angle?

        // Quaternion rotation = Quaternion.Euler(0, desiredYAngle, 0); 
        // transform.position = target.position - (rotation * offset);

        // transform.position = target.position - offset; // defines the transform position as the target versus the difference between the target and camera. 

        transform.position = target.TransformPoint(offset);
    
        transform.LookAt(target); // transforms the camera to look at the target

       }
}
