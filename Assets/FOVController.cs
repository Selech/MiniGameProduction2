using UnityEngine;
using System.Collections;

public class FOVController : MonoBehaviour {
	public float speedTransition = 4;
	public float timeTransition = 4;
	public float minFov,maxFov;
	Camera cam;
	void OnEnable () 
	{
		cam = GetComponent<Camera> ();
		cam.fieldOfView = maxFov;
		EventManager.Instance.StartListening <StartGame>(ChangeFov);
	}

	void OnDisable () 
	{
		EventManager.Instance.StopListening <StartGame>(ChangeFov);
	}
	
	void ChangeFov(GameEvent e)
	{
		StartCoroutine ("ChangeFieldOfViewCo");
	}


	IEnumerator ChangeFieldOfViewCo()
	{
		float v = timeTransition;

		while(v>0)
		{
			
			v -= Time.deltaTime;
			cam.fieldOfView = Mathf.MoveTowards (cam.fieldOfView,minFov,speedTransition*Time.deltaTime);
			yield return null;
		}
		cam.fieldOfView = minFov;
	}
}
