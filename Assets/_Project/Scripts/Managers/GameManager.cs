using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum GameState {
  Playing,
  Paused
}

public class GameManager : MonoBehaviour {

  public GameState _GameState;

  [HideInInspector] 
  public float currentTime = 0.0f;

  [Space(10)]
  public float maxTimeCompletion = 10f;

  [HideInInspector] 
  public bool isPaused = false;
  [HideInInspector] 
  public bool hasGameStarted = false;

  void Start () {
    StartGame ();
  }

  void Awake() {
    Screen.sleepTimeout = SleepTimeout.NeverSleep;
    _instance = this;
  }

  void Update () {
    if(hasGameStarted)  {
      if(!isPaused) {
        UpdateTime ();
      }
    }
  }

  /// <summary>
  /// Restart Game
  /// Call this function when you want to restart the scene
  /// </summary>
  public void RestartGame() {
    SceneManager.LoadScene (SceneManager.GetActiveScene().name);
    //  EventManager.TriggerEvent (_eventsContainer.resetGame);
  }

  /// <summary>
  /// Toggle Pause
  /// Call this function when you want to pause
  /// </summary>
  public void TogglePause() {

    isPaused = !isPaused;

    if(isPaused) {
      PauseGame ();
    } else {
      ResumeGame();
    }
  }

		void OnEnable(){
		EventManager.Instance.StartListening<WinChunkEnteredEvent>(ReactToWin);
		}

		void OnDisable(){
		EventManager.Instance.StopListening<WinChunkEnteredEvent>(ReactToWin);
		}

	void ReactToWin(WinChunkEnteredEvent e){
		PauseGame ();
	}

  private void StartGame() {
    // EventManager.TriggerEvent (_eventsContainer.beginGame);
    hasGameStarted = true;
    _GameState = GameState.Playing;
    Time.timeScale = 1;
  }

  private void PauseGame() {
    // EventManager.TriggerEvent (_eventsContainer.pauseGame);
    _GameState = GameState.Paused;
    Time.timeScale = Mathf.Epsilon;
  }

  private void ResumeGame() {
    //EventManager.TriggerEvent (_eventsContainer.resumeGame);
    _GameState = GameState.Playing;
    Time.timeScale = 1;
  }

  private void UpdateTime() {
    currentTime += Time.deltaTime;

//    if(currentTime > maxTimeCompletion) {
//      RestartGame ();
//    }
  }

  private static GameManager _instance;
  public static GameManager Instance    {
    get { 
      if(_instance == null) {
        GameObject go = new GameObject ("GameManager");
        go.AddComponent<GameManager> ();
      }
      return _instance;
    }
  }
}