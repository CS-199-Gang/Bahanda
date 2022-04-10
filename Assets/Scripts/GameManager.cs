using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    public string nextScene;

    private void Update() {
        if (OVRInput.GetDown(OVRInput.Button.Four)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }
    }
}