using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    public LayerMask groundLayer = -1;
    [Tooltip("Player mass for gravity and acceleration calculus")]
    public float mass = 1.0f;
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
    [Tooltip("How fast the player turns")]
    public float rotateSpeed = 1.45F;
    [Tooltip("Down Angle from the horizon line in order to accelerate for a downhill.")]
    public float steepAccelerationDegree = 5;
    [Tooltip("Up Angle from the horizon line in order to decelerate for a downhill.")]
    public float steepDecelerationDegree = 5;
    [Tooltip("For debugging purposes, the current forward speed of the player.")]
    public float currentForwardSpeed = 0f;
    [Tooltip("For debugging purposes, the gravity force currently applied to the player.")]
    public float currentVerticalSpeed = 0f;//used for gravity

    [Tooltip("How fast the player re-align itself with the ground. You probably don't need to change this!")]
    public float reorientSpeed = 7.0F;
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
    public float rayToGroundLength = 1.1f;
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
    bool isFrontPointHit;
    bool isBackPointHit;
    bool isLeftPointHit;
    bool isRightPointHit;

    public float speedFactor = 1f;

    void OnEnable()
    {
        charController = GetComponent<CharacterController>();

    }

    void Update()
    {
        StabilizeOrientation();
        if (!GameManager.Instance.isPaused)
        {
            MoveForward();
        }
    }

    void StabilizeOrientation()
    {

        RaycastHit groundHit;
        float step = 10 * Time.deltaTime;
        /*
		if (Physics.Raycast (frontBikeLimit.position, -transform.up, out groundHit, rayToGroundLength, groundLayer)) {
			Debug.DrawRay (frontBikeLimit.position, -transform.up);
			frontGroundpoint = groundHit.point;
			if (Physics.Raycast (endBikeLimit.position, -transform.up, out groundHit, rayToGroundLength, groundLayer)) {
				Debug.DrawRay (endBikeLimit.position, -transform.up);
				backGroundPoint = groundHit.point;
				if (Physics.Raycast (leftBikeLimit.position, -transform.up, out groundHit, rayToGroundLength, groundLayer)) {
					Debug.DrawRay (leftBikeLimit.position, -transform.up);
					leftGroundPoint = groundHit.point;
					if (Physics.Raycast (rightBikeLimit.position, -transform.up, out groundHit, rayToGroundLength, groundLayer)) {
						Debug.DrawRay (rightBikeLimit.position, -transform.up);
						rightGroundPoint = groundHit.point;

						updatedPlayerForward = frontGroundpoint - backGroundPoint;
						updatedPlayerRight = rightGroundPoint - leftGroundPoint;
						updatedPlayerNormal = Vector3.Cross (updatedPlayerForward, updatedPlayerRight);

						if (Vector3.Dot (updatedPlayerNormal, Vector3.up) <= 0)
							updatedPlayerNormal = Vector3.up;

						Quaternion rot = Quaternion.Euler (updatedPlayerNormal);
						transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation (updatedPlayerForward, updatedPlayerNormal), Time.deltaTime * reorientSpeed);
					}
				}
			}
		}
        */

        isFrontPointHit = Physics.Raycast(frontBikeLimit.position, -transform.up, out frontHit, rayToGroundLength, groundLayer);
        frontGroundpoint = frontHit.point;
        isBackPointHit = Physics.Raycast(endBikeLimit.position, -transform.up, out backHit, rayToGroundLength, groundLayer);
        backGroundPoint = backHit.point;
        isLeftPointHit = Physics.Raycast(leftBikeLimit.position, -transform.up, out leftHit, rayToGroundLength, groundLayer);
        leftGroundPoint = leftHit.point;
        isRightPointHit = Physics.Raycast(rightBikeLimit.position, -transform.up, out rightHit, rayToGroundLength, groundLayer);
        rightGroundPoint = rightHit.point;

        if (!charController.isGrounded)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(updatedPlayerForward * currentForwardSpeed + Vector3.down * currentVerticalSpeed), Time.deltaTime * reorientSpeed);
        }
        else if (isFrontPointHit) { 
            if (isBackPointHit & isLeftPointHit & isRightPointHit)
            {
                Debug.DrawRay(rightBikeLimit.position, -transform.up);
           
                updatedPlayerForward = frontGroundpoint - backGroundPoint;
                updatedPlayerRight = rightGroundPoint - leftGroundPoint;
                updatedPlayerNormal = Vector3.Cross(updatedPlayerForward, updatedPlayerRight);

                if (Vector3.Dot(updatedPlayerNormal, Vector3.up) <= 0)
                    updatedPlayerNormal = Vector3.up;

                Quaternion rot = Quaternion.Euler(updatedPlayerNormal);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(updatedPlayerForward, updatedPlayerNormal), Time.deltaTime * reorientSpeed);

            }
        }
        else
        {
            if (isBackPointHit & isLeftPointHit & isRightPointHit)
            {
                updatedPlayerNormal = backHit.normal;
                updatedPlayerRight = rightGroundPoint - leftGroundPoint;
                updatedPlayerForward = Vector3.Cross(updatedPlayerRight, updatedPlayerNormal);

                if (Vector3.Dot(updatedPlayerNormal, Vector3.up) <= 0)
                    updatedPlayerNormal = Vector3.up;

                Quaternion rot = Quaternion.Euler(updatedPlayerNormal);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(updatedPlayerForward, updatedPlayerNormal), Time.deltaTime * reorientSpeed);

            }
        }


    }

    public void MoveForward()
    {


        //ACCELERATION CALCULUS
        float steepAngle = Vector3.Angle(updatedPlayerForward, Vector3.up);


        if (charController.isGrounded)
        {
            if (steepAngle < 85) //decelerate
            {
                Debug.Log("I'm climbing!");
                //add to the current velocity according while accelerating
                currentForwardSpeed = currentForwardSpeed - (decelerationRate * Time.deltaTime);
            }
            else if (steepAngle > 95)
            {
                //add to the current velocity according while accelerating
                currentForwardSpeed = currentForwardSpeed + (accelerationRate * Time.deltaTime);
                Debug.Log("I'm going down!");
            }
            else
            {
                //currentForwardSpeed = defaultSpeed;
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
        { // forward speed remains the same and gravity will drag the player down
            currentVerticalSpeed = currentVerticalSpeed + mass * 9.81f * Time.deltaTime;

        }

        //ACCELERATION CALCULUS ENDS

        //if (charController.enabled == true)  Debug.Log("Grounded!!");
        charController.Move((updatedPlayerForward * currentForwardSpeed * speedFactor + Vector3.down * currentVerticalSpeed));



    }

    public void Turn(float horizontalInputValue)
    {
        if (!GameManager.Instance.isPaused)
        {
            transform.Rotate(0, horizontalInputValue * rotateSpeed, 0);
        }



    }

}