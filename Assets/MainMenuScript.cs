using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenario 1");
    }
}
