using UnityEngine;
using System.Collections;

public class CarriableHealth : MonoBehaviour {

	public int currentLifeCounter = 0;
	public int maxLifeCounter = 3;
	// Use this for initialization
	void Start () {
		ResetCurrentLifeCounter ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoseHealth(){
		currentLifeCounter--;
		if (currentLifeCounter <= 0)
			BreakJoint (GetComponent<SpringJoint> ());
	}

	public void BreakJoint(SpringJoint joint){
		if (joint)
			joint.breakForce = 0;
	}

	public void ResetCurrentLifeCounter(){
		currentLifeCounter = maxLifeCounter;
	}
}
