using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StackingList : MonoBehaviour {

  CarriableManager carriableManager;

  public List<GameObject> CollectedCarriables = new List<GameObject>();
  public GameObject CurrentCarriable;
  public float currentHeight = 0.0f;

  private Vector3 target;
  private float extraHeight = 0.3f;
  private float extraDepth = 0.18f;
  public bool stackingDone = false;
  [HideInInspector]
  public int maxAmountOfCarriables;

  void Start() {
    target = Camera.main.transform.position;
    carriableManager = GetComponent<CarriableManager>();
    maxAmountOfCarriables = carriableManager.maxStackCarriables;
  }

  public void addObject(GameObject go, float height) {
    CurrentCarriable = go;
    EventManager.Instance.TriggerEvent(new SnapSoundEvent());
    if (CollectedCarriables.Count < maxAmountOfCarriables) { 
      if (!CollectedCarriables.Contains (go)) {
        CollectedCarriables.Add (go);
        currentHeight += height;

        target = new Vector3 (3.83f, 1.41f + (currentHeight * extraHeight), 0.37f - (currentHeight * extraDepth));
      } else {
        sortObjects ();
      }
    }
  }

  void Update(){
    if(!stackingDone){
        Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, target, 0.02f);
    }
  }

  public void removeObject(GameObject go, float height) {
    foreach (var v in CollectedCarriables) {
      if (go == v) {
        CollectedCarriables.Remove (v);
        currentHeight -= height;
        sortObjects ();     
        break;
      }
    }

    //target = new Vector3 (3.82624f, 1.46f + (currentHeight * extraHeight), 0.73f - (currentHeight * extraDepth));
    target = new Vector3 (3.83f, 1.41f + (currentHeight * extraHeight), 0.37f - (currentHeight * extraDepth));
    CurrentCarriable = null;
  }

  public void sortObjects() {
    currentHeight = 0.0f;
    foreach (var obj in CollectedCarriables) {
      obj.GetComponent<CarriablesDrag> ().Sort ();
    }
  }
}
