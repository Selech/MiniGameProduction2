using UnityEngine;

public class TakeDamageEvent : GameEvent
{
    public string message { get; private set; }

    public TakeDamageEvent(string message)
    {
        this.message = message;
    }
}


public class ExposeStackingListEvent:GameEvent{
	public StackingList stackingList;

	public ExposeStackingListEvent(StackingList stackingList){
		this.stackingList = stackingList;
	}
}

public class SetStartButtonEvent:GameEvent{
	public bool enableBtn;

	public SetStartButtonEvent(bool enableBtn){
		this.enableBtn = enableBtn;
	}
}

public class TriggerPlayerExposure:GameEvent{
	public TriggerPlayerExposure(){
	}
}

public class ExposePlayerOnSwipe:GameEvent{
	public Transform playerTransform;

	public ExposePlayerOnSwipe(Transform playerTransform){
		this.playerTransform = playerTransform;
	}
}

public class ChunkEnteredEvent:GameEvent{
	public ChunkEnteredEvent(){
	}
}

public class WinChunkEnteredEvent:GameEvent{
	public WinChunkEnteredEvent(){
	}
}

public class ChangeParentToPlayer:GameEvent{
	public GameObject gameobject;
	public bool attachToPlayer;

	public ChangeParentToPlayer(){
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


public class MovementInput : GameEvent 
{
	// -1 is left, 0 is center, 1 is right
	public float touchPosition;

	public MovementInput(float touchPosition) 
	{	
		this.touchPosition = touchPosition;
	}
}

// Used to change language in language selection prefab
public class LanguageSelect : GameEvent
{
	public bool isDanish;

	public LanguageSelect(bool isDanish){
		this.isDanish = isDanish;
	}
}

// used to change control scheme in menu prefab
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

	public MapStartedEvent(int numberOfChunks){
		this.numberOfChunks = numberOfChunks;
	}

}

// used to mute sound from the menu prefab
public class MuteMusicEvent : GameEvent 
{
	public bool musicMuted;

	public MuteMusicEvent(bool soundMuted) 
	{
		this.musicMuted = soundMuted;
	}
}
	
public class StartGame:GameEvent{
    public StartGame(){
    }
}

public class BeginRaceEvent : GameEvent {
	public BeginRaceEvent() {
	}
}

public class LoseCarriableEvent : GameEvent {
	public LoseCarriableEvent() {
	}
}