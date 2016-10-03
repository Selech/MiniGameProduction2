using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    #region Booleans for game states

   
    [HideInInspector]
    public bool musicMuted = false;

    [HideInInspector]
    public bool isDanish;

    [HideInInspector]
    public bool isStrafing = false;

    [HideInInspector]
    public bool isTutorial = true;

    bool drivingStarted = false;
    bool stackingSceneActive = false;

    #endregion

    #region Booleans for sounds being played only once

    bool ohNoSoundPlayed = false;

    #endregion

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
        EventManager.Instance.StartListening<LoseCarriableEvent>(LoseCarriable);
        EventManager.Instance.StartListening<GetBackCarriableHitEvent>(GainCarriable);
        EventManager.Instance.StartListening<StartWindEvent>(HitByWind);
        EventManager.Instance.StartListening<IntroVO1event>(IntroVoice1);
        EventManager.Instance.StartListening<IntroVO2event>(IntroVoice2);
        EventManager.Instance.StartListening<IntroVO3event>(IntroVoice3);
        EventManager.Instance.StartListening<BoostPickupHitEvent>(BoostPickUp);
        EventManager.Instance.StartListening<StartStackingSceneEvent>(StackingSceneSound);


        // tutorial sounds below
        EventManager.Instance.StartListening<ChangeSchemeEvent>(ChangeScheme);
        EventManager.Instance.StartListening<FirstSoundEvent>(FirstSound);
        EventManager.Instance.StartListening<RoadWorkAheadEvent>(RoadWorkAhead);
        EventManager.Instance.StartListening<WhatOccursThereEvent>(WhatOccursThere);
        EventManager.Instance.StartListening<CanUseAsRampEvent>(WeCanUseThatAsRamp);
        EventManager.Instance.StartListening<PeopleAreDirtyEvent>(PeopleAreDirty);
        EventManager.Instance.StartListening<ThatWasCoolEvent>(ThatWasCool);
        EventManager.Instance.StartListening<ThatWentFastEvent>(ThatWentFast);
        EventManager.Instance.StartListening<SuperEvent>(Superr);
        EventManager.Instance.StartListening<WatchOutOrHeMightHitUsEvent>(WatchOutCar);
        EventManager.Instance.StartListening<SpeedPowerUpAheadEvent>(SpeedPowerUpAhead);
        EventManager.Instance.StartListening<AfterSpeedPowerUpEvent>(AfterSpeedPowerUp);
        EventManager.Instance.StartListening<CarWatchOutEvent>(CarWatchOut);
        EventManager.Instance.StartListening<WhereDidThatComeFromEvent>(WhereDidThatComeFrom);
        EventManager.Instance.StartListening<NowItsEasyEvent>(NowItsEasy);
        EventManager.Instance.StartListening<NowItsHarderEvent>(NowItsHarder);
        EventManager.Instance.StartListening<NowItsHardEvent>(NowItsHard);
        EventManager.Instance.StartListening<WatchOutEvent>(WatchOut);
        EventManager.Instance.StartListening<YouHaveToAvoidEvent>(YouHaveToAvoid);
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
        EventManager.Instance.StopListening<LoseCarriableEvent>(LoseCarriable);
        EventManager.Instance.StopListening<GetBackCarriableHitEvent>(GainCarriable);
        EventManager.Instance.StopListening<StartWindEvent>(HitByWind);
        EventManager.Instance.StopListening<IntroVO1event>(IntroVoice1);
        EventManager.Instance.StopListening<IntroVO2event>(IntroVoice2);
        EventManager.Instance.StopListening<IntroVO3event>(IntroVoice3);
        EventManager.Instance.StopListening<BoostPickupHitEvent>(BoostPickUp);
        EventManager.Instance.StopListening<StartStackingSceneEvent>(StackingSceneSound);

        // tutorial sounds below
        EventManager.Instance.StopListening<ChangeSchemeEvent>(ChangeScheme);
        EventManager.Instance.StopListening<FirstSoundEvent>(FirstSound);
        EventManager.Instance.StopListening<RoadWorkAheadEvent>(RoadWorkAhead);
        EventManager.Instance.StopListening<WhatOccursThereEvent>(WhatOccursThere);
        EventManager.Instance.StopListening<CanUseAsRampEvent>(WeCanUseThatAsRamp);
        EventManager.Instance.StopListening<PeopleAreDirtyEvent>(PeopleAreDirty);
        EventManager.Instance.StopListening<ThatWasCoolEvent>(ThatWasCool);
        EventManager.Instance.StopListening<ThatWentFastEvent>(ThatWentFast);
        EventManager.Instance.StopListening<SuperEvent>(Superr);
        EventManager.Instance.StopListening<WatchOutOrHeMightHitUsEvent>(WatchOutCar);
        EventManager.Instance.StopListening<SpeedPowerUpAheadEvent>(SpeedPowerUpAhead);
        EventManager.Instance.StopListening<AfterSpeedPowerUpEvent>(AfterSpeedPowerUp);
        EventManager.Instance.StopListening<CarWatchOutEvent>(CarWatchOut);
        EventManager.Instance.StopListening<WhereDidThatComeFromEvent>(WhereDidThatComeFrom);
        EventManager.Instance.StopListening<NowItsEasyEvent>(NowItsEasy);
        EventManager.Instance.StopListening<NowItsHarderEvent>(NowItsHarder);
        EventManager.Instance.StopListening<NowItsHardEvent>(NowItsHard);
        EventManager.Instance.StopListening<WatchOutEvent>(WatchOut);
        EventManager.Instance.StopListening<YouHaveToAvoidEvent>(YouHaveToAvoid);
    }

    #endregion

    #region Event Methods
    private void MuteMusic(MuteMusicEvent e)
    {
        musicMuted = e.musicMuted;
        if (musicMuted)
        {
            PlaySound("Stop_MusicDrive");
            if (stackingSceneActive)
            {
                PlaySound("Stop_MenuMusic");
            }
        }
        else if (!musicMuted && drivingStarted)
        {

            PlaySound("Play_MusicDrive");

        } else if (!musicMuted && !drivingStarted)
        {
            if (stackingSceneActive)
            {
                PlaySound("Play_MenuMusic");
            }
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

        isDanish = e.isDanish;
        if (isDanish)
        {
            AkSoundEngine.SetCurrentLanguage("Danish");
            AkBankManager.LoadBank("nysb", false, true);
            PlaySound("Play_UITap");
        }
        else
        {
            AkSoundEngine.SetCurrentLanguage("English(US)");
            AkBankManager.LoadBank("nysb", false, true);
            PlaySound("Play_UITap");
        }

    }

    private void WonGame(WinChunkEnteredEvent e)
    {
        drivingStarted = false;
        PlaySound("Stop_MusicDrive");
        PlaySound("Stop_Pedal");
        PlaySound("Play_MusicWin");
    }

    private void HitObstacle(DamageCarriableEvent e)
    {
        PlaySound("Play_LoseItem");
        if (ohNoSoundPlayed == false)
        {
            PlaySound("Play_MusVO16");
            ohNoSoundPlayed = true;
        }
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

    // place all stops in here, happens only at end of game
    private void StopAllSounds(RestartGameEvent e)
    {
        drivingStarted = false;
        PlaySound("Stop_MusicDrive");
        PlaySound("Stop_MenuMusic");
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
        }
        else if (!GameManager.Instance.isPaused && drivingStarted)
        {
            PlaySound("Play_Pedal");
        }
    }

    private void LoseCarriable(LoseCarriableEvent e)
    {
        PlaySound("Play_LoseItem");
        PlaySound("Play_MusVO17");
    }

    private void GainCarriable(GetBackCarriableHitEvent e)
    {
        PlaySound("Play_MisVO10");
        PlaySound("Play_Pickup");
    }

    private void HitByWind(StartWindEvent e)
    {
        if (isTutorial)
        {
            PlaySound("Play_MusVO5");
        }
    }

    private void BoostPickUp(BoostPickupHitEvent e)
    {
        PlaySound("Play_SpeedUp");
    }

    private void StackingSceneSound(StartStackingSceneEvent e)
    {
        PlaySound("Play_MenuMusic");
    }
    #endregion

    #region Tutorial sounds:

    private void IntroVoice1(IntroVO1event e)
    {
        PlaySound("Play_IntroVO1");
    }

    private void IntroVoice2(IntroVO2event e)
    {
        PlaySound("Play_IntroVO1");
    }

    private void IntroVoice3(IntroVO3event e)
    {
        PlaySound("Play_IntroVO1");
    }

    private void ChangeScheme(ChangeSchemeEvent e)
    {
        if (isTutorial)
        {
            if (GameManager.Instance.isGyro)
            {
                PlaySound("Play_MisVO19");
            }
            else
            {
                PlaySound("Play_MisVO20");
            }
        }
    }

    private void FirstSound(FirstSoundEvent e)
    {
        PlaySound("Play_MisVO4");
    }

    private void RoadWorkAhead(RoadWorkAheadEvent e)
    {
        PlaySound("Play_MusVO4");
    }

    private void WindBlown()
    {
        PlaySound("Play_MusVO5");
    }

    private void WhatOccursThere(WhatOccursThereEvent e)
    {
        PlaySound("Play_MisVO6");
    }

    private void WeCanUseThatAsRamp(CanUseAsRampEvent e)
    {
        PlaySound("Play_MisVO7");
    }

    private void PeopleAreDirty(PeopleAreDirtyEvent e)
    {
        PlaySound("Play_MusVO6");
    }

    private void ThatWasCool(ThatWasCoolEvent e)
    {
        PlaySound("Play_MisVO8");
    }

    private void ThatWentFast(ThatWentFastEvent e)
    {
        PlaySound("Play_MusVO7");
    }

    private void Superr(SuperEvent e)
    {
        PlaySound("Play_MusVO8");
    }

    private void WatchOutCar(WatchOutOrHeMightHitUsEvent e)
    {
        PlaySound("Play_MusVO10");
    }

    private void SpeedPowerUpAhead(SpeedPowerUpAheadEvent e)
    {
        PlaySound("Play_MisVO18");
    }

    private void AfterSpeedPowerUp(AfterSpeedPowerUpEvent e)
    {
        PlaySound("Play_MusVO20");
    }

    private void CarWatchOut(CarWatchOutEvent e)
    {
        PlaySound("Play_MusVO19");
    }

    private void WhereDidThatComeFrom(WhereDidThatComeFromEvent e)
    {
        PlaySound("Play_MusVO9");
    }
    private void NowItsEasy(NowItsEasyEvent e)
    {
        PlaySound("Play_MusVO21");
    }

    private void NowItsHarder(NowItsHarderEvent e)
    {
        PlaySound("Play_MusVO22");
    }

    private void NowItsHard(NowItsHardEvent e)
    {
        PlaySound("Play_MusVO23");
    }

    private void WatchOut(WatchOutEvent e)
    {
        PlaySound("Play_MusVO14");
    }

    private void YouHaveToAvoid(YouHaveToAvoidEvent e)
    {
        PlaySound("Play_MisVO16");
    }

    private void NearEnd01(NearEnd01Event e)
    {
        PlaySound("Play_MusVO11");
    }

    private void NearEnd02(NearEnd02Event e)
    {
        PlaySound("Play_MisVO11");
    }

    private void NearEnd03(NearEnd03Event e)
    {
        PlaySound("Play_MusVO12");
    }

    private void AtEnd01(AtEnd01Event e)
    {
        PlaySound("Play_MisVO12");
    } 

    private void AtEnd02(AtEnd02Event e)
    {
        PlaySound("Play_MisVO13");
    }
    #endregion

    #region 
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


    #endregion
}
