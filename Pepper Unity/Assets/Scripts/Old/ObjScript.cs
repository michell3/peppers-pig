using UnityEngine;
using System.Collections;

public class ObjScript : MonoBehaviour {

	public float torque;
	public Rigidbody rb;

	void Start () {
		transform.position = new Vector3 (0, 0, 0);
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
	
	}

	void OnMouseDown() {
		float turn = Input.GetAxis ("Horizontal");
		rb.AddTorque (transform.up * torque * turn);
	}
}
