using Mirror;
using UnityEngine;

public class SpinnerGenerator : NetworkBehaviour
{
	public Rigidbody[] Spinners;

	public float minForce = 5f;
	public float maxForce = 15f;

	public override void OnStartServer() {
		base.OnStartServer();
		Setup();
	}

	void Setup() {
		foreach (Rigidbody rb in Spinners) {
			rb.angularVelocity = rb.transform.right * Random.Range(minForce, maxForce);
		}
	}
}