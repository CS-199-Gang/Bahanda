using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// creates an option in the Asset tab to make a Task
[CreateAssetMenu(fileName = "task", menuName = "ScriptableObjects/Task", order =1)]

/// Class for Task instances
public class Task : ScriptableObject
{
    public string name;
    string description;
    string itemRequired;
    public int currQuantity = 0;
    public int targetQuantity = 1;
    public string msgComplete;
    public string msgIncomplete;
    public string msgZero;
    //public string[] feedbackMessages = new string[3];
    //public int state = 0;
}
