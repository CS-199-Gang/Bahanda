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

    public int GetQuantity(string item) {
        if (inventory.ContainsKey(item)) {
            return inventory[item];
        }
        return 0;
    }

    public void AddItem(string item) {
        if (inventory.ContainsKey(item)) {
            inventory[item] += 1;
        }
        else {
            inventory.Add(item, 1);
        }
    }

    public void RemoveItem(string item) {
        if (inventory.ContainsKey(item)) {
            if (inventory[item] > 0) {
                inventory[item] -= 1;
            }
        }
        else {
            inventory.Add(item, 0);
        }
    }
}
