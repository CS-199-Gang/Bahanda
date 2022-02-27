using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HandScript : MonoBehaviour
{

    LineRenderer lineRenderer;
    GameManager gameManager;
    List<GameObject> grabbables = new List<GameObject>();

    public string[] controls;
    public bool isLeft;
    GameObject holding;
    public Transform player;
    public Transform holdPos;
    public Transform pointStart;
    public Transform pointEnd;
    public GameObject text;
    public Text endText;
    const float throwMult = 75;
    Vector3 prevGrabPos;
    Vector3? teleportPos;

    void Awake() {
        lineRenderer = GetComponent<LineRenderer>(); 
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update() {
        // Teleporting
        if ((Input.GetAxisRaw("L_Horizontal") != 0 || Input.GetAxisRaw("L_Vertical") != 0) && isLeft) {
            GravCast(transform.position, (pointEnd.position-pointStart.position).normalized, 15, 0.5f);
        } else {
            if (teleportPos != null) {
                player.transform.position = (Vector3) teleportPos;
            }
            ClearTeleportLine();
        }
        // Grabbing
        if(Input.GetKeyDown(controls[0])){
            GameObject nearest = null;
            float dist = 0;
            foreach(GameObject g in grabbables) {
                if (nearest == null) {
                    nearest = g;
                    dist = Vector3.Distance(transform.position, g.transform.position);
                } else {
                    float newDist = Vector3.Distance(transform.position,g.transform.position);
                    if (newDist < dist) {
                        nearest = g;
                        dist = newDist;
                    }
                }
            }
            if (nearest != null) {
                holding = nearest;
            }
        }

        // Release Grab
        if(Input.GetKeyUp(controls[0])){ 
            if (holding != null) {
                holding.GetComponent<Rigidbody>().velocity = (holding.transform.position-prevGrabPos) * throwMult;
                holding = null;
            }
        }

        // Make grabbed object stay in hand
        if (holding != null) {
            prevGrabPos = new Vector3(holding.transform.position.x, holding.transform.position.y,holding.transform.position.z);
            holding.transform.position = holdPos.position;
            holding.transform.rotation = holdPos.rotation;
            holding.GetComponent<Rigidbody>().velocity = Vector3.zero;
            text.SetActive(true);
            if(Input.GetKeyDown("joystick 1 button 3")) {
                gameManager.AddItem(holding.GetComponent<Grabbable>().type);
                grabbables.Remove(holding);
                Destroy(holding);
                holding = null;
            }
        } else {
            text.SetActive(false);
        }
        if (Input.GetKeyDown("joystick 1 button 2")) {
            gameManager.ShowEndText();
        }
    }

    private void OnTriggerEnter(Collider other) {
        // Detect if can grab object nearby
        if(other.CompareTag("Grabbable")) {
            grabbables.Add(other.gameObject);
        }    
    }

    private void OnTriggerExit(Collider other) {
        // Detect if object cannot be grabbed anymore
        if(other.CompareTag("Grabbable")) {
            grabbables.Remove(other.gameObject);
        }    
    }

    // Gravcast to create the teleportation line 
    void GravCast(Vector3 startPos, Vector3 direction, int killAfter, float lineDist) {
        List<Vector3> vectors = new List<Vector3>();
        Ray ray = new Ray(startPos, direction);

        vectors.Add(pointEnd.position);
        for (int i = 0; i < killAfter; i++) {
            if(Physics.Raycast(ray, out RaycastHit hit, 1f)) {
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
