using UnityEngine;

public class ConvertJoints : MonoBehaviour
{

	public void Start() {
		Debug.Log("Converting Character Joints to Configurable Joints...");
		int j = Convert(transform);
		Debug.Log("Done. Converted " + j + " joints");
	}


	public int Convert(Transform trans) {
		int j = 0;

		CharacterJoint cj = trans.GetComponent<CharacterJoint>();
		if (cj != null) {
			ConfigurableJoint conj = trans.gameObject.AddComponent<ConfigurableJoint>();
			//conj.SetupAsCharacterJoint();
			conj.connectedBody = cj.connectedBody;
			conj.connectedAnchor = cj.connectedAnchor;
			conj.axis = cj.axis;
			conj.secondaryAxis = cj.swingAxis;
			conj.lowAngularXLimit = cj.lowTwistLimit;
			conj.highAngularXLimit = cj.highTwistLimit;
			conj.enablePreprocessing = cj.enablePreprocessing;
			Destroy(cj);
			j++;
		}

		if (trans.childCount > 0) {
			foreach (Transform t in trans) {
				j += Convert(t);
			}
		}

		return j;
	}
}
