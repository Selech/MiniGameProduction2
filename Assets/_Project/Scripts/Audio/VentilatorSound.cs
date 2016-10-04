using UnityEngine;
using System.Collections;

public class VentilatorSound : MonoBehaviour {

    void Start()
    {
        AkSoundEngine.PostEvent("Play_Fan", this.gameObject);
    }

}
