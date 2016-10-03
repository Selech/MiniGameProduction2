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

	public float springForce = 90000000f;
	public float maxSpringDistance = 0.01f;
	public float springDampener = 1.0f;

    void OnEnable() {
        EventManager.Instance.StartListening<StartGame>(BeginGame);
    }

    void OnDisable() {
        EventManager.Instance.StopListening<StartGame>(BeginGame);
    }

	void Start ()
	{
		stacking = GetComponent<StackingList>();
	}

	public void BeginGame ()	{
		EventManager.Instance.TriggerEvent(new StartGame());
	}

    public void BeginGame (StartGame e)	{
		stacking.stackingDone = true;
		startPlaying = true;
		AddJoints ();
		DisableDragging ();
		SetupCamera ();

		//EventManager.Instance.TriggerEvent(new ChunkEnteredEvent());
	}

	private void DisableDragging ()
	{
    
	}

	private void SetupCamera ()
	{
		EventManager.Instance.TriggerEvent(new TriggerPlayerExposure());
	}

	private void AddJoints ()
	{
		List<GameObject> collectedObjects = stacking.CollectedCarriables;
		int size = collectedObjects.Count;

		foreach(var collectedObject in collectedObjects){
			collectedObject.AddComponent<Rigidbody> ();
			var boxColliders = collectedObject.GetComponents<BoxCollider> ();
			foreach (var boxCollider in boxColliders) {
				boxCollider.enabled = !boxCollider.enabled;
			}
		}

		for (int i = size-1; i >= 0; i--) {
			var setParentEvent = new ChangeParentToPlayer ();

			Destroy (collectedObjects [i].GetComponent<CarriablesDrag> ());
			SpringJoint joint = collectedObjects [i].AddComponent<SpringJoint> ();
			if (i > 0) {
				joint.connectedBody = collectedObjects [i - 1].GetComponent<Rigidbody> ();
			} else {
				setParentEvent.attachToPlayer = true;
			}
			//setting joint parameters
			joint.breakForce = Mathf.Infinity;
			joint.breakTorque = Mathf.Infinity;
			joint.spring = springForce;
			joint.damper = springDampener;
			joint.enableCollision = true;
			joint.damper = 0f;
			joint.tolerance = 0f;
			//moving the joint anchor
			joint.anchor = new Vector3(0, collectedObjects [i].GetComponent<Renderer>().bounds.min.y,0);
			joint.maxDistance = 0f;

			//end of setting joint parameters
			setParentEvent.gameobject = collectedObjects [i];
			EventManager.Instance.TriggerEvent(setParentEvent);
		}

	}
}
