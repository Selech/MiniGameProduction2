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

public class MovementInput : GameEvent 
{
	// -1 is left, 0 is center, 1 is right
	public float touchPosition;

	public MovementInput(float touchPosition) 
	{
	
		this.touchPosition = touchPosition;
	
	}

}
