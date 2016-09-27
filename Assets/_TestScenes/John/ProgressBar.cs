using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {


	void OnEnable(){
		EventManager.Instance.StartListening<ChunkEnteredEvent> (PlayerProgression);
		EventManager.Instance.StartListening<MapStartedEvent> (InitializeSize);
	}

	void OnDisable(){
		EventManager.Instance.StopListening<ChunkEnteredEvent> (PlayerProgression);
		EventManager.Instance.StopListening<MapStartedEvent> (InitializeSize);
	}

	private int numberOfChunks = 10;
	public GameObject ProgressionPlayerAlongBar;

	public Transform startMarker;
	public Transform endMarker;
	private Transform targetPosition;
	private float speed = 0.8f;
	private float journeyLength;
	float distanceToMove;

	void Update(){
		ProgressionPlayerAlongBar.transform.position = Vector3.MoveTowards(ProgressionPlayerAlongBar.transform.position, targetPosition.position, speed);
	}

	void Init() {
		//startTime = Time.time;
		journeyLength = endMarker.position.x - startMarker.position.x ;
		distanceToMove = journeyLength / numberOfChunks;
		targetPosition = startMarker;
		PlayerProgression (new ChunkEnteredEvent ());
	}

	public void PlayerProgression(ChunkEnteredEvent e) {
		targetPosition.position = new Vector3(targetPosition.position.x + distanceToMove, startMarker.position.y, startMarker.position.z);
	}

	public void InitializeSize(MapStartedEvent e){
		numberOfChunks = e.numberOfChunks;
		Init ();
	}
}