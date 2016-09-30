﻿using UnityEngine;
using System.Collections;

public class CameraShakeController : MonoBehaviour 
{
	ShakeCam shakeCam;
	bool isShaking = false;
	float initPosSpeed,initRotSpeed;

	void OnEnable()
	{
		shakeCam = GetComponent<ShakeCam> ();
		initPosSpeed = shakeCam.positionShakeSpeed ;
		initRotSpeed = shakeCam.rotationShakeSpeed ;
		EventManager.Instance.StartListening <FeedbackCameraShakeEvent>(ShakeCameraMore);
	}

	void OnDisable()
	{
		EventManager.Instance.StopListening <FeedbackCameraShakeEvent>(ShakeCameraMore);
	}

	public void ShakeCameraMore(FeedbackCameraShakeEvent e)
	{
		if(!isShaking)
		{
			print ("camera is shaking");
			isShaking = true;
			StartCoroutine (ShakeCameraMoreCo(e.amount,e.duration));
		}
	}

	IEnumerator ShakeCameraMoreCo(float amount,float durationShake)
	{
		shakeCam.positionShakeSpeed = amount;
		shakeCam.rotationShakeSpeed = amount;
		yield return new WaitForSeconds(durationShake);
		shakeCam.positionShakeSpeed = initPosSpeed;
		shakeCam.rotationShakeSpeed = initRotSpeed;
		isShaking = false;
	}
}
