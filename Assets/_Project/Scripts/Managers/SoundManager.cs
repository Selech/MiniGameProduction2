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
		EventManager.Instance.StartListening<BeginRaceEvent> (StartRaceMusic);
		EventManager.Instance.StartListening<MuteMusicEvent> (MuteMusic);
	}

	void OnDisable ()
	{
		EventManager.Instance.StopListening<BeginRaceEvent> (StartRaceMusic);
		EventManager.Instance.StopListening<MuteMusicEvent> (MuteMusic);
	}

	private void MuteMusic(MuteMusicEvent e)
	{
		musicMuted = e.soundMuted;
	}

	private void StartRaceMusic(BeginRaceEvent e)
	{

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


// TODO: Implement this
//	//generic method for playing sound
//	public void PlaySoundVO(int index)
//	{
//
//		foreach (Sound_Item v in _soundEventsContainer._voiceOver_Collection.soundsCollection) 
//		{
//			if (CarriableManager.Instance.isEnglish) {
//				if (v.soundIndex == index && v._Language == Language.English) {
//					PlaySound (v.soundEventName);
//					break;
//				}
//			} else {
//				if (v.soundIndex == index && v._Language == Language.Danish) {
//					PlaySound (v.soundEventName);
//					break;
//				}
//			}
//
//		}
//	}

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
