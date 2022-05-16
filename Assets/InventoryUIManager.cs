using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : InventoryManager
{
    [SerializeField]
    GameObject taskBoxGO;
    [SerializeField]
    Transform inventoryGrid;
    Dictionary<string, TaskBoxScript> taskBoxes = new Dictionary<string, TaskBoxScript>();
    bool isActive = false;
    bool canInventory = true;

    protected override void Awake() {
        base.Awake();
        taskManager = FindObjectOfType<TaskManager>();
    }
    
    private void Update() {
        if (canInventory && (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two)
            || OVRInput.GetDown(OVRInput.Button.Three) || OVRInput.GetDown(OVRInput.Button.Four))) {
            isActive = !isActive;
            inventoryGrid.gameObject.SetActive(isActive);
        }
    }

    public override void AddItem(string item, int quantity, string name) {
        base.AddItem(item, quantity, name);

        if (!taskBoxes.ContainsKey(item)) {
            TaskBoxScript tbScript = Instantiate(taskBoxGO, inventoryGrid.position, inventoryGrid.rotation,
                inventoryGrid).GetComponent<TaskBoxScript>();
            taskBoxes.Add(item, tbScript);
        }

        Task task = taskManager.findTask(item);
        int qty = inventory[item];
        taskBoxes[item].SetInventoryItem(task, qty);
  }

    public void setCanIventory(bool canInventory) {
        this.canInventory = canInventory;
    }
}
