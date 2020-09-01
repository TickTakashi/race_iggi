using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public float bulletSpeed;
	public Rigidbody reusableBullet;
	public float timer;
	public bool isFiring;

	// Start is called before the first frame update
	void Start() {
		StartCoroutine(Fire());
	}

	IEnumerator Fire() {
		float lastShot = Time.time;
		while (true) {
			if (isFiring && (Time.time - lastShot) > timer) {
				lastShot = Time.time;

				reusableBullet.position = transform.position;
				reusableBullet.angularVelocity = Vector3.zero;
				reusableBullet.velocity = transform.forward * bulletSpeed;
			}

			yield return null;
		}
	}
}
