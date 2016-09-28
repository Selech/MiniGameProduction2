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
  private float extraDepth = 0.15f;
  public bool stackingDone = false;
  [HideInInspector]
  public int maxAmountOfCarriables;

  // DELETE LATER
  GameObject button;

  void Start(){
    target = Camera.main.transform.position;
    carriableManager = GameObject.FindGameObjectWithTag("CarriableManager").GetComponent<CarriableManager>();
    maxAmountOfCarriables = carriableManager.maxStackCarriables;

    // DELETE LATER
    button = GameObject.Find("Button");
  }

  public void addObject(GameObject go, float height) {
    CurrentCarriable = go;

    if (CollectedCarriables.Count < maxAmountOfCarriables) { 
      if (!CollectedCarriables.Contains (go)) {
        CollectedCarriables.Add (go);
        currentHeight += height;

        target = new Vector3 (3.82624f, 1.34537f + (currentHeight * extraHeight), 1.471423f - (currentHeight * extraDepth));
      } else {
        sortObjects ();
      }
    }
  }

  void Update(){
    if(!stackingDone){
        Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, target, 0.02f);
    }

    // DELETE LATER
    if (CollectedCarriables.Count > 0 && !carriableManager.startPlaying) {
      button.SetActive(true);
    } else {
      button.SetActive(false); 
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
    target = new Vector3 (3.82624f, 1.34537f + (currentHeight * extraHeight), 1.471423f - (currentHeight * extraDepth));
    CurrentCarriable = null;
  }

  public void sortObjects() {
    currentHeight = 0.0f;
    foreach (var obj in CollectedCarriables) {
      obj.GetComponent<CarriablesDrag> ().Sort ();
    }
  }
}
