using UnityEngine;
using System.Collections;

public enum ObstacleKind
{
	general,
	car,
	dumpster,
	roadblock,
	roadblockBig,
	trashcan_Body,
	trashcan_Top
}

public class StaticObstacle : MonoBehaviour {

	public ObstacleKind _obstacleType;

	public float shakeAmount = 5f;
	public float shakeDuration = 1f;
	public float pushForce = 10;
	Rigidbody staticObstacleRigid;
	// Use this for initialization
	void Start () {
		staticObstacleRigid = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.CompareTag("BikePlate"))
		{
			Debug.Log ("inside static");
			EventManager.Instance.TriggerEvent(new DamageCarriableEvent());
			EventManager.Instance.TriggerEvent (new FeedbackCameraShakeEvent (shakeAmount,shakeDuration));
		}
	}

	void PushCar(Transform transform){
		Debug.Log ("car jump");
		if (staticObstacleRigid) {
			staticObstacleRigid.AddForce (transform.forward * pushForce, ForceMode.Force);
		}
	}
}
