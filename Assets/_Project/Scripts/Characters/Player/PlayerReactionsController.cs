using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerReactionsController : MonoBehaviour {

	GameObject currentCarriable;
	int indexOfCarriable;
	CarriableHealth carriableHealth;
	PlayerMovementController movementController;
	Rigidbody bikePlate;
	public PlayerPickupController playerPickupController;
	StackingList stackedList;
	GameObject carriableManager;
	CarriableManager carriableManagerScript;

	void Awake () {
		movementController = GetComponent<PlayerMovementController> ();
		bikePlate = GetComponentInChildren<Rigidbody> ();
		indexOfCarriable = 0;
		playerPickupController = GetComponent<PlayerPickupController> ();
		carriableManager = GameObject.FindGameObjectWithTag ("CarriableManager");
		stackedList = carriableManager.GetComponent<StackingList>();
		carriableManagerScript = carriableManager.GetComponent<CarriableManager>();
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
		Debug.Log ("CURRENTCARIABLE : " + currentCarriable);

		if (carriableManagerScript.runningHeight > 0f) {
			indexOfCarriable++;
			Debug.Log ("HEIGHT : " + carriableManagerScript.runningHeight);
			carriableManagerScript.runningHeight -= currentCarriable.GetComponent<CarriablesDrag> ().heightOfObject;
			Debug.Log ("HEIGHT 2 : " + carriableManagerScript.runningHeight);
		}
	}

	void GetBackCarriable(GetBackCarriableHitEvent e){
		if (playerPickupController.lastLostCarriable != null) {

			int trueIndex = (stackedList.CollectedCarriables.Count - 1) - indexOfCarriable;

			carriableManagerScript.PutBackCarriable (indexOfCarriable);
			Debug.Log("OBJ : " + stackedList.CollectedCarriables[trueIndex] + " HEIGHT : " + stackedList.CollectedCarriables[trueIndex].GetComponent<CarriablesDrag> ().heightOfObject);
			Debug.Log ("HEIGHT : " + carriableManagerScript.runningHeight);
			carriableManagerScript.runningHeight += stackedList.CollectedCarriables[trueIndex].GetComponent<CarriablesDrag> ().heightOfObject;
			Debug.Log ("HEIGHT 2 : " + carriableManagerScript.runningHeight);

			indexOfCarriable--;
		}
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

	public void DamageObstacle(DamageCarriableEvent e){
		GetTopCarriable ();
		if (currentCarriable) {
			carriableHealth = currentCarriable.GetComponent<CarriableHealth> ();
			carriableHealth.LoseHealth ();
		}
	}

	public void PushBikeBack(ObstacleHitEvent e){
		Debug.Log (e.upForce);
	}

	public void GetTopCarriable(){

		for (int i = 0; i < stackedList.CollectedCarriables.Count; i++) {
			Debug.Log (" IDX : " + i + " OBJ : " + stackedList.CollectedCarriables [i]);
		}

		Debug.Log (indexOfCarriable);

		int trueIndex = (stackedList.CollectedCarriables.Count - 1) - indexOfCarriable;

		if (stackedList.CollectedCarriables.Count > trueIndex) {
			currentCarriable = stackedList.CollectedCarriables[trueIndex];
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
		e.gameobject.transform.SetParent (this.transform);
		if (e.attachToPlayer)
			e.gameobject.GetComponent<SpringJoint> ().connectedBody = bikePlate;
	}

	void SetupCamera(TriggerPlayerExposure e){
		EventManager.Instance.TriggerEvent (new ExposePlayerOnSwipe(this.transform));
	}
}
