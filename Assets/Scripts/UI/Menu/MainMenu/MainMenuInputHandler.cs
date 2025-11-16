using UnityEngine;

public class MainMenuInputHandler : MonoBehaviour {
    private MainMenuHandler mainMenuHandler;

    void Awake() {
        mainMenuHandler = GetComponent<MainMenuHandler>();
    }

    void Update() {
        if (ErrorDialog.Instance.isVisible() && (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Submit"))) {
            handleErrorDialog();
        }
        else if (Input.GetButtonDown("Cancel")) {
            onCancel();
        }
        else if (Input.GetButtonDown("Submit")) {
            onEnter();
        }
    }

    private void handleErrorDialog() {
        PhotonManager.Instance.disconnectLobby();
        ErrorDialog.Instance.hide();
        mainMenuHandler.hideEverything();
    }

    private void onCancel() {


        switch (mainMenuHandler.currentMenu) {
            case MainMenuPage.MAIN_MENU:
            case MainMenuPage.LOADING_SCREEN:
            break;

            case MainMenuPage.FIND_SERVER:
            case MainMenuPage.CREATE_SERVER:
            PhotonManager.Instance.disconnectLobby();
            mainMenuHandler.hideSetupPanel();
            mainMenuHandler.showMainMenu();
            break;

            default:
            Debug.Log("MainMenuHandler: undefined menu");
            break;
        }
    }

    private void onEnter() {
        switch (mainMenuHandler.currentMenu) {
            case MainMenuPage.MAIN_MENU:
            case MainMenuPage.LOADING_SCREEN:
            break;

            case MainMenuPage.FIND_SERVER:
            GetComponentInChildren<FindServer>().onEnter();
            break;

            case MainMenuPage.CREATE_SERVER:
            GetComponentInChildren<CreateServer>().onEnter();
            break;

            default:
            Debug.Log("MainMenuHandler: undefined menu");
            break;
        }
    }
}
