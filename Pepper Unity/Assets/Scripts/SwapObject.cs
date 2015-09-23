using UnityEngine;
using System.Collections;

public class SwapObject : MonoBehaviour {

	// GameObject presets
	public Vector3 objCenter = new Vector3 (0f, 0.53f, 0f);
	public GameObject pig1;
	public GameObject pig2;
	public GameObject pig3;
	public GameObject pig4;

	private GameObject[] pigPrefabs= new GameObject[4];
	private int pigIndex = 0;

	// Variables for shrinking/growing animation
	public float targetScaleUp = .11f;
	public float targetScaleDown = 0.01f;
	public float speed = 0.15f;
	
	private bool shrinkingS = false;
	private bool growingS = true;
	private bool shrinkingG = false;
	private bool growingG = false;

	void Start () {
		// Create an array to index pigs easily
		pigPrefabs = new GameObject[] {pig1, pig2, pig3, pig4};

		// Make a new pig if none exist
		if (GameObject.FindGameObjectsWithTag ("Subject").Length == 0) {
			NewPig ();
		}
	}

	void Update () {
		
		#if UNITY_EDITOR
		
		if (Input.GetKeyDown("space")) {
			if (!shrinkingS) {
				shrinkingS = true;
			}
		}
		// Shrinking animation for dying pig
		if (shrinkingS) {
			ShrinkPig ();
		}
		// Growing animation for new pig
		if (growingG) {
			GrowPig();
		}
		
		#endif

		// Using two-touch input to activate swap-out
		if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Began) {
			if (!shrinkingS) {
				shrinkingS = true;
			}
		}
		// Shrinking animation for dying pig
		if (shrinkingS) {
			ShrinkPig();
		}
		// Growing animation for new pig
		if (growingG) {
			GrowPig();
		}
	}
	
	void NewPig() {
		// Destroy the current pig if it exists
		GameObject currentPig = GameObject.FindGameObjectWithTag ("Subject");
		if (currentPig != null) {
			Destroy (currentPig);
		}

		// Instantiate the pig from the next index
		currentPig = Instantiate (pigPrefabs[pigIndex], objCenter, Quaternion.identity) as GameObject;
		// Start at the minimum scale for animation
		if (growingG) {
			currentPig.transform.localScale = Vector3.one * targetScaleDown;
		}

		// Increment the index
		pigIndex = (pigIndex + 1) % pigPrefabs.Length;
	}

	void ShrinkPig() {

		GameObject currentPig = GameObject.FindGameObjectWithTag ("Subject");

		// The pig grows slightly before it shrinks away
		if (growingS) {
			currentPig.transform.localScale += Vector3.one * Time.deltaTime * speed;

			if (currentPig.transform.localScale.x > targetScaleUp) {
				growingS = false;
			}

		} else {
		
			currentPig.transform.localScale -= Vector3.one * Time.deltaTime * speed;

			if (currentPig.transform.localScale.x < targetScaleDown) {
				shrinkingS = false;
				growingS = true;
				growingG = true;
				NewPig();
			}
		}
	}

	void GrowPig() {

		GameObject currentPig = GameObject.FindGameObjectWithTag ("Subject");

		// The pig expands slightly too much and then settles to the right scale
		if (shrinkingG) {
			currentPig.transform.localScale -= Vector3.one * Time.deltaTime * speed;

			if (currentPig.transform.localScale.x < 0.1f) {
				shrinkingG = false;
				growingG = false;
			}
		} else {

			currentPig.transform.localScale += Vector3.one * Time.deltaTime * speed;

			if (currentPig.transform.localScale.x > targetScaleUp) {
				shrinkingG = true;
			}
		}
	}
}
