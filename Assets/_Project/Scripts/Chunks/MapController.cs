using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {

	public GameObject Straight;
	public GameObject RightTurn;
	public GameObject LongRightTurn;

	private Vector3 currentPosition;
	private Vector3 currentRotation;

	// Use this for initialization
	void Start () {
		currentPosition = this.gameObject.transform.position;
		currentRotation = this.gameObject.transform.rotation.eulerAngles;

		for (int i = 0; i < 50; i++) {
			GenerateChunk ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void GenerateChunk(){
		GameObject chunk = null;
		int ran = Random.Range (0,5);
		int ran2 = Random.Range (0,1);
		switch(ran){
		case 5:
			chunk = (GameObject)Instantiate (Straight);
			break;

		case 4:
			chunk = (GameObject)Instantiate (Straight);
			break;

		case 0:
			chunk = (GameObject)Instantiate (Straight);
			break;

		case 1:
			chunk = (GameObject)Instantiate (RightTurn);
			break;

		case 2:
			chunk = (GameObject)Instantiate (LongRightTurn);
			break;

		case 3:
			chunk = (GameObject)Instantiate (Straight);
			break;

		}

		chunk.transform.position = currentPosition;
		chunk.transform.rotation = Quaternion.Euler(currentRotation);
		ChunkScript script = chunk.GetComponent<ChunkScript> ();

		currentPosition = script.EndPoint.transform.position;
		currentRotation += script.EndPoint.transform.localRotation.eulerAngles;
	}
}
