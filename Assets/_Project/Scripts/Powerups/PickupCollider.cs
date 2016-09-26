using UnityEngine;
using System.Collections;

public class PickupCollider : MonoBehaviour {

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			Destroy (gameObject);
		}
	}
}
