
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
    Animator animator;

    private float animationFloat;
    private float tiltFloat; 

    private int indexOfCurrentTopCarriable = 0;
    private bool inAir;

    void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        bikePlate = GetComponentInChildren<Rigidbody>();
        numberOfLostCarriables = 0;
        playerPickupController = GetComponent<PlayerPickupController>();
        carriableManager = GameObject.FindGameObjectWithTag("CarriableManager");
        stackedList = carriableManager.GetComponent<StackingList>();
        carriableManagerScript = carriableManager.GetComponent<CarriableManager>();
        animator = GetComponentInChildren<Animator>();
        animationFloat = 0.0f;
        tiltFloat = 0.0f;
        inAir = false;
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
        EventManager.Instance.StartListening<IntroVO1event>(StartIntroAnimation);
        EventManager.Instance.StartListening<LoseCarriableEvent>(LostCarriable);
		EventManager.Instance.StartListening<StartWindEvent>(StartWind);
		EventManager.Instance.StartListening<StopWindEvent>(StopWind);
        EventManager.Instance.StartListening<ChunkEnteredEvent>(StartTrack);


        EventManager.Instance.StartListening<IntroAnimation2event>(IntroVoice2);
        EventManager.Instance.StartListening<IntroAnimation3event>(IntroVoice3);
    }

   

    void OnDisable()
    {
        EventManager.Instance.StopListening<MovementInput>(RetrieveInput);
        EventManager.Instance.StopListening<BoostPickupHitEvent>(BoostSpeed);
        EventManager.Instance.StopListening<GetBackCarriableHitEvent>(GetBackCarriable);
        EventManager.Instance.StopListening<ChangeSchemeEvent>(ChangeScheme);
        EventManager.Instance.StopListening<StartGame>(EnableMovement);
        EventManager.Instance.StopListening<IntroVO1event>(StartIntroAnimation);
        EventManager.Instance.StopListening<WinChunkEnteredEvent>(StopMovement);
        EventManager.Instance.StopListening<ChangeParentToPlayer>(ChangeParent);
        EventManager.Instance.StopListening<TriggerPlayerExposure>(SetupCamera);
        EventManager.Instance.StopListening<DamageCarriableEvent>(DamageObstacle);
        EventManager.Instance.StopListening<ObstacleHitEvent>(PushBikeBack);
        EventManager.Instance.StopListening<LoseCarriableEvent>(LostCarriable);
		EventManager.Instance.StopListening<StartWindEvent>(StartWind);
		EventManager.Instance.StopListening<StopWindEvent>(StopWind);


        EventManager.Instance.StopListening<IntroAnimation2event>(IntroVoice2);
        EventManager.Instance.StopListening<IntroAnimation3event>(IntroVoice3);

    }

    void Update()
    {
        var angle = movementController.steepAngle - 90;
        if (angle > -10f && angle < 10f)
        {
            if (tiltFloat < 0.0f)
            {
                tiltFloat += 0.05f;
            }
            else if (tiltFloat > 0.0f)
            {
                tiltFloat -= 0.05f;
            }
        }
        else if (angle > 0 && tiltFloat < 1f)
        {
            tiltFloat += 0.05f;
        }
        else if (angle < 0 && tiltFloat > -1f)
        {
            tiltFloat -= 0.05f;
        }

        animator.SetFloat("TiltAmount", tiltFloat);
    }

    /// <summary>
    /// Being called inside "DamageObstacle" after we have breaked the joint
    /// </summary>
    void LostCarriable(LoseCarriableEvent e)
    {
        numberOfLostCarriables++;
        carriableManagerScript.runningHeight -= carriableFallingOff.GetComponent<CarriablesDrag>().heightOfObject;
    }

    void StartIntroAnimation(IntroVO1event e)
    {
        animator.SetTrigger("StartIntro1");
    }

    private void IntroVoice2(IntroAnimation2event e)
    {
        animator.SetTrigger("StartIntro2");
    }

    private void IntroVoice3(IntroAnimation3event e)
    {
        animator.SetTrigger("StartIntro3");
    }

    void RetrieveInput(MovementInput horizontalInput)
    {
        if (movementController.enabled)
        {
            if (horizontalInput.touchPosition == 0.0f)
            {
                if(animationFloat < 0.0f)
                {
                    animationFloat += 0.05f;
                }
                else if (animationFloat > 0.0f)
                {
                    animationFloat -= 0.05f;
                }
            }
            else if (horizontalInput.touchPosition > 0 && animationFloat < 1f)
            {
                animationFloat += 0.05f;
            }
            else if (horizontalInput.touchPosition < 0 && animationFloat > -1f)
            {
                animationFloat -= 0.05f;
            }

            movementController.Turn(horizontalInput.touchPosition);
            animator.SetFloat("TurnAmount", animationFloat);
        }
            
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
        movementController.defaultSpeed += speed;
        movementController.maximumSpeed += speed;
        movementController.accelerationRate *= speed;

        animator.SetTrigger("SuperHappyTime");

        yield return new WaitForSeconds(time);

        movementController.defaultSpeed -= speed;
        movementController.maximumSpeed -= speed;
        movementController.accelerationRate /= speed;

        isBoosted = false;
        playerPickupController.isLastPickupBoost = false;
        EventManager.Instance.TriggerEvent(new HappyFunTimeEndsEvent());
        animator.SetTrigger("SuperHappyTimeStop");
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
        animator.SetTrigger("Bump");

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
        int amountOfCarriablesOnBike = stackedList.CollectedCarriables.Count - numberOfLostCarriables;
        amountOfCarriablesOnBike = amountOfCarriablesOnBike == 0 ? 1 : amountOfCarriablesOnBike;
        PlayerPrefs.SetInt("Amount of Carriables", amountOfCarriablesOnBike);
        movementController.StartDecelerating();

        animator.SetTrigger("StopBike");

        EventManager.Instance.StopListening<MovementInput>(RetrieveInput);
    }

    void EnableMovement(StartGame e)
    {
        animator.SetTrigger("StartDriving");

        movementController.enabled = true;
        //start accelerating from zero speed
        movementController.StartAccelerating();
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

        //if (hittedObject.layer == LayerMask.NameToLayer("Environment") && inAir)
        //{
        //    animator.SetTrigger("Landed");
        //    inAir = false;
        //}

        switch (hittedObject.tag)
        {
            case "AirGuide": //like the floating "get back carriable"
                EventManager.Instance.TriggerEvent(new FlyingEvent());
                //inAir = true;
                //animator.SetTrigger("DownRamp");
                break;
            case "RoadProp": //like ground speed boosts or sebastian ramps
                //animator.SetTrigger("UpRamp");
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
	    movementController.windDir = e.windDir;
		movementController.windForce = e.windForce;
	}

	public void StopWind(StopWindEvent e){
		movementController.wind = false;
		movementController.windForce = 0;
	}

    private void StartTrack(ChunkEnteredEvent e)
    {
        movementController.StartTrack();
        EventManager.Instance.StopListening<ChunkEnteredEvent>(StartTrack);
    }

}
