using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGrabbable : OVRGrabbable
{
    [SerializeField]
    private string type;
    private TutorialManager tutorialManager;
    private Grabbable grabbable;
    
    protected override void Start() {
        tutorialManager = FindObjectOfType<TutorialManager>();
        grabbable = GetComponent<Grabbable>();
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint) {
        base.GrabBegin(hand, grabPoint);
        tutorialManager.GrabObjective(type);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("TutorialSafe")) {
            tutorialManager.RecoverObject(gameObject);
        }
    }

    private void OnDestroy() {
        if (m_grabbedBy != null)
        {
            m_grabbedBy.ForceRelease(this);
        }

        tutorialManager.GrabObjective("backpack");
    }
}
