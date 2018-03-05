using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour {

	private AudioSource audio;

	//Object[] myMusic; //delcare this as Object Array

	public Object[] myMusic;

	void Awake(){
		audio = GetComponent<AudioSource> ();
		//myMusic = Resources.LoadAll ("game_music", typeof(AudioClip));
		audio.clip = myMusic [0] as AudioClip;
	}

	// Use this for initialization
	void Start () {
		//audio.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!audio.isPlaying & PlayerPrefs.GetInt ("checkMusic") == 0) {
			playRandomMusic ();
		}

		if (PlayerPrefs.GetInt ("checkMusic") == 1) {
			audio.Stop ();
		}
	}

	void playRandomMusic(){
		audio.clip = myMusic [Random.Range (0, myMusic.Length)] as AudioClip;
		audio.Play ();
	}
}
