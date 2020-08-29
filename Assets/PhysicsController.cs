using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PhysicsController : MonoBehaviour
{
	public ConfigurableJoint Hips;

	public float Speed = 5f;
	public float TurningForce = 30f;
	public float JumpForce = 2f;
	public float GroundDistance = 0.2f;
	public LayerMask Ground;
	public Transform GroundCheckOrigin;
	public Rigidbody Body;

	[Tooltip("The transform that input is relative to, usually the 3rd person camera.")]
	public Transform ForwardGuide;

	private Vector3 _inputs = Vector3.zero;
	private bool _isGrounded = true;
	private Quaternion _cachedRotation;
	private void Start() {
		_cachedRotation = Hips.transform.rotation;
	}

	void Update() {
		_isGrounded = Physics.CheckSphere(GroundCheckOrigin.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

		_inputs = Vector3.zero;
		_inputs.x = Input.GetAxis("Horizontal");
		_inputs.z = Input.GetAxis("Vertical");

		if (Input.GetButtonDown("Jump") && _isGrounded) {
			Body.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
		}
	}


	void FixedUpdate() {
		if (_isGrounded) {
			Vector3 relativeInputs = Quaternion.Euler(0, ForwardGuide.rotation.eulerAngles.y, 0) * _inputs;

			// Step 1: Find our velocty vector V
			// Step 2: Calculate our "Desired" velocity vector based on input, D.
			// Step 3: Find Vector X such that V + X = D
			// Step 4: Apply X.normalized * Acceleration
			if (relativeInputs.magnitude > 0.01f) {
				Vector3 deltaV = relativeInputs * Speed - Body.velocity;
				Body.AddForce(deltaV.normalized * TurningForce * relativeInputs.magnitude, ForceMode.Force);
				//Body.MovePosition(Body.position + relativeInputs * Speed * Time.fixedDeltaTime);

				Debug.DrawLine(transform.position, transform.position + relativeInputs.normalized * 8);
				Hips.SetTargetRotation(Quaternion.LookRotation(relativeInputs, Vector3.up), _cachedRotation);
			}
		}
	}
}