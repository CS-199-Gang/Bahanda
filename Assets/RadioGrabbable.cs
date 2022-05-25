using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioGrabbable : OVRGrabbable
{
    [SerializeField]
    private GameObject message;

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint) {
        base.GrabBegin(hand, grabPoint);
        
        // show stuff :3
        message.SetActive(true);
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity) {
        base.GrabEnd(linearVelocity, angularVelocity);
        
        message.SetActive(false);
    }
}
