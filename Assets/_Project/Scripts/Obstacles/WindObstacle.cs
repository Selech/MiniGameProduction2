using UnityEngine;
using System.Collections;

public enum WindDirection { Left, Right }

public class WindObstacle : MonoBehaviour {

	[Tooltip("Push when Player is blown away by wind")]
	[Range(0, 1)]
	public float windForce = 0.05f;

	[Tooltip("Direction from which the wind blows")]
	public WindDirection windDirection;

	Vector3 windPosition;

	// Use this for initialization
	void Start () {
		windPosition = transform.position;
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.CompareTag("BikePlate") || col.gameObject.CompareTag("Player"))
		{
			
			if (windDirection == WindDirection.Right)
				windPosition = windPosition - transform.GetChild (0).position;
			else
				windPosition = transform.GetChild (0).position - windPosition;
			EventManager.Instance.TriggerEvent (new StartWindEvent (windPosition, windForce));
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.gameObject.CompareTag("BikePlate") || col.gameObject.CompareTag("Player"))
		{
			
			EventManager.Instance.TriggerEvent (new StopWindEvent ());
		}
	}
}
