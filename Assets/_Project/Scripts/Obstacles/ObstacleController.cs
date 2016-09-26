using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	public float obstacleInfluenceHit = 10;

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			EventManager.Instance.TriggerEvent(new ObstacleHitEvent(obstacleInfluenceHit));
		}
	}
}
