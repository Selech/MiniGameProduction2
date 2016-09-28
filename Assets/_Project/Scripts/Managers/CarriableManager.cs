using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarriableManager : MonoBehaviour
{

	StackingList stacking;
	GameObject player;
	GameObject controller;

	[Range (0f, 10f)]
	public int maxStackCarriables = 4;

	[HideInInspector]
	public bool startPlaying = false;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		controller = GameObject.FindGameObjectWithTag ("GameController");
//		player.GetComponent<PlayerMovementController> ().speed = 0.0f;
		player.GetComponent<CharacterController> ().enabled = false;
		controller.GetComponent<SwipeController> ().enabled = false;
		player.GetComponent<BoxCollider> ().enabled = false;
		stacking = GameObject.FindGameObjectWithTag ("CarriableDetector").GetComponent<StackingList>();
	}

	public void beginGame ()
	{
		stacking.stackingDone = true;
		startPlaying = true;
		AddJoints ();
		DisableDragging ();
		SetupCamera ();

//		player.GetComponent<PlayerMovementController> ().speed = 1.0f;
		player.GetComponent<CharacterController> ().enabled = true;
		controller.GetComponent<SwipeController> ().enabled = true;
		player.GetComponent<BoxCollider> ().enabled = true;

		EventManager.Instance.TriggerEvent(new ChunkEnteredEvent());
	}

	private void DisableDragging ()
	{
    
	}

	private void SetupCamera ()
	{
		CameraController camControl = Camera.main.gameObject.AddComponent<CameraController> ();
		camControl.target = GameObject.FindGameObjectWithTag ("Player").transform;  
	}

	private void AddJoints ()
	{
		List<GameObject> objects = stacking.CollectedCarriables;
		int size = objects.Count;

		foreach(var lols in objects){
			Rigidbody body = lols.AddComponent<Rigidbody> ();
			body.isKinematic = true;
			body.useGravity = false;
		}

		for (int i = size-1; i >= 0; i--) {
			Destroy (objects [i].GetComponent<CarriablesDrag> ());
			HingeJoint joint = objects [i].AddComponent<HingeJoint> ();
			if (i > 0) {
				joint.connectedBody = objects [i - 1].GetComponent<Rigidbody> ();
			} else {
				joint.connectedBody = GameObject.Find ("bikePlate").GetComponent<Rigidbody> ();
			}
			JointLimits limits = joint.limits;
			limits.max = 3;
			joint.limits = limits;
			joint.useLimits = true;
			objects [i].GetComponent<BoxCollider> ().enabled = false;
			objects [i].transform.SetParent (player.transform);
		}
	}
}
