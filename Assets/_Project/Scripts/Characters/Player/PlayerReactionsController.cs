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
		EventManager.Instance.StartListening <BoostPickupHitEvent>(BoostSpeed);
		EventManager.Instance.StartListening <GetBackCarriableHitEvent>(GetBackCarriable);
		EventManager.Instance.StartListening <ChangeSchemeEvent>(ChangeScheme);
        EventManager.Instance.StartListening <StartGame>(EnableMovement);
		EventManager.Instance.StartListening <WinChunkEnteredEvent> (StopMovement);
		EventManager.Instance.StartListening <ChangeParentToPlayer>(ChangeParent);
		EventManager.Instance.StartListening <TriggerPlayerExposure>(SetupCamera);
		EventManager.Instance.StartListening <DamageCarriableEvent>(DamageObstacle);
		EventManager.Instance.StartListening <ObstacleHitEvent>(PushBikeBack);
		EventManager.Instance.StartListening <LoseCarriableEvent>(LostCarriable);
		EventManager.Instance.StartListening <WindBlowEvent>(StartWind);

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
		EventManager.Instance.StopListening <WindBlowEvent>(StartWind);

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

	void StartWind(WindBlowEvent e){
		StartCoroutine (StartWindCoroutine (e.windPosition, e.windForce));
	}

	IEnumerator StartWindCoroutine(Vector3 windPosition, float windForce){
		movementController.wind = true;
		movementController.windPosition = windPosition;
		movementController.windForce = windForce;
		yield return new WaitForSeconds (3);
		Debug.Log ("wind done");
		movementController.wind = false;
		movementController.windForce = 0;
	}

	public void BoostSpeed(BoostPickupHitEvent e)
	{
		StartCoroutine (ChangeSpeed(e.boost, e.time));
	}

	IEnumerator ChangeSpeed(float speed, float time){
		movementController.defaultSpeed += speed;
		yield return new WaitForSeconds (time);
		movementController.defaultSpeed -= speed;
		GetComponent<PlayerPickupController> ().isLastPickupBoost = false;
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
        movementController.enabled = false;
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
