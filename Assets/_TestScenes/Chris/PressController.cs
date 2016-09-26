using UnityEngine;
using System.Collections;

public class PressController : MonoBehaviour {
	
	int screenWidth;

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

		if (Input.mousePosition.x < screenWidth / 2) 
		{
			EventManager.Instance.TriggerEvent (new MovementInput (-1f));
		}
		if (Input.mousePosition.x >= screenWidth / 2) 
		{
			EventManager.Instance.TriggerEvent (new MovementInput (1f));	
		} 

			

	}


}
