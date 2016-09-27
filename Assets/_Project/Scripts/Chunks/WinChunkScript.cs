using UnityEngine;
using System.Collections;

public class WinChunkScript : MonoBehaviour {

	public GameObject StartPoint;
	public GameObject EndPoint;

	void OnTriggerEnter(Collider col){
		EventManager.Instance.TriggerEvent(new WinChunkEnteredEvent());
	}
}
