using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHelper : MonoBehaviour {

    public static float timer;

    void LateUpdate() {
        if (!TimeController.rewinding)
            timer += Time.deltaTime;
        else
            timer -= Time.deltaTime;

    }
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
