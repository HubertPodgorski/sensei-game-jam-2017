using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame () {
		Application.LoadLevel("Menu");
	}

	public void ShowHelp() {
		Debug.Log ("Help");
	}

	public void QuitGame() {
		Application.Quit ();
		Debug.Log("Quit");
	}

	public void GotoMenu() {
		Application.LoadLevel("MainMenu");
	}
}
