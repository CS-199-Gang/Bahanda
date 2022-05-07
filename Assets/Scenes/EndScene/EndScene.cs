using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour {

    [SerializeField]
    private Transform hoverCanvas;
    [SerializeField]
    private Transform grid;
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
            GameObject tb = Instantiate(taskBox, grid.position, grid.rotation, grid);
            TaskBoxScript tbs = tb.GetComponent<TaskBoxScript>();
            int currQuantity = inventory.ContainsKey(t.itemRequired) ? inventory[t.itemRequired] : 0; 
            tbs.SetItems(t, currQuantity);
            tbs.GetHoverBoxTransform().SetParent(hoverCanvas);
        }
    }

    public void NextScene() {
        gameManager.NextScene();
    }
}
