using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Class for the Inventory system
public class InventoryManager : MonoBehaviour
{
    Dictionary<string, int> inventory = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Dictionary<string, int> GetInventory() {
        return inventory;
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
