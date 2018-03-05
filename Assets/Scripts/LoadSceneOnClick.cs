using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	public GameObject loadingImage;

	public void LoadByIndex (int sceneIndex){
		loadingImage.SetActive (true);
		SceneManager.LoadScene (sceneIndex);
	}

}
