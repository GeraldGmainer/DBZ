using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RespawnMenu : MonoBehaviour {

    [SerializeField]
    private RectTransform respawnPanel;
    public Button respawnButton;
    public Text respawnButonText;

    public static RespawnMenu Instance;
    private RespawnMenuButtonTextHandler respawnMenuButtonTextHandler;

    void Awake() {
        Instance = this;
        respawnMenuButtonTextHandler = GetComponent<RespawnMenuButtonTextHandler>();
    }

    public void showMenu() {
        respawnPanel.gameObject.SetActive(true);
        respawnPanel.offsetMax = Vector2.zero;
        respawnPanel.offsetMin = Vector2.zero;
        respawnMenuButtonTextHandler.startCountdown();
    }

    public void respawn() {
        respawnPanel.gameObject.SetActive(false);
        CharacterSpawner.Instance.respawn();
    }
}
