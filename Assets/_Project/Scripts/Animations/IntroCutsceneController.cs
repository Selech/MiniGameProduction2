using UnityEngine;
using System.Collections;

public class IntroCutsceneController : MonoBehaviour {

    [Header("Main settings")]
    public float MotionSpeed = 0.1f;
    private int sceneIndex;
    public Animation cameraAnimation;

    [Header("Scene references")]
    //Scenes
    public GameObject scene1;
    public GameObject scene2;
    public GameObject scene3;
    public GameObject scene4;

    [Header("Scene2 objects")]
    //Scene2 moving objects
    public Animation elevator;

    [Header("Scene3 objects")]
    //Scene3 elements
    public Animation title;

    // Use this for initialization
    void Start () {
        Time.timeScale = MotionSpeed;
        sceneIndex = 1;
        PlayScene1();
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

	void FixedUpdate () {
        if (!cameraAnimation.isPlaying)
        {
            PlayNextScene();
        }
    }

    void PlayNextScene()
    {
        if(sceneIndex == 2)
        {
            PlayScene2();
        }
        else if (sceneIndex == 3)
        {
            PlayScene3();
        }
        else if (sceneIndex == 4)
        {
            PlayScene4();
        }
        else if (sceneIndex == 5)
        {
            StartGame();
        }
    }

    void PlayScene1()
    {
        scene1.SetActive(true);
        scene2.SetActive(false);
        scene3.SetActive(false);
        scene4.SetActive(false);

        cameraAnimation.Play("Scene1_Camera");
        sceneIndex++;
    }

    void PlayScene2()
    {
        scene1.SetActive(false);
        scene2.SetActive(true);
        scene3.SetActive(false);
        scene4.SetActive(false);

        cameraAnimation.Play("Scene2_Camera");
        elevator.Play("Scene2_Elevator");
        sceneIndex++;
    }

    void PlayScene3()
    {
        scene1.SetActive(false);
        scene2.SetActive(false);
        scene3.SetActive(true);
        scene4.SetActive(false);

        cameraAnimation.Play("Scene3_Camera");
        title.Play("Scene3_Title");
        sceneIndex++;
    }

    void PlayScene4()
    {
        scene1.SetActive(false);
        scene2.SetActive(false);
        scene3.SetActive(false);
        scene4.SetActive(true);

        cameraAnimation.Play("Scene4_Camera");
        sceneIndex++;
    }

    void StartGame()
    {
        scene1.SetActive(false);
        scene2.SetActive(false);
        scene3.SetActive(false);
        scene4.SetActive(false);

        Debug.Log("Start game");
    }
}
