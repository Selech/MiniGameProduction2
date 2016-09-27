using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapController : MonoBehaviour
{

	public GameObject Straight;
	public GameObject DownStraight;
	public GameObject UpStraight;
	public GameObject RightTurn;
	public GameObject RightTurnDown;
	public GameObject LeftTurn;
	public GameObject LeftTurnDown;

	public GameObject winChunk;

	private Vector3 currentPosition;
	private Vector3 currentRotation;

	public List<GameObject> list;

	private List<GameObject> currentChunks = new List<GameObject> ();
	private int chunks = 0;

	[Tooltip ("Amount of chunks to win")]
	public int winAmountOfChunks = 10;

	[Tooltip ("Amount of initial chunks to load")]
	public int maxAmountOfChunks = 5;

	void OnEnable ()
	{
		EventManager.Instance.StartListening<ChunkEnteredEvent> (GenerateChunk);
		EventManager.Instance.StartListening<StartLevelEvent> (BeginProcedure);
	}

	void OnDisable ()
	{
		EventManager.Instance.StopListening<ChunkEnteredEvent> (GenerateChunk);
		EventManager.Instance.StopListening<StartLevelEvent> (BeginProcedure);
	}

	void BeginProcedure(StartLevelEvent e) {
		currentPosition = this.gameObject.transform.position;
		currentRotation = this.gameObject.transform.rotation.eulerAngles;

		for (int i = 0; i < maxAmountOfChunks-1; i++) {
			GenerateChunk (new ChunkEnteredEvent ());
		}
	}

	private void GenerateChunk (ChunkEnteredEvent e)
	{
		GameObject chunk = null;

		if (chunks == winAmountOfChunks) {
			chunk = (GameObject)Instantiate (winChunk);
			SpawnWinChunk (chunk);
		} else if (chunks < winAmountOfChunks) {
			int ran = Random.Range (0, 10);
			int ran2 = Random.Range (0, 1);
			switch (ran) {
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

			SpawnChunk (chunk);
		} else {
			ArrangeChunkList ();
		}
	}

	void SpawnWinChunk(GameObject chunk){
		chunk.transform.position = currentPosition;
		chunk.transform.rotation = Quaternion.Euler (currentRotation);
		ChunkScript script = chunk.GetComponent<ChunkScript> ();

		ArrangeChunkList (chunk);
	}

	void SpawnChunk(GameObject chunk){
		chunk.transform.position = currentPosition;
		chunk.transform.rotation = Quaternion.Euler (currentRotation);
		ChunkScript script = chunk.GetComponent<ChunkScript> ();

		currentPosition = script.EndPoint.transform.position;
		currentRotation += script.EndPoint.transform.localRotation.eulerAngles;

		ArrangeChunkList (chunk);
	}

	private void ArrangeChunkList ()
	{
		GameObject oldChunk = (GameObject)currentChunks [0];
		Destroy (oldChunk);
		ReturnToPool (oldChunk);
		currentChunks.Remove (oldChunk);
	}

	private void ArrangeChunkList (GameObject newChunk)
	{
		if (currentChunks.Count > maxAmountOfChunks) {
			GameObject oldChunk = (GameObject)currentChunks [0];
			Destroy (oldChunk);
			ReturnToPool (oldChunk);
			currentChunks.Remove (oldChunk);
		}

currentChunks.Add (newChunk);
		chunks++;
	}

	private void ReturnToPool (GameObject oldChunk)
	{

	}
}
