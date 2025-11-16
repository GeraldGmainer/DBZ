using UnityEngine;

public class PlayerLog : MonoBehaviour {
    private const int maxMessages = 3;
    private const float messageLiveTime = 7;
    private const float fadeOutTime = 2f;
    private const string LOG_PANEL_NAME = "PlayerLogPanel";
    private const string messagePrefabName = "PlayerLogMessage";
    private static Color errorColor = new Color(1f, 0, 0, 1f);
    private static Color warningColor = new Color(1f, 0.6f, 0, 1f);

    private static GameObject logPanel;
    private static PlayerLog instance;

    void Start() {
        logPanel = GameObject.Find(LOG_PANEL_NAME);
        if (logPanel == null) {
            Debug.LogError("PlayerLogPanel: PlayerLogPanel not found");
            return;
        }
    }

    public static void addErrorMessage(string message) {
        addMessage(message, errorColor);
    }

    public static void addWarningMessage(string message) {
        addMessage(message, warningColor);
    }

    private static void addMessage(string message, Color messageColor) {
        if (logPanel.transform.childCount >= maxMessages) {
            removeFirstMessage();
        }
        createMessage(message, messageColor);
    }

    private static void createMessage(string message, Color messageColor) {
        GameObject go = (GameObject)Instantiate(Resources.Load(messagePrefabName));
        go.transform.SetParent(logPanel.transform);
        go.GetComponent<PlayerLogMessage>().setMessage(message, messageColor, messageLiveTime, fadeOutTime);
    }

    private static void removeFirstMessage() {
        Destroy(logPanel.transform.GetChild(0).gameObject);
    }
}
