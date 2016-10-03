using UnityEngine;

public class TakeDamageEvent : GameEvent
{
    public string message { get; private set; }

    public TakeDamageEvent(string message)
    {
        this.message = message;
    }
}

public class SkipSwipeTutorial : GameEvent
{
    public SkipSwipeTutorial()
    {
    }
}

public class ExposeStackingListEvent : GameEvent
{
    public StackingList stackingList;

    public ExposeStackingListEvent(StackingList stackingList)
    {
        this.stackingList = stackingList;
    }
}

public class SetStartButtonEvent : GameEvent
{
    public bool enableBtn;

    public SetStartButtonEvent(bool enableBtn)
    {
        this.enableBtn = enableBtn;
    }
}

public class TriggerPlayerExposure : GameEvent
{
    public TriggerPlayerExposure()
    {
    }
}

public class ExposePlayerOnSwipe : GameEvent
{
    public Transform playerTransform;

    public ExposePlayerOnSwipe(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }
}

public class ChunkEnteredEvent : GameEvent
{

    public GameObject chunk;

    public ChunkEnteredEvent()
    {
    }

    public ChunkEnteredEvent(GameObject chunk)
    {
        this.chunk = chunk;
    }
}

public class WinChunkEnteredEvent : GameEvent
{
    public WinChunkEnteredEvent()
    {
    }
}

public class ChangeParentToPlayer : GameEvent
{
    public GameObject gameobject;
    public bool attachToPlayer;

    public ChangeParentToPlayer()
    {
    }
}

public class ObstacleHitEvent : GameEvent
{
    public float upForce;

    public ObstacleHitEvent(float upForce)
    {
        this.upForce = upForce;
    }
}

public class DamageCarriableEvent : GameEvent
{
    public DamageCarriableEvent()
    {
    }
}

public class BoostPickupHitEvent : GameEvent
{
    public float boost;
    public float time;

    public BoostPickupHitEvent(float boost, float time)
    {
        this.boost = boost;
        this.time = time;
    }
}

public class GetBackCarriableHitEvent : GameEvent
{
    public GetBackCarriableHitEvent()
    {
    }
}

/// <summary>
/// Controls input from gyro and swipe
/// </summary>
public class MovementInput : GameEvent
{
    // -1 is left, 0 is center, 1 is right
    public float touchPosition;

    public MovementInput(float touchPosition)
    {
        this.touchPosition = touchPosition;
    }
}

/// <summary>
/// Used to change language in language selection prefab
/// </summary>
public class LanguageSelect : GameEvent
{
    public bool isDanish;

    public LanguageSelect(bool isDanish)
    {
        this.isDanish = isDanish;
    }
}

/// <summary>
/// used to change control scheme in menu prefab
/// </summary>
public class ChangeSchemeEvent : GameEvent
{
    public bool isGyro;

    public ChangeSchemeEvent(bool isGyro)
    {
        this.isGyro = isGyro;
    }
}

public class MapStartedEvent : GameEvent
{
    public int numberOfChunks;

    public MapStartedEvent(int numberOfChunks)
    {
        this.numberOfChunks = numberOfChunks;
    }

}

/// <summary>
/// used to mute sound in soundmanager from the menu prefab
/// </summary>
public class MuteMusicEvent : GameEvent
{
    public bool musicMuted;

    public MuteMusicEvent(bool soundMuted)
    {
        this.musicMuted = soundMuted;
    }
}

public class StartGame : GameEvent
{
    public StartGame()
    {
    }
}

public class BeginRaceEvent : GameEvent
{
    public BeginRaceEvent()
    {
    }
}

public class LoseCarriableEvent : GameEvent
{
    public LoseCarriableEvent()
    {
    }
}

/// <summary>
/// used for menu tapping sounds in soundmanager
/// </summary>

public class UISoundEvent : GameEvent
{
    public UISoundEvent()
    {
    }
}

public class RestartGameEvent : GameEvent
{
    public RestartGameEvent()
    {
    }
}

/// <summary>
/// used in soundmanager to play sounds when carriable snap to bike
/// </summary>
public class SnapSoundEvent : GameEvent
{
    public SnapSoundEvent()
    {
    }
}

/// <summary>
/// used to inform soundmanager whether the menu is active
/// </summary>
public class MenuActiveEvent : GameEvent
{
    public bool menuActive = false;

    public MenuActiveEvent(bool menuActive)
    {
        this.menuActive = menuActive;
    }
}

public class FeedbackCameraShakeEvent : GameEvent
{
    public float amount;
    public float duration;

    public FeedbackCameraShakeEvent(float amount, float duration)
    {
        this.amount = amount;
        this.duration = duration;
    }
}

#region Tutorial level sound events
// Tutorial level sound events below:

public class FirstSoundEvent : GameEvent
{
    public FirstSoundEvent()
    {
    }
}

public class RoadWorkAheadEvent : GameEvent
{
    public RoadWorkAheadEvent()
    {

    }
}

public class WhatOccursThereEvent : GameEvent
{
    public WhatOccursThereEvent()
    {

    }
}

public class CanUseAsRampEvent : GameEvent
{
    public CanUseAsRampEvent()
    {

    }
}

public class PeopleAreDirtyEvent : GameEvent
{
    public PeopleAreDirtyEvent()
    {

    }
}

public class ThatWasCoolEvent : GameEvent
{
    public ThatWasCoolEvent()
    {

    }
}

public class ThatWentFastEvent : GameEvent
{
    public ThatWentFastEvent()
    {

    }
}

public class SuperEvent : GameEvent
{
    public SuperEvent()
    {

    }
}

public class WatchOutOrHeMightHitUsEvent : GameEvent
{
    public WatchOutOrHeMightHitUsEvent()
    {

    }
}

public class SpeedPowerUpAheadEvent : GameEvent
{
    public SpeedPowerUpAheadEvent()
    {

    }
}

public class AfterSpeedPowerUpEvent : GameEvent
{
    public AfterSpeedPowerUpEvent()
    {

    }
}

public class CarWatchOutEvent : GameEvent
{
    public CarWatchOutEvent()
    {

    }
}

public class WhereDidThatComeFromEvent : GameEvent
{
    public WhereDidThatComeFromEvent()
    {

    }
}

public class NowItsEasyEvent : GameEvent
{
    public NowItsEasyEvent()
    {

    }
}

public class NowItsHarderEvent : GameEvent
{
    public NowItsHarderEvent()
    {

    }
}

public class NowItsHardEvent : GameEvent
{
    public NowItsHardEvent()
    {

    }
}

public class WatchOutEvent : GameEvent
{
    public WatchOutEvent()
    {

    }
}

public class YouHaveToAvoidEvent : GameEvent
{
    public YouHaveToAvoidEvent()
    {

    }
}

public class SetAberrationEvent : GameEvent
{
	public float strenght;
	public float duration;

	public SetAberrationEvent(float strenght, float duration)
	{
		this.strenght = strenght;
		this.duration = duration;
	}
}
#endregion