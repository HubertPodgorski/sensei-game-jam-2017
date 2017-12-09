using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame () {
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		Debug.Log ("PLAY!");
	}

	public void ShowHelp() {
		Debug.Log ("Help");
	}

	public void QuitGame() {
		Application.Quit ();
		Debug.Log("Quit");
	}
}
