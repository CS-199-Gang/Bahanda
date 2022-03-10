using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public string type;
    
    private bool isGrabbed = false;
    private bool isReleased = false;
    private Vector3 prevGrabPos;
    private Rigidbody rb;

    const float THROWMULT = 75f;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (isGrabbed) {
            prevGrabPos = transform.position;
            
        }
        if (isReleased) {
            rb.velocity = (transform.position - prevGrabPos) * THROWMULT;
            isReleased = false;
        }
    }

    public void onGrab() {
        isGrabbed = true;
        rb.isKinematic = true;
    }

    public void onRelease(bool touchingBackpack) {
        isGrabbed = false;
        isReleased = true;
        rb.isKinematic = false;
        if (touchingBackpack) {
            FindObjectOfType<InventoryManager>().AddItem(type);
            Destroy(gameObject);
        }
    }
}
