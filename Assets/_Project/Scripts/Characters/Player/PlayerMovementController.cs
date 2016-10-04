using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    //GENERAL
    public LayerMask groundLayer = -1;
    [Tooltip("Player mass for gravity and acceleration calculus")]
    public float mass = 0.02f;
    //SPEED
    [Header("Player Speed")]
    [Tooltip("Acceleration just after stacking scene.")]
    public float initialAccelerationRate = 0.040f;
    [Tooltip("Deceleration when the player arrives to the end chunk.")]
    public float finalDecelerationRate = 0.08f;
    [Tooltip("Player speed when out of slopes. Slopes are defined by SteepAcceleration/Deceleration angles.")]
    public float defaultSpeed = 0.12f;
    [Tooltip("Acceleration taken by the player in downhills and also the opposite of deceleration to climb.")]
    public float accelerationRate = 0.075f;
    [Tooltip("Deceleration to climb ramps and uphills.")]
    public float decelerationRate = 0.45f;
    [Tooltip("Maximum forward speed the player can reach in ANY situation.")]
    public float maximumSpeed = 0.172f;
    [Tooltip("Minimum forward speed the player can reach in ANY situation.")]
    public float minimumSpeed = 0.08f;
    //ROTATION
    [Header("Player Rotation")]
    [Tooltip("How fast the player turns")]
    public float rotateSpeed = 1.45F;
    [Tooltip("Down Angle from the horizon line in order to accelerate for a downhill.")]
    public float steepAccelerationDegree = 5;
    [Tooltip("Up Angle from the horizon line in order to decelerate for a downhill.")]
    public float steepDecelerationDegree = 5;
    [Tooltip("Maximum vertical angle the player forward can have with the up direction")]
    public float maxVerticalAngle = 145;
    [Tooltip("How fast the player re-align itself with the ground. You probably don't need to change this!")]
    public float reorientSpeed = 4.0F;
    //GROUND DETECTION
    [Header("Ground Detection")]
    [Tooltip("Side rays to reorient the player with the ground shape.")]
    public float rayToGroundLength = 2.0f;
    [Tooltip("Central ray length to detect if the player is grounded.")]
    public float centeredRayToGroundLength = 0.6f;
    //DEBUG VALUES
    [Header("Debug Values")]
    [Tooltip("For debugging purposes, the current forward speed of the player.")]
    public float currentForwardSpeed = 0f;
    [Tooltip("For debugging purposes, the gravity force currently applied to the player.")]
    public float currentVerticalSpeed = 0f;//used for gravity
    [Tooltip("For debugging purposes, the current angle between the player forward and the horizon.")]
    public float steepAngle = 0f;

    bool isTurning = true;
    public Transform playerModel;
    public Transform respawnPoint;
    public Transform frontBikeLimit;
    public Transform endBikeLimit;
    public Transform leftBikeLimit;
    public Transform rightBikeLimit;
    Vector3 frontGroundpoint;
    Vector3 backGroundPoint;
    Vector3 leftGroundPoint;
    Vector3 rightGroundPoint;

    Vector3 updatedPlayerForward;
    Vector3 updatedPlayerRight;
    Vector3 updatedPlayerNormal;

    float horizontalValue = 0.0f;

    CharacterController charController;
    //linear accelerated motion variables

    RaycastHit frontHit;
    RaycastHit backHit;
    RaycastHit leftHit;
    RaycastHit rightHit;
    RaycastHit centerGroundHit;
    bool isFrontPointHit;
    bool isBackPointHit;
    bool isLeftPointHit;
    bool isRightPointHit;
    private bool isCenterGroundHit;
    public float speedFactor = 1f;

    public bool wind = false;
    public Vector3 windDir;
    public float windForce = 0;
    private float oldMinimumSpeed;
    private float oldAccelerationRate;
    

    private bool isOnChunkRoad = false;

    void OnEnable()
    {
        EventManager.Instance.StartListening<PlayerHitsTheFirstRoadChunk>(EnteredFirstChunk);
    }

    void OnDisable()
    {
        EventManager.Instance.StopListening<PlayerHitsTheFirstRoadChunk>(EnteredFirstChunk);
    }

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        StabilizeOrientation();
        if (!GameManager.Instance.isPaused)
        {
            MoveForward();
            if (wind)
                MoveAside(windDir, windForce);
        }
    }

    void StabilizeOrientation()
    {
        //cast ground rays
        isFrontPointHit = Physics.Raycast(frontBikeLimit.position, -transform.up, out frontHit, rayToGroundLength, groundLayer);
        frontGroundpoint = frontHit.point;
        isBackPointHit = Physics.Raycast(endBikeLimit.position, -transform.up, out backHit, rayToGroundLength, groundLayer);
        backGroundPoint = backHit.point;
        isLeftPointHit = Physics.Raycast(leftBikeLimit.position, -transform.up, out leftHit, rayToGroundLength, groundLayer);
        leftGroundPoint = leftHit.point;
        isRightPointHit = Physics.Raycast(rightBikeLimit.position, -transform.up, out rightHit, rayToGroundLength, groundLayer);
        rightGroundPoint = rightHit.point;

        isCenterGroundHit = Physics.Raycast(transform.TransformPoint(charController.center), -transform.up, out centerGroundHit, centeredRayToGroundLength, groundLayer);
        Debug.DrawRay(transform.TransformPoint(charController.center), -transform.up, Color.red);
        if (!isCenterGroundHit) //if is flying
        {
            if (isFrontPointHit) //landing with the front wheel
            {
                updatedPlayerNormal = frontHit.normal;
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(updatedPlayerForward, updatedPlayerNormal), Time.deltaTime * reorientSpeed);
            }
            else
            {
                if (
                Vector3.Angle(Vector3.up,
                    updatedPlayerForward * currentForwardSpeed +
                                                    Vector3.down * currentVerticalSpeed) < maxVerticalAngle)
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(updatedPlayerForward * currentForwardSpeed + Vector3.down * currentVerticalSpeed), Time.deltaTime * reorientSpeed);
            }
        }
        else if (isBackPointHit & isFrontPointHit & isLeftPointHit & isRightPointHit) //if all rays are hitting ground
        {
            Debug.DrawRay(rightBikeLimit.position, -transform.up);

            updatedPlayerForward = frontGroundpoint - backGroundPoint;
            updatedPlayerRight = rightGroundPoint - leftGroundPoint;
            updatedPlayerNormal = Vector3.Cross(updatedPlayerForward, updatedPlayerRight);

            Quaternion rot = Quaternion.Euler(updatedPlayerNormal);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(updatedPlayerForward, updatedPlayerNormal), Time.deltaTime * reorientSpeed);

        }
        //else hitting the center ground but not all the other rays


    }

    public void MoveForward()
    {


        //ACCELERATION CALCULUS
        steepAngle = Vector3.Angle(updatedPlayerForward, Vector3.up);


        if (isCenterGroundHit) //player is touching the ground
        {
            if (steepAngle < 85) //going up
            {
                //decelerate
                currentForwardSpeed = currentForwardSpeed - (decelerationRate * Time.deltaTime);
            }
            else if (steepAngle > 95) //going down
            {
                //accelerate
                currentForwardSpeed = currentForwardSpeed + (accelerationRate * Time.deltaTime);
            }
            else //running horizontal
            {

                if (currentForwardSpeed > defaultSpeed)
                    currentForwardSpeed = currentForwardSpeed - (accelerationRate * Time.deltaTime);
                else
                    currentForwardSpeed = currentForwardSpeed + (accelerationRate * Time.deltaTime);
            }


            currentVerticalSpeed = 0F;

            //ensure the velocity never goes out of the initial/final boundaries
            currentForwardSpeed = Mathf.Clamp(currentForwardSpeed, minimumSpeed, maximumSpeed);
        }
        else
        {
            // forward speed remains the same and gravity will drag the player down
            currentVerticalSpeed = currentVerticalSpeed + mass * 9.81f * Time.deltaTime;
        }

        //ACCELERATION CALCULUS ENDS

        charController.Move(updatedPlayerForward * currentForwardSpeed * speedFactor + Vector3.down * currentVerticalSpeed);

    }

    void EnteredFirstChunk(PlayerHitsTheFirstRoadChunk e)
    {
        isOnChunkRoad = true;
    }

    public void Turn(float horizontalInputValue)
    {
        if (isOnChunkRoad) {
            if (!GameManager.Instance.isPaused)
            {
                transform.Rotate(0, horizontalInputValue * rotateSpeed, 0);
            }
        }
    }

    public void MoveAside(Vector3 windDir, float windForce)
    {
        //transform.Translate(((updatedPlayerForward * Mathf.Clamp(currentForwardSpeed, minimumSpeed, maximumSpeed)) + (windDir * windForce)) * Time.deltaTime);
        //charController.SimpleMove(((updatedPlayerForward * Mathf.Clamp(currentForwardSpeed, minimumSpeed, maximumSpeed)) + (windDir * windForce)) * Time.deltaTime);
        Debug.DrawRay(transform.position, -Vector3.ProjectOnPlane(charController.center - windDir, Vector3.up), Color.blue);
        charController.SimpleMove(-Vector3.ProjectOnPlane(charController.center- windDir,Vector3.up));
    }

    //called when the stacking is finished (start button)
    public void StartAccelerating()
    {
        oldMinimumSpeed = minimumSpeed;
        minimumSpeed = 0f;
        oldAccelerationRate = accelerationRate;
        accelerationRate = initialAccelerationRate;
    }
    //called when the player enters the first chunk
    public void StartTrack()
    {
        minimumSpeed = oldMinimumSpeed;
        accelerationRate = oldAccelerationRate;
    }
    //called when you enter the last chunk
    public void StartDecelerating()
    {
        minimumSpeed = 0f;
        defaultSpeed = 0f;
        decelerationRate = finalDecelerationRate;
    }
}