using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerReactionsController : MonoBehaviour {
	
	PlayerMovementController movementController;

	// Use this for initialization
	void Start () {
		movementController = GetComponent<PlayerMovementController> ();
	}

	void OnEnable() {
		EventManager.Instance.StartListening <MovementInput>(RetrieveInput);
		EventManager.Instance.StartListening <ChangeSchemeEvent>(ChangeScheme);
		EventManager.Instance.StartListening <BoostPickupHitEvent>(BoostSpeed);
		EventManager.Instance.StartListening <GetBackCarriableHitEvent>(GetBackCarriable);
	}

	void OnDisable(){
		EventManager.Instance.StopListening <MovementInput>(RetrieveInput);
		EventManager.Instance.StopListening <BoostPickupHitEvent>(BoostSpeed);
		EventManager.Instance.StopListening <GetBackCarriableHitEvent>(GetBackCarriable);
		EventManager.Instance.StopListening <ChangeSchemeEvent> (ChangeScheme);
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
	}

	void GetBackCarriable(GetBackCarriableHitEvent e){
	}
}
