using UnityEngine;
using System.Collections;

public class CarriableManager : MonoBehaviour {

  GameObject[] objects;

  [Range(0f, 10f)]
  public int maxStackCarriables = 4;

  [HideInInspector]
  public bool startPlaying = false;

  public void beginGame() {
    startPlaying = true;
    AddJoints();
    DisableDragging();
    SetupCamera();
  }

  private void DisableDragging() {
    
  }

  private void SetupCamera() {
    CameraController camControl = Camera.main.gameObject.AddComponent<CameraController>();
    camControl.target = GameObject.FindGameObjectWithTag("Player").transform;  
  }

  private void AddJoints(){
    objects = GameObject.FindGameObjectsWithTag ("Carriable");
    int size = objects.Length;
    for (int i = 0; i < size; i++) {
      Debug.Log("Pos: "+ i + " Name: " + objects[i].name);
      Destroy(objects[i].GetComponent<CarriablesDrag>());
      HingeJoint joint = objects[i].AddComponent<HingeJoint>();
      if (i != size - 1) {
        joint.connectedBody = objects [i + 1].GetComponent<Rigidbody>();
      }
      JointLimits limits = joint.limits;
      limits.max = 3;
      joint.limits = limits;
      joint.useLimits = true;
    }
  }
}
