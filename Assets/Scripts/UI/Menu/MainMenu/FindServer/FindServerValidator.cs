using UnityEngine;

public class FindServerValidator : MonoBehaviour {
    private FindServer findServer;
    private ServerListSelectionHandler serverListSelectionHandler;

    public bool isValid { get; private set; }

    void Awake() {
        findServer = GetComponent<FindServer>();
        serverListSelectionHandler = GetComponent<ServerListSelectionHandler>();
    }

    void Start() {
        validate();
    }

    public void onPlayerNameChange(string value) {
        if (findServer == null) {
            return;
        }
        validate();
    }

    public void validate() {
        if (isInputEmpty() || isServerListEmpty() || isRoomFull()) {
            isValid = false;
        }
        else {
            isValid = true;
        }
        findServer.joinServerButton.interactable = isValid;
    }

    private bool isInputEmpty() {
        return string.IsNullOrEmpty(findServer.playerNameInput.text);
    }

    private bool isServerListEmpty() {
        return serverListSelectionHandler.getSelectedServerEntry() == null;
    }

    private bool isRoomFull() {
        ServerEntryModel serverEntryModel = serverListSelectionHandler.getSelectedServerEntry();
        return serverEntryModel.playerCount >= serverEntryModel.maxPlayers;
    }
}
