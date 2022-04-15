using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputUI : InputField
{
    UIInputManager uiInputManager;

    protected override void Awake() {
        base.Awake();
        uiInputManager = FindObjectOfType<UIInputManager>();
    }

    public override void OnSelect(BaseEventData eventData) {
        base.OnSelect(eventData);
        uiInputManager.Select(this);
    }

    // public override void OnDeselect(BaseEventData eventData) {
    //     base.OnDeselect(eventData);
    //     uiInputManager.Deselect();
    // }
}
