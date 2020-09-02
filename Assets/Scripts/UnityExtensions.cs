using UnityEngine;

public static class UnityExtensions
{
	public static void SetKinematicWithChildren(this Rigidbody rigid, bool isKinematic) {
		foreach (Rigidbody rb in rigid.GetComponentsInChildren<Rigidbody>())
			rb.isKinematic = isKinematic;
	}
}
