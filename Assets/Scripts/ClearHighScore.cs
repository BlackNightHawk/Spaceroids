using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearHighScore : MonoBehaviour {
	
	public void ResetHighScore () {
		PlayerPrefs.DeleteKey ("highScore");
		Debug.Log("High Score has been reset");
	}
}
