using UnityEngine;
using UnityEngine.UI;

public class FindServer : MonoBehaviour {
    public InputField playerNameInput;
    public Button joinServerButton;

    private MainMenuHandler mainManuHandler;
    private ServerListUIUpdater serverListUIUpdater;
    private FindServerValidator findServerValidator;
    private ServerListSelectionHandler serverListSelectionHandler;

    void Awake() {
        mainManuHandler = transform.root.GetComponentInChildren<MainMenuHandler>();
        serverListUIUpdater = GetComponent<ServerListUIUpdater>();
        findServerValidator = GetComponent<FindServerValidator>();
        serverListSelectionHandler = GetComponent<ServerListSelectionHandler>();
    }

    public void onEnter() {
        findServerValidator.validate();
        if (findServerValidator.isValid) {
            play();
        }
    }

    public void enterFindServer() {
        findServerValidator.validate();
        PhotonManager.Instance.enterFindServer();
        serverListUIUpdater.updateLoadingMessage();
    }

    public void play() {
        PhotonCharacterInitializer.init(playerNameInput.text);
        mainManuHandler.showLoadingScreen();
        PhotonManager.Instance.joinRoom(serverListSelectionHandler.getSelectedServerEntry().name);
    }
}
