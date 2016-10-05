using UnityEngine;
using System.Collections;

public class CarriableHealth : MonoBehaviour {

	public int currentLifeCounter = 0;
	public int maxLifeCounter = 1;
	public float upForce = 300.0f;

    PlayerPickupController playerPickUpController;
	GameObject player;
    CarriableManager carriableManager;

	public int waitTimeDrop = 2;

	// Use this for initialization
	void Start () {
		ResetCurrentLifeCounter ();
		player = GameObject.FindGameObjectWithTag ("Player");
        carriableManager = GameObject.FindGameObjectWithTag("CarriableManager").GetComponent<CarriableManager>();
        playerPickUpController = player.GetComponent<PlayerPickupController>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoseHealth(){
        if (carriableManager.canBreak) {
            currentLifeCounter--;
			if (currentLifeCounter <= 0)
            {
                carriableManager.canBreak = false;
                BreakJoint (GetComponent<SpringJoint> ());
			    EventManager.Instance.TriggerEvent (new LoseCarriableEvent ());
            }
        }
	}

	public void BreakJoint(SpringJoint joint){
		StartCoroutine (BreakJointCo (joint));
	}

	public void ResetCurrentLifeCounter(){
		currentLifeCounter = maxLifeCounter;
	}

	IEnumerator BreakJointCo (SpringJoint joint) {
		if (joint) {
			playerPickUpController = player.GetComponent<PlayerPickupController> ();
			playerPickUpController.SetLastLostCarriable(joint.gameObject);
			joint.gameObject.transform.parent = null;	
			Destroy (joint);
            yield return new WaitForSeconds (waitTimeDrop);
            carriableManager.canBreak = true;
        }
	}
}
