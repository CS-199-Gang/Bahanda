using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackDetector : MonoBehaviour
{
    [SerializeField]
    private HandScript hs;

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        // Detect if can grab object nearby
        if (other.CompareTag("Backpack")) {
            hs.SetTouchingBackpack(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        // Detect if object cannot be grabbed anymore
        if (other.CompareTag("Backpack")) {
            hs.SetTouchingBackpack(false);
        }
    }
}
