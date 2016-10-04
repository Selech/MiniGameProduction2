using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WinCutsceneController : MonoBehaviour {

    public GameObject hill;
    public GameObject hillHouse;

    public List<GameObject> houseObjectsReward;

    public GameObject RestackButton;
    public int test;
    private int counter;
    private int amount;

    private bool Building;

    // Use this for initialization
    void Start () {
        AkSoundEngine.PostEvent("Play_MisVO13", this.gameObject);
       // AkSoundEngine.PostEvent("Play_Build", this.gameObject);
        AkSoundEngine.PostEvent("Play_BuildMusic", this.gameObject);

        Time.timeScale = 1f;
        StartCoroutine(GetReward1());
        Building = true;

        counter = 0;
        test = PlayerPrefs.GetInt("Amount of Carriables");
        amount = Mathf.FloorToInt((houseObjectsReward.Count*(test/6f)));
    }

    void Update()
    {
        if (!Building)
        {
            
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && Building)
            {
                AkSoundEngine.SetRTPCValue("SkipScene", 1f);
                Time.timeScale = 10f;
            }

            if (Input.GetMouseButtonUp(0) && Building)
            {
                AkSoundEngine.SetRTPCValue("SkipScene", 0f);
                Time.timeScale = 1f;
            }
        }
    }

    IEnumerator GetReward1()
    {
        yield return new WaitForSeconds(1f);

        hillHouse.SetActive(false);
        hill.SetActive(true);

        foreach (var obj in houseObjectsReward)
        {
            counter++;

            if (counter > amount)
                yield return new WaitForSeconds(0f);
            else
            {
                obj.SetActive(true);
                AkSoundEngine.PostEvent("Play_CloudPuff", this.gameObject);

                yield return new WaitForSeconds(1f);
            }
        }

        HouseBuilt();
    }

    IEnumerator GetReward2()
    {
        //yield return new WaitForSeconds(1f);

        foreach (var obj in houseObjectsReward)
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

        foreach (var obj in houseObjectsReward)
        {
            obj.SetActive(true);
            AkSoundEngine.PostEvent("Play_CloudPuff", this.gameObject);
            yield return new WaitForSeconds(1f);
        }

        HouseBuilt();
    }

    void HouseBuilt()
    {
        Building = false;

        AkSoundEngine.SetRTPCValue("SkipScene", 0f);
        Time.timeScale = 1f;

        AkSoundEngine.PostEvent("Stop_Build", this.gameObject);
        AkSoundEngine.PostEvent("Play_MisVO14",this.gameObject);

        StartCoroutine(ShowButton());
    }

    IEnumerator ShowButton()
    {
        yield return new WaitForSeconds(5f);

        AkSoundEngine.PostEvent("Play_MusVO13", this.gameObject);
        RestackButton.SetActive(true);
    }

    public void BackToRestack()
    {
        AkSoundEngine.PostEvent("Stop_BuildMusic", this.gameObject);
        SceneManager.LoadScene(1);
    }
}
