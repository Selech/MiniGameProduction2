using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerReactionsController : MonoBehaviour {
	
	PlayerMovementController movementController;
	public GameObject currentCarriable;
	private CarriableHealth carriableHealth;
	private StackingList stackingList;
	Rigidbody bikePlate;
	void Awake () {
		movementController = GetComponent<PlayerMovementController> ();
		stackingList = GameObject.FindGameObjectWithTag ("CarriableDetector").GetComponent<StackingList>();
		bikePlate = GetComponentInChildren<Rigidbody> ();
	}

	void OnEnable() {
		EventManager.Instance.StartListening <MovementInput>(RetrieveInput);
		EventManager.Instance.StartListening <ChangeSchemeEvent>(ChangeScheme);
		EventManager.Instance.StartListening <BoostPickupHitEvent>(BoostSpeed);
		EventManager.Instance.StartListening <GetBackCarriableHitEvent>(GetBackCarriable);
		EventManager.Instance.StartListening <ObstacleHitEvent>(PushBikeBack);
		EventManager.Instance.StartListening <DamageCarriableEvent>(DamageObstacle);
		EventManager.Instance.StartListening <ChunkEnteredEvent> (GetTopCarriable);
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
		EventManager.Instance.StopListening <ObstacleHitEvent>(PushBikeBack);
		EventManager.Instance.StopListening <DamageCarriableEvent>(DamageObstacle);
		EventManager.Instance.StopListening <ChunkEnteredEvent> (GetTopCarriable);
		EventManager.Instance.StopListening <BeginRaceEvent>(EnableMovement);
		EventManager.Instance.StopListening <WinChunkEnteredEvent> (StopMovement);
		EventManager.Instance.StopListening <ChangeParentToPlayer>(ChangeParent);
		EventManager.Instance.StopListening <TriggerPlayerExposure>(ExposePlayerToCamera);
	}

	void RetrieveInput(MovementInput horizontalInput) {
		if(movementController.enabled)
			movementController.Turn(horizontalInput.touchPosition);
	}

	void ChangeScheme(ChangeSchemeEvent e){
		GameManager.Instance.ChangeScheme (e.isGyro);
	}

	public void BoostSpeed(BoostPickupHitEvent e)
	{
		StartCoroutine (ChangeSpeed(e.boost, e.time));
	}

	IEnumerator ChangeSpeed(float speed, float time){
		movementController.speed += speed;
		yield return new WaitForSeconds (time);
		movementController.speed -= speed;
		GetComponent<PlayerPickupController> ().isLastPickupBoost = false;
	}

	void GetBackCarriable(GetBackCarriableHitEvent e){
	}

	public void DamageObstacle(GameEvent e){
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
		if (stackingList) {
			print ("cariable found " + stackingList.gameObject.name);
			currentCarriable = stackingList.CollectedCarriables [stackingList.CollectedCarriables.Count - 1];
		
		}
	}

	public void GetTopCarriable(ChunkEnteredEvent e){
		if (stackingList) {
			print ("cariable found " + stackingList.gameObject.name);
			currentCarriable = stackingList.CollectedCarriables [stackingList.CollectedCarriables.Count - 1];

		}
	}
	
	void StopMovement(WinChunkEnteredEvent e) {
		EventManager.Instance.StopListening <MovementInput> (RetrieveInput);
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
