using UnityEngine;
using System.Collections;

public enum WindDirection { Left, Right }

public class WindObstacle : MonoBehaviour {

	[Tooltip("Push when Player is blown away by wind")]
	[Range(0, 1)]
	public float windForce = 0.05f;

	[Tooltip("Direction from which the wind blows")]
	public WindDirection windDirection;

	Vector3 windDir;
    private Vector3 ventilatorPosition;
    private Vector3 windTriggerPosition;

    // Use this for initialization
    void Start () {
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.CompareTag("BikePlate") || col.gameObject.CompareTag("Player"))
		{
		    ventilatorPosition = transform.GetChild(0).position;
		    windTriggerPosition = transform.position;

            if (windDirection == WindDirection.Right)
				windDir = windTriggerPosition - ventilatorPosition;
			else
				windDir = ventilatorPosition - windTriggerPosition;

			EventManager.Instance.TriggerEvent (new StartWindEvent (windDir, windForce));
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
