using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// creates an option in the Asset tab to make a Task
[CreateAssetMenu(fileName = "task", menuName = "ScriptableObjects/Task", order =1)]

/// Class for Task instances
public class Task : ScriptableObject
{
    public string taskName;
    public string itemRequired;
    public int targetQuantity = 1;
    public string unit;
    public bool isNeeded;
    public bool dontShowNumber;
    public string msgComplete;
    public string msgIncomplete;
    public string msgZero;
    public Sprite sprite;
    

    public static Task CreateInstance(string taskName, string itemRequired, int targetQuantity,
        bool isNeeded, bool dontShowNumber, string msgComplete, string msgIncomplete, string msgZero) {
        Task task = CreateInstance<Task>();
        task.taskName = taskName;
        task.itemRequired = itemRequired;
        task.isNeeded = isNeeded;
        task.dontShowNumber = dontShowNumber;
        task.targetQuantity = targetQuantity;
        task.msgComplete = msgComplete;
        task.msgIncomplete = msgIncomplete;
        task.msgZero = msgZero;

        return task;
    }
}
