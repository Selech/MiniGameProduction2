using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	//[HideInInspector]
	public bool musicMuted = false;

	void Awake ()
	{
		_instance = this;
	}

	void OnEnable ()
	{
		EventManager.Instance.StartListening<MuteMusicEvent> (MuteMusic);
	}

	void OnDisable ()
	{
		EventManager.Instance.StopListening<MuteMusicEvent> (MuteMusic);
	}

	private void MuteMusic(MuteMusicEvent e)
	{
		musicMuted = e.soundMuted;
	}

	private static SoundManager _instance;

	public static SoundManager Instance {
		get { 
			if (_instance == null) {
				Debug.Log ("No SoundManager");
			}
			return _instance;
		}
	}
}
