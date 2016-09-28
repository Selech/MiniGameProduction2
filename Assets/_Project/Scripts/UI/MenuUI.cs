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

	public GameObject gyroButton;
	public GameObject swipeButton;
	public GameObject muteButton;

	bool toggleMenuOn = false;
	bool swipeButtonEnabled = true;
	bool gyroButtonEnabled = false;

	public GameObject menuUI;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void ChangeMovementOptionToGyro ()
	{
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
		
		EventManager.Instance.TriggerEvent (new MuteMusicEvent (!SoundManager.Instance.musicMuted));
		if (SoundManager.Instance.musicMuted == true) {
			muteButton.GetComponent<Image> ().sprite = spriteMuted;
		} else {
			muteButton.GetComponent<Image> ().sprite = spriteUnmuted;

		}
	}

	public void ReturnToStackingButton()
	{
		GameManager.Instance.RestartGame ();
	}

	public void ToggleMenu ()
	{
		if (GameManager.Instance.hasWon == false) {
			GameManager.Instance.TogglePause ();
			menuUI.SetActive (GameManager.Instance.isPaused);
		} else {
			toggleMenuOn = !toggleMenuOn;
			menuUI.SetActive (toggleMenuOn);
		}
	}

}
