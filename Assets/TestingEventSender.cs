using UnityEngine;
using System.Collections;

//made by Riccardo
//this class is for testing purposes, don't use it in final builds!! 
public class TestingEventSender : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//starting the game
		EventManager.Instance.TriggerEvent (new StartGame ());	

		EventManager.Instance.TriggerEvent (new TriggerPlayerExposure ());	

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
