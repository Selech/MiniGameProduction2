using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	[HideInInspector]
	public bool musicMuted = false;

	//[HideInInspector]
	public bool isDanish;

	[HideInInspector]
	public bool isStrafing = false;

	void Awake ()
	{
		
		_instance = this;
	}

	#region Listeners

	void OnEnable ()
	{
		EventManager.Instance.StartListening<StartGame> (StartRaceMusic);
		EventManager.Instance.StartListening<MuteMusicEvent> (MuteMusic);
		EventManager.Instance.StartListening<LanguageSelect> (LanguageSelection);
		EventManager.Instance.StartListening<WinChunkEnteredEvent> (WonGame);
		EventManager.Instance.StartListening<LoseCarriableEvent> (LostCarriable);
		EventManager.Instance.StartListening<MovementInput> (StrafeBike);
		EventManager.Instance.StartListening<UISoundEvent>(UITap);
	}

	void OnDisable ()
	{
		EventManager.Instance.StopListening<StartGame> (StartRaceMusic);
		EventManager.Instance.StopListening<MuteMusicEvent> (MuteMusic);
		EventManager.Instance.StopListening<LanguageSelect> (LanguageSelection);
		EventManager.Instance.StopListening<WinChunkEnteredEvent> (WonGame);
		EventManager.Instance.StopListening<LoseCarriableEvent> (LostCarriable);
		EventManager.Instance.StopListening<MovementInput> (StrafeBike);
		EventManager.Instance.StartListening<UISoundEvent>(UITap);
	}

	#endregion

	#region Event Methods
	private void MuteMusic(MuteMusicEvent e)
	{
		musicMuted = e.musicMuted;
		PlaySound ("Stop_MusicDrive");
	}

	private void StartRaceMusic(StartGame e)
	{
		PlaySound ("Play_MusicDrive");
		PlaySound ("Play_Pedal");
	}

	private void LanguageSelection(LanguageSelect e) 
	{
		PlaySound ("Play_UITap");
		isDanish = e.isDanish;
	}

	private void WonGame(WinChunkEnteredEvent e)
	{
		PlaySound ("Play_MusicWin");
		PlaySound ("Stop_Pedal");
	}

	private void LostCarriable(LoseCarriableEvent e) 
	{
		PlaySound ("Play_LoseItem");

	}

	private void StrafeBike(MovementInput e)
	{
		if (e.touchPosition == 0f) {
			AkSoundEngine.SetRTPCValue ("Strafing", 0f);
		} else {
			AkSoundEngine.SetRTPCValue ("Strafing",1f);
		}
	}

	private void UITap(UISoundEvent e) {
		PlaySound ("Play_UITap");
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
