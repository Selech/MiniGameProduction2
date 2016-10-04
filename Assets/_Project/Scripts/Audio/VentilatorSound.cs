using UnityEngine;
using System.Collections;
using System;

public class VentilatorSound : MonoBehaviour {

    void Awake()
    {
        AkSoundEngine.PostEvent("Play_Fan", this.gameObject);
        Debug.Log("LOLOL");
    }


}
