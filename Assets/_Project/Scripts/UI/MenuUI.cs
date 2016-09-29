using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{

    public Sprite spriteSwipeImgDeactive;
    public Sprite spriteSwipeImgActive;

    public Sprite spriteGyroImgDeactive;
    public Sprite spriteGyroImgActive;

    public Sprite spriteMuted;
    public Sprite spriteUnmuted;

    public Image menuButton;
    public GameObject gyroButton;
    public GameObject swipeButton;
    public GameObject muteButton;

    public Sprite playSprite;
    public Sprite pauseSprite;

    bool toggleMenuOn = false;
    bool gyroButtonEnabled = false;

    public GameObject menuUI;

    void OnEnable()
    {
        EventManager.Instance.StartListening<RestartGameEvent>(KeepControlScheme);
    }
    void OnDisable()
    {
        EventManager.Instance.StartListening<RestartGameEvent>(KeepControlScheme);
    }

    public void ChangeMovementOptionToGyro()
    {
        gyroButtonEnabled = GameManager.Instance.isGyro;
        EventManager.Instance.TriggerEvent(new UISoundEvent());
        if (!gyroButtonEnabled)
        {
            EventManager.Instance.TriggerEvent(new ChangeSchemeEvent(true));
            gyroButtonEnabled = true;
            swipeButton.GetComponent<Image>().sprite = spriteSwipeImgDeactive;
            gyroButton.GetComponent<Image>().sprite = spriteGyroImgActive;
        }
    }

    public void KeepControlScheme(RestartGameEvent e)
    {
        if (gyroButtonEnabled)
        {
            EventManager.Instance.TriggerEvent(new ChangeSchemeEvent(true));
        } else
        {
            EventManager.Instance.TriggerEvent(new ChangeSchemeEvent(false));
        }
    }

    public void ChangeMovementOptionToSwipe()
    {
        gyroButtonEnabled = GameManager.Instance.isGyro;
        EventManager.Instance.TriggerEvent(new UISoundEvent());
        if (gyroButtonEnabled)
        {
            EventManager.Instance.TriggerEvent(new ChangeSchemeEvent(false));

            gyroButtonEnabled = false;
            swipeButton.GetComponent<Image>().sprite = spriteSwipeImgActive;
            gyroButton.GetComponent<Image>().sprite = spriteGyroImgDeactive;
        }
    }

    public void MuteMusicButton()
    {
        EventManager.Instance.TriggerEvent(new UISoundEvent());
        EventManager.Instance.TriggerEvent(new MuteMusicEvent(!SoundManager.Instance.musicMuted));
        if (SoundManager.Instance.musicMuted == true)
        {
            muteButton.GetComponent<Image>().sprite = spriteMuted;
        }
        else
        {
            muteButton.GetComponent<Image>().sprite = spriteUnmuted;

        }
    }

    public void ReturnToStackingButton()
    {
        GameManager.Instance.isPaused = false;
        EventManager.Instance.TriggerEvent(new RestartGameEvent());
        GameManager.Instance.RestartGame();
    }

    public void ToggleMenu()
    {
        EventManager.Instance.TriggerEvent(new UISoundEvent());
        if (GameManager.Instance.hasWon == false)
        {
            GameManager.Instance.TogglePause();
            menuUI.SetActive(GameManager.Instance.isPaused);
            menuButton.sprite = GameManager.Instance.isPaused ? playSprite : pauseSprite;
        }
        else
        {
            toggleMenuOn = !toggleMenuOn;
            menuUI.SetActive(toggleMenuOn);
        }
        // Ensures correct sprite is showing in menu
        if (GameManager.Instance.isGyro)
        {
            swipeButton.GetComponent<Image>().sprite = spriteSwipeImgDeactive;
            gyroButton.GetComponent<Image>().sprite = spriteGyroImgActive;
        }
        else
        {
            swipeButton.GetComponent<Image>().sprite = spriteSwipeImgActive;
            gyroButton.GetComponent<Image>().sprite = spriteGyroImgDeactive;
        }
    }


}
