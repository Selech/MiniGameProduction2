﻿using UnityEngine;
using System.Collections;

public class CarObstacle : MonoBehaviour {
	public float shakeAmount = 5f;
	public float shakeDuration = 1f;
	public float pushPlayerBackForce = 1;
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

//	void OnTriggerEnter(Collider c)
//	{
//		Debug.Log ("hit car " + c.name);
//		if(c.CompareTag ("Player"))
//		{
//			if (playerPickupController.isLastPickupBoost) {
//				PushCar (c.transform);
//			} else {
//				EventManager.Instance.TriggerEvent (new DamageCarriableEvent ());
//				EventManager.Instance.TriggerEvent (new ObstacleHitEvent (pushPlayerBackForce));
//			}
//		}
//	}

	void OnTriggerEnter(Collider c)
	{
		Debug.Log ("hit car " + c.GetComponent<Collider>().name);
		if(c.GetComponent<Collider>().CompareTag ("BikePlate"))
		{
			EventManager.Instance.TriggerEvent (new FeedbackCameraShakeEvent (shakeAmount,shakeDuration));
			
			if (playerPickupController.isLastPickupBoost) {
				PushCar (c.transform);
			} else {
				EventManager.Instance.TriggerEvent (new DamageCarriableEvent ());
				EventManager.Instance.TriggerEvent (new ObstacleHitEvent (pushPlayerBackForce));
			}
		}
	}



	void PushCar(Transform transform){
		Debug.Log ("car jump");
		if (carRigidBody) {
			carRigidBody.AddForce (transform.forward * carPushForce, forceMode);
		}
	}
}
