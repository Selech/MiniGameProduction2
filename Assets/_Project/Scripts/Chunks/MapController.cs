using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapController : MonoBehaviour
{
	public GameObject winChunk;
	public GameObject blockChunk;

    private GameObject roundabout;

    private Vector3 currentPosition;
	private Vector3 currentRotation;

	public List<GameObject> chunkList;

	private List<GameObject> currentChunks = new List<GameObject> ();
	private int chunks = 0;

	[Tooltip ("Amount of chunks to win")]
	public int winAmountOfChunks = 10;

	[Tooltip ("Amount of initial chunks to load")]
	public int maxAmountOfChunks = 5;

    private bool moveRoundAbout;
    private int counter;

	// Use this for initialization
	void Start ()
	{
        counter = 30;
        // puts the roundabout below the ground 
        roundabout = (GameObject) Instantiate(blockChunk,new Vector3(0,-50f,0),Quaternion.identity);

        currentPosition = this.gameObject.transform.position;
		currentRotation = this.gameObject.transform.rotation.eulerAngles;

        chunkList = new List<GameObject>();

        var chunkList2 = Resources.LoadAll("GeneratedChunks");
        foreach (var c in chunkList2) {
            chunkList.Add(c as GameObject);
        }

        for (int i = 0; i < maxAmountOfChunks; i++) {
			GenerateChunk (new ChunkEnteredEvent ());
		}

		EventManager.Instance.TriggerEvent (new MapStartedEvent (winAmountOfChunks));

	}

    void FixedUpdate()
    {
        if (moveRoundAbout)
        {
            if (counter <= 0)
            {
                UpdateRoundabout();
            }
            counter--;
        }
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
			int ran = Random.Range (0, chunkList.Count);
			int ran2 = Random.Range (0, 1);

            chunk = (GameObject)Instantiate(chunkList[ran]);

            //chunk = (GameObject)Instantiate (list [ran]);

			SpawnChunk (chunk);
		} else {
			ArrangeChunkList ();
		}
	}

	void SpawnWinChunk (GameObject chunk)
	{
		WinChunkScript script = chunk.GetComponent<WinChunkScript> ();

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
        if (currentChunks.Count > maxAmountOfChunks)
        {
            GameObject oldChunk = (GameObject)currentChunks[0];
            Destroy(oldChunk, 1);
            ReturnToPool(oldChunk);
            currentChunks.Remove(oldChunk);
            moveRoundAbout = true;
        }
	}

	private void ArrangeChunkList (GameObject newChunk)
	{
		if (currentChunks.Count > maxAmountOfChunks) {
			GameObject oldChunk = (GameObject)currentChunks [0];
			Destroy (oldChunk,1);
			ReturnToPool (oldChunk);
			currentChunks.Remove (oldChunk);
            moveRoundAbout = true;
        }

		currentChunks.Add (newChunk);
		chunks++;
	}

    private void UpdateRoundabout()
    {
        moveRoundAbout = false;
        counter = 30;
        var start = currentChunks[0].GetComponent<ChunkScript>().StartPoint;
        roundabout.transform.position = start.transform.position;
        roundabout.transform.rotation = start.transform.rotation;
    }

	private void ReturnToPool (GameObject oldChunk)
	{

	}
}
