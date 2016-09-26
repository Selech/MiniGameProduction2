public class TakeDamageEvent : GameEvent
{
    public string message { get; private set; }

    public TakeDamageEvent(string message)
    {
        this.message = message;
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

public class BoostPickupHitEvent : GameEvent
{
	public float boost;

	public BoostPickupHitEvent(float boost)
	{
		this.boost = boost;
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
