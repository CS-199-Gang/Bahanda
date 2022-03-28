using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Camera mainCamera;
    public string nextScene;

    private void Update() {
        if (OVRInput.Get(OVRInput.Button.Four)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }
    }
}
