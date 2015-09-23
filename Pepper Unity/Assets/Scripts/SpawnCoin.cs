using UnityEngine;
using System.Collections;

public class SpawnCoin : MonoBehaviour {

	// Coin spawn settings
	public Rigidbody coinPrefab;
	public float vertDist = 3f;
	public float timer = 0.05f;
	public float playTimer = .5f;

	private float counter;
	private bool shouldPlay = false;
	private bool canSpawn = true;
	private AudioSource coinDrop;	

	void Start () {
		coinDrop = GetComponent<AudioSource>();
		counter = playTimer;
	}

	void Update () {
		
		#if UNITY_EDITOR
		
		if (Input.GetMouseButton(0)) {
			if (canSpawn) {
				Spawn();
				canSpawn = false;
			}
		} else if (Input.GetMouseButtonUp(0)) {
			canSpawn = true;
		}
		
		#endif
		
		if (Input.touchCount > 0) {
			// Using one-touch to activate spawning
			if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) {
				if (canSpawn) {
					Spawn();
					canSpawn = false;
				}
			}
		} else {
			canSpawn = true;
		}

		if (shouldPlay) {
			if (counter < 0f) {
				coinDrop.Play ();
				counter = playTimer;
				shouldPlay = false;
			}
			counter -= Time.deltaTime;
		}
	}

	void OnTriggerEnter (Collider other) {
		// Destroys coin a short while after it enters the piggy's belly
		if (other.gameObject != null) {
			Destroy(other.gameObject, timer);
		}
	}

	void Spawn() {

		Vector3 objCenter = transform.position;

		// Spawns the coin at the appropriate position to match the object
		Instantiate (coinPrefab, new Vector3(objCenter.x, vertDist, objCenter.z), Quaternion.identity);

		// Play a coin sound
		shouldPlay = true;
//		coinDrop.Play ();
	}
}
