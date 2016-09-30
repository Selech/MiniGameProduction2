using UnityEngine;
using System.Collections;

public enum WindDirection {Left, Right};

public class WindObstacle : MonoBehaviour {

	[Tooltip("Push when Player is blown away by wind")]
	[Range(0, 1)]
	public float windForce = 0.5f;

	public Vector3 windPosition;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("triggered");
		if(col.gameObject.CompareTag("BikePlate") || col.gameObject.CompareTag("Player"))
		{
			Debug.Log ("hit by wind");
			windPosition = GameObject.FindGameObjectWithTag ("WindSource").transform.position;
			EventManager.Instance.TriggerEvent(new WindBlowEvent(windPosition, windForce));
		}
	}
}
