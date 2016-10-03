using UnityEngine;
using System.Collections;

public class PlayerPickupController : MonoBehaviour {

	public bool isLastPickupBoost;
	public GameObject lastLostCarriable;
	public SpringJoint lastUsedJoint;
	public CarriablesDrag carriablesDrag;

	public void SetLastLostCarriable(GameObject carriable){
		if (carriable) {
			lastLostCarriable = carriable;
			lastUsedJoint = carriable.GetComponent<SpringJoint> ();
			carriablesDrag = carriable.GetComponent<CarriablesDrag> ();
		}
	}
}
