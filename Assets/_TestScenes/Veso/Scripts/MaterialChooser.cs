using UnityEngine;
using System.Collections;

public enum BuildingType{
	a,b,c
}

public class MaterialChooser : MonoBehaviour {

	MeshRenderer renderer;

	public BuildingType _buildingType;
	public Material[] materialA;
	public Material[] materialB;
	public Material[] materialC;

	// Use this for initialization
	void OnEnable () 
	{

		renderer = GetComponent<MeshRenderer> ();

		ChangeMaterial ();
	}
	
	void ChangeMaterial()
	{
		int rand = Random.Range (0,2);

		switch(_buildingType)
		{
		case BuildingType.a:
			renderer.sharedMaterial = materialA [rand];
			break;
		case BuildingType.b:
			renderer.sharedMaterial = materialB [rand];
			break;
		case BuildingType.c:
			renderer.sharedMaterial = materialC [rand];
			break;
		}
	}
}
