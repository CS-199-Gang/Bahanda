using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private float charsPerSec;
    [SerializeField]
    private Dialogue currDialogue;
    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private Text nextButtonText;
    private int currMessageInd;
    private string currMessage;
    private int currChar;
    private bool typingMessage;
    TutorialManager tutorialManager;


    public void StartDialogue(Dialogue dialogue) {
        currDialogue = dialogue;
        typingMessage = false;
        currMessageInd = -1;
        if (currDialogue.startFunction.Length > 0) {
            tutorialManager.DoAction(currDialogue.startFunction);
        }
        NextMessage();
    }

    private void Awake() {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            NextMessage();
        }
    }

    private bool isLastMessage() {
        return currMessageInd + 1 == currDialogue.message.Length;
    }

    public void NextMessage() {
        if (typingMessage) {
            return;
        }
        if (isLastMessage()) {
            if (currDialogue.next != null) {
                StartDialogue(currDialogue.next);
            } else {
                FindObjectOfType<GameManager>().NextScene();
            }
            
            return;
        }
        nextButton.SetActive(false);
        currMessageInd++;
        text.text = "";
        typingMessage = true;
        currChar = 0;
        currMessage = currDialogue.message[currMessageInd];
        InvokeRepeating("DisplayNextCharacter", 0, 1/charsPerSec);
    }

    private void DisplayNextCharacter() {
        if (currChar >= currMessage.Length) {
            if (currDialogue.next == null) {
                nextButtonText.text = "Begin!";
            }
            CancelInvoke("DisplayNextCharacter");
            tutorialManager.SetObjective(currDialogue.objective);
            bool isbuttonActive = !isLastMessage() || currDialogue.objective == null || currDialogue.objective == "";
            nextButton.SetActive(isbuttonActive);
            typingMessage = false;
            return;
        }
        text.text += currMessage[currChar];
        currChar++;
    }
}
