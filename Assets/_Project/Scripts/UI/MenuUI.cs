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
	bool swipeButtonEnabled = true;
	bool gyroButtonEnabled = false;

	public GameObject menuUI;
    
	public void ChangeMovementOptionToGyro ()
	{
		EventManager.Instance.TriggerEvent(new UISoundEvent ());
		if (!gyroButtonEnabled) {
			EventManager.Instance.TriggerEvent (new ChangeSchemeEvent (true));
			gyroButtonEnabled = true;
			swipeButtonEnabled = false;
			swipeButton.GetComponent<Image> ().sprite = spriteSwipeImgDeactive;
			gyroButton.GetComponent<Image> ().sprite = spriteGyroImgActive;
		}
	}

	public void ChangeMovementOptionToSwipe ()
	{
		EventManager.Instance.TriggerEvent(new UISoundEvent ());
		if (!swipeButtonEnabled) {
			EventManager.Instance.TriggerEvent (new ChangeSchemeEvent (false));

			gyroButtonEnabled = false;
			swipeButtonEnabled = true;
			swipeButton.GetComponent<Image> ().sprite = spriteSwipeImgActive;
			gyroButton.GetComponent<Image> ().sprite = spriteGyroImgDeactive;
		}
	}

	public void MuteMusicButton() 
	{
		EventManager.Instance.TriggerEvent(new UISoundEvent ());
		EventManager.Instance.TriggerEvent (new MuteMusicEvent (!SoundManager.Instance.musicMuted));
		if (SoundManager.Instance.musicMuted == true) {
			muteButton.GetComponent<Image> ().sprite = spriteMuted;
		} else {
			muteButton.GetComponent<Image> ().sprite = spriteUnmuted;

		}
	}

	public void ReturnToStackingButton()
	{
        EventManager.Instance.TriggerEvent(new RestartGameEvent());
		GameManager.Instance.RestartGame ();
	}

	public void ToggleMenu ()
	{
		EventManager.Instance.TriggerEvent(new UISoundEvent ());
		if (GameManager.Instance.hasWon == false) {
			GameManager.Instance.TogglePause ();
			menuUI.SetActive (GameManager.Instance.isPaused);
			menuButton.sprite = GameManager.Instance.isPaused ? playSprite : pauseSprite;
		} else {
			toggleMenuOn = !toggleMenuOn;
			menuUI.SetActive (toggleMenuOn);
		}
	}


}
