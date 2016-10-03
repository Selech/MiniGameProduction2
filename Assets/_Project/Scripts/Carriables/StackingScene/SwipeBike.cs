using UnityEngine;
using System.Collections;

public class SwipeBike : MonoBehaviour {

    bool swiping = false;
    StackingList stackingList;

    void Start() {
        stackingList = GetComponent<StackingList>();
    }

    void Update() {
        controlSwipe();
    }

    void controlSwipe() {
        if (Input.touchCount != 0) {
            foreach (Touch touch in Input.touches) {
                if (touch.phase == TouchPhase.Began) {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit[] testhit;
                    testhit = Physics.RaycastAll (ray);

                    if (testhit.Length > 0 && stackingList.CollectedCarriables.Count > 0) {
                        foreach (var hit in testhit) {
                            if (hit.collider.gameObject.tag == "StartSwipe") {
                                swiping = true;
                                break;
                            } else {
                                swiping = false;
                            }
                        }
                    } else {
                        swiping = false;
                    }
                } else if (touch.phase == TouchPhase.Ended) {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit[] testhit;
                    testhit = Physics.RaycastAll (ray);

                    if (testhit.Length > 0 && stackingList.CollectedCarriables.Count > 0) {
                        foreach (var hit in testhit) {
                            if (hit.collider.gameObject.tag == "EndSwipe" && swiping) {
                                EventManager.Instance.TriggerEvent(new StartGame());
                                break;
                            }   
                        }
                    }               
                }
            } 
        }
    }
}
