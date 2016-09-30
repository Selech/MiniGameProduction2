using UnityEngine;
using System.Collections;

public class CarObstacle : MonoBehaviour {
	public float shakeAmount = 5f;
	public float shakeDuration = 1f;
	public float pushPlayerBackForce = 1;
	public float carPushForceForward = 10;
	public float carPushForceUp = 10;
	public ForceMode forceMode;
	private PlayerPickupController playerPickupController;
	Rigidbody carRigidBody;
	public Collider carMeshCollider;


	void Start () 
	{
		playerPickupController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerPickupController> ();
		carRigidBody = GetComponent<Rigidbody> ();
	}

	void OnTriggerEnter(Collider c)
	{
		
		if(c.GetComponent<Collider>().CompareTag ("BikePlate"))
		{
			
			
			if (playerPickupController.isLastPickupBoost) {
				EventManager.Instance.TriggerEvent (new FeedbackCameraShakeEvent (shakeAmount*0.2f,shakeDuration));
				PushCar (c.transform);
			} else {
				EventManager.Instance.TriggerEvent (new FeedbackCameraShakeEvent (shakeAmount,shakeDuration));
				HurtPlayerCarriables ();
			}
		}
	}

	/// <summary>
	/// car is pushed if bike has speed powerup
	/// </summary>
	/// <param name="transform">Transform.</param>
	void PushCar(Transform t)
	{
		
		carRigidBody.constraints = RigidbodyConstraints.None;
		if(carMeshCollider)
		{
			carMeshCollider.enabled = false;
		}
		carRigidBody.AddForce (t.forward * carPushForceForward + Vector3.up*carPushForceUp, forceMode);

	}

	void HurtPlayerCarriables()
	{
		carRigidBody.constraints = RigidbodyConstraints.FreezeAll;

		if(carMeshCollider)
		{
			carMeshCollider.enabled = true;
		}

		EventManager.Instance.TriggerEvent (new DamageCarriableEvent ());
		EventManager.Instance.TriggerEvent (new ObstacleHitEvent (pushPlayerBackForce));
	}
}
