using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(OVRGrabber))]
public class HandScript : MonoBehaviour
{
    [SerializeField]
    private bool isLeft;
    [SerializeField]
    private bool canTeleport;
    [SerializeField]
    private Transform playerAnchor;
    [SerializeField]
    private Transform pointStart;
    [SerializeField]
    private Transform pointEnd;
    [SerializeField]
    private LayerMask teleportWhitelist;
    [SerializeField]
    private LayerMask teleportBlacklist;
    [SerializeField]
    private SkinnedMeshRenderer mesh;
    [SerializeField]
    private Material dryMat;
    [SerializeField]
    private Material wetMat;
    [SerializeField]
    private GameObject electricity;
    [SerializeField]
    private Transform wetParticles;
    
    private LineRenderer lineRenderer;
    private Vector3? teleportPos;
    private bool touchingBackpack;
    private OVRGrabber ovrg;
    private Grabbable currGrabbed;
    private GameObject toDestroy;
    private ParticleSystem ps;
    private bool isWet;

    public void SetTouchingBackpack(bool touchingBackpack) {
        this.touchingBackpack = touchingBackpack;
    }

    public void setCanTeleport(bool canTeleport) {
        this.canTeleport = canTeleport;
    }

    public bool GetIsWet() {
        return isWet;
    }

    public void Electrocute() {
        electricity.SetActive(true);
        Invoke("CancelElectrocute", 1.5f);
    }

    private void CancelElectrocute() {
        electricity.SetActive(false);
    }

    private void Awake() {
        if (isLeft) {
            lineRenderer = GetComponent<LineRenderer>(); 
        }
        ovrg = GetComponent<OVRGrabber>();
        if (wetParticles != null) {
            ps = wetParticles.GetComponent<ParticleSystem>();
        }
    }

    private void Update() {
        // Check if grabbed object
        if (ovrg.grabbedObject != null && currGrabbed == null) {
            currGrabbed = ovrg.grabbedObject.GetComponent<Grabbable>();
            currGrabbed.OnGrab();
            if(toDestroy != null) {
                Destroy(toDestroy);
                toDestroy = null;
            }
        } else if (ovrg.grabbedObject == null && currGrabbed != null) {
            if(touchingBackpack && currGrabbed.GetComponent<ConstrainedGrabbable>() == null) {
                toDestroy = currGrabbed.gameObject;
            }
            currGrabbed.OnRelease(touchingBackpack);
            currGrabbed = null;
        }

        
        // Teleporting
        if (!isLeft) {
            return;
        }
        if (canTeleport) {
            if (Input.GetAxisRaw("L_Horizontal") != 0 || Input.GetAxisRaw("L_Vertical") != 0) {
                GravCast(transform.position, (pointEnd.position - pointStart.position).normalized, 15, 0.5f);
            } else {
                if (teleportPos != null) {
                    playerAnchor.transform.position = (Vector3) teleportPos;
                }
                ClearTeleportLine();
            }
        } else {
            ClearTeleportLine();
        }
    }

    // Gravcast to create the teleportation line 
    void GravCast(Vector3 startPos, Vector3 direction, int killAfter, float lineDist) {
        List<Vector3> vectors = new List<Vector3>();
        Ray ray = new Ray(startPos, direction);

        vectors.Add(transform.position);
        for (int i = 0; i < killAfter; i++) {
            if(Physics.Raycast(ray, out RaycastHit hit, 1f, teleportWhitelist)) {
                teleportPos = hit.point;
                vectors.Add(hit.point);
                DrawTeleportLine(vectors);
                return;
            } else if (Physics.Raycast(ray, 1f, teleportBlacklist)) {
                ClearTeleportLine();
                return;
            }
            ray = new Ray(ray.origin + ray.direction * lineDist, ray.direction + (Physics.gravity / 50));
            vectors.Add(ray.origin);
        }
        ClearTeleportLine();
    }
    
    void ClearTeleportLine() {
        lineRenderer.positionCount = 0;
        teleportPos = null;
    }

    void DrawTeleportLine(List<Vector3> vectors) {
        lineRenderer.positionCount = vectors.Count;
        lineRenderer.SetPositions(vectors.ToArray());
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Flood")) {
            GetWet();
        } else if (other.CompareTag("Towel")) {
            GetDry();
        }
    }

    private void GetWet() {
        ps.Play();
        mesh.material = wetMat;
        isWet = true;
    }

    private void GetDry() {
        ps.Stop();
        mesh.material = dryMat;
        isWet = false;
    }
}
