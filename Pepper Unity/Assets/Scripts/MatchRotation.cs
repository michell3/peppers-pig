using UnityEngine;
using System.Collections;

public class MatchRotation : MonoBehaviour {

	private GameObject target;

	void Start() {
		// Find the object to match rotation
		target = GameObject.FindGameObjectWithTag("Subject");
	}

	void Update () {
		// Make sure the target was not switched out
		if (target != null) {
			Vector3 euler = target.transform.rotation.eulerAngles;
			Quaternion rot = Quaternion.Euler (0, euler.y, 0);
			transform.rotation = rot;
		} else {
			Destroy (gameObject);
		}
	}
}
