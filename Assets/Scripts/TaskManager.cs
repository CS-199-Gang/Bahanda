using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Class for the Task system
public class TaskManager : MonoBehaviour
{
    [SerializeField]
    private List<Task> tasks = new List<Task>();
    private InventoryManager inventoryManager;

    public List<Task> GetTasks() {
        return tasks;
    }

    public bool IsTask(string item, out string taskName) {
        foreach(Task t in tasks) {
            if (t.itemRequired == item) {
                taskName = t.taskName;
                return true;
            }
        }
        taskName = "";
        return false;
    }

    void Awake() {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    string GetTaskFeedback(Task task) {
        int quantity = inventoryManager.GetQuantity(task.itemRequired);
        if (quantity == 0) {
            return task.msgZero;
        }
        else if(quantity == task.targetQuantity) {
            return task.msgComplete;
        } else {
            return task.msgIncomplete;
        }
    }

    public string GetSceneFeedback() {
        string msg = "";
        foreach(Task task in tasks) {
            msg += GetTaskFeedback(task) + "\n";
        }
        return msg;
    }
}
