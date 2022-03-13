using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(OVRGrabbable))]
[RequireComponent(typeof(Outline))]
public class Grabbable : MonoBehaviour
{
    public string type;

    [SerializeField]
    private string description;
    [SerializeField]
    private GameObject grabbableTextGO;
    private GrabbableText text;

    [SerializeField]
    private float textHeightOffset;
    [SerializeField]
    private float textPadding;

    private int pointCounter = 0;
    private Outline outline;
    private float timer;
    

    private void Awake() {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");  
        Debug.Log(canvas);
        text = Instantiate(grabbableTextGO, transform.position, Quaternion.identity, canvas.transform).GetComponent<GrabbableText>();
        text.SetGrabbable(this);
        text.gameObject.SetActive(false);
    }

    private void Start() {
        text.SetDescription(description);
        text.SetHeightOffset(GetComponent<Collider>().bounds.size.y + textHeightOffset);
        text.SetTextPadding(textPadding);
    }

    private void Update() {
        if (pointCounter > 0) {
            if (timer < text.showTimer) {
                timer += Time.deltaTime;
            } else {
                text.gameObject.SetActive(true);
            }
        }
    }

    public void OnGrab() {
        return;
    }

    public void OnRelease(bool touchingBackpack) {
        if (touchingBackpack) {
            FindObjectOfType<InventoryManager>().AddItem(type);
            gameObject.SetActive(false);
        }
    }

    public void OnPoint() {
        pointCounter++;
        if (pointCounter > 0) {
            outline.enabled = true;
        }
    }

    public void OnLeave() {
        pointCounter--;
        if (pointCounter == 0) {
            outline.enabled = false;
            text.gameObject.SetActive(false);
            timer = 0;
        }
    }
    
    private void OnDestroy() {
        if (text != null) {
            Destroy(text.gameObject);
        }
    }
}
