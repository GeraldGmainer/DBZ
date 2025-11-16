using UnityEngine;
using System.Collections;

public class PlayerListInputHandler : MonoBehaviour {
    private PlayerListHandler playerListHandler;

    void Awake() {
        playerListHandler = GetComponent<PlayerListHandler>();
    }

    void Update() {
        if (CellArenaMenuHandler.Instance.isMenuOpen && PhotonNetwork.connected && PhotonNetwork.connectedAndReady) {
            return;
        }
        handlePlayerListInput();
    }

    private void handlePlayerListInput() {
        if (Input.GetButtonDown("ActionTab")) {
            playerListHandler.enable();
        }
        if (Input.GetButtonUp("ActionTab")) {
            playerListHandler.disable();
        }
        if (Input.GetButton("ActionTab")) {
            playerListHandler.update();
        }
    }
}
