using UnityEngine;

public class JointTarget : MonoBehaviour
{
	public ConfigurableJoint cj;
	public Transform target;

	private Quaternion initialRotation;

	public void Start() {
		if (cj == null)
			cj = GetComponent<ConfigurableJoint>();


		initialRotation = cj.transform.localRotation;
	}

	void Update() {
		cj.SetTargetRotationLocal(target.transform.localRotation, initialRotation);
	}
}
