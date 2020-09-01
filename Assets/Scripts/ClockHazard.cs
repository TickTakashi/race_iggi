using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class ClockHazard : NetworkBehaviour
{
	public Rigidbody ClockHand;
	public float spinSpeed;
	public float acceleration;
	public float speedTolerance = 0.9f;

	public void Start() {
		ClockHand.centerOfMass = Vector3.zero;
	}

	[ServerCallback]
	private void FixedUpdate() {
		if (ClockHand.angularVelocity.y < speedTolerance * spinSpeed * Mathf.Deg2Rad) {
			ClockHand.AddTorque(0, acceleration * Mathf.Deg2Rad, 0, ForceMode.Acceleration);
		}
	}
}


