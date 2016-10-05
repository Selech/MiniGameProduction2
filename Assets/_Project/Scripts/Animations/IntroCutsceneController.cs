using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroCutsceneController : MonoBehaviour {

    [Header("Main settings")]
    private int sceneIndex;
    public Animation cameraAnimation;

    [Header("Scene references")]
    //Scenes
    public GameObject scene1;
    public GameObject scene2;
    public GameObject scene3;
    public GameObject scene4;

    [Header("Scene1 objects")]
    //Scene2 moving objects
    public Animation mouse;
    public Animation cat;

    [Header("Scene2 objects")]
    //Scene2 moving objects
    public Animation elevator;
    public Animation elevator2;

    [Header("Scene3 objects")]
    //Scene3 elements
    public Animation title;
    public Animation bike;

    void OnEnable()
    {
        EventManager.Instance.StartListening<LanguageSelect>(PlayCutscene);
    }

    void OnDisable()
    {
        EventManager.Instance.StopListening<LanguageSelect>(PlayCutscene);
    }
    
    void Start()
    {
        PlayerPrefs.SetInt("Tutorial", 1);
    }

    // Use this for initialization
    public void PlayCutscene(LanguageSelect e) {
        sceneIndex = 1;
        PlayScene1();

        AkSoundEngine.PostEvent("Play_MusicCutScene", this.gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AkSoundEngine.SetRTPCValue("SkipScene", 1f);
            Time.timeScale = 18.5f;
        }

        if (Input.GetMouseButtonUp(0))
        {
            AkSoundEngine.SetRTPCValue("SkipScene", 0f);
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
        AkSoundEngine.PostEvent("Play_IntroSc1",this.gameObject);

        scene1.SetActive(true);
        scene2.SetActive(false);
        scene3.SetActive(false);
        scene4.SetActive(false);

        cameraAnimation.Play("Scene1_Camera");
        mouse.Play("Scene1_Mouse");
        cat.Play("Scene1_Cat");
        sceneIndex++;
    }

    void PlayScene2()
    {
        AkSoundEngine.PostEvent("Play_IntroSc2", this.gameObject);

        scene1.SetActive(false);
        scene2.SetActive(true);
        scene3.SetActive(false);
        scene4.SetActive(false);

        cameraAnimation.Play("Scene2_Camera");
        elevator.Play("Scene2_Elevator2");
        elevator2.Play("Scene2_Elevator");
        sceneIndex++;
    }

    void PlayScene3()
    {
        AkSoundEngine.PostEvent("Play_IntroSc3", this.gameObject);

        scene1.SetActive(false);
        scene2.SetActive(false);
        scene3.SetActive(true);
        scene4.SetActive(false);

        bike.Play("Scene3_Bike");
        cameraAnimation.Play("Scene3_Camera");
        title.Play("Scene3_Title");
        sceneIndex++;
    }

    void PlayScene4()
    {
        //AkSoundEngine.PostEvent("Stop_MusicCutScene", this.gameObject);
        AkSoundEngine.PostEvent("Play_TwigSnap", this.gameObject);
        //AkSoundEngine.PostEvent("Play_EvilCatCue", this.gameObject);

        scene1.SetActive(false);
        scene2.SetActive(false);
        scene3.SetActive(false);
        scene4.SetActive(true);

        cameraAnimation.Play("Scene4_Camera");
        sceneIndex++;
    }

    void StartGame()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 != SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
