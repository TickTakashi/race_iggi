using System.Collections.Generic;
using Mirror;
using UnityEngine;

[DisallowMultipleComponent]
public class NetworkedPhysicsObject : MonoBehaviour
{
	public NetworkIdentity ParentID;
	public NetworkedPhysicsManager ParentNPM;
	public NetworkTransformChild NTC;
	public Rigidbody Rigid;

	public void Reset() {
		ParentID = gameObject.GetComponentInParent<NetworkIdentity>();
		ParentNPM = ParentID.GetComponent<NetworkedPhysicsManager>();
		NetworkTransform ParentNT = ParentID.GetComponent<NetworkTransform>();

		if (ParentNT) {
			Setup();
		} else {
			Debug.LogError("There is no network transform in parents. This is required.");
			DestroyImmediate(this);
		}

	}

	public void Setup() {
		if (ParentID && ParentNPM) {
			Rigid = GetComponent<Rigidbody>();
			if (!Rigid) {
				Debug.LogError("Object has no rigidbody. Please attach this directly to rigidbody gameobjects");
				DestroyImmediate(this);
				return;
			}

			if (ParentNPM.ManagedBodies == null) {
				ParentNPM.ManagedBodies = new List<Rigidbody>();
			}

			if (ParentNPM.ManagedBodies.Contains(GetComponent<Rigidbody>())) {
				Debug.LogWarning("A parent physics manager already contains this object! Nothing was done.");
				return;
			}

			Rigid.isKinematic = true;
			ParentNPM.ManagedBodies.Add(Rigid);
			NTC = ParentID.gameObject.AddComponent<NetworkTransformChild>();
			NTC.target = transform;
		} else {
			Debug.LogError("You've added a networked physics object, but its parents are missing essential networking components. Is this a mistake? (Ask Charles)");
			DestroyImmediate(this);
		}
	}
}
