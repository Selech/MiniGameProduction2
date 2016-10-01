using UnityEngine;
using System.Collections;

public class TestPlayerController : MonoBehaviour {
	public float speed;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}
	
	void OnEnable()
	{
		EventManager.Instance.StartListening<BoostPickupHitEvent>(BoostSpeed);
		EventManager.Instance.StartListening<GetBackCarriableHitEvent>(GetBackCarriable);
	}

	void OnDisable()
	{
		EventManager.Instance.StopListening<BoostPickupHitEvent>(BoostSpeed);
		EventManager.Instance.StopListening<GetBackCarriableHitEvent>(GetBackCarriable); 
	}

	public void BoostSpeed(BoostPickupHitEvent e)
	{
		StartCoroutine (ChangeSpeed(e.boost, e.time));
	}

	IEnumerator ChangeSpeed(float speed, float time){
		this.speed += speed;
		yield return new WaitForSeconds (time);
		this.speed -= speed;
	}

	public void GetBackCarriable(GetBackCarriableHitEvent e){
	}

}