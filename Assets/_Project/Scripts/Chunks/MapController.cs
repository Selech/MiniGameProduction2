using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapController : MonoBehaviour
{
	public GameObject winChunk;
	public GameObject blockChunk;

	private Vector3 currentPosition;
	private Vector3 currentRotation;

	public List<GameObject> list;

	private List<GameObject> currentChunks = new List<GameObject> ();
	private int chunks = 0;

	[Tooltip ("Amount of chunks to win")]
	public int winAmountOfChunks = 10;

	[Tooltip ("Amount of initial chunks to load")]
	public int maxAmountOfChunks = 5;



	// Use this for initialization
	void Start ()
	{
		currentPosition = this.gameObject.transform.position;
		currentRotation = this.gameObject.transform.rotation.eulerAngles;

		for (int i = 0; i < maxAmountOfChunks - 1; i++) {
			GenerateChunk (new ChunkEnteredEvent ());
		}

		EventManager.Instance.TriggerEvent (new MapStartedEvent (winAmountOfChunks));

	}

	void OnEnable ()
	{
		EventManager.Instance.StartListening<ChunkEnteredEvent> (GenerateChunk);
	}

	void OnDisable ()
	{
		EventManager.Instance.StopListening<ChunkEnteredEvent> (GenerateChunk);
	}

	private void GenerateChunk (ChunkEnteredEvent e)
	{
		GameObject chunk = null;

		if (chunks == winAmountOfChunks) {
			chunk = (GameObject)Instantiate (winChunk);
			SpawnWinChunk (chunk);
		} else if (chunks < winAmountOfChunks) {
			int ran = Random.Range (0, list.Count);
			int ran2 = Random.Range (0, 1);
			chunk = (GameObject)Instantiate (list [ran]);

			SpawnChunk (chunk);
		} else {
			ArrangeChunkList ();
		}
	}

	void SpawnWinChunk (GameObject chunk)
	{
		ChunkScript script = chunk.GetComponent<ChunkScript> ();

		chunk.transform.position = currentPosition - script.StartPoint.transform.localPosition;
		chunk.transform.rotation = Quaternion.Euler (currentRotation);

		ArrangeChunkList (chunk);
	}

	void SpawnChunk (GameObject chunk)
	{
		ChunkScript script = chunk.GetComponent<ChunkScript> ();

		chunk.transform.position = currentPosition - script.StartPoint.transform.localPosition;
		chunk.transform.rotation = Quaternion.Euler (currentRotation);

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
