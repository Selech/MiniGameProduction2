using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinCutsceneController : MonoBehaviour {

    public GameObject hill;
    public GameObject hillHouse;

    public List<GameObject> houseObjectsReward1;
    public List<GameObject> houseObjectsReward2;
    public List<GameObject> houseObjectsReward3;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
        StartCoroutine(GetReward1());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 10f;
        }

        if (Input.GetMouseButtonUp(0))
        {
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
            yield return new WaitForSeconds(1f);
        }
    }
}
