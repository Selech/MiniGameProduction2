using UnityEngine;
using System.Collections;

public class TutorialColliderSounds : MonoBehaviour
{


    public int TriggerID = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            switch (TriggerID)
            {
                case 1:
                    EventManager.Instance.TriggerEvent(new FirstSoundEvent());
                    break;
                case 2:
                    EventManager.Instance.TriggerEvent(new RoadWorkAheadEvent());
                    break;
                case 3:
                    EventManager.Instance.TriggerEvent(new WhatOccursThereEvent());
                    break;
                case 4:
                    EventManager.Instance.TriggerEvent(new CanUseAsRampEvent());
                    break;
                case 5:
                    EventManager.Instance.TriggerEvent(new PeopleAreDirtyEvent());
                    break;
                case 6:
                    EventManager.Instance.TriggerEvent(new ThatWasCoolEvent());
                    break;
                case 7:
                    EventManager.Instance.TriggerEvent(new ThatWentFastEvent());
                    break;
                case 8:
                    EventManager.Instance.TriggerEvent(new SuperEvent());
                    break;
                case 9:
                    EventManager.Instance.TriggerEvent(new WatchOutOrHeMightHitUsEvent());
                    break;
                case 10:
                    EventManager.Instance.TriggerEvent(new SpeedPowerUpAheadEvent());
                    break;
                case 11:
                    EventManager.Instance.TriggerEvent(new AfterSpeedPowerUpEvent());
                    break;
                default:
                    Debug.Log("Invalid TriggerID");
                    break;
            }
        }
    }

}
