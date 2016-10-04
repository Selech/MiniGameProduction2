using UnityEngine;
using System.Collections;

public class ChromaticController : MonoBehaviour {

	SimpleChromaticAberration aberration;

	// Use this for initialization
	void Start () {
		aberration = GetComponent<SimpleChromaticAberration> ();
	}

	void OnEnable()
	{
		EventManager.Instance.StartListening<SetAberrationEvent>(SetAberration);
	}

	void OnDisable()
	{
		EventManager.Instance.StopListening<SetAberrationEvent>(SetAberration);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.H))
		{
			EventManager.Instance.TriggerEvent(new SetAberrationEvent (3,0.35f));
		}
	}

	void SetAberration(SetAberrationEvent e)
	{
		StartCoroutine ("SetAberrationCo",e);
	}

	public IEnumerator SetAberrationCo(SetAberrationEvent e)
	{
		aberration.amount = e.strenght;
		yield return new WaitForSeconds (e.duration);
		aberration.amount = 0.1f;
	}
}
