using UnityEngine;
using System.Collections;

public class CarriableHealth : MonoBehaviour {

	public int currentLifeCounter = 0;
	public int maxLifeCounter = 3;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoseHealth(){
		currentLifeCounter--;
		if (currentLifeCounter <= 0)
			Debug.Log ("lose Carriable");
	}
}
