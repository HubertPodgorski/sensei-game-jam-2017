using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHelper : MonoBehaviour {


    public void StartNewGame() {
		Debug.Log ("Next Level");
        SceneManager.LoadScene(1);
    }

    public void ShowHelp() {
		Debug.Log ("Help");
    }

    public void QuitGame() {
		Debug.Log ("Quit");
        Application.Quit();
    }
}
