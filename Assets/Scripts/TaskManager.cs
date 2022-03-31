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
        DontDestroyOnLoad(this);
        tasks[0] = new List<Task>();
        tasks[0].Add(Task.CreateInstance(
            "Gather Food",
            "Gather food",
            "food",
            5,
            "You've collected enough food",
            "More food would be advisable.",
            "You have not collected any food"
        ));
        tasks[0].Add(Task.CreateInstance(
            "Collect Water",
            "Collect water",
            "water",
            5,
            "You've collected enough water",
            "You will need more water than that.",
            "You have not collected any water"
        ));
        tasks[0].Add(Task.CreateInstance(
            "Get First Aid",
            "Get First Aid",
            "firstaid",
            1,
            "The first aid you got will help in emergencies.",
            "Some first aid would be very helpful.",
            "Some first aid would be very helpful."
        ));
        tasks[0].Add(Task.CreateInstance(
            "Get a Flashlight",
            "Get a flashlight",
            "firstAid",
            1,
            "You have a flashlight for when power goes out.",
            "A flashlight would help in case the power goes out.",
            "Some first aid would be very helpful."
        ));
        tasks[0].Add(Task.CreateInstance(
            "Get Matchsticks",
            "Get matchsticks",
            "matchbox",
            1,
            "The matchsticks you got will help if you need to start a fire.",
            "Some matchsticks would help in case you need to start a fire.",
            "Some matchsticks would help in case you need to start a fire."
        ));
    }

    // Update is called once per frame
    void Update()
    {
        // hardcode calling scenario 1 tasks for now
        UpdateTasks(0);
    }

    void Awake() {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    void UpdateTasks(int scenario) {
        Dictionary<string, int> inventory = inventoryManager.GetInventory();
        foreach(Task task in tasks[scenario]) {
            if (inventory.ContainsKey(task.itemRequired))
                task.currQuantity = inventory[task.itemRequired];
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

    public string GetSceneFeedback(int scenario) {
        string msg = "";
        foreach(Task task in tasks[scenario]) {
            msg += GetTaskFeedback(task) + "\n";
        }
        return msg;
    }
}
