using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarriableManager : MonoBehaviour
{
	StackingList stacking;

	[Range (0f, 10f)]
	public int maxStackCarriables = 4;

	[HideInInspector]
	public bool startPlaying = false;

	public float springForce = 20000;
	public float maxSpringDistance = 0.01f;

	void Start ()
	{
//		player = GameObject.FindGameObjectWithTag ("Player");
//		controller = GameObject.FindGameObjectWithTag ("GameController");
////		player.GetComponent<PlayerMovementController> ().speed = 0.0f;
//		player.GetComponent<CharacterController> ().enabled = false;
//		controller.GetComponent<SwipeController> ().enabled = false;
//		player.GetComponent<BoxCollider> ().enabled = false;
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
//		player.GetComponent<CharacterController> ().enabled = true;
//		controller.GetComponent<SwipeController> ().enabled = true;
//		player.GetComponent<BoxCollider> ().enabled = true;
		EventManager.Instance.TriggerEvent(new BeginRaceEvent());
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
		}
		/*
		for (int i = size-1; i >= 0; i--) {
			var setParentEvent = new ChangeParentToPlayer ();

			Destroy (objects [i].GetComponent<CarriablesDrag> ());
			FixedJoint joint = objects [i].AddComponent<FixedJoint> ();
			if (i > 0) {
				joint.connectedBody = objects [i - 1].GetComponent<Rigidbody> ();
			} else {
				setParentEvent.attachToPlayer = true;
			}
			joint.breakForce = Mathf.Infinity;
			joint.breakTorque = Mathf.Infinity;
			objects [i].GetComponent<BoxCollider> ().enabled = true;
			setParentEvent.gameobject = objects [i];
			EventManager.Instance.TriggerEvent(setParentEvent);
		}
		*/
		for (int i = size-1; i >= 0; i--) {
			var setParentEvent = new ChangeParentToPlayer ();

			Destroy (objects [i].GetComponent<CarriablesDrag> ());
			SpringJoint joint = objects [i].AddComponent<SpringJoint> ();
			if (i > 0) {
				joint.connectedBody = objects [i - 1].GetComponent<Rigidbody> ();
			} else {
				setParentEvent.attachToPlayer = true;
			}
			//setting joint parameters
			joint.breakForce = Mathf.Infinity;
			joint.breakTorque = Mathf.Infinity;
			joint.spring = springForce;
			joint.enableCollision = true;
			//moving the joint anchor
			joint.anchor = new Vector3(0,objects [i].GetComponent<Renderer>().bounds.min.y,0);
			joint.maxDistance = maxSpringDistance;

			//end of setting joint parameters
			objects [i].GetComponent<BoxCollider> ().enabled = true;
			setParentEvent.gameobject = objects [i];
			EventManager.Instance.TriggerEvent(setParentEvent);
		}

	}
}
