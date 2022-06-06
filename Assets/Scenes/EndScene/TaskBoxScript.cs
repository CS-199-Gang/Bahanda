using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TaskBoxScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Text quantity;
    [SerializeField]
    private Text hoverText;
    [SerializeField]
    private GameObject hoverBox;
    [SerializeField]
    private Transform hoverBoxPos;
    private bool hasHoverBox;
    private Task task;

    public void SetItems(Task task, int currQuantity) {
        this.task = task;
        title.text = task.taskName;
        image.sprite = task.sprite;
        if (task.isNeeded)  {
            quantity.text = currQuantity + "/" + task.targetQuantity + " " + task.unit;
            hasHoverBox = true;
            if (currQuantity >= task.targetQuantity) {
                quantity.color = Color.green;
                hoverText.text = task.msgComplete;
            } else if (currQuantity == 0) {
                quantity.color = Color.red;
                hoverText.text = task.msgZero;
            } else {
                quantity.color = Color.yellow;
                hoverText.text = task.msgIncomplete;
            }
        } else {
            quantity.text = currQuantity.ToString() + task.unit;
            hoverText.text = task.msgComplete;
            hasHoverBox = task.msgComplete != "";
        }
    }

    public void SetInventoryItem(Task task, int currQuantity) {
        this.task = task;
        title.text = task.taskName;
        image.sprite = task.sprite;
        quantity.text = currQuantity + task.unit;
        hasHoverBox = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (hasHoverBox) {
            hoverBox.transform.position = hoverBoxPos.position;
            hoverBox.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (hasHoverBox) {
            hoverBox.SetActive(false);
        }
    }

    public Transform GetHoverBoxTransform() {
        return hoverBox.transform;
    }
}
