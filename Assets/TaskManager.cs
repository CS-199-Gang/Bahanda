using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Class for the Task system
public class TaskManager : MonoBehaviour
{
    List<Task>[] tasks = new List<Task>[2];
    // List<Task> sceneTasks1 = new List<Task>();
    // List<Task> sceneTasks2 = new List<Task>();
    InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake() {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    void UpdateTasks(int scenario) {
        Dictionary<string, int> inventory = inventoryManager.GetInventory();
        foreach(Task task in tasks[scenario]) {
            task.currQuantity = inventory[task.name];

            
        }
    }

    string GetTaskFeedback(Task task) {
        if (task.currQuantity == 0) {
            return task.msgZero;
        }
        else if(task.currQuantity == task.targetQuantity) {
            return task.msgComplete;
        } else {
            return task.msgIncomplete;
        }
    }

    void GetSceneFeedback(int scenario) {
        string msg = "";
        foreach(Task task in tasks[scenario]) {
            msg += GetTaskFeedback(task) + "\n";
        }
    }
}
