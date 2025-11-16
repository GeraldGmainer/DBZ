using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ErrorDialog : MonoBehaviour {
    [SerializeField]
    private Text headerText;
    [SerializeField]
    private Text contentText;
    [SerializeField]
    private Button okButton;
    [SerializeField]
    private RectTransform dialogPanel;

    public static ErrorDialog Instance;

    void Awake() {
        Instance = this;
    }

    public void show(string header, string content, UnityAction okButtonAction) {
        showDialog(header, content);
        okButton.onClick.AddListener(okButtonAction);
    }

    public void show(string header, string content) {
        showDialog(header, content);
    }

    private void showDialog(string header, string content) {
        dialogPanel.gameObject.SetActive(true);
        headerText.text = header;
        contentText.text = content;
        dialogPanel.offsetMin = Vector2.zero;
        dialogPanel.offsetMax = Vector2.zero;

        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(hide);
    }

    public void hide() {
        dialogPanel.gameObject.SetActive(false);
    }

    public bool isVisible() {
        return dialogPanel.gameObject.activeSelf;
    }

}
