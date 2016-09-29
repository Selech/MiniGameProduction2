using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	[HideInInspector]
	public bool musicMuted = false;

	//[HideInInspector]
	public bool isDanish;

	void Awake ()
	{
		
		_instance = this;
	}

	#region Listeners

	void OnEnable ()
	{
		EventManager.Instance.StartListening<BeginRaceEvent> (StartRaceMusic);
		EventManager.Instance.StartListening<MuteMusicEvent> (MuteMusic);
		EventManager.Instance.StartListening<LanguageSelect> (LanguageSelection);
		EventManager.Instance.StartListening<WinChunkEnteredEvent> (WonGame);
	}

	void OnDisable ()
	{
		EventManager.Instance.StopListening<BeginRaceEvent> (StartRaceMusic);
		EventManager.Instance.StopListening<MuteMusicEvent> (MuteMusic);
		EventManager.Instance.StopListening<LanguageSelect> (LanguageSelection);
		EventManager.Instance.StopListening<WinChunkEnteredEvent> (WonGame);
	}

	#endregion

	#region Listener Methods
	private void MuteMusic(MuteMusicEvent e)
	{
		musicMuted = e.musicMuted;
	}

	private void StartRaceMusic(BeginRaceEvent e)
	{

	}

	private void LanguageSelection(LanguageSelect e) 
	{
		PlaySound ("Play_UITap");
		isDanish = e.isDanish;
	}

	private void WonGame(WinChunkEnteredEvent e)
	{
		PlaySound ("Play_MusicWin");
	}

	#endregion

	private static SoundManager _instance;

	public static SoundManager Instance {
		get { 
			if (_instance == null) {
				Debug.Log ("No SoundManager");
			}
			return _instance;
		}
	}

	public void PlaySound(string s)
	{
		if(!string.IsNullOrEmpty(s))
		{
			AkSoundEngine.PostEvent (s, this.gameObject);
		}
	}

	//play sound from other object
	public void PlaySound(string s,GameObject b)
	{
		if(!string.IsNullOrEmpty(s) && b != null )
		{
			AkSoundEngine.PostEvent (s, b);
		}
	}
}
