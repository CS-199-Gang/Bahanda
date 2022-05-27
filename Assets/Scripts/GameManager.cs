using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const int LASTSCENE = 6;
    int FIRSTSCENETIME = 300;
    int SECONDSCENETIME = 300;
    private Dictionary<string, int> inventory;
    private List<Task> tasks;

    public Dictionary<string, int> GetInventory() {
        return inventory;
    }
    
    public List<Task> GetTasks() {
        return tasks;
    }

    public int GetTime(int scene) {
        return scene == 1 ? FIRSTSCENETIME : SECONDSCENETIME;
    }
    
    private void Start() {
        DontDestroyOnLoad(this);

        // Get times from preference
        FIRSTSCENETIME = PlayerPrefs.GetInt("scenario1Time", (int)FIRSTSCENETIME);
        SECONDSCENETIME = PlayerPrefs.GetInt("scenario1Time", (int)SECONDSCENETIME);

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            NextScene();
        }
    }

    public void NextScene() {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Scenario 1" || sceneName == "Scenario 2" ) {
            SaveStats();
        }
        int currScene = SceneManager.GetActiveScene().buildIndex; 
        int nextScene = currScene+1 > LASTSCENE ? 1 : currScene+1; 
        SceneManager.LoadScene(nextScene);
    }

    private void SaveStats() {
        inventory = FindObjectOfType<InventoryManager>().GetInventory();
        tasks = FindObjectOfType<TaskManager>().GetTasks();
    }
}
