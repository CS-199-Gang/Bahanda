using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugGrabbable : OVRGrabbable
{
    [SerializeField]
    private float attachCD = 1;

    private Socket socket;
    private bool canAttach = true;
    private Rigidbody rb;

    protected override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody>();
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint) {
        base.GrabBegin(hand, grabPoint);

        if (socket != null) {
            socket.Release();
            socket = null;
            canAttach = false;
            //rb.isKinematic = false;
            Invoke("AllowAttach", attachCD);
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Socket") && canAttach && m_grabbedBy != null) {
            Socket socket = other.GetComponent<Socket>();
            if (!socket.IsOccupied()) {
                socket.Occupy(this);
                this.socket = socket;
                transform.position = other.transform.position;
                transform.rotation = other.transform.rotation;
                m_grabbedBy.ForceRelease(this);
                rb.isKinematic = true;
            }
        }    
    } 

    private void AllowAttach() {
        canAttach = true;
    }
}
