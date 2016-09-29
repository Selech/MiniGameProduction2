using UnityEngine;
using System.Collections;

public class StaticObstacle : MonoBehaviour {
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
		}
	}

	void PushCar(Transform transform){
		Debug.Log ("car jump");
		if (staticObstacleRigid) {
			staticObstacleRigid.AddForce (transform.forward * pushForce, ForceMode.Force);
		}
	}
}
