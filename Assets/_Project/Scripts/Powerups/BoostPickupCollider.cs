using UnityEngine;
using System.Collections;

public class BoostPickupCollider : MonoBehaviour {

	[Tooltip("Boost when Player collides with Pickup")]
	[Range(0, 100)]
	public float boost = 0;

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			EventManager.Instance.TriggerEvent (new BoostPickupHitEvent (boost));
			Destroy (this.gameObject);
		}
	}
}