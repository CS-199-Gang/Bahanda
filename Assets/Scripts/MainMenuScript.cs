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
}
