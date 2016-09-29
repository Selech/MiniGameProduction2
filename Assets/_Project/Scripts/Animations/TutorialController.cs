using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {

	public Animation[] animations;

	void Start(){
		animations = GetComponentsInChildren<Animation> ();
		StartCoroutine (StartStackTutorial());
	}

    void OnEnable()
    {
        EventManager.Instance.StartListening<StartGame>(SkipSwipeTutorial);
        EventManager.Instance.StartListening<SkipSwipeTutorial>(SkipStackTutorial);
    }

    void OnDisable()
    {
        EventManager.Instance.StopListening<StartGame>(SkipSwipeTutorial);
        EventManager.Instance.StopListening<SkipSwipeTutorial>(SkipStackTutorial);
    }

    void SkipStackTutorial(SkipSwipeTutorial e)
    {
        StopAllCoroutines();
        StartCoroutine(StartBikeTutorial());
    }

    void SkipSwipeTutorial(StartGame e)
    {
        StopAllCoroutines();
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
			animations [1].Play ("LineStackAnimation");
			animations [2].Play ("FingerSwipeAnimation");
			yield return new WaitForSeconds(2f);
		}
	}
}
