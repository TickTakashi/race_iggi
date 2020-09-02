using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkedPhysicsManager : NetworkBehaviour
{
	public List<Rigidbody> ManagedBodies;

	public override void OnStartServer() {
		base.OnStartServer();
		foreach(Rigidbody r in ManagedBodies)
			r.isKinematic = false;
	}
}