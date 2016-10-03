
﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerReactionsController : MonoBehaviour
{

    GameObject carriableFallingOff;
    int numberOfLostCarriables;
    CarriableHealth carriableHealth;
    PlayerMovementController movementController;
    Rigidbody bikePlate;
    bool isBoosted = false;

    public PlayerPickupController playerPickupController;
    StackingList stackedList;
    GameObject carriableManager;
    CarriableManager carriableManagerScript;

    private int indexOfCurrentTopCarriable = 0;

    void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        bikePlate = GetComponentInChildren<Rigidbody>();
        numberOfLostCarriables = 0;
        playerPickupController = GetComponent<PlayerPickupController>();
        carriableManager = GameObject.FindGameObjectWithTag("CarriableManager");
        stackedList = carriableManager.GetComponent<StackingList>();
        carriableManagerScript = carriableManager.GetComponent<CarriableManager>();
    }

    void OnEnable()
    {
        EventManager.Instance.StartListening<MovementInput>(RetrieveInput);
        EventManager.Instance.StartListening<ChangeSchemeEvent>(ChangeScheme);
        EventManager.Instance.StartListening<BoostPickupHitEvent>(BoostSpeed);
        EventManager.Instance.StartListening<GetBackCarriableHitEvent>(GetBackCarriable);
        EventManager.Instance.StartListening<StartGame>(EnableMovement);
        EventManager.Instance.StartListening<WinChunkEnteredEvent>(StopMovement);
        EventManager.Instance.StartListening<ChangeParentToPlayer>(ChangeParent);
        EventManager.Instance.StartListening<TriggerPlayerExposure>(SetupCamera);
        EventManager.Instance.StartListening<DamageCarriableEvent>(DamageObstacle);
        EventManager.Instance.StartListening<ObstacleHitEvent>(PushBikeBack);
        EventManager.Instance.StartListening<LoseCarriableEvent>(LostCarriable);
		EventManager.Instance.StartListening<StartWindEvent>(StartWind);
		EventManager.Instance.StartListening<StopWindEvent>(StopWind);
    }

    void OnDisable()
    {
        EventManager.Instance.StopListening<MovementInput>(RetrieveInput);
        EventManager.Instance.StopListening<BoostPickupHitEvent>(BoostSpeed);
        EventManager.Instance.StopListening<GetBackCarriableHitEvent>(GetBackCarriable);
        EventManager.Instance.StopListening<ChangeSchemeEvent>(ChangeScheme);
        EventManager.Instance.StopListening<StartGame>(EnableMovement);
        EventManager.Instance.StopListening<WinChunkEnteredEvent>(StopMovement);
        EventManager.Instance.StopListening<ChangeParentToPlayer>(ChangeParent);
        EventManager.Instance.StopListening<TriggerPlayerExposure>(SetupCamera);
        EventManager.Instance.StopListening<DamageCarriableEvent>(DamageObstacle);
        EventManager.Instance.StopListening<ObstacleHitEvent>(PushBikeBack);
        EventManager.Instance.StopListening<LoseCarriableEvent>(LostCarriable);
		EventManager.Instance.StopListening<StartWindEvent>(StartWind);
		EventManager.Instance.StopListening<StopWindEvent>(StopWind);
    }

    /// <summary>
    /// Being called inside "DamageObstacle" after we have breaked the joint
    /// </summary>
    void LostCarriable(LoseCarriableEvent e)
    {
        numberOfLostCarriables++;
        carriableManagerScript.runningHeight -= carriableFallingOff.GetComponent<CarriablesDrag>().heightOfObject;
    }

    void RetrieveInput(MovementInput horizontalInput)
    {
        if (movementController.enabled)
            movementController.Turn(horizontalInput.touchPosition);
    }

    void ChangeScheme(ChangeSchemeEvent e)
    {
        GameManager.Instance.ChangeScheme(e.isGyro);
    }

    void BoostSpeed(BoostPickupHitEvent e)
    {
        if (isBoosted == false)
        {
            isBoosted = true;
            StartCoroutine(BoostPickUp(e.boost, e.time));
        }
    }

    IEnumerator BoostPickUp(float speed, float time)
    {
        movementController.speedFactor = speed;
        yield return new WaitForSeconds(time);
        movementController.speedFactor = 1;
        isBoosted = false;

    }

    void GetBackCarriable(GetBackCarriableHitEvent e)
    {
        if (numberOfLostCarriables != 0)
        {
            carriableManagerScript.PutBackCarriable(numberOfLostCarriables);
            numberOfLostCarriables--;
        }
    }

    /// <summary>
    /// BEING CALLED WHEN PLAYER TRIGGER ON OBSTACLES (1)
    /// </summary>
	public void DamageObstacle(DamageCarriableEvent e)
    {
        if (numberOfLostCarriables != stackedList.CollectedCarriables.Count)
        {
            GetCarriableFallingOff();
            carriableHealth = carriableFallingOff.GetComponent<CarriableHealth>();
            carriableHealth.LoseHealth();
        }
    }

    public void PushBikeBack(ObstacleHitEvent e)
    {
        
    }

    public void GetCarriableFallingOff()
    {
        indexOfCurrentTopCarriable = (stackedList.CollectedCarriables.Count - 1) - numberOfLostCarriables;
        carriableFallingOff = stackedList.CollectedCarriables[indexOfCurrentTopCarriable];
    }

    void StopMovement(WinChunkEnteredEvent e)
    {
        movementController.enabled = false;
        EventManager.Instance.StopListening<MovementInput>(RetrieveInput);
    }

    void EnableMovement(StartGame e)
    {
        movementController.enabled = true;
    }

    void ChangeParent(ChangeParentToPlayer e)
    {
        e.gameobject.transform.SetParent(this.transform);
        if (e.attachToPlayer)
            e.gameobject.GetComponent<SpringJoint>().connectedBody = bikePlate;
    }

    void SetupCamera(TriggerPlayerExposure e)
    {
        EventManager.Instance.TriggerEvent(new ExposePlayerOnSwipe(this.transform));
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject hittedObject = hit.gameObject;

        switch (hittedObject.tag)
        {
            case "RoadProp": //like ground speed boosts or sebastian ramps
                EventManager.Instance.TriggerEvent(new PlayerHitRoadProp(hittedObject));
                break;
            case "Pickable": //like the floating "get back carriable"
                break;
            case "Obstacle":
                break;
            default:
                break;
        }
    }

	public void StartWind(StartWindEvent e){
		movementController.wind = true;
		movementController.windPosition = e.windPosition;
		movementController.windForce = e.windForce;
	}

	public void StopWind(StopWindEvent e){
		movementController.wind = false;
		movementController.windForce = 0;
	}

}
