using UnityEngine;

public class Toast : MonoBehaviour {
    private const float messageLiveTime = 2;
    private const float fadeOutTime = 1f;
    private const string TOAST_PANEL_NAME = "ToastPanel";
    private const string messagePrefabName = "ToastMessage";

    private static GameObject toastPanel;

    void Start() {
        toastPanel = GameObject.Find(TOAST_PANEL_NAME);
        if (toastPanel == null) {
            Debug.LogError("PlayerLogPanel: PlayerLogPanel not found");
            return;
        }
    }

    public static void add(string message) {
        addMessage(message);
    }

    private static void addMessage(string message) {
        foreach (Transform t in toastPanel.GetComponentInChildren<Transform>()) {
            Destroy(t.gameObject);
        }
        createMessage(message);
    }

    private static void createMessage(string message) {
        GameObject go = (GameObject)Instantiate(Resources.Load(messagePrefabName));
        go.transform.SetParent(toastPanel.transform);
        go.GetComponent<ToastMessage>().setMessage(message, messageLiveTime, fadeOutTime);
    }
}
