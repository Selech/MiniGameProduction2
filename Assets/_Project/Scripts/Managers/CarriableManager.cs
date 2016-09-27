using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarriableManager : MonoBehaviour
{

	public List<GameObject> objects;

	[Range (0f, 10f)]
	public int maxStackCarriables = 4;

	[HideInInspector]
	public bool startPlaying = false;

	public void beginGame ()
	{
		objects =  GameObject.FindGameObjectWithTag ("CarriableDetector").GetComponent<StackingList> ().CollectedCarriables;
		startPlaying = true;
		AddJoints ();
		DisableDragging ();
		SetupCamera ();

		print ("Want to start"); 
		EventManager.Instance.TriggerEvent (new StartLevelEvent());
	}

	private void DisableDragging ()
	{
    
	}

	private void SetupCamera ()
	{
		FollowCam_Test camControl = Camera.main.gameObject.AddComponent<FollowCam_Test> ();
		camControl.player = GameObject.FindGameObjectWithTag ("Player");  
	}

	private void AddJoints ()
	{
		int size = objects.Count-1;
		for (int i = size; i >= 0; i--) {
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
		}
	}
}
