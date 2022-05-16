using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Grabbable")  || other.CompareTag("Towel")) {
            other.GetComponent<Grabbable>().OnPoint();
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Grabbable") || other.CompareTag("Towel")) {
            other.GetComponent<Grabbable>().OnLeave();
        }
    }
}
