using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCutsceneController : MonoBehaviour
{
    private bool _building;

    public GameObject Hill;
    public GameObject HillHouse;

    public List<GameObject> HouseObjectsReward1;
    public List<GameObject> HouseObjectsReward2;
    public List<GameObject> HouseObjectsReward3;

    public GameObject RestackButton;

    // Use this for initialization
    private void Start()
    {
        AkSoundEngine.PostEvent("Play_MisVO13", gameObject);
        AkSoundEngine.PostEvent("Play_Build", gameObject);
        AkSoundEngine.PostEvent("Play_BuildMusic", gameObject);

        Time.timeScale = 1f;
        StartCoroutine(GetReward1());
        _building = true;
    }

    private void Update()
    {
        if (!_building)
        {
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && _building)
            {
                AkSoundEngine.SetRTPCValue("SkipScene", 1f);
                Time.timeScale = 10f;
            }

            if (Input.GetMouseButtonUp(0) && _building)
            {
                AkSoundEngine.SetRTPCValue("SkipScene", 0f);
                Time.timeScale = 1f;
            }
        }
    }

    private IEnumerator GetReward1()
    {
        yield return new WaitForSeconds(1f);

        HillHouse.SetActive(false);
        Hill.SetActive(true);

        foreach (var obj in HouseObjectsReward1)
        {
            obj.SetActive(true);
            AkSoundEngine.PostEvent("Play_CloudPuff", gameObject);
            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(GetReward2());
    }

    private IEnumerator GetReward2()
    {
        //yield return new WaitForSeconds(1f);

        foreach (var obj in HouseObjectsReward2)
        {
            obj.SetActive(true);
            AkSoundEngine.PostEvent("Play_CloudPuff", gameObject);
            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(GetReward3());
    }

    private IEnumerator GetReward3()
    {
        //yield return new WaitForSeconds(1f);

        foreach (var obj in HouseObjectsReward3)
        {
            obj.SetActive(true);
            AkSoundEngine.PostEvent("Play_CloudPuff", gameObject);
            yield return new WaitForSeconds(1f);
        }

        HouseBuilt();
    }

    private void HouseBuilt()
    {
        _building = false;

        AkSoundEngine.SetRTPCValue("SkipScene", 0f);
        Time.timeScale = 1f;

        AkSoundEngine.PostEvent("Stop_Build", gameObject);
        AkSoundEngine.PostEvent("Play_MisVO14", gameObject);

        StartCoroutine(ShowButton());
    }

    private IEnumerator ShowButton()
    {
        yield return new WaitForSeconds(5f);

        AkSoundEngine.PostEvent("Play_MusVO13", gameObject);
        RestackButton.SetActive(true);
    }

    public void BackToRestack()
    {
        AkSoundEngine.PostEvent("Stop_BuildMusic", gameObject);
        SceneManager.LoadScene(1);
    }
}