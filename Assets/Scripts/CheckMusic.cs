using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckMusic : MonoBehaviour {

	private int checkMusic;

	public Toggle myToggle;

	// Use this for initialization
	void Start () {
		checkMusic = PlayerPrefs.GetInt ("checkMusic");
//		myToggle = PlayerPrefs.GetFloat ("myToggle");
	}
		
	void Update (){
		if (Input.GetKey ("c")) {
			PlayerPrefs.DeleteKey ("checkMusic");
			Debug.Log ("checkMusic has been reset");
		}

		if (Input.GetKey ("t")) {
			myToggle.isOn = false; //this works kinda aka the command works
		}
	}
	
	public void turnMusicOnOrOff(){
		if (checkMusic == 0) {
			checkMusic = 1;
			PlayerPrefs.SetInt ("checkMusic", checkMusic);
			Debug.Log ("CheckMusic is now set to 1");
		} else {
			checkMusic = 0;
			PlayerPrefs.SetInt ("checkMusic", checkMusic);
			Debug.Log ("CheckMusic is now set to 0");
		}
	}
}
