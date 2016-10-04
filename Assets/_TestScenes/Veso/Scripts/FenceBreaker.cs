using UnityEngine;
using System.Collections;

public class FenceBreaker : MonoBehaviour {
	public Rigidbody[] fencePieces;
	public float explosionForce=10;
	public float explosionRadius=10;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		print ("made collision" + col.name);
		if(col.CompareTag("Player") || col.CompareTag("BikePlate"))
		{
			BlowFences (col.transform.position);
		}

	}

	public void BlowFences(Vector3 impactPosition)
	{
		foreach(var v in fencePieces)
		{
			v.constraints = RigidbodyConstraints.None;
			v.AddExplosionForce (explosionForce,impactPosition,explosionRadius);
		}
	}
}
