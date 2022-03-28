using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScene : MonoBehaviour {

    GameManager gameManager;

    public Text endText;

    TaskManager taskManager;

    private void Awake() {
        taskManager = FindObjectOfType<TaskManager>();
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += ShowEndText;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= ShowEndText;
    }

    private void ShowEndText(Scene scene, LoadSceneMode mode) {
        endText.text = taskManager.GetSceneFeedback(0);
        endText.gameObject.SetActive(true);
    }

}
