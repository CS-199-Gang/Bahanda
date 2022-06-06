using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private Dialogue tutorialDialogue;
    [SerializeField]
    private GameObject grabbableObjects;
    [SerializeField]
    private GameObject triggerModel;
    [SerializeField]
    private GameObject backpackDiagram;
    [SerializeField]
    private GameObject inventoryModel;
    [SerializeField]
    private GameObject lookModel;
    [SerializeField]
    private GameObject moveModel;
    [SerializeField]
    private Transform modelAnchor;
    [SerializeField]
    private HandScript teleportHand;
    [SerializeField]
    private float nextObjectiveTimer;
    [SerializeField]
    private Transform playerTransform;
    
    private Vector3 playerStartPos;
    private string nextObjective;
    private GameObject[] objects = new GameObject[2];
    private DialogueManager dialogueManager;
    private InventoryUIManager iuiManager;

    public void SetObjective(string type) {
        nextObjective = type;
    }

    public void GrabObjective(string type) {
        if (type == nextObjective) {
            Invoke("NextMessage", nextObjectiveTimer);
            nextObjective = null;
        }
    }

    public void DoAction(string diagram) {
        switch (diagram) {
            case "clearModel":
                SetObject(1);
                break;
            case "triggers":
                ShowTrigger();
                break;
            case "backpack":
                ShowBackpack();
                break;
            case "inventory":
                ShowInventory();
                break;
            case "turn":
                ShowLook();
                break;
            case "move":
                ShowMove();
                break;
        }
    }

    public void RecoverObject(GameObject obj) {
        obj.transform.position = modelAnchor.transform.position;
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null) {
            rb.velocity = Vector3.zero;
        }
    }
    
    private void Awake() {
        dialogueManager = FindObjectOfType<DialogueManager>();
        playerStartPos = playerTransform.position;
        iuiManager = FindObjectOfType<InventoryUIManager>();
    }

    private void Start() { 
        dialogueManager.StartDialogue(tutorialDialogue);
        iuiManager.setCanIventory(false);
    }

    private void Update() {
        if (Vector3.Distance(playerTransform.position, playerStartPos) > 1 && nextObjective == "teleport") {
            Invoke("TeleportBack", 5);
            GrabObjective("teleport");
        }
    }

    private void ShowTrigger() {
        SetObject(0, grabbableObjects);
        SetObject(1, triggerModel);
        nextObjective = "item";
    }

    private void ShowBackpack() {
        Canvas canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        SetObject(1, backpackDiagram, canvas.transform);

        TutorialGrabbable[] objs = FindObjectsOfType<TutorialGrabbable>();
        foreach(TutorialGrabbable obj in objs) {
            obj.GetComponent<Grabbable>().SetBackpack(true);
        }
    }

    private void ShowInventory () {
        SetObject(0);
        SetObject(1, inventoryModel);
        iuiManager.setCanIventory(true);
    }

    private void ShowLook() {
        SetObject(0);
        SetObject(1, lookModel);
    }

    private  void ShowMove() {
        SetObject(1, moveModel);
        teleportHand.setCanTeleport(true);
    }

    private void TeleportBack() {
        SetObject(1);
        playerTransform.position = playerStartPos;
        teleportHand.setCanTeleport(false);
    }

    private void SetObject(int index,  GameObject obj = null, Transform parent = null) {
        if (objects[index] != null) {
            Destroy(objects[index]);
        }
        if (obj == null) {
            return;
        }
        Transform objParent = parent ?? modelAnchor;
        objects[index] = obj != null ? Instantiate(obj, modelAnchor.position, modelAnchor.rotation, objParent) : null;
    }

    private void NextMessage() {
        dialogueManager.NextMessage();
    }
}
