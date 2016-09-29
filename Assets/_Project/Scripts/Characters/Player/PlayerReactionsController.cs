using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerReactionsController : MonoBehaviour {

	GameObject currentCarriable;
	int indexOfCarriable;
	CarriableHealth carriableHealth;
	List<GameObject> stackingList;
	PlayerMovementController movementController;
	Rigidbody bikePlate;

	void Awake () {
		movementController = GetComponent<PlayerMovementController> ();
		stackingList = new List<GameObject> ();
		bikePlate = GetComponentInChildren<Rigidbody> ();
		indexOfCarriable = 0;
	}

	void OnEnable() {
		EventManager.Instance.StartListening <MovementInput>(RetrieveInput);
		EventManager.Instance.StartListening <ChangeSchemeEvent>(ChangeScheme);
		EventManager.Instance.StartListening <BoostPickupHitEvent>(BoostSpeed);
		EventManager.Instance.StartListening <GetBackCarriableHitEvent>(GetBackCarriable);
        EventManager.Instance.StartListening <StartGame>(EnableMovement);
		EventManager.Instance.StartListening <WinChunkEnteredEvent> (StopMovement);
		EventManager.Instance.StartListening <ChangeParentToPlayer>(ChangeParent);
		EventManager.Instance.StartListening <TriggerPlayerExposure>(SetupCamera);
		EventManager.Instance.StartListening <DamageCarriableEvent>(DamageObstacle);
		EventManager.Instance.StartListening <ObstacleHitEvent>(PushBikeBack);
		EventManager.Instance.StartListening <LoseCarriableEvent>(LostCarriable);


	}

	void OnDisable(){
		EventManager.Instance.StopListening <MovementInput>(RetrieveInput);
		EventManager.Instance.StopListening <BoostPickupHitEvent>(BoostSpeed);
		EventManager.Instance.StopListening <GetBackCarriableHitEvent>(GetBackCarriable);
		EventManager.Instance.StopListening <ChangeSchemeEvent> (ChangeScheme);
		EventManager.Instance.StopListening <StartGame>(EnableMovement);
		EventManager.Instance.StopListening <WinChunkEnteredEvent> (StopMovement);
		EventManager.Instance.StopListening <ChangeParentToPlayer>(ChangeParent);
		EventManager.Instance.StopListening <TriggerPlayerExposure>(SetupCamera);
		EventManager.Instance.StopListening <DamageCarriableEvent>(DamageObstacle);
		EventManager.Instance.StopListening <ObstacleHitEvent>(PushBikeBack);
		EventManager.Instance.StopListening <LoseCarriableEvent>(LostCarriable);


	}

	void LostCarriable(LoseCarriableEvent e){
		indexOfCarriable++;
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

	public void DamageObstacle(DamageCarriableEvent e){
		GetTopCarriable ();
		Debug.Log ("damaged  obstacle");
		if (currentCarriable) {
			carriableHealth = currentCarriable.GetComponent<CarriableHealth> ();
			carriableHealth.LoseHealth ();
		}
	}

	public void PushBikeBack(ObstacleHitEvent e){
		Debug.Log (e.upForce);
	}

	public void GetTopCarriable(){
		if (stackingList.Count > 0) {
			currentCarriable = stackingList [indexOfCarriable];
		
		}
	}

	public void GetTopCarriable(ChunkEnteredEvent e){
		if (stackingList.Count > 0) {
			currentCarriable = stackingList[indexOfCarriable];

		}
	}
	
	void StopMovement(WinChunkEnteredEvent e) {
		EventManager.Instance.StopListening <MovementInput>(RetrieveInput);
	}

	void EnableMovement(StartGame e){
		movementController.enabled = true;
	}

	void ChangeParent(ChangeParentToPlayer e){
		stackingList.Add (e.gameobject);
		e.gameobject.transform.SetParent (this.transform);
		if (e.attachToPlayer)
			e.gameobject.GetComponent<SpringJoint> ().connectedBody = bikePlate;
	}

	void SetupCamera(TriggerPlayerExposure e){
		EventManager.Instance.TriggerEvent (new ExposePlayerOnSwipe(this.transform));
	}
}
