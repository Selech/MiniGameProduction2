using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class NonPhysicsTurning : MonoBehaviour {
  public LayerMask groundLayer = -1;
  public float mass = 1.0f;
  public float speed = 3.0F;
  public float rotateSpeed = 3.0F;
  public bool isTurning = true;
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
  float rayToGroundLength = 4f;
  Vector3 updatedPlayerForward;
  Vector3 updatedPlayerRight;
  Vector3 updatedPlayerNormal;

  float horizontalValue = 0.0f;

  void OnEnable() {
    EventManager.Instance.StartListening <MovementInput>(retrieveInput);
  }

  void OnDisable(){
    EventManager.Instance.StopListening <MovementInput>(retrieveInput);
  }

  void retrieveInput(MovementInput horizontalInput) {
    horizontalValue = horizontalInput.touchPosition;
  }

  void Update () {
    Move(horizontalValue);
  }

  public void Move(float horizontalInputValue) {
    RaycastHit groundHit;
    float step = 10 * Time.deltaTime;

    if (Physics.Raycast(frontBikeLimit.position, -transform.up, out groundHit,rayToGroundLength,groundLayer)) {
      Debug.DrawRay(frontBikeLimit.position, -transform.up);
      frontGroundpoint = groundHit.point;
      if (Physics.Raycast(endBikeLimit.position, -transform.up, out groundHit, rayToGroundLength,groundLayer)) {
        Debug.DrawRay(endBikeLimit.position, -transform.up);
        backGroundPoint = groundHit.point;
        if (Physics.Raycast(leftBikeLimit.position, -transform.up, out groundHit, rayToGroundLength,groundLayer)) {
          Debug.DrawRay(leftBikeLimit.position, -transform.up);
          leftGroundPoint = groundHit.point;
          if (Physics.Raycast(rightBikeLimit.position, -transform.up, out groundHit, rayToGroundLength,groundLayer)) {
            Debug.DrawRay(rightBikeLimit.position, -transform.up);
            rightGroundPoint = groundHit.point;

            updatedPlayerForward = frontGroundpoint - backGroundPoint;
            updatedPlayerRight = rightGroundPoint - leftGroundPoint;
            updatedPlayerNormal = Vector3.Cross(updatedPlayerForward, updatedPlayerRight);

            if(Vector3.Dot(updatedPlayerNormal,Vector3.up) <= 0)
              updatedPlayerNormal = Vector3.up;

            Quaternion rot = Quaternion.Euler(updatedPlayerNormal);
            transform.rotation = Quaternion.LookRotation(updatedPlayerForward, updatedPlayerNormal);
          }
        }
      }
    }

    CharacterController controller = GetComponent<CharacterController>();

    if(isTurning)
      transform.Rotate(0, horizontalInputValue * rotateSpeed, 0);
    else
      controller.SimpleMove(updatedPlayerRight * horizontalInputValue * rotateSpeed);

    float curSpeed = speed;
    controller.Move((updatedPlayerForward * curSpeed + Vector3.down * mass * 3.81f)*Time.deltaTime);
  }
}