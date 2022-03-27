using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(OVRGrabbable))]
public class Grabbable : MonoBehaviour
{
    public string type;

    [SerializeField]
    private string description;
    [SerializeField]
    private GameObject grabbableTextGO;
    [SerializeField]
    private bool canBackpack;
    [SerializeField]
    private bool hasOutline;
    private GrabbableText text;

    [SerializeField]
    private float textHeightOffset;
    [SerializeField]
    private float textPadding;

    private int pointCounter = 0;
    [SerializeField]
    private Outline outline;
    private float timer;
    private InventoryManager im;

    private void Awake() {
        if (hasOutline){
            outline ??= GetComponent<Outline>();
            outline.enabled = false;
            GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");  
            text = Instantiate(grabbableTextGO, transform.position - Vector3.up * 100,
                Quaternion.identity, canvas.transform).GetComponent<GrabbableText>();
            text.SetGrabbable(this);
            text.gameObject.SetActive(false);
        }
        im = FindObjectOfType<InventoryManager>();
    }

    private void Start() {
        if (hasOutline){
            text.SetDescription(description);
            text.SetHeightOffset(outline.GetComponent<Collider>().bounds.size.y + textHeightOffset);
            text.SetTextPadding(textPadding);
        }
    }

    private void Update() {
        if (pointCounter > 0 && hasOutline) {
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
        if (touchingBackpack && canBackpack) {
            AddThisItem();
            gameObject.SetActive(false);
        }
    }

    public void OnPoint() {
        pointCounter++;
        if (pointCounter > 0 && hasOutline) {
            outline.enabled = true;
        }
    }

    public void OnLeave() {
        pointCounter--;
        if (pointCounter == 0 && hasOutline) {
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

    public void AddThisItem() {
        im.AddItem(type);
    }

    public void RemoveThisItem() {
        im.RemoveItem(type);
    }
}
