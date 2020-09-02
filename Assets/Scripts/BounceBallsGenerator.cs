using Mirror;
using UnityEngine;

public class BounceBallsGenerator : NetworkBehaviour
{
	public Transform BallContainer;
	public Transform Path;

	public float minWidth = .15f;
	public float maxWidth = .4f;

	public float minForce = 5f;
	public float maxForce = 15f;

	public override void OnStartServer() {
		base.OnStartServer();
		Setup();
	}

	void Setup() {
		Path.transform.localScale = new Vector3(Random.Range(minWidth, maxWidth), Path.transform.localScale.y, Path.transform.localScale.z);

		foreach (Transform child in BallContainer.transform) {
			child.GetComponent<Rigidbody>().velocity = child.transform.right * Random.Range(minForce, maxForce);
		}
	}
}
