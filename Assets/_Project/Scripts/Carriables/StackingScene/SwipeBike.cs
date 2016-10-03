using UnityEngine;
using System.Collections;

public class SwipeBike : MonoBehaviour {

    bool swiping = false;
    StackingList stackingList;

    private int tapCount;
    private bool tapped;
    private Touch touch;

    void Start() {
        stackingList = GetComponent<StackingList>();
    }

	void Update() {
        ControlTap();
    }

    void ControlTap()
    {
        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            var ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (touch.phase == TouchPhase.Ended && stackingList.CollectedCarriables.Count > 0 && Physics.Raycast(ray, out hit)) {
                if (hit.collider.gameObject.tag == "StartSwipe") {
                    tapped = true;

                    if (tapCount <= 0) {
                        tapCount = 20;
                    }
                    else if (tapCount >= 0) {
                        EventManager.Instance.TriggerEvent(new StartGame());
                    }
                }
            }
        }

        if (tapCount <= 0) {
            tapped = false;
        }
        tapCount--;
    }

    public void StartGame()
    {
        EventManager.Instance.TriggerEvent(new StartGame());
    }

	void ControlSwipe() {
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
