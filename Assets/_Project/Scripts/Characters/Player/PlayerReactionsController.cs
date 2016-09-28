using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerReactionsController : MonoBehaviour {
	
	PlayerMovementController movementController;
	Rigidbody bikePlate;

	// Use this for initialization
	void Start () {
		movementController = GetComponent<PlayerMovementController> ();
		bikePlate = GetComponentInChildren<Rigidbody> ();
	}

	void OnEnable() {
		EventManager.Instance.StartListening <MovementInput>(RetrieveInput);
		EventManager.Instance.StartListening <ChangeSchemeEvent>(ChangeScheme);
		EventManager.Instance.StartListening <BoostPickupHitEvent>(BoostSpeed);
		EventManager.Instance.StartListening <GetBackCarriableHitEvent>(GetBackCarriable);
		EventManager.Instance.StartListening <BeginRaceEvent>(EnableMovement);
		EventManager.Instance.StartListening <ChangeParentToPlayer>(ChangeParent);
	}

	void OnDisable(){
		EventManager.Instance.StopListening <MovementInput>(RetrieveInput);
		EventManager.Instance.StopListening <BoostPickupHitEvent>(BoostSpeed);
		EventManager.Instance.StopListening <GetBackCarriableHitEvent>(GetBackCarriable);
		EventManager.Instance.StopListening <ChangeSchemeEvent> (ChangeScheme);
		EventManager.Instance.StartListening <BeginRaceEvent>(EnableMovement);
		EventManager.Instance.StartListening <ChangeParentToPlayer>(ChangeParent);

	}

	void RetrieveInput(MovementInput horizontalInput) {
		if(movementController.enabled)
			movementController.Turn(horizontalInput.touchPosition);
	}

	void ChangeScheme(ChangeSchemeEvent e){
		GameManager.Instance.ChangeScheme (e.isGyro);
	}

	void BoostSpeed(BoostPickupHitEvent e){
	}

	void GetBackCarriable(GetBackCarriableHitEvent e){
	}

	void EnableMovement(BeginRaceEvent e){
		movementController.enabled = true;
	}

	void ChangeParent(ChangeParentToPlayer e){
		e.gameobject.transform.SetParent (this.transform);
		if (e.attachToPlayer)
			e.gameobject.GetComponent<SpringJoint> ().connectedBody = bikePlate;
	}
}
