using UnityEngine;
using System.Collections;

[System.Serializable]
public enum ObstacleType{
	statik,
	car,
	throwable
}

[System.Serializable]
[CreateAssetMenu(menuName = "Obstacle")]
public class ObstacleStats : ScriptableObject 
{
	public ObstacleType _obstacleType;
	public Rigidbody obstaclePrefab;
	public ForceMode obstacleForceMode = ForceMode.Force;
	public float obstacleSpeed=0;
	public float obstacleLife = 10;
}
