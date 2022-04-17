using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInputManager : MonoBehaviour
{
    [SerializeField]
    private GameObject keyboard;
    private List<Text> letters = new List<Text>();
    private bool isCaps = true;
    private InputUI currInput;

    private void Start() {
        keyboard.SetActive(false);

        Text[] buttonsText = keyboard.GetComponentsInChildren<Text>();

        foreach (Text t in buttonsText) {
            if (t.text.Length == 1 && char.IsLetter(t.text[0])) {
                letters.Add(t);
            } 
        }
    }

    public void Select(InputUI input) {
        currInput = input;
        keyboard.SetActive(true);
    }

    public void Deselect() {
        currInput = null;
        keyboard.SetActive(false);
    }
    
    public void InputText(Text text) {
        if (currInput != null) {
            if (text.text == "Enter") {
                Deselect();
            } else if (text.text == "Shift") {
                SwitchCase();
            } else if (text.text == "bs") {
                currInput.text = currInput.text.Remove(currInput.text.Length - 1, 1);
            } else {
                currInput.text += text.text;
                if (isCaps) {
                    SwitchCase();
                }
            }
        }
    }

    private void SwitchCase() {
        if (isCaps) {
            foreach (Text t in letters) {
                t.text = t.text.ToLower();
            }
        } else {
            foreach (Text t in letters) {
                t.text = t.text.ToUpper();
            }
        }
        isCaps = !isCaps;
    }
}
