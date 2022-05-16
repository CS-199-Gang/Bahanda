using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour {

    [SerializeField]
    private GameObject menuOne;
    [SerializeField]
    private GameObject menuTwo;
    [SerializeField]
    private Transform hoverCanvas;
    [SerializeField]
    private Transform gridOne;
    [SerializeField]
    private Transform gridTwo;
    [SerializeField]
    private GameObject taskBox;
    private Dictionary<string, int> inventory;
    private List<Task> tasks;
    private GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        inventory = gameManager.GetInventory();
        tasks = gameManager.GetTasks();
    }

    private void Start() {
        foreach (Task t in tasks) {
            int currQuantity = inventory.ContainsKey(t.itemRequired) ? inventory[t.itemRequired] : 0; 

            if (!t.isNeeded && currQuantity <= 0) {
                continue;
            }

            Transform grid = t.isNeeded ? gridOne : gridTwo; 
            GameObject tb = Instantiate(taskBox, grid.position, grid.rotation, grid);
            TaskBoxScript tbs = tb.GetComponent<TaskBoxScript>();
            tbs.SetItems(t, currQuantity);
            tbs.GetHoverBoxTransform().SetParent(hoverCanvas);
        }
    }

    public void ShowMenuOne() {
        menuOne.SetActive(true);
        menuTwo.SetActive(false);
    }

    public void ShowMenuTwo() {
        if (gridTwo.childCount == 0) {
            NextScene();
            return;
        }
        menuOne.SetActive(false);
        menuTwo.SetActive(true);
    }

    public void NextScene() {
        gameManager.NextScene();
    }
}
