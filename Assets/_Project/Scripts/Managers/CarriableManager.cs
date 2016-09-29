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
		startPlaying = true;
		SetupCamera ();
		DisableDragging ();
		AddJoints ();

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

		for (int i = size-1; i >= 0; i--) 
		{
			var setParentEvent = new ChangeParentToPlayer ();

			Destroy (collectedObjects [i].GetComponent<CarriablesDrag> ());

			SpringJoint joint = collectedObjects [i].AddComponent<SpringJoint> ();
			Rigidbody previousObjectInList_Rigidbody = collectedObjects [i - 1].GetComponent<Rigidbody> ();

			SetUpCarriableOnStart (joint,previousObjectInList_Rigidbody,setParentEvent,i,size);

			//end of setting joint parameters
			//setting rigidbody parameters
			//Debug.Log("number of carriables"+size);

			//carriableRigidbody.drag = 2;
			//carriableRigidbody.angularDrag = 2;
		}

	}


	public void SetUpCarriableOnStart(SpringJoint joint,Rigidbody previousObject,ChangeParentToPlayer changeParentToPlayer,int index,int size)
	{
		if (index > 0) {
			joint.connectedBody = previousObject;
		} else {
			changeParentToPlayer.attachToPlayer = true;
		}

		//setting joint parameters
		joint.breakForce = Mathf.Infinity;
		joint.breakTorque = Mathf.Infinity;
		joint.spring = springForce;
		joint.damper = springDampener;
		joint.enableCollision = true;
		joint.tolerance = lengthTolerance;


		//moving the joint anchor
		joint.anchor = new Vector3(0, joint.gameObject.GetComponent<Renderer>().bounds.min.y,0);
		joint.maxDistance = 0f;

		Rigidbody carriableRigidbody = joint.gameObject.GetComponent<Rigidbody>();
		//carriableRigidbody.mass = carriableMass-CarriableMassModifierFactor*(i/size)*(2*collectedObjects [i].GetComponent<Renderer>().bounds.extents.y/maxCarriableHeight);

		carriableRigidbody.mass = size * carriableMass - carriableMass * index;//* (2 * collectedObjects [i].GetComponent<Renderer> ().bounds.extents.y / maxCarriableHeight);
		//end setting rigidbody parameters
		changeParentToPlayer.gameobject = joint.gameObject;
		EventManager.Instance.TriggerEvent(changeParentToPlayer);
	}
}