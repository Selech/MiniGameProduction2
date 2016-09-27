using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerReactionsController : MonoBehaviour {
	
	PlayerMovementController movementController;

	void OnEnable() {
		EventManager.Instance.StartListening <MovementInput>(RetrieveInput);
		EventManager.Instance.StartListening <BoostPickupHitEvent>(BoostSpeed);
		EventManager.Instance.StartListening <GetBackCarriableHitEvent>(GetBackCarriable);
	}

	void OnDisable(){
		EventManager.Instance.StartListening <MovementInput>(RetrieveInput);
		EventManager.Instance.StartListening <BoostPickupHitEvent>(BoostSpeed);
		EventManager.Instance.StartListening <GetBackCarriableHitEvent>(GetBackCarriable);
	}

	void RetrieveInput(MovementInput horizontalInput) {
		movementController.Turn(horizontalInput.touchPosition);
	}

	// Use this for initialization
	void Start () {
		movementController = GetComponent<PlayerMovementController> ();
	}

	void BoostSpeed(float speed, float time){
		movementController.speed += speed;
	}

	void GetBackCarriable(){
		
	}
}
