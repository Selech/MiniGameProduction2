using UnityEngine;
using System.Collections;

public class CarObstacle : MonoBehaviour 
{
	public float camShakeAmountOnImpact = 5f;
	public float camShakeDurationOnImpact = 1f;
	public float pushPlayerBackForce = 1;
	public float carPushForceForward = 10;
	public float carPushForceUp = 10;
	public ForceMode forceMode;
	private PlayerPickupController playerPickupController;
	Rigidbody carRigidBody;
	public Collider carMeshCollider;
	public bool isCarStatic = false;
	public float carMovementSpeed=10;
	public float carMovementLife=10;
	public float delay =1;

	void OnEnable () 
	{
		
		carRigidBody = GetComponent<Rigidbody> ();

		//move car if it aint static
		if(!isCarStatic)
		{
			StartCoroutine (MoveCar(this.gameObject,carMovementLife,carMovementSpeed,ForceMode.Acceleration));
		}
	}

	public IEnumerator MoveCar(GameObject carObject,float duration,float speed,ForceMode forceModeCar)
	{

		Rigidbody r = carObject.GetComponent<Rigidbody> ();
		if(carObject)
		{
			yield return new WaitForSeconds (delay);
			float curTime = duration;
			while(curTime > 0)
			{
				curTime -= Time.deltaTime;
				print ("spawn car " + curTime);
				if(r)
				{
					r.AddForce (-transform.right*speed*Time.deltaTime,forceModeCar);
				}
				yield return null;
			}
			//destroy car when finishes going FWD
			Destroy (carObject);
		}

	}

	void OnTriggerEnter(Collider c)
	{
		
		if(c.GetComponent<Collider>().CompareTag ("BikePlate"))
		{
			StopAllCoroutines ();
			playerPickupController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerPickupController> ();

			if (playerPickupController.isLastPickupBoost) {
				EventManager.Instance.TriggerEvent (new FeedbackCameraShakeEvent (camShakeAmountOnImpact*0.2f,camShakeDurationOnImpact));
				PushCar (c.transform);
			} else {
				
				EventManager.Instance.TriggerEvent (new FeedbackCameraShakeEvent (camShakeAmountOnImpact,camShakeDurationOnImpact));
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
		EventManager.Instance.TriggerEvent (new DamageCarriableEvent ());
		EventManager.Instance.TriggerEvent (new ObstacleHitEvent (pushPlayerBackForce));

		if(isCarStatic)
		{
			carRigidBody.constraints = RigidbodyConstraints.FreezeAll;
		}

		if(carMeshCollider)
		{
			carMeshCollider.enabled = true;
		}


	}
}
