#pragma strict

var speed : float = 3.0;
var rotateSpeed : float = 10.0;
private var isTouchDevice : boolean = false;

var lastX : float = 0.0;
var difX : float = 0.5;
var scale : float = 0.1;
var direction : int = 1;

function Awake() {
	if (Application.platform == RuntimePlatform.IPhonePlayer) 
		isTouchDevice = true; 
	else
		isTouchDevice = false; 
}

function Update () {
	
	var clickDetected : boolean;
	var clickHold : boolean;
	var touchPosition : Vector3;
	var inputX : float;
	
	// Detect click and calculate touch position
	if (isTouchDevice) {
		clickDetected = (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
		clickHold = (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved);
		touchPosition = Input.GetTouch(0).position;
//		Debug.Log(Input.GetTouch(0).position.x);
//		if (clickDetected || clickHold) {
//			inputX = Input.GetTouch(0).position.x * scale;
//		} else {
//			inputX = 0.0;
//		}
	} else {
		clickDetected = (Input.GetMouseButtonDown(0));
		clickHold = (Input.GetMouseButton(0));
		touchPosition = Input.mousePosition;
//		inputX = Input.GetAxis("Mouse X");
	}
	
	
	
	// Detect clicks
	if (clickDetected) {        
//		// Check if the GameObject is clicked by casting a
//		// Ray from the main camera to the touched position.
//		var ray : Ray = Camera.main.ScreenPointToRay 
//			(touchPosition);
//		var hit : RaycastHit;
//		// Cast a ray of distance 100, and check if this
//		// collider is hit.
//		if (GetComponent.<Collider>().Raycast (ray, hit, 100.0)) {
//			// Log a debug message
//			Debug.Log("Moving the target");
//			// Move the target forward
//			transform.Translate(Vector3.forward * speed);       
//			// Rotate the target along the y-axis
//			transform.Rotate(Vector3.up * rotateSpeed);
		difX = 0.0;
//		} else {
//			// Clear the debug message
//			Debug.Log("");
//		}
	} else if (clickHold) {
		
		difX = Mathf.Abs(lastX - inputX);
		
		Debug.Log("lastX: " + lastX + " inputX " + inputX);
		
		if (lastX < inputX) {
			direction = -1;
			transform.Rotate(Vector3.up, -difX);
		}
		
		if (lastX > inputX) {
			direction = 1;
			transform.Rotate(Vector3.up, difX);
		}
		
		lastX = -inputX;
		
	} else {
		if (difX > 0.5) difX -= 0.05;
		if (difX < 0.5) difX += 0.05;
		
		transform.Rotate(Vector3.up, difX * direction);
	}
}
