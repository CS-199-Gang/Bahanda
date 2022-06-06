using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitchScript : ConstrainedGrabbable
{
    [SerializeField]
    private GameObject mainObject;
    [SerializeField]
    private float midPointRotation;
    [SerializeField]
    private Light indicator;
    [SerializeField]
    private Grabbable grabbable;
    private bool isOn = true;
    private List<Light> houseLights = new List<Light>();

    public bool GetOn() {
        return isOn;
    }

    protected override void Start() {
        base.Start();
        GameObject[] houseLightsObject = GameObject.FindGameObjectsWithTag("HouseLight");
        foreach (GameObject go in houseLightsObject) {
            houseLights.Add(go.GetComponent<Light>());
        }

        indicator.color = Color.green;
    }

    private void Update() {
        if (mainObject.transform.rotation.x > midPointRotation && !isOn) {
            TurnOn();
        } else if (mainObject.transform.rotation.x <= midPointRotation && isOn) {
            TurnOff();
        }
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint) {
        base.GrabBegin(hand, grabPoint);

        if (hand.GetComponent<HandScript>().GetIsWet()) {
            FindObjectOfType<InventoryManager>().AddItem("touchedWet");
            hand.GetComponent<HandScript>().Electrocute();
            hand.ForceRelease(this);
            return;
        }
    }

    private void TurnOn() {
        isOn = true;
        indicator.color = Color.green;
        grabbable.RemoveThisItem();

        foreach (Light l in houseLights) {
            l.intensity *= 8f;
        }
    }

    private void TurnOff() {
        isOn = false;
        indicator.color = Color.red;
        grabbable.AddThisItem();

        foreach (Light l in houseLights) {
            l.intensity /= 8f;
        }
    }
}
