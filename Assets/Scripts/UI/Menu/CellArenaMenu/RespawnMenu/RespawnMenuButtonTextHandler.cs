using UnityEngine;
using System.Collections;

public class RespawnMenuButtonTextHandler : MonoBehaviour {
    private const int RESPAWN_SEC = 5;

    private RespawnMenu respawnMenu;

    void Awake() {
        respawnMenu = GetComponent<RespawnMenu>();
    }

    public void startCountdown() {
        StartCoroutine("start");
    }

    IEnumerator start() {
        respawnMenu.respawnButton.interactable = false;
        for (int i = RESPAWN_SEC; i >= 1; i--) {
            respawnMenu.respawnButonText.text = "RESPAWN (" + i + ")";
            yield return new WaitForSeconds(1);
        }
        respawnMenu.respawnButonText.text = "RESPAWN";
        respawnMenu.respawnButton.interactable = true;
    }
}
