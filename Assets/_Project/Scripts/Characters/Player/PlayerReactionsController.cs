﻿using UnityEngine;
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
		EventManager.Instance.StartListening <WinChunkEnteredEvent> (StopMovement);
		EventManager.Instance.StartListening <ChangeParentToPlayer>(ChangeParent);
		EventManager.Instance.StartListening <TriggerPlayerExposure>(ExposePlayerToCamera);
	}

	void OnDisable(){
		EventManager.Instance.StopListening <MovementInput>(RetrieveInput);
		EventManager.Instance.StopListening <BoostPickupHitEvent>(BoostSpeed);
		EventManager.Instance.StopListening <GetBackCarriableHitEvent>(GetBackCarriable);
		EventManager.Instance.StopListening <ChangeSchemeEvent> (ChangeScheme);
		EventManager.Instance.StartListening <BeginRaceEvent>(EnableMovement);
		EventManager.Instance.StopListening <WinChunkEnteredEvent> (StopMovement);
		EventManager.Instance.StartListening <ChangeParentToPlayer>(ChangeParent);
		EventManager.Instance.StartListening <TriggerPlayerExposure>(ExposePlayerToCamera);

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

	void StopMovement(WinChunkEnteredEvent e) {
		EventManager.Instance.StopListening <MovementInput>(RetrieveInput);
	}

	void EnableMovement(BeginRaceEvent e){
		movementController.enabled = true;
	}

	void ChangeParent(ChangeParentToPlayer e){
		e.gameobject.transform.SetParent (this.transform);
		if (e.attachToPlayer)
			e.gameobject.GetComponent<SpringJoint> ().connectedBody = bikePlate;
	}

	void ExposePlayerToCamera(TriggerPlayerExposure e){
		EventManager.Instance.TriggerEvent(new ExposePlayerToCamera(this.transform));
	}
}
