using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour 
{
	public Transform obstacleDirectionIndicator;
	public ObstacleStats currentObstacle;
	public float obstacleInfluenceHit = 10;
	public float delay = 1;

	void OnEnable(){
		SpawnStatikObstacle ();
	}

	void Update()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			SpawnDynamicObstacle ();
		}
	}

	/// <summary>
	/// spawns obstacle when player enters big collider if obstacle is dynamic(car or throwable)
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			SpawnDynamicObstacle ();
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			//EventManager.Instance.TriggerEvent(new ObstacleHitEvent(obstacleInfluenceHit));
		}
	}

	/// <summary>
	/// Used onEnable when the type of the obstacle is static
	/// </summary>
	void SpawnStatikObstacle()
	{
		if(currentObstacle._obstacleType == ObstacleType.statik)
		{
			Rigidbody obstacle = Instantiate (currentObstacle.obstaclePrefab,transform.position,Quaternion.identity) as Rigidbody;
			obstacle.transform.parent = this.transform;
		}
	}

	void SpawnDynamicObstacle()
	{
		switch(currentObstacle._obstacleType)
		{
		case ObstacleType.car:
			SpawnCar ();
			break;
		case ObstacleType.throwable:
			SpawnThrowable ();
			break;
		}
	}

	void SpawnThrowable ()
	{
		Rigidbody r = Instantiate (currentObstacle.obstaclePrefab,transform.position,Quaternion.identity) as Rigidbody;
		//Rigidbody r = obstacle.GetComponent<Rigidbody> ();
		r.AddForce (obstacleDirectionIndicator.forward*currentObstacle.obstacleSpeed,currentObstacle.obstacleForceMode);
		print ("has pan obstacle");
		Destroy (r.gameObject,currentObstacle.obstacleLife);
	}

	void SpawnCar()
	{
		Rigidbody obstacle = Instantiate (currentObstacle.obstaclePrefab,transform.position,obstacleDirectionIndicator.rotation) as Rigidbody;

		obstacle.transform.parent = this.transform;

		StartCoroutine (MoveCar(obstacle.gameObject,currentObstacle.obstacleLife,currentObstacle.obstacleSpeed,currentObstacle.obstacleForceMode));
	}
	/// <summary>
	/// Moves the car and destroys it after some time
	/// </summary>
	/// <returns>The car.</returns>
	/// <param name="carObject">Car object.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="speed">Speed.</param>
	/// <param name="forceModeCar">Force mode car.</param>
	public IEnumerator MoveCar(GameObject carObject,float duration,float speed,ForceMode forceModeCar)
	{
		Rigidbody r = carObject.GetComponent<Rigidbody> ();
		if(carObject)
		{
			yield return new WaitForSeconds (delay);
			float curTime = duration;
			while(curTime > 0)
			{
				
				curTime -= Time.deltaTime;
				if(r)
				{
					r.AddForce (obstacleDirectionIndicator.forward*speed*Time.deltaTime,forceModeCar);
				}
				yield return null;
			}
			//destroy car when finishes going FWD
			Destroy (carObject);
		}

	}
}
