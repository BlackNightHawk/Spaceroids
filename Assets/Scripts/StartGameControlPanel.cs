using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameControlPanel : MonoBehaviour {

	public GameController gameController;

	public void StartWaves (){
		gameController.StartWaves ();
	}
}
