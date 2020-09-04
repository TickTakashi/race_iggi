using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativePlayerController : MonoBehaviour
{
    public float jumpForce;
    public float moveSpeed;
    public Rigidbody playerBody;

    private Vector3 inputVector;


    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, 0f, Input.GetAxisRaw("Vertical") * moveSpeed);
        playerBody.AddForce(inputVector);

        
    }

    private void Update()
    {
        float disstanceToTheGround = GetComponent<Collider>().bounds.extents.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, disstanceToTheGround + 0.1f); // if the distance to the ground is 0 (+/- 1) as determined by the raycast to the ground then is grounded is true
        Debug.Log(isGrounded);

        if (Input.GetButtonDown("Jump") && isGrounded) // if key is pressed and 'is grounded' is true then the player should be able to jump

        {
            inputVector = Vector3.up * jumpForce;
            playerBody.AddForce(inputVector, ForceMode.Impulse);

        }
    }
}