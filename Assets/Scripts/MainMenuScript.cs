using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
    }

}
