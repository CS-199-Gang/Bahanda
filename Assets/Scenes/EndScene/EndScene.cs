using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour {

    GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += ShowEndText;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= ShowEndText;
    }

    private void ShowEndText(Scene scene, LoadSceneMode mode) {
        gameManager.ShowEndText();
    }

}
