using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	public float timer = 1.0f;

	void Start () {
		Destroy (gameObject, timer);
	}
}
