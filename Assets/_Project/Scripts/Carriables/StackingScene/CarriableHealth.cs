using UnityEngine;
using System.Collections;

public class CarriableHealth : MonoBehaviour {

	public int currentLifeCounter = 0;
	public int maxLifeCounter = 3;
	public float upForce = 300.0f;
	// Use this for initialization
	void Start () {
		ResetCurrentLifeCounter ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoseHealth(){
		print ("Lost health"); 
		currentLifeCounter--;
		if (currentLifeCounter <= 0) {
			BreakJoint (GetComponent<SpringJoint> ());
			EventManager.Instance.TriggerEvent (new LoseCarriableEvent ());
		}
			
	}

	public void BreakJoint(SpringJoint joint){
		StartCoroutine (BreakJointCo (joint));
	}

	public void ResetCurrentLifeCounter(){
		currentLifeCounter = maxLifeCounter;
	}

	IEnumerator BreakJointCo (SpringJoint joint) {
		if (joint) {
			joint.gameObject.transform.parent = null;	
			Destroy (joint);
			yield return new WaitForSeconds (0.1f);
			//GetComponent<Rigidbody> ().AddForce (-transform.forward * upForce * 0.5f + Vector3.up * upForce, ForceMode.Impulse);
		}
	}
}
