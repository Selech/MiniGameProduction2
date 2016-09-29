using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {


	void OnEnable(){
		EventManager.Instance.StartListening<ChunkEnteredEvent> (PlayerProgression);
		EventManager.Instance.StartListening<MapStartedEvent> (InitializeSize);
		EventManager.Instance.StartListening<WinChunkEnteredEvent> (StopMovingBar);
	}

	void OnDisable(){
		EventManager.Instance.StopListening<ChunkEnteredEvent> (PlayerProgression);
		EventManager.Instance.StopListening<MapStartedEvent> (InitializeSize);
		EventManager.Instance.StopListening<WinChunkEnteredEvent> (StopMovingBar);
	}

	private float numberOfChunks = 10;
	public GameObject ProgressionPlayerAlongBar;
	public GameObject ProgBar;
	public Transform startMarker;
	public Transform endMarker;
	private Transform targetPosition;
	private float speed = 0.5f;
	private float journeyLength;
	float distanceToMove;
	bool stopMoving = false;


	/// <summary>
	/// Updating position of the character on the bar
	/// </summary>
	void Update(){
		if (GameManager.Instance.isPaused) {
			
		} else {

			if (!stopMoving) {
				ProgressionPlayerAlongBar.transform.position = Vector3.MoveTowards (ProgressionPlayerAlongBar.transform.position, targetPosition.position, speed);
			} else {
				//ProgressionPlayerAlongBar.transform.position = Vector3.MoveTowards (ProgressionPlayerAlongBar.transform.position, endMarker.position, speed);
			}
		}
	}

	/// <summary>
	/// Calculates by how much the character will move on the bar depending on the total number of chunks
	/// </summary>
	void Init() {
		journeyLength = endMarker.position.x - startMarker.position.x ;
		distanceToMove = journeyLength / numberOfChunks;
		targetPosition = startMarker;
		speed = speed * 10 / numberOfChunks;
		//PlayerProgression (new ChunkEnteredEvent ());
	}

	/// <summary>
	/// Defines what the new target position is
	/// </summary>
	/// <param name="e">E.</param>
	public void PlayerProgression(ChunkEnteredEvent e) {
		targetPosition.position = new Vector3(targetPosition.position.x + distanceToMove, startMarker.position.y, 0);
	}

	/// <summary>
	/// Get the total number of chunks
	/// </summary>
	/// <param name="e">E.</param>
	public void InitializeSize(MapStartedEvent e){
		numberOfChunks = e.numberOfChunks;
		Init ();
	}

	/// <summary>
	/// Stops the character on the progress bar
	/// </summary>
	/// <param name="e">E.</param>
	public void StopMovingBar(WinChunkEnteredEvent e){
		stopMoving = true;
	}
}