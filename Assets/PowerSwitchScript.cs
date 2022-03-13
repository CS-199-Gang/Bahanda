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

    void Update()
    {
        if (mainObject.transform.rotation.y < midPointRotation) {
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

        public static float Clamp0360(float eulerAngles)
      {
          float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
          if (result < 0)
          {
              result += 360f;
          }
          return result;
      }
}
