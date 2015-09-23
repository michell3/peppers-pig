using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour {

	// Gesture settings
	public float maxSpeed = 5.0f;
	public float scaleSpeed = 0.1f;
	public float threshold = 0.1f;
	public float step = 0.02f;

	private float lastX = 0.0f;
	private float lastY = 0.0f;
	private float diff = 0.5f;
	private float difX = 0.5f;
	private float difY = 0.5f;
	private int direction = 1;
	
	void Update () {
		
		if (Input.touchCount > 0) {
			
			if (Input.GetTouch(0).phase == TouchPhase.Began) {
				// Reset difX when a touch begins
				difX = 0.0f;
				
			} else if (Input.GetTouch(0).phase == TouchPhase.Moved) {

				// Calculate the rotational speed based on input position
				float inputX = Input.GetTouch(0).position.x;
				float inputY = Input.GetTouch(0).position.y;

				difX = Mathf.Min(maxSpeed, Mathf.Abs(lastX - inputX) * scaleSpeed);
				difY = Mathf.Min(maxSpeed, Mathf.Abs(lastY - inputY) * scaleSpeed);

				// Speed depends on direction of swipe since the screen is in
				// four quadrants
				if (inputX > inputY) {
					// Right triangle
					if (inputX > Screen.height - inputY) {
						diff = difY;
						if (lastY < inputY) {
							direction = 1;
						} else {
							direction = -1;
						}

					} else { // Bottom triangle
						diff = difX;
						if (lastX < inputX) {
							direction = 1;
						} else {
							direction = -1;
						}
					}

				} else {
					// Top triangle
					if (inputX > Screen.height - inputY) {
						diff = difX;
						if (lastX < inputX) {
							direction = -1;
						} else {
							direction = 1;
						}

					} else { // Left triangle
						diff = difY;
						if (lastY < inputY) {
							direction = -1;
						} else {
							direction = 1;
						}
					}
				}

				transform.Rotate (Vector3.up, direction * diff);
				lastX = inputX;
				lastY = inputY;
			}
			
		} else {
			// Rotate object without input
			if (diff > threshold) diff -= step;
			if (diff < threshold) diff += step;
			
			transform.Rotate(Vector3.up, diff * direction);
		}
	}
}