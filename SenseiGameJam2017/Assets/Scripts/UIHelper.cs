using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHelper : MonoBehaviour {


    public void StartNewGame() {
        SceneManager.LoadScene(1);
    }

    public void ShowHelp() {

    }

    public void QuitGame() {
        Application.Quit();
    }
}
