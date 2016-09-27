using UnityEngine;
using System.Collections;

public class PCinput : MonoBehaviour {

	void Update () {
		if (Input.GetKey(KeyCode.A)) {
			EventManager.Instance.TriggerEvent (new MovementInput (-0.7f));
		} else if (Input.GetKey(KeyCode.D)) {
			EventManager.Instance.TriggerEvent (new MovementInput (0.7f));
		} else {
			EventManager.Instance.TriggerEvent (new MovementInput (0.0f));
		}
	}
}
