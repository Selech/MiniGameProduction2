using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {

	public Animation[] animations;

	void Start(){
		animations = GetComponentsInChildren<Animation> ();
		StartCoroutine (StartStackTutorial());
	}

	void OnAwake(){
		
	}

	IEnumerator StartStackTutorial(){
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < 3; i++) {
			animations [0].Play ("LineStackAnimation");
			animations [2].Play ("FingerStackAnimation");
			yield return new WaitForSeconds(2f);
		}

		StartCoroutine (StartBikeTutorial());
	}

	IEnumerator StartBikeTutorial(){
		yield return new WaitForSeconds(2);

		for (int i = 0; i < 3; i++) {
			animations [1].Play ("CircleAnimation");
			animations [2].Play ("TapAnimation");
			yield return new WaitForSeconds(2f);
		}
	}
}
