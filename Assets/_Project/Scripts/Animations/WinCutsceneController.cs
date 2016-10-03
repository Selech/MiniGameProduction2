using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WinCutsceneController : MonoBehaviour {

    public GameObject hill;
    public GameObject hillHouse;

    public List<GameObject> houseObjectsReward1;
    public List<GameObject> houseObjectsReward2;
    public List<GameObject> houseObjectsReward3;

    public GameObject RestackButton;

    // Use this for initialization
    void Start () {
        AkSoundEngine.PostEvent("Play_MisVO13", this.gameObject);
        AkSoundEngine.PostEvent("Play_Build", this.gameObject);
        AkSoundEngine.PostEvent("Play_BuildMusic", this.gameObject);

        Time.timeScale = 1f;
        StartCoroutine(GetReward1());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AkSoundEngine.SetRTPCValue("SkipScene", 1f);
            Time.timeScale = 10f;
        }

        if (Input.GetMouseButtonUp(0))
        {
            AkSoundEngine.SetRTPCValue("SkipScene", 0f);
            Time.timeScale = 1f;
        }
    }

    IEnumerator GetReward1()
    {
        yield return new WaitForSeconds(1f);

        hillHouse.SetActive(false);
        hill.SetActive(true);

        foreach (var obj in houseObjectsReward1)
        {
            obj.SetActive(true);
            AkSoundEngine.PostEvent("Play_CloudPuff", this.gameObject);
            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(GetReward2());
    }

    IEnumerator GetReward2()
    {
        //yield return new WaitForSeconds(1f);

        foreach (var obj in houseObjectsReward2)
        {
            obj.SetActive(true);
            AkSoundEngine.PostEvent("Play_CloudPuff", this.gameObject);
            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(GetReward3());
    }

    IEnumerator GetReward3()
    {
        //yield return new WaitForSeconds(1f);

        foreach (var obj in houseObjectsReward3)
        {
            obj.SetActive(true);
            AkSoundEngine.PostEvent("Play_CloudPuff", this.gameObject);
            yield return new WaitForSeconds(1f);
        }

        HouseBuilt();
    }

    void HouseBuilt()
    {
        AkSoundEngine.PostEvent("Stop_Build", this.gameObject);
        AkSoundEngine.PostEvent("Play_MisVO14",this.gameObject);

        StartCoroutine(ShowButton());
    }

    IEnumerator ShowButton()
    {
        yield return new WaitForSeconds(2f);

        AkSoundEngine.PostEvent("Play_MusVO13", this.gameObject);
        RestackButton.SetActive(true);
    }

    public void BackToRestack()
    {
        AkSoundEngine.PostEvent("Stop_BuildMusic", this.gameObject);
        SceneManager.LoadScene(1);
    }
}
