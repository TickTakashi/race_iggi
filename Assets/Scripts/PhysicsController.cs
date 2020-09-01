﻿using Cinemachine;
using Mirror;
using UnityEngine;

public class PhysicsController : NetworkBehaviour
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
	public CinemachineFreeLook ForwardGuidePrefab;
	
	private CinemachineFreeLook _forwardGuide;
	private Vector3 _inputs = Vector3.zero;
	private Quaternion _cachedRotation;

	private bool isGrounded => Physics.CheckSphere(GroundCheckOrigin.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);


	public override void OnStartClient() {
		base.OnStartClient();

		if (isLocalPlayer) {
			CinemachineFreeLook cfl = Instantiate(ForwardGuidePrefab);
			cfl.Follow = transform;
			cfl.LookAt = transform;
			
			_forwardGuide = cfl;

		}
	}


	[Client]
	void Update() {
		if (isLocalPlayer) {
			_inputs = Vector3.zero;
			_inputs.x = Input.GetAxis("Horizontal");
			_inputs.z = Input.GetAxis("Vertical");

			if (Input.GetButtonDown("Jump")) {
				CmdJump();
			}
		}
	}

	[Client]
	void FixedUpdate() {
		if (isLocalPlayer) {
			if (isGrounded) {
				Vector3 relativeInputs = Quaternion.Euler(0, _forwardGuide.State.CorrectedOrientation.eulerAngles.y, 0) * _inputs;

				if (relativeInputs.magnitude > 0.01f) {
					CmdMove(relativeInputs);
				}
			}
		}
	}

	public override void OnStartServer() {
		base.OnStartServer();
		Body.SetKinematicWithChildren(false);
		_cachedRotation = Hips.transform.rotation;
	}

	[Command]
	void CmdJump() {
		if (isGrounded) {
			Body.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
		}
	}

	[Command]
	void CmdMove(Vector3 relativeInputs) {
		if (isGrounded) {
			// Step 1: Find our Current velocty vector V
			// Step 2: Calculate our Desired velocity vector based on input D, and Speed S.
			// Step 3: Find Vector X such that V + X = D * S
			// Step 4: Apply X.normalized * Turning Force

			Vector3 deltaV = relativeInputs * Speed - Body.velocity;
			Body.AddForce(deltaV.normalized * TurningForce * relativeInputs.magnitude, ForceMode.Force);

			Debug.DrawLine(transform.position, transform.position + relativeInputs.normalized * 8);
			Hips.SetTargetRotation(Quaternion.LookRotation(relativeInputs, Vector3.up), _cachedRotation);
		}
	}


}