using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    [HideInInspector]
    public bool musicMuted = false;

    [HideInInspector]
    public bool isDanish;

    [HideInInspector]
    public bool isStrafing = false;

    bool drivingStarted = false;


    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    #region Listeners

    void OnEnable()
    {
        EventManager.Instance.StartListening<StartGame>(StartRaceMusic);
        EventManager.Instance.StartListening<MuteMusicEvent>(MuteMusic);
        EventManager.Instance.StartListening<LanguageSelect>(LanguageSelection);
        EventManager.Instance.StartListening<WinChunkEnteredEvent>(WonGame);
        EventManager.Instance.StartListening<DamageCarriableEvent>(HitObstacle);
        EventManager.Instance.StartListening<MovementInput>(StrafeBike);
        EventManager.Instance.StartListening<UISoundEvent>(UITap);
        EventManager.Instance.StartListening<RestartGameEvent>(StopAllSounds);
        EventManager.Instance.StartListening<SnapSoundEvent>(SnapSound);
        EventManager.Instance.StartListening<MenuActiveEvent>(MenuActive);
    }

    void OnDisable()
    {
        EventManager.Instance.StopListening<StartGame>(StartRaceMusic);
        EventManager.Instance.StopListening<MuteMusicEvent>(MuteMusic);
        EventManager.Instance.StopListening<LanguageSelect>(LanguageSelection);
        EventManager.Instance.StopListening<WinChunkEnteredEvent>(WonGame);
        EventManager.Instance.StopListening<DamageCarriableEvent>(HitObstacle);
        EventManager.Instance.StopListening<MovementInput>(StrafeBike);
        EventManager.Instance.StopListening<UISoundEvent>(UITap);
        EventManager.Instance.StopListening<RestartGameEvent>(StopAllSounds);
        EventManager.Instance.StopListening<SnapSoundEvent>(SnapSound);
        EventManager.Instance.StopListening<MenuActiveEvent>(MenuActive);
    }

    #endregion

    #region Event Methods
    private void MuteMusic(MuteMusicEvent e)
    {
        musicMuted = e.musicMuted;
        if (musicMuted)
        {
            PlaySound("Stop_MusicDrive");
        }
        else if (!musicMuted && drivingStarted)
        {

            PlaySound("Play_MusicDrive");
        }
    }

    private void StartRaceMusic(StartGame e)
    {
        drivingStarted = true;
        if (!musicMuted)
        {
            PlaySound("Play_MusicDrive");
        }
        PlaySound("Play_Pedal");
    }

    private void LanguageSelection(LanguageSelect e)
    {
        PlaySound("Play_UITap");
        isDanish = e.isDanish;
        if (isDanish)
        {
            AkSoundEngine.SetCurrentLanguage("Danish");
        }
        else
        {
            AkSoundEngine.SetCurrentLanguage("English(US)");
        }

    }

    private void WonGame(WinChunkEnteredEvent e)
    {
        PlaySound("Play_MusicWin");
        PlaySound("Stop_MusicDrive");
        PlaySound("Stop_Pedal");
    }

    private void HitObstacle(DamageCarriableEvent e)
    {
        PlaySound("Play_LoseItem");

    }

    private void StrafeBike(MovementInput e)
    {
        if (e.touchPosition == 0f)
        {
            AkSoundEngine.SetRTPCValue("Strafing", 0f);
        }
        else
        {
            AkSoundEngine.SetRTPCValue("Strafing", 1f);
        }
    }

    private void UITap(UISoundEvent e)
    {
        PlaySound("Play_UITap");
    }

    // place all stops in here
    private void StopAllSounds(RestartGameEvent e)
    {
        PlaySound("Stop_MusicDrive");
        PlaySound("Stop_Pedal");
    }

    private void SnapSound(SnapSoundEvent e)
    {
        PlaySound("Play_UISnap");
    }

    private void MenuActive(MenuActiveEvent e)
    {
       
        if (GameManager.Instance.isPaused)
        {
            PlaySound("Stop_Pedal");
        } else
        {
            PlaySound("Play_Pedal");
        }
    }

    #endregion

    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("No SoundManager");
            }
            return _instance;
        }
    }

    public void PlaySound(string s)
    {
        if (!string.IsNullOrEmpty(s))
        {
            AkSoundEngine.PostEvent(s, this.gameObject);
        }
    }

    //play sound from other object
    public void PlaySound(string s, GameObject b)
    {
        if (!string.IsNullOrEmpty(s) && b != null)
        {
            AkSoundEngine.PostEvent(s, b);
        }
    }
}
