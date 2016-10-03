using UnityEngine;
using System.Collections;

public class GetBackCarriablePickupCollider : MonoBehaviour {

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			EventManager.Instance.TriggerEvent (new GetBackCarriableHitEvent());
			other.GetComponent<PlayerPickupController> ().isLastPickupBoost = false;
			Destroy (this.gameObject);
		}
	}
}