using UnityEngine;
using System.Collections;

public class SwipeController : MonoBehaviour {

	int screenWidth;

	/// <summary>
	/// The width of the center area in which the player will only move forward (in pixels)
	/// </summary>
	[Tooltip("The width of the center area in which the player will only move forward (in pixels)")]
	public float centerWidth = 200f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Mouse0)) {
			CheckTouchPosition ();
		}
	}

	/// <summary>
	/// Returns -1 for left, 0 for center and 1 for right
	/// </summary>
	public void CheckTouchPosition() {
		
		screenWidth = Screen.width;

		if (Input.mousePosition.x < screenWidth/2 - centerWidth/2 && Input.mousePosition.x >= 0) 
		{
			EventManager.Instance.TriggerEvent(new MovementInput(-1f));
			Debug.Log ("Left");
		} 
		if (Input.mousePosition.x > screenWidth/2 - centerWidth/2 && Input.mousePosition.x < screenWidth/2 + centerWidth/2) 
		{
			EventManager.Instance.TriggerEvent(new MovementInput(0f));
			Debug.Log ("Center");
		} 
		if (Input.mousePosition.x > screenWidth/2 + centerWidth/2 && Input.mousePosition.x < screenWidth) 
		{
			EventManager.Instance.TriggerEvent(new MovementInput(1f));
			Debug.Log ("Right");
		} 
			
	}
}
