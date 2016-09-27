using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapController : MonoBehaviour {

	public GameObject Straight;
	public GameObject DownStraight;
	public GameObject UpStraight;
	public GameObject RightTurn;
	public GameObject RightTurnDown;
	public GameObject LeftTurn;
	public GameObject LeftTurnDown;

	private Vector3 currentPosition;
	private Vector3 currentRotation;

	public List<GameObject> list;

	private List<GameObject> currentChunks = new List<GameObject>();

	[Tooltip("Amount of initial chunks to load")]
	public int maxAmountOfChunks = 5;



	// Use this for initialization
	void Start () {
		currentPosition = this.gameObject.transform.position;
		currentRotation = this.gameObject.transform.rotation.eulerAngles;

		for (int i = 0; i < maxAmountOfChunks; i++) {
			GenerateChunk (new ChunkEnteredEvent());
		}
	}

	void OnEnable(){
		EventManager.Instance.StartListening<ChunkEnteredEvent>(GenerateChunk);
	}

	void OnDisable(){
		EventManager.Instance.StopListening<ChunkEnteredEvent>(GenerateChunk);
	}

	private void GenerateChunk(ChunkEnteredEvent e){
		GameObject chunk = null;
		int ran = Random.Range (0,10);
		int ran2 = Random.Range (0,1);
		switch(ran){
		case 0:
			chunk = (GameObject)Instantiate (Straight);
			break;

		case 1:
			chunk = (GameObject)Instantiate (Straight);
			break;

		case 2:
			chunk = (GameObject)Instantiate (LeftTurn);
			break;

		case 3:
			chunk = (GameObject)Instantiate (RightTurn);
			break;

		case 4:
			chunk = (GameObject)Instantiate (DownStraight);
			break;

		case 5:
			chunk = (GameObject)Instantiate (UpStraight);
			break;

		case 6:
			chunk = (GameObject)Instantiate (RightTurn);
			break;

		case 7:
			chunk = (GameObject)Instantiate (RightTurnDown);
			break;

		case 8:
			chunk = (GameObject)Instantiate (LeftTurn);
			break;

		case 9:
			chunk = (GameObject)Instantiate (LeftTurnDown);
			break;

		case 10:
			chunk = (GameObject)Instantiate (Straight);
			break;

		}

		chunk.transform.position = currentPosition;
		chunk.transform.rotation = Quaternion.Euler(currentRotation);
		ChunkScript script = chunk.GetComponent<ChunkScript> ();

		currentPosition = script.EndPoint.transform.position;
		currentRotation += script.EndPoint.transform.localRotation.eulerAngles;

		ArrangeChunkList (chunk);
	}

	private void ArrangeChunkList(GameObject newChunk){
		if (currentChunks.Count > maxAmountOfChunks) {
			GameObject oldChunk = (GameObject) currentChunks[0];
			oldChunk.SetActive (false);
			ReturnToPool (oldChunk);
			currentChunks.Remove (oldChunk);
		}

		currentChunks.Add (newChunk);
	}

	private void ReturnToPool(GameObject oldChunk){

	}
}
