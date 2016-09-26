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

public class SwipeMovement : GameEvent 
{
	// -1 is left, 0 is center, 1 is right
	public float touchPosition;

	public SwipeMovement(float touchPosition) 
	{
	
		this.touchPosition = touchPosition;
	
	}

}

public class PressMovement : GameEvent 
{
	// -1 is left, 0 is center, 1 is right
	public float buttonPressed;

	public PressMovement(float buttonPressed)
	{
	
		this.buttonPressed = buttonPressed;

	}

}