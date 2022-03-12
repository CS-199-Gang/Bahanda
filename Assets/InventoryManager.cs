using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Class for the Inventory system
public class InventoryManager : MonoBehaviour
{
    Dictionary<string, int> inventory = new Dictionary<string, int>();

    public Dictionary<string, int> GetInventory() {
        return inventory;
    }

    void Start() {
        DontDestroyOnLoad(this);
    }

    public void AddItem(string item) {
        if (inventory.ContainsKey(item)) {
            inventory[item] += 1;
        }
        else {
            inventory.Add(item, 1);
        }
    }
}
