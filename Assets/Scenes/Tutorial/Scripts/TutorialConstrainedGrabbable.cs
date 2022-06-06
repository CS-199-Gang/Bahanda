using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialConstrainedGrabbable : OVRGrabbable
{
    [SerializeField]
    private Rigidbody objectRB;
    [SerializeField]
    private Transform handle;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private string type;
    private TutorialManager tutorialManager;
    private Rigidbody handleRB;
    private bool grabbed;
    private int stop = 0;

    protected override void Start() {
        base.Start();
        tutorialManager = FindObjectOfType<TutorialManager>();
        handleRB = handle.GetComponent<Rigidbody>();
        objectRB.isKinematic = true;
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint) {
        base.GrabBegin(hand, grabPoint);
        StartCoroutine(UpdateHandle());
        grabbedBy.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        objectRB.isKinematic = false;
        tutorialManager.GrabObjective(type);
    }

    IEnumerator UpdateHandle() {
        grabbed = true;
        while (grabbed) {
            handleRB.MovePosition(handle.position + (transform.position - handle.position) * speed);
            yield return null;
        }
    }

    private void FixedUpdate() {
        if (stop > 0) {
            handleRB.velocity = Vector3.zero;
            handleRB.angularVelocity = Vector3.zero;
            objectRB.velocity = Vector3.zero;
            objectRB.angularVelocity = Vector3.zero;
            stop--;
        }
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity) {
        grabbed = false;
        stop = 3;
        transform.position = handle.position;
        transform.rotation = handle.rotation;
        
        grabbedBy.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        base.GrabEnd(linearVelocity, angularVelocity);
        objectRB.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("TutorialSafe")) {
            tutorialManager.RecoverObject(gameObject);
        }
    }
}
