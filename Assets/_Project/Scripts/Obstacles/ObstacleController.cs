using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour 
{
	public bool isRandom = false;
	public Transform obstacleDirectionIndicator;
	[Tooltip("Use if spawner is not random")]
	public ObstacleStats currentObstacle;
	[Tooltip("Use for static objects if spawner is random")]
	public ObstacleCollection obstacleCollection_Statik;
	[Tooltip("Use for throwables objects if spawner is random")]
	public ObstacleCollection obstacleCollection_Throwables;
	[Tooltip("Use for car objects if spawner is random")]
	public ObstacleCollection obstacleCollection_Cars;
	public float obstacleInfluenceHit = 10;
	public float delay = 1;
	bool isTriggered = false;
	void OnEnable()
	{
		SpawnStatikObstacle ();
		isTriggered = false;
	}

	/// <summary>
	/// spawns obstacle when player enters big collider if obstacle is dynamic(car or throwable)
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.CompareTag("BikePlate") && !isTriggered)
		{
			isTriggered = true;
			SpawnDynamicObstacle ();
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.CompareTag("BikePlate"))
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
			if (isRandom) {
				int rand = Random.Range (0,obstacleCollection_Statik._ObstacleStatCollection.Count);
				Rigidbody obstacle = Instantiate (obstacleCollection_Statik._ObstacleStatCollection[rand].obstaclePrefab,transform.position,obstacleDirectionIndicator.rotation) as Rigidbody;
				obstacle.transform.parent = this.transform;
			} else {
				Rigidbody obstacle = Instantiate (currentObstacle.obstaclePrefab,transform.position,obstacleDirectionIndicator.rotation) as Rigidbody;
				obstacle.transform.parent = this.transform;
			}

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
		if (isRandom) {
			int rand = Random.Range (0,obstacleCollection_Throwables._ObstacleStatCollection.Count);
			Rigidbody r = Instantiate (obstacleCollection_Throwables._ObstacleStatCollection[rand].obstaclePrefab,transform.position,Quaternion.identity) as Rigidbody;
			//Rigidbody r = obstacle.GetComponent<Rigidbody> ();
			r.AddForce (obstacleDirectionIndicator.forward*obstacleCollection_Throwables._ObstacleStatCollection[rand].obstacleSpeed,obstacleCollection_Throwables._ObstacleStatCollection[rand].obstacleForceMode);
			print ("has pan obstacle");
			Destroy (r.gameObject,obstacleCollection_Throwables._ObstacleStatCollection[rand].obstacleLife);
		} else {
			Rigidbody r = Instantiate (currentObstacle.obstaclePrefab,transform.position,Quaternion.identity) as Rigidbody;
			//Rigidbody r = obstacle.GetComponent<Rigidbody> ();
			r.AddForce (obstacleDirectionIndicator.forward*currentObstacle.obstacleSpeed,currentObstacle.obstacleForceMode);
			print ("has pan obstacle");
			Destroy (r.gameObject,currentObstacle.obstacleLife);
		}


	}

	void SpawnCar()
	{

		if (isRandom) {
			int rand = Random.Range (0, obstacleCollection_Cars._ObstacleStatCollection.Count);
			Rigidbody obstacle = Instantiate (obstacleCollection_Cars._ObstacleStatCollection[rand].obstaclePrefab,transform.position,obstacleDirectionIndicator.rotation) as Rigidbody;

			obstacle.transform.parent = this.transform;

			StartCoroutine (MoveCar(obstacle.gameObject,obstacleCollection_Cars._ObstacleStatCollection[rand].obstacleLife,obstacleCollection_Cars._ObstacleStatCollection[rand].obstacleSpeed,obstacleCollection_Cars._ObstacleStatCollection[rand].obstacleForceMode));

		} else {
			Rigidbody obstacle = Instantiate (currentObstacle.obstaclePrefab,transform.position,obstacleDirectionIndicator.rotation) as Rigidbody;

			obstacle.transform.parent = this.transform;

			StartCoroutine (MoveCar(obstacle.gameObject,currentObstacle.obstacleLife,currentObstacle.obstacleSpeed,currentObstacle.obstacleForceMode));
		}


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
