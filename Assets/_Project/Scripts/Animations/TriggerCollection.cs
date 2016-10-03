using UnityEngine;
using System.Collections;

public class TriggerCollection : MonoBehaviour {

	public void TriggerTitleMelody() { AkSoundEngine.PostEvent("Play_TextCue",this.gameObject); }

    public void TriggerEvilCatLaughter() { AkSoundEngine.PostEvent("Play_EvilCat", this.gameObject); }
}
