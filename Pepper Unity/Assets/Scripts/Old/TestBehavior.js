#pragma strict

var swipeThresh    :   float = 2.0;
var swipeStart     : Vector2 = Vector2.zero;
var swipeCurrent   : Vector2 = Vector2.zero;
var swipeEnd       : Vector2 = Vector2.zero;
var swipeWasActive : boolean = false;

var lastX          : float = 0.0;
var difX           : float = 0.5;
var rotDirection   : int = 1;
 
function Update (){
    if ( Input.touchCount == 1 ) {
        processSwipe();
    }
}
// 
//function OnGUI () {
//    processSwipe();
//    drawStartBox();
//    drawEndBox();
//}
 
function processSwipe () {
    if ( Input.touchCount != 1 ) {
        return;
    }
 
    var theTouch : Touch = Input.touches[0];
 
    if ( theTouch.deltaPosition == Vector2.zero ) {
        return;
    }
 
    var speedVec : Vector2 = theTouch.deltaPosition * theTouch.deltaTime;
    var theSpeed :   float = speedVec.magnitude;
 
    var swipeIsActive : boolean = ( theSpeed > swipeThresh );
 
    if ( swipeIsActive ) {
 
        if ( ! swipeWasActive ) {
            difX = 0.0;
            swipeStart = theTouch.position;
        } else {
        	swipeCurrent = theTouch.position;
        
            difX = Mathf.Abs(lastX - swipeCurrent.x);
        
            if (lastX < swipeCurrent.x) {
                rotDirection = -1;
                transform.Rotate(Vector3.up, -difX);
            }
            
            if (lastX > swipeCurrent.x) {
                rotDirection = 1;
                transform.Rotate(Vector3.up, difX);
            }
            
            lastX = -swipeCurrent.x;
        }

    } else if ( swipeWasActive ) {
            swipeEnd = theTouch.position;
            Debug.Log("Swipe Complete");
    }
 
    swipeWasActive = swipeIsActive;
}
 
function drawStartBox () {

    if ( swipeStart == Vector2.zero ) {
        return;
    }
 
    /* don't forget to invert the y-coordinate */
 
    var theY = Screen.height - swipeStart.y;
    var theX = swipeStart.x;
 
    var theRect : Rect = Rect(theX, theY, 140, 40);
 
    GUI.Label(theRect, "Start");
}
 
function drawEndBox () {
    if ( swipeEnd == Vector2.zero ) {
        return;
    }
 
    /* don't forget to invert the y-coordinate */
 
    var theY = Screen.height - swipeEnd.y;
    var theX = swipeEnd.x;
 
    var theRect : Rect = Rect(theX, theY, 140, 40);
 
    GUI.Label(theRect, "End");
}