using System;
using UnityEngine;
using UnityEngine.Events;

public class PhotonMainMenuErrorHandler : PhotonGenericErrorHandler {
    [SerializeField]
    private RectTransform mainMenuHandlerPanel;

    private MainMenuHandler mainMenuHandler;

    void Awake() {
        mainMenuHandler = mainMenuHandlerPanel.GetComponent<MainMenuHandler>();
    }

    private void goToMainMenu() {
        PhotonManager.Instance.disconnectLobby();
        mainMenuHandler.hideEverything();
    }

    public override UnityAction getButtonAction() {
        return goToMainMenu;
    }
}
