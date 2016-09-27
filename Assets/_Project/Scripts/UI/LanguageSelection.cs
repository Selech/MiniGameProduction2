using UnityEngine;
using System.Collections;

public class LanguageSelection : MonoBehaviour {

	public void ChooseDanish() 
	{
		EventManager.Instance.TriggerEvent (new LanguageSelect (true));
		Destroy (gameObject);
	}

	public void ChooseEnglish() 
	{
		EventManager.Instance.TriggerEvent (new LanguageSelect (false));
		Destroy (gameObject);
	}

}
