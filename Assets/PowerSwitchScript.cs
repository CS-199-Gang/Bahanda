using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitchScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mainObject;
    [SerializeField]
    private float midPointRotation;
    [SerializeField]
    private Light indicator;
    [SerializeField]
    private Grabbable grabbable;
    private bool isOn = false;

    void Update() {
        if (mainObject.transform.rotation.y < midPointRotation && mainObject.transform.rotation.x < midPointRotation) {
            if (!isOn) {
                isOn = true;
                indicator.color = Color.green;
                grabbable.AddThisItem();
            }
        } else {
            if (isOn) {
                isOn = false;
                indicator.color = Color.red;
                grabbable.RemoveThisItem();
            }
        }
    }
}
