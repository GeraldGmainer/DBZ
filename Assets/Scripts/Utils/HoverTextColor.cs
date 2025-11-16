using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverTextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField]
    private Color hoverColor = new Color(1, 1, 1, 1);

    private Text theText;
    private Color normalColor;
    private Button button;

    void Awake() {
        theText = GetComponent<Text>();
        normalColor = theText.color;
        button = transform.parent.GetComponent<Button>();
    }

    void OnEnable() {
        theText.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (button != null && button.interactable == false) {
            return;
        }
        theText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (button != null && button.interactable == false) {
            return;
        }
        theText.color = normalColor;
    }
}
