using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeButtonImage : MonoBehaviour {

	public Sprite spriteImg1;
	public Sprite spriteImg2;
	bool toggle = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeImage() 
	{
		toggle = !toggle;
		if (!toggle) {
			GetComponent<Image> ().sprite = spriteImg1;
		} else {
			GetComponent<Image> ().sprite = spriteImg2;
		}
	}
}
