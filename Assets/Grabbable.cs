using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OVRGrabbable))]
public class Grabbable : MonoBehaviour
{
    public string type;
    
    public void onGrab() {
        return;
    }

    public void onRelease(bool touchingBackpack) {
        if (touchingBackpack) {
            FindObjectOfType<InventoryManager>().AddItem(type);
            Destroy(gameObject);
        }
    }
}
