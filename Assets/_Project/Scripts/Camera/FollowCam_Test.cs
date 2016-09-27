using UnityEngine;
using System.Collections;

public class FollowCam_Test : MonoBehaviour {

	public GameObject player;
	private Vector3 dist;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = (player.transform.position - player.transform.forward * 5 + player.transform.up * 3);
		//this.transform.rotation = Quaternion.Slerp (this.transform.rotation, player.transform.rotation, 0.015f);
		this.transform.LookAt (player.transform.position);
	}
}
