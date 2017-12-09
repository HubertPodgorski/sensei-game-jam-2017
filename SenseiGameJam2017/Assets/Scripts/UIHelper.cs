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
        SceneManager.LoadScene(1);
    }

    public void ShowHelp() {

    }

    public void QuitGame() {
        Application.Quit();
    }
}
