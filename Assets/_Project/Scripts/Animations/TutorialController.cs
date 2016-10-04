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

    void OnEnable()
    {
        EventManager.Instance.StartListening<StartGame>(DisableTutorial);
        EventManager.Instance.StartListening<SkipSwipeTutorial>(DisableStackTutorial);
    }

    void OnDisable()
    {
        EventManager.Instance.StopListening<StartGame>(DisableTutorial);
        EventManager.Instance.StopListening<SkipSwipeTutorial>(DisableStackTutorial);
    }

    void DisableStackTutorial(SkipSwipeTutorial e)
    {
        StopAllCoroutines();
        StartCoroutine(StartBikeTutorial());
    }

    void DisableTutorial(StartGame e)
    {
        this.gameObject.SetActive(false);
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
		yield return new WaitForSeconds(1f);
        
        for (int i = 0; i < 3; i++) {
			animations [1].Play ("CircleAnimation");
			animations [2].Play ("TapAnimation");
			yield return new WaitForSeconds(2f);
		}
        EventManager.Instance.TriggerEvent(new StartStackTutorialEvent());
        yield return new WaitForSeconds(5f);
        StartCoroutine(StartBikeTutorial());
    }
}
