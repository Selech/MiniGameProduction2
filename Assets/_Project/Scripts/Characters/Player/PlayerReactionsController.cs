using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerReactionsController : MonoBehaviour {
	
	PlayerMovementController movementController;
	public GameObject currentCarriable;
	private CarriableHealth carriableHealth;
	private StackingList stackingList;
	void Awake () {
		movementController = GetComponent<PlayerMovementController> ();
		stackingList = GameObject.FindGameObjectWithTag ("CarriableDetector").GetComponent<StackingList>();

	}

	void OnEnable() {
		EventManager.Instance.StartListening <MovementInput>(RetrieveInput);
		EventManager.Instance.StartListening <ChangeSchemeEvent>(ChangeScheme);
		EventManager.Instance.StartListening <BoostPickupHitEvent>(BoostSpeed);
		EventManager.Instance.StartListening <GetBackCarriableHitEvent>(GetBackCarriable);
		EventManager.Instance.StartListening <ObstacleHitEvent>(PushBikeBack);
		EventManager.Instance.StartListening <DamageCarriableEvent>(DamageObstacle);
		EventManager.Instance.StartListening <WinChunkEnteredEvent> (StopMovement);
		EventManager.Instance.StartListening<ChunkEnteredEvent> (GetTopCarriable);
	}

	void OnDisable(){
		EventManager.Instance.StopListening <MovementInput>(RetrieveInput);
		EventManager.Instance.StopListening <BoostPickupHitEvent>(BoostSpeed);
		EventManager.Instance.StopListening <GetBackCarriableHitEvent>(GetBackCarriable);
		EventManager.Instance.StopListening <ChangeSchemeEvent> (ChangeScheme);
		EventManager.Instance.StopListening <ObstacleHitEvent>(PushBikeBack);
		EventManager.Instance.StopListening <DamageCarriableEvent>(DamageObstacle);
		EventManager.Instance.StopListening <WinChunkEnteredEvent> (StopMovement);
		EventManager.Instance.StopListening<ChunkEnteredEvent> (GetTopCarriable);
	}

	void RetrieveInput(MovementInput horizontalInput) {
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
}
