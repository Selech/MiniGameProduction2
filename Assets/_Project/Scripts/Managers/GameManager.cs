using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum GameState
{
	Playing,
	Paused,
	Won
}

public class GameManager : MonoBehaviour
{
    public GameState _GameState;
	GyroInput gyroInput;
	SwipeController swipeController;

	[HideInInspector] 
	public float currentTime = 0.0f;

	[Space (10)]
	public float maxTimeCompletion = 10f;
    [Tooltip("Seconds between when the player enters the win chunk and the winning scene start.")]
    public int secondsUntilWinScene = 4;
    [HideInInspector] 
	public bool isPaused = false;
	[HideInInspector] 
	public bool hasGameStarted = false;
	[HideInInspector]
	public bool hasWon = false;
    [HideInInspector]
    public bool isGyro = false;
    [HideInInspector]
    public bool tutorialCompleted = false;
    [HideInInspector]
    public bool isStackingScene = false;
    [HideInInspector]
    public bool isDanish = true;

	void Start ()
	{
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        gyroInput = GetComponent<GyroInput> ();
		swipeController = GetComponent<SwipeController> ();
        DontDestroyOnLoad(gameObject);
	}

	void Awake ()
	{
        
        Time.timeScale = 1;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		
	}

	void Update ()
	{
		if (hasGameStarted) {
			if (!isPaused) {
				UpdateTime ();
			}
		}
	}

	/// <summary>
	/// Restart Game
	/// Call this function when you want to restart the scene
	/// </summary>
	public void RestartGame ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		//  EventManager.TriggerEvent (_eventsContainer.resetGame);
	}

	/// <summary>
	/// Toggle Pause
	/// Call this function when you want to pause
	/// </summary>
	public void TogglePause ()
	{

		isPaused = !isPaused;

		if (isPaused) {
			PauseGame ();
		} else {
			ResumeGame ();
		}
	}

	void OnEnable ()
	{
		EventManager.Instance.StartListening<WinChunkEnteredEvent> (ReactToWin);
        EventManager.Instance.StartListening<ChangeSchemeEvent>(ReactToControlSchemeChange);
        EventManager.Instance.StartListening<StartGame>(StartGame);
        EventManager.Instance.StartListening<RestartGameEvent>(ResetWin);
        EventManager.Instance.StartListening<LanguageSelect>(Language);
    }

	void OnDisable ()
	{
        EventManager.Instance.StopListening<WinChunkEnteredEvent> (ReactToWin);
        EventManager.Instance.StopListening<ChangeSchemeEvent>(ReactToControlSchemeChange);
        EventManager.Instance.StopListening<StartGame>(StartGame);
        EventManager.Instance.StopListening<RestartGameEvent>(ResetWin);
        EventManager.Instance.StopListening<LanguageSelect>(Language);
    }

	void ReactToWin (WinChunkEnteredEvent e)
	{
        StartCoroutine(WinReaction());
        WinGame ();
	}

    IEnumerator WinReaction()
    {
        yield return new WaitForSeconds(secondsUntilWinScene);

        SceneManager.LoadScene(2);
    }

    void ResetWin(RestartGameEvent e)
    {
        hasWon = false;
        _GameState = GameState.Playing;
        isPaused = false;
    }

    void ReactToControlSchemeChange(ChangeSchemeEvent e)
    {
        isGyro = e.isGyro;
    }

    private void StartGame (StartGame e)
	{
		hasGameStarted = true;
		_GameState = GameState.Playing;
		Time.timeScale = 1;
	}

	private void PauseGame ()
	{
		_GameState = GameState.Paused;
		Time.timeScale = Mathf.Epsilon;
	}

	private void ResumeGame ()
	{
		_GameState = GameState.Playing;
		Time.timeScale = 1;
	}

	private void WinGame () 
	{
		_GameState = GameState.Won;
		//Time.timeScale = Mathf.Epsilon;
		hasWon = true;
	}

	private void UpdateTime ()
	{
		currentTime += Time.deltaTime;

//    if(currentTime > maxTimeCompletion) {
//      RestartGame ();
//    }
	}

	public void ChangeScheme (bool isGyro)
	{
		if (isGyro == true) {
			swipeController.enabled = false;
			gyroInput.enabled = true;
		} else {
			swipeController.enabled = true;
			gyroInput.enabled = false;
		}
	}

    public void Language (LanguageSelect e)
    {
        isDanish = e.isDanish;
    }

	private static GameManager _instance;

    public static GameManager Instance {
		get { 
			if (_instance == null) {
				GameObject go = new GameObject ("GameManager");
				go.AddComponent<GameManager> ();
			} 
			return _instance;
		}
	}
}