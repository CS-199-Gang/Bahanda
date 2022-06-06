using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideDetector : MonoBehaviour
{
    private bool isOut;
    private float outTime;
    private InventoryManager inventoryManager;

    private void Awake() {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void Update() {
        if (isOut) {
            outTime += Time.deltaTime;
            if (outTime > 1) {
                outTime -= 1;
                inventoryManager.AddItem("outside", 1, "Stayed Outside");
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("FloodDetector")) {
            isOut = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("FloodDetector")) {
            isOut = true;
        }
    }
}
