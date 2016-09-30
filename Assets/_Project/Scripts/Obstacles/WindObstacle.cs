using UnityEngine;
using System.Collections;

public enum WindDirection {Left, Right};

public class WindObstacle : MonoBehaviour {

	[Tooltip("Push when Player is blown away by wind")]
	[Range(0, 1)]
	public float windForce = 0.05f;

	public Vector3 windPosition;

	// Use this for initialization
	void Start () {
		windPosition = transform.position;
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.CompareTag("BikePlate") || col.gameObject.CompareTag("Player"))
		{
			Debug.Log ("hit by wind");
			windPosition = windPosition - transform.GetChild (0).position;
			EventManager.Instance.TriggerEvent (new StartWindEvent (windPosition, windForce));
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.gameObject.CompareTag("BikePlate") || col.gameObject.CompareTag("Player"))
		{
			Debug.Log ("stop wind");
			EventManager.Instance.TriggerEvent (new StopWindEvent ());
		}
	}
}
