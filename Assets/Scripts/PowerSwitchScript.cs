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
    private bool isOn = true;
    private List<Light> houseLights = new List<Light>();


    private void Start() {
        GameObject[] houseLightsObject = GameObject.FindGameObjectsWithTag("HouseLight");
        foreach (GameObject go in houseLightsObject) {
            houseLights.Add(go.GetComponent<Light>());
        }

        indicator.color = Color.green;
    }

    void Update() {
        if (mainObject.transform.rotation.x > midPointRotation && !isOn) {
            TurnOn();
        } else if (mainObject.transform.rotation.x <= midPointRotation && isOn) {
            TurnOff();
        }
    }

    private void TurnOn() {
        isOn = true;
        indicator.color = Color.green;
        grabbable.RemoveThisItem();

        foreach (Light l in houseLights) {
            l.intensity *= 5f;
        }
    }

    private void TurnOff() {
        isOn = false;
        indicator.color = Color.red;
        grabbable.AddThisItem();

        foreach (Light l in houseLights) {
            l.intensity /= 5f;
        }
    }
}
