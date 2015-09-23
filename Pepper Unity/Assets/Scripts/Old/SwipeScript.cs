using UnityEngine;
using System.Collections;

public class SwipeScript : MonoBehaviour {

	private float baseAngle = 0.0f;
	
	void OnMouseDown(){
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		pos = Input.mousePosition - pos;
		baseAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
		baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) *Mathf.Rad2Deg;
	}
	
	void OnMouseDrag(){
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		pos = Input.mousePosition - pos;
		float ang = Mathf.Atan2(pos.y, pos.x) *Mathf.Rad2Deg - baseAngle;
		transform.rotation = Quaternion.AngleAxis(ang, Vector3.forward);
	}
}



//using UnityEngine;
//using System.Collections;
//
//public class SwipeScript : MonoBehaviour {
//	
//	float f_lastX = 0.0f;
//	float f_difX = 0.5f;
//	float f_steps = 0.0f;
//	int i_direction = 1;
//	
//	// Use this for initialization
//	void Start () 
//	{
//		
//	}
//	
//	// Update is called once per frame
//	void Update () 
//	{
//		if (Input.GetMouseButtonDown(0))
//		{
//			f_difX = 0.0f;
//		}
//		else if (Input.GetMouseButton(0))
//		{
//			f_difX = Mathf.Abs(f_lastX - Input.GetAxis ("Mouse X"));
//			
//			if (f_lastX < Input.GetAxis ("Mouse X"))
//			{
//				i_direction = -1;
//				transform.Rotate(Vector3.up, -f_difX);
//			}
//			
//			if (f_lastX > Input.GetAxis ("Mouse X"))
//			{
//				i_direction = 1;
//				transform.Rotate(Vector3.up, f_difX);
//			}
//			
//			f_lastX = -Input.GetAxis ("Mouse X");
//		}
//		else 
//		{
//			if (f_difX > 0.5f) f_difX -= 0.05f;
//			if (f_difX < 0.5f) f_difX += 0.05f;
//			
//			transform.Rotate(Vector3.up, f_difX * i_direction);
//		}
//	}
//}