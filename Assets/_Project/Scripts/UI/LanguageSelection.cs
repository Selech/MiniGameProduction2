using UnityEngine;
using System.Collections;

public class LanguageSelection : MonoBehaviour {

    void Start()
    {
       AkSoundEngine.PostEvent("Play_MenuMusic", this.gameObject);
    }

	public void ChooseDanish() 
	{
		EventManager.Instance.TriggerEvent (new LanguageSelect (true));
        AkSoundEngine.PostEvent("Stop_MenuMusic", this.gameObject);
        Destroy (gameObject);
    }

	public void ChooseEnglish() 
	{
		EventManager.Instance.TriggerEvent (new LanguageSelect (false));
        AkSoundEngine.PostEvent("Stop_MenuMusic", this.gameObject);
        Destroy (gameObject);

	}

}
