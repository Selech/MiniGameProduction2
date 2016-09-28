using UnityEngine;
using System.Collections;

public class CarObstacle : MonoBehaviour {

	public float force = 1;
	public float carPushForce = 10;
	public ForceMode forceMode;
	private PlayerPickupController playerPickupController;
	Rigidbody carRigidBody;
	// Use this for initialization
	void Start () {
		playerPickupController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerPickupController> ();
		carRigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			if (playerPickupController.istLastPickupBoost) {
				PushCar (col.transform);
			} else {
				EventManager.Instance.TriggerEvent (new DamageCarriableEvent ());
				EventManager.Instance.TriggerEvent (new ObstacleHitEvent (force));
			}
		}
	}

	void PushCar(Transform transform){
		if (carRigidBody) {
			carRigidBody.AddForce (transform.forward * carPushForce, forceMode);
		}
	}
}
