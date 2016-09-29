using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(menuName = "ObstacleCollection")]
public class ObstacleCollection : ScriptableObject 
{
	[SerializeField]
	public List<ObstacleStats> _ObstacleStatCollection;

}
