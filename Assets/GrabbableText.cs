using UnityEngine;
using UnityEngine.UI;

public class GrabbableText : MonoBehaviour
{

    public float showTimer;
    [SerializeField]
    private Text text;
    private RectTransform rt;
    private Grabbable grabbable;
    private float heightOffset;
    
    public bool isPointing;
    float padding;

    private void Awake() {
        rt = GetComponent<RectTransform>();
        text.transform.localScale = new Vector3(-1, 1, 1);
    }

    private void Update() {
        transform.position = grabbable.transform.position + (Vector3.up * heightOffset); 
        transform.LookAt(GameManager.Instance.mainCamera.transform);
        rt.sizeDelta = text.rectTransform.sizeDelta + Vector2.one * padding;
    }

    public void SetGrabbable(Grabbable grabbable) {
        this.grabbable = grabbable;
    }

    public void SetDescription(string description) {
        text.text = description;
    }

    public void SetHeightOffset(float heightOffset) {
        this. heightOffset = heightOffset;
    }

    public void SetTextPadding(float padding) {
        this.padding = padding;
    }
}
