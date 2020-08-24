using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; // defining the player move speed, adjustable in the inspector
    // public Rigidbody RbOne; // asigning a rigid body to the object in the inspector, removed as this is has some issues with the physics
    public float jumpForce;
    public CharacterController controller;
    private Vector3 moveDirection;
    public float gravityScale;

       
    // Start is called before the first frame update
    void Start()
    {
        // RbOne = GetComponent<Rigidbody>(); // immediately calling the rigid body in the inspector 
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        /* RbOne.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, RbOne.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
        // telling the game that the rigid bodies velocity on x and y is defined by wasd input

        if (Input.GetButtonDown("Jump"))
        {
            RbOne.velocity = new Vector3(RbOne.velocity.x, jumpForce, RbOne.velocity.z); 
        } */

        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0f, Input.GetAxis("Vertical") * moveSpeed);
      

        if (Input.GetButtonDown("Jump"))

        { moveDirection.y = jumpForce;
            
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        controller.Move(moveDirection * Time.deltaTime);

    }
}
