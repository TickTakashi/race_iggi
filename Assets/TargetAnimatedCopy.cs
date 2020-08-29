using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetAnimatedCopy : MonoBehaviour
{
	public Transform animatedCopy;
	public float springForce;
	public float dampening;
	public bool forcePassiveRotation = false;
	public float passiveRotationSpeed = 5f;
	public bool isAnimating = true;
	public ConfigurableJoint physicsRoot;
	public Transform targetRoot;

	private List<Tuple<Transform, Transform>> passiveBones;

	// Start is called before the first frame update
	void Start() {
		ResetCharacter();
	}

	//void OnValidate() {
	//	ResetCharacter();
	//}

	void ResetCharacter() {
		passiveBones = new List<Tuple<Transform, Transform>>();
		Setup(transform, animatedCopy);
	}

	private void Update() {
		if (forcePassiveRotation && isAnimating) {
			foreach (var pair in passiveBones) {
				pair.Item1.localRotation = Quaternion.Lerp(pair.Item1.localRotation, pair.Item2.localRotation, Time.deltaTime * passiveRotationSpeed);
			}
		}

		if (physicsRoot) {
			physicsRoot.targetPosition = targetRoot.position;
		}
	}

	void Setup(Transform trans, Transform copyTrans) {

		ConfigurableJoint cj = trans.GetComponent<ConfigurableJoint>();
		if (cj) {
			SetTarget(cj, copyTrans);
		} else if (trans != transform) {
			ForceTarget(trans, copyTrans);
		}

		for (int i = 0; i < trans.childCount; i++) {
			Setup(trans.GetChild(i), copyTrans.GetChild(i));
		}
	}

	// Update is called once per frame
	void SetTarget(ConfigurableJoint joint, Transform target) {
		if (!joint.GetComponent<JointTarget>()) {
			joint.gameObject.AddComponent<JointTarget>();
		}
		
		JointDrive jd = new JointDrive();
		jd.positionSpring = springForce;
		jd.maximumForce = float.MaxValue;
		jd.positionDamper = dampening;
		joint.angularXDrive = jd;
		joint.angularYZDrive = jd;
		joint.xDrive = jd;
		joint.yDrive = jd;
		joint.zDrive = jd;
		joint.slerpDrive = jd;

		JointTarget jt = joint.gameObject.GetComponent<JointTarget>();
		jt.cj = joint;
		jt.target = target;
	}

	void ForceTarget(Transform passiveBone, Transform passiveTarget) {
		passiveBones.Add(new Tuple<Transform, Transform>(passiveBone, passiveTarget));
	}
}
