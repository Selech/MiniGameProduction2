using UnityEngine;
using System.Collections;

public class ChunkScript : MonoBehaviour {

	public GameObject StartPoint;
	public GameObject EndPoint;

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player")
			EventManager.Instance.TriggerEvent(new ChunkEnteredEvent(this.gameObject));
	}
}
