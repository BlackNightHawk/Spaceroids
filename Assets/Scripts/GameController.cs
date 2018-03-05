using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int startHazardCount;
	private int hazardCount;

	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text highScoreText;
	//public Text restartText;
	public Text gameOverText;
	public Text earthIsLostText;
	public GameObject overPanel;
	public GameObject restartButton;
	public GameObject exitButton;

	public GameObject controlPanel;
	private int contPanel;

	private bool gameOver;
	private bool earthLost;
	private bool restart;
	private int score;
	private int highScore;

	private bool playerHit;
	private bool earthHit;

	void Start (){
		playerHit = false;
		earthHit = false;
		gameOver = false;
		restart = false;
		earthLost = false;
//		restartText.text = "";
		gameOverText.text = "";
		earthIsLostText.text = "";
		highScoreText.text = "";
		overPanel.SetActive (false);
		restartButton.SetActive (false);
		exitButton.SetActive (false);
		score = 0;
		highScore = PlayerPrefs.GetInt("highScore");
		contPanel = PlayerPrefs.GetInt("contPanel");
		UpdateScore ();
		hazardCount = startHazardCount;
		Controls ();
	}

	void Update (){
		if (Input.GetKeyDown (KeyCode.R)) {
			contPanel = 0;
			PlayerPrefs.SetInt("contPanel", contPanel);
			Debug.Log ("Control Panel is reset back to 0");
		}

/*		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
*/	} 

	public void Controls (){
		if (contPanel == 0) {
			controlPanel.SetActive (true);
			contPanel = 1;
			PlayerPrefs.SetInt("contPanel", contPanel);
			Debug.Log ("Control Panel is up and now set to 1");
		} else {
			StartCoroutine (SpawnWaves ());
			Debug.Log ("Control Panel is down and is set to 1");
		}
	}

	public void StartWaves (){
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves (){
		yield return new WaitForSeconds (startWait);
		while (true){
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity; 
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			hazardCount += 1;

			if (gameOver) {
				restartButton.SetActive (true);
				exitButton.SetActive (true);
//				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore(){
		scoreText.text = "Score: " + score;
	}

	void LoadHighScore (int newScore){
		if (newScore > highScore) {
			highScore = newScore;
			PlayerPrefs.SetInt("highScore", highScore);
		}

	}

	public void GameOver (){
		playerHit = true;
		LoadHighScore (score);
		overPanel.SetActive (true);
		highScoreText.text = "Highscore: " + highScore.ToString();
		gameOverText.text = "Mission Failed";
		if (earthHit == false) {
			earthIsLostText.text = "You Died";
		}
		gameOver = true;
	}

	public void EarthHit (){
		earthHit = true;
		LoadHighScore (score);
		overPanel.SetActive (true);
		highScoreText.text = "Highscore: " + highScore.ToString();
		gameOverText.text = "Mission Failed";
		if (playerHit == false) {
			earthIsLostText.text = "An Asteroid has slipped past you crashing into Earth";
		}
		gameOver = true;
	}

	public void RestartGame () {
		Application.LoadLevel (Application.loadedLevel);
	}
}
