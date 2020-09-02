using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class BasicMovement : NetworkBehaviour
{
	public Rigidbody rigid;
	public float force = 300;
	public float jumpForce = 100;
	public float maxSpeed = 3f;
	public Renderer rend;

	[Server]
	void Start() {
		rigid.isKinematic = false;
	}

	public override void OnStartClient() {
		base.OnStartClient();

		if (isLocalPlayer) {
			rend.material.color = Color.blue;
		} else {
			rend.material.color = Color.red;
		}
	}

	void FixedUpdate() {
		if (!isLocalPlayer) {
			return;
		}

		Vector3 moveDelta = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
		
		CmdMove(moveDelta);

	}

	private void Update() {
		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetButtonDown("Jump")) {
			CmdJump();
		}
	}

	[Command]
	void CmdMove(Vector3 delta) {
		if (Vector3.Dot(rigid.velocity, delta.normalized) < maxSpeed) {
			rigid.AddForce(delta * force);
			RpcMove(delta);
		}
	}

	[Command]
	void CmdJump() {
		bool canJump = true;

		if (canJump) {
			rigid.AddForce(transform.up * jumpForce, ForceMode.Impulse);
		}
	}

	[ClientRpc]
	void RpcMove(Vector3 delta) {
		// ?
	}
}
