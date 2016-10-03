
﻿using UnityEngine;
using System.Collections;
using System;

public class SebastianRampManager : MonoBehaviour
{
    public string guidelinesName = "InvisibleAirGuidelines";
    public int guideSpawnSeconds = 3;
    public int reactivateRampAfterSeconds = 5;
    private GameObject guideLines;
    private bool isHittable = true;
    void OnEnable()
    {
        EventManager.Instance.StartListening<PlayerHitRoadProp>(handlePlayerHitEvent);
    }

    void OnDisable()
    {
        EventManager.Instance.StopListening<PlayerHitRoadProp>(handlePlayerHitEvent);
    }

    private void handlePlayerHitEvent(PlayerHitRoadProp roadPropEvent)
    {
        if (isHittable)
        {
            if (roadPropEvent.roadProp == this.gameObject)
            {
                //spawn guides for tot seconds
                StartCoroutine(spawnGuidesForSeconds(guideSpawnSeconds, reactivateRampAfterSeconds));
                print("Player hit on " + this.gameObject.name);
                isHittable = false;
            }
        }
    }

    private IEnumerator spawnGuidesForSeconds(int seconds, int reactivationSeconds)
    {
        guideLines.SetActive(true);
        yield return new WaitForSeconds(seconds);
        guideLines.SetActive(false);
        yield return new WaitForSeconds(reactivationSeconds);
        isHittable = true;
    }


    // Use this for initialization
    void Start()
    {

        guideLines = transform.parent.FindChild(guidelinesName).gameObject;

        guideLines.SetActive(false);
    }



}