using UnityEngine;
using System.Collections;

public class CarSounds : MonoBehaviour {

    void Awake()
    {
        AkSoundEngine.PostEvent("Play_Car", this.gameObject);
    }

}
