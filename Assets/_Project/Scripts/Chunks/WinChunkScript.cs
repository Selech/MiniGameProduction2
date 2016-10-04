using UnityEngine;
using System.Collections;

public class WinChunkScript : MonoBehaviour {

	public GameObject StartPoint;
	public GameObject EndPoint;

	void OnTriggerEnter(Collider col){
	    if (col.gameObject.tag == "Player")
	    {
	        EventManager.Instance.TriggerEvent(new WinChunkEnteredEvent());
	    }
	}
}
