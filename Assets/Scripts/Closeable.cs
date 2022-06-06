using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closeable : MonoBehaviour
{
    [SerializeField]
    private string type;
    private bool isClosed;
    private InventoryManager inventoryManager;
    private bool doesAffectSound;
    private ScenarioTwoScript s2Script;

    public bool IsClosed() {
        return isClosed;
    }

    private void Awake() {
        inventoryManager = FindObjectOfType<InventoryManager>();
        s2Script = FindObjectOfType<ScenarioTwoScript>();
    }

    private void Start() {
        doesAffectSound = type == "window" || type == "door";
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("ClosedDetector")) {
            Close();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("ClosedDetector")) {
            Open();
        }
    }

    private void Close() {
        isClosed = true;
        inventoryManager.AddItem(type);
        if (doesAffectSound && s2Script != null) {
            s2Script.DampenSound(-1);
        }
    }

    private void Open() {
        inventoryManager.RemoveItem(type);
        isClosed = false;
        if (doesAffectSound && s2Script != null) {
            s2Script.DampenSound(1);
        }
    }
}
