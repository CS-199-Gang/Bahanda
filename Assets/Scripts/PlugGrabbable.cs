using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugGrabbable : OVRGrabbable
{
    [SerializeField]
    private float attachCD = 1;
    [SerializeField]
    Socket attachedSocket;

    private InventoryManager inventoryManager;
    private Socket socket;
    private bool canAttach = true;
    private Rigidbody rb;

    protected override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        Attach(attachedSocket);
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint) {
        base.GrabBegin(hand, grabPoint);

        if (socket != null) {
            socket.Release();
            socket = null;
            canAttach = false;
            Invoke("AllowAttach", attachCD);
            inventoryManager.AddItem("plug");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Socket") && canAttach && m_grabbedBy != null) {
            Socket socket = other.GetComponent<Socket>();
            Attach(socket);
        }    
    }

    private void Attach(Socket socket) {
        if (!socket.IsOccupied()) {
            socket.Occupy(this);
            this.socket = socket;
            transform.position = socket.transform.position;
            transform.rotation = socket.transform.rotation;
            if(m_grabbedBy != null) {
                m_grabbedBy.ForceRelease(this);
            }
            rb.isKinematic = true;
            if (inventoryManager != null) {
                inventoryManager.RemoveItem("plug");
            }
        }
    } 

    private void AllowAttach() {
        canAttach = true;
    }
}
