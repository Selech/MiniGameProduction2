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

	public float springForce = Mathf.Infinity;
	public float maxSpringDistance = 0.01f;
	public float springDampener = 1.0f;
	public float lengthTolerance = 0f;
	public float carriableMass = 1f;
	public float CarriableMassModifierFactor = 1.2f;
	public float maxCarriableHeight = 3f;

	public float runningHeight;

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

	public void BeginGame ()    {
		EventManager.Instance.TriggerEvent(new StartGame());
	}

	public void BeginGame (StartGame e)    {
		stacking.stackingDone = true;
		runningHeight = stacking.currentHeight;
		startPlaying = true;
		SetupCamera ();
		AddJoints ();
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

			//Destroy (collectedObjects [i].GetComponent<CarriablesDrag> ());

			SpringJoint joint = collectedObjects [i].AddComponent<SpringJoint> ();
			//Rigidbody previousObjectInList_Rigidbody = collectedObjects [i - 1].GetComponent<Rigidbody> ();

			//SetUpCarriableOnStart (joint,previousObjectInList_Rigidbody,setParentEvent,i,size);

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
			joint.tolerance = lengthTolerance;


			//moving the joint anchor
			joint.anchor = new Vector3(0, collectedObjects[i].GetComponent<Renderer>().bounds.min.y,0);
			joint.maxDistance = 0f;

			Rigidbody carriableRigidbody = joint.gameObject.GetComponent<Rigidbody>();
			//carriableRigidbody.mass = carriableMass-CarriableMassModifierFactor*(i/size)*(2*collectedObjects [i].GetComponent<Renderer>().bounds.extents.y/maxCarriableHeight);

			carriableRigidbody.mass = size * carriableMass - carriableMass * i;//* (2 * collectedObjects [i].GetComponent<Renderer> ().bounds.extents.y / maxCarriableHeight);
			//end setting rigidbody parameters
			setParentEvent.gameobject = collectedObjects[i];
			EventManager.Instance.TriggerEvent(setParentEvent);
		}

	}
		
	public void PutBackCarriable(int indexOfCarriable) {
		GameObject carriable = stacking.CollectedCarriables[indexOfCarriable];

		var setParentEvent = new ChangeParentToPlayer ();

		SpringJoint joint = carriable.AddComponent<SpringJoint> ();

		if (indexOfCarriable > 0) {
			joint.connectedBody = stacking.CollectedCarriables[indexOfCarriable - 1].GetComponent<Rigidbody> ();
		} else {
			setParentEvent.attachToPlayer = true;
		}

		Vector3 middleOfBike = carriable.GetComponent<CarriablesDrag> ().MiddleofBike.transform.position;
		carriable.transform.position = new Vector3(middleOfBike.x, middleOfBike.y + runningHeight, middleOfBike.z);

		//setting joint parameters
		joint.breakForce = Mathf.Infinity;
		joint.breakTorque = Mathf.Infinity;
		joint.spring = springForce;
		joint.damper = springDampener;
		joint.enableCollision = true;
		joint.tolerance = lengthTolerance;

		//moving the joint anchor
		joint.anchor = new Vector3(0, carriable.GetComponent<Renderer>().bounds.min.y + runningHeight,0);
		joint.maxDistance = 0f;

		Rigidbody carriableRigidbody = joint.gameObject.GetComponent<Rigidbody>();

		carriableRigidbody.mass = indexOfCarriable + 1;
		setParentEvent.gameobject = carriable;
		EventManager.Instance.TriggerEvent(setParentEvent);
	}
}