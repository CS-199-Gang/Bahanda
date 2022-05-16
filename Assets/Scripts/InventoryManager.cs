using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Class for the Inventory system
public class InventoryManager : MonoBehaviour
{
    protected Dictionary<string, int> inventory = new Dictionary<string, int>();
    private UIManager uiManager;
    protected TaskManager taskManager;

    public Dictionary<string, int> GetInventory() {
        return inventory;
    }

    public int GetQuantity(string item) {
        if (inventory.ContainsKey(item)) {
            return inventory[item];
        }
        return 0;
    }

    public virtual void AddItem(string item) {
        if (inventory.ContainsKey(item)) {
            inventory[item] += 1;
        }
        else {
            inventory.Add(item, 1);
        }
        if (taskManager.IsTask(item, out string taskName)) {
            uiManager.DisplayItem(taskName, true);
        }
    }

    
    public virtual void AddItem(string item, int quantity, string name) {
        if (inventory.ContainsKey(item)) {
            inventory[item] += quantity;
        }
        else {
            inventory.Add(item, quantity);
        }
        uiManager.DisplayItem(name, true);
    }

    public virtual void RemoveItem(string item) {
        if (inventory.ContainsKey(item)) {
            if (inventory[item] <= 0) {
                return;
            }
            inventory[item] -= 1;
        }
        else {
            inventory.Add(item, 0);
        }
        string taskName;
        if (taskManager.IsTask(item, out taskName)) {
            uiManager.DisplayItem(taskName, false);
        }
    }

    protected virtual void Awake() {
        uiManager = FindObjectOfType<UIManager>();
        taskManager = FindObjectOfType<TaskManager>();
    }
}
