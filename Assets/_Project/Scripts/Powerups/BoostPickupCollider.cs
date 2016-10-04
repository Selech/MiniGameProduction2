using UnityEngine;
using System.Collections;

public class BoostPickupCollider : MonoBehaviour {

	//[Tooltip("Boost when Player collides with Pickup")]
	//[Range(0, 100)]
	private float boost = 1.5f;

	//[Tooltip("How many seconds should the boost last")]
	//[Range(0, 10)]
	private float time = 6;

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			EventManager.Instance.TriggerEvent (new BoostPickupHitEvent (boost, time));
			other.GetComponent<PlayerPickupController> ().isLastPickupBoost = true;
			Destroy (this.gameObject);
		}
	}

}