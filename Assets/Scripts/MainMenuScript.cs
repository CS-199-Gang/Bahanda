using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
    }

    public void SetName(string value) {
        PlayerPrefs.SetString("name", value);
    }

    public void SetStudentNumber(string value) {
        PlayerPrefs.SetString("number", value);
    }
}
