
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
    public ObstacleKind obstacleType;
    public DamageCarriableEvent(ObstacleKind obstacleType)
    {
        this.obstacleType = obstacleType;
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

public class StartWindEvent : GameEvent
{
    public Vector3 windPosition;
    public float windForce;

    public StartWindEvent(Vector3 windPosition, float windForce)
    {
        this.windPosition = windPosition;
        this.windForce = windForce;
    }
}

public class StopWindEvent : GameEvent
{
    public StopWindEvent()
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

public class PlayerHitRoadProp : GameEvent
{
    public GameObject roadProp;
    public PlayerHitRoadProp(GameObject roadProp)
    {
        this.roadProp = roadProp;
    }
}

public class NearEnd01Event : GameEvent
{
    public NearEnd01Event()
    {

    }
}
public class NearEnd02Event : GameEvent
{
    public NearEnd02Event()
    {

    }
}
public class NearEnd03Event : GameEvent
{
    public NearEnd03Event()
    {

    }
}

public class AtEnd01Event : GameEvent
{
    public AtEnd01Event()
    {

    }
}

public class AtEnd02Event : GameEvent
{
    public AtEnd02Event()
    {

    }
}

public class StartStackingSceneEvent : GameEvent
{
    public StartStackingSceneEvent()
    {

    }
}

public class MapProgressionForSoundEvent : GameEvent
{
    public float percentage;
    
    public MapProgressionForSoundEvent(float percentage)
    {
        this.percentage = percentage;
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

public class IntroVO1event : GameEvent
{
    public IntroVO1event()
    {

    }
}

public class IntroVO2event : GameEvent
{
    public IntroVO2event()
    {

    }
}

public class IntroVO3event : GameEvent
{
    public IntroVO3event()
    {

    }
}
#endregion

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

public class EnterRampEvent : GameEvent
{
    public EnterRampEvent()
    {

    }
}

public class FlyingEvent : GameEvent
{
    public FlyingEvent()
    {

    }
}

public class LandingEvent : GameEvent
{
    public LandingEvent()
    {

    }
}

