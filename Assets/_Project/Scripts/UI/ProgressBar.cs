using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{


    void OnEnable()
    {
        EventManager.Instance.StartListening<ChunkEnteredEvent>(PlayerProgression);
        EventManager.Instance.StartListening<MapStartedEvent>(InitializeSize);
        EventManager.Instance.StartListening<ExposePlayerOnSwipe>(SetupPlayerTransform);
    }

    void OnDisable()
    {
        EventManager.Instance.StopListening<ChunkEnteredEvent>(PlayerProgression);
        EventManager.Instance.StopListening<MapStartedEvent>(InitializeSize);
        EventManager.Instance.StopListening<ExposePlayerOnSwipe>(SetupPlayerTransform);
    }

    private float numberOfChunks = 10;
    public GameObject ProgressionPlayerAlongBar;
    public GameObject ProgBar;
    public Transform startMarker;
    public Transform endMarker;
    private Transform targetPosition;
    private float journeyLength;
    float distanceToMove;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private float chunkLength = 1;
    private Transform playerTransform;
    private float positionPercentage = 0;
    [SerializeField]
    private float playerPositionInChunk = 0;
    private int numberOfPreviousChunks = -1;
    private bool gameStarted = false;
    // used to map sound progression
    private float positionInPercentage;

    /// <summary>
    /// Updating position of the character on the bar
    /// </summary>
    void Update()
    {

        if (gameStarted)
        {

            if (GameManager.Instance.isPaused)
            {

            }
            else
            {

                playerPositionInChunk = Vector3.Distance(playerTransform.position, startPoint);
                positionPercentage = playerPositionInChunk / chunkLength;
                ProgressionPlayerAlongBar.transform.position = new Vector3(startMarker.position.x + numberOfPreviousChunks * distanceToMove + positionPercentage * distanceToMove, startMarker.position.y, 0);
                positionInPercentage = ProgressionPlayerAlongBar.transform.position.x/endMarker.transform.position.x;
                EventManager.Instance.TriggerEvent(new MapProgressionForSoundEvent(positionInPercentage));
            }
        }
    }

    /// <summary>
    /// Calculates by how much the character will move on the bar depending on the total number of chunks
    /// </summary>
    void Init()
    {
        journeyLength = endMarker.position.x - startMarker.position.x;
        distanceToMove = journeyLength / numberOfChunks;
    }

    /// <summary>
    /// Defines what the new target position is
    /// </summary>
    /// <param name="e">E.</param>
    public void PlayerProgression(ChunkEnteredEvent e)
    {
        if (numberOfPreviousChunks == -1)
        {
            EventManager.Instance.TriggerEvent(new PlayerHitsTheFirstRoadChunk());
            gameStarted = true;
            ProgBar.SetActive(true);
        }
       
        ChunkScript script = e.chunk.GetComponent<ChunkScript>();
        startPoint = script.StartPoint.transform.position;
        endPoint = script.EndPoint.transform.position;
        chunkLength = Vector3.Distance(startPoint, endPoint);
        numberOfPreviousChunks++;
    }

    /// <summary>
    /// Get the total number of chunks
    /// </summary>
    /// <param name="e">E.</param>
    public void InitializeSize(MapStartedEvent e)
    {
        numberOfChunks = e.numberOfChunks;
        Init();
    }

    /// <summary>
    /// Setups the player transform and sets a variable that detects when game starts
    /// </summary>
    /// <param name="e">E.</param>
    void SetupPlayerTransform(ExposePlayerOnSwipe e)
    {
        playerTransform = e.playerTransform;
    }

}