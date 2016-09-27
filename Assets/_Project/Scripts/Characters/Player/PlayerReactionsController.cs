using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerReactionsController : MonoBehaviour {
	
	PlayerMovementController movementController;

	void OnEnable() {
		EventManager.Instance.StartListening <MovementInput>(retrieveInput);
	}

	void OnDisable(){
		EventManager.Instance.StopListening <MovementInput>(retrieveInput);
	}

	void retrieveInput(MovementInput horizontalInput) {
		movementController.Turn(horizontalInput.touchPosition);
	}

	// Use this for initialization
	void Start () {
		movementController = GetComponent<PlayerMovementController> ();
	}
}
