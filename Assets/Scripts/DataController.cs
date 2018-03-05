using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour {

	private PlayerProgress playerProgress;
	private int highestScore;

	// Use this for initialization
	void Start () {

		LoadPlayerProgress ();
		highestScore = 0;
	}
		
	public void SubmitNewPlayer (int newScore) {
		if (newScore > highestScore) {
			highestScore = newScore;
			SavePlayerProgress ();
		}
	}

	public int GetHighestPlayerScore (){
		return highestScore;
	}

	private void LoadPlayerProgress (){
		playerProgress = new PlayerProgress ();

		if (PlayerPrefs.HasKey ("highestScore")) {
			playerProgress.highestScore = PlayerPrefs.GetInt ("highestScore");
		}
	}

	private void SavePlayerProgress (){
		PlayerPrefs.SetInt ("highestScore", playerProgress.highestScore);
	}
}