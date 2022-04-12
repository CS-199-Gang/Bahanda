using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(OVRGrabber))]
public class HandScript : MonoBehaviour
{
    [SerializeField]
    bool isLeft;
    [SerializeField]
    bool canTeleport;
    [SerializeField]
    Transform playerAnchor;
    [SerializeField]
    Transform pointStart;
    [SerializeField]
    Transform pointEnd;
    [SerializeField]
    LayerMask teleportLayerMask;
    
    private LineRenderer lineRenderer;
    private Vector3? teleportPos;
    private bool touchingBackpack;
    private OVRGrabber ovrg;
    private Grabbable currGrabbed;
    private GameObject toDestroy;

    public void SetTouchingBackpack(bool touchingBackpack) {
        this.touchingBackpack = touchingBackpack;
    }

    private void Awake() {
        if (isLeft) {
            lineRenderer = GetComponent<LineRenderer>(); 
        }
        ovrg = GetComponent<OVRGrabber>();
    }

    private void Update() {
        // Teleporting
        if (isLeft && canTeleport) {
            if (Input.GetAxisRaw("L_Horizontal") != 0 || Input.GetAxisRaw("L_Vertical") != 0) {
                GravCast(transform.position, (pointEnd.position - pointStart.position).normalized, 15, 0.5f);
            } else {
                if (teleportPos != null) {
                    playerAnchor.transform.position = (Vector3) teleportPos;
                }
                ClearTeleportLine();
            }
        }

        // Check if grabbed object
        if (ovrg.grabbedObject != null && currGrabbed == null) {
            currGrabbed = ovrg.grabbedObject.GetComponent<Grabbable>();
            currGrabbed.OnGrab();
            if(toDestroy != null) {
                Destroy(toDestroy);
                toDestroy = null;
            }
        } else if (ovrg.grabbedObject == null && currGrabbed != null) {
            if(touchingBackpack) {
                toDestroy = currGrabbed.gameObject;
            }
            currGrabbed.OnRelease(touchingBackpack);
            currGrabbed = null;
        }
    }

    // Gravcast to create the teleportation line 
    void GravCast(Vector3 startPos, Vector3 direction, int killAfter, float lineDist) {
        List<Vector3> vectors = new List<Vector3>();
        Ray ray = new Ray(startPos, direction);

        vectors.Add(transform.position);
        for (int i = 0; i < killAfter; i++) {
            if(Physics.Raycast(ray, out RaycastHit hit, 1f, teleportLayerMask)) {
                teleportPos = hit.point;
                vectors.Add(hit.point);
                DrawTeleportLine(vectors);
                return;
            }
            ray = new Ray(ray.origin + ray.direction * lineDist, ray.direction + (Physics.gravity / 50));
            vectors.Add(ray.origin);
        }
        ClearTeleportLine();
        return;
    }
    
    void ClearTeleportLine() {
        lineRenderer.positionCount = 0;
        teleportPos = null;
    }

    void DrawTeleportLine(List<Vector3> vectors) {
        lineRenderer.positionCount = vectors.Count;
        lineRenderer.SetPositions(vectors.ToArray());
    }
}
