using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIDebugger : MonoBehaviour {

	private string timeDisplayText;
	private bool toggle = false;

	void OnGUI () {


		if (GUI.Button (new Rect (Screen.width - 200, 0, 200, 200), "", GUIStyle.none)) {

			toggle = !toggle;
		}


		if (toggle) {

			GUIStyle myStyle = new GUIStyle ();

			//myStyle.fontSize = fontSize;

			timeDisplayText = GUI.TextField (new Rect (Screen.width / 2f, 10, 200, 50), GameManager.Instance.currentTime.ToString (), 25);

			if (GUI.Button (new Rect (Screen.width / 2.5f, Screen.height - 200, 400, 200), "Pause/Resume")) {
				GameManager.Instance.TogglePause ();
			}

			if (GUI.Button (new Rect (Screen.width - 600, Screen.height - 200, 400, 200), "Next scene")) {
				LoadNextLevel ();
			}

			if (GUI.Button (new Rect (200, Screen.height - 200, 400, 200), "Previous scene")) {
				LoadPreviousLevel ();
			}

			if (GUI.Button (new Rect (200, 0, 400, 200), "Restart scene")) {
				LoadSameLevel ();
			}
		}
	}

	/// <summary>
	/// Loads the previous level.
	/// </summary>
	public void LoadPreviousLevel(){
		if (SceneManager.GetActiveScene().buildIndex != 0) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);
		}
	}

	/// <summary>
	/// Loads the next level.
	/// </summary>
	public void LoadNextLevel() {
		if (SceneManager.GetActiveScene ().buildIndex + 1 != SceneManager.sceneCountInBuildSettings) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}
	}

	/// <summary>
	/// Loads the same level.
	/// </summary>
	public void LoadSameLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}


}