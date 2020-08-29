using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{
	public ConfigurableJoint Hips;

	public float Speed = 5f;
	public float JumpHeight = 2f;
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
			Body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
		}
	}


	void FixedUpdate() {
		Vector3 relativeInputs = Quaternion.Euler(0, ForwardGuide.rotation.eulerAngles.y, 0) * _inputs;
		Body.MovePosition(Body.position + relativeInputs * Speed * Time.fixedDeltaTime);
		
		if (relativeInputs.magnitude > 0.01f) {
			Debug.DrawLine(transform.position, transform.position + relativeInputs.normalized * 8);
			Hips.SetTargetRotation(Quaternion.LookRotation(relativeInputs, Vector3.up), _cachedRotation);
		}
	}
}