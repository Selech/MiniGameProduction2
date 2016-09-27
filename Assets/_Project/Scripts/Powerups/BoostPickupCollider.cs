using UnityEngine;
using System.Collections;

public class BoostPickupCollider : MonoBehaviour {

	[Tooltip("Boost when Player collides with Pickup")]
	[Range(0, 100)]
	public float boost = 10;

	[Tooltip("How many seconds should the boost last")]
	[Range(0, 10)]
	public float time = 5;

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			EventManager.Instance.TriggerEvent (new BoostPickupHitEvent (boost, time));
			Destroy (this.gameObject);
		}
	}
}