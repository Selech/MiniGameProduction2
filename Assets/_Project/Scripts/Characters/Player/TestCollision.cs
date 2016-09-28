using UnityEngine;
using System.Collections;

public class TestCollision : MonoBehaviour {

	CharacterController controller;
	// Use this for initialization
	void Start () {
		controller = GetComponent <CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log ("plaer" + col.gameObject.name);
	}


}
