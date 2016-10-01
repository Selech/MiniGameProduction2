using UnityEngine;
using System.Collections;

public class GyroInput : MonoBehaviour {

	public float deadZone = 0.1f;

	// Use this for initialization
	void Start () {
		Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetGyro ();
	}

	public void GetGyro()
	{
		float value = Input.gyro.gravity.x;
		//print (value);
		if (Mathf.Abs (value) > deadZone) {
			
			EventManager.Instance.TriggerEvent (new MovementInput (value));
		} else {
			EventManager.Instance.TriggerEvent (new MovementInput (0.0f));
		}
	}
}
