using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class NonPhysicsTurning : MonoBehaviour {
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



  // Update is called once per frame
  void FixedUpdate () {
    
    RaycastHit groundHit;
    float step = 10 * Time.deltaTime;

    if (Physics.Raycast(frontBikeLimit.position, -transform.up, out groundHit,rayToGroundLength)) { //-transform.up
      Debug.DrawRay(frontBikeLimit.position, -transform.up);
      frontGroundpoint = groundHit.point;
      if (Physics.Raycast(endBikeLimit.position, -transform.up, out groundHit, rayToGroundLength)) {
        Debug.DrawRay(endBikeLimit.position, -transform.up);
        backGroundPoint = groundHit.point;
        if (Physics.Raycast(leftBikeLimit.position, -transform.up, out groundHit, rayToGroundLength)) {
          Debug.DrawRay(leftBikeLimit.position, -transform.up);
          leftGroundPoint = groundHit.point;
          if (Physics.Raycast(rightBikeLimit.position, -transform.up, out groundHit, rayToGroundLength)) {
            Debug.DrawRay(rightBikeLimit.position, -transform.up);
            rightGroundPoint = groundHit.point;

            updatedPlayerForward = frontGroundpoint - backGroundPoint;
            updatedPlayerRight = rightGroundPoint - leftGroundPoint;
            updatedPlayerNormal = Vector3.Cross(updatedPlayerForward, updatedPlayerRight);

            if(Vector3.Dot(updatedPlayerNormal,Vector3.up) <= 0)
              updatedPlayerNormal = Vector3.up;


            Quaternion rot = Quaternion.Euler(updatedPlayerNormal);//Quaternion.Euler(Vector3.Cross(-groundHit.normal, frontGroundpoint - backGroundPoint));//Quaternion.LookRotation(frontGroundpoint-backGroundPoint,groundHit.normal);//Quaternion.Euler(Vector3.Cross(-groundHit.normal, transform.right));
            transform.rotation = Quaternion.LookRotation(updatedPlayerForward, updatedPlayerNormal);
          }
        }
      }
    }

    CharacterController controller = GetComponent<CharacterController>();

    if(isTurning)
      transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
    else
      controller.SimpleMove(updatedPlayerRight * Input.GetAxis("Horizontal") * rotateSpeed);

    float curSpeed = speed;
    controller.Move((updatedPlayerForward * curSpeed + Vector3.down * mass * 3.81f)*Time.deltaTime);
  }

  void OnControllerColliderHit(ControllerColliderHit hit)  {
    if (hit.gameObject.tag == "RespawnPlayerOnTouch")
      transform.position = respawnPoint.position;
    // transform.Rotate(Vector3.RotateTowards(transform.up, hit.normal, 0.5f, 0.5f));
  }
}