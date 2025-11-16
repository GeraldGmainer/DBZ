using UnityEngine;
using UnityEngine.UI;

public class CreateServer : MonoBehaviour {
    public InputField serverNameInput;
    public InputField playerNameInput;
    public Button createServerButton;

    private MainMenuHandler mainManuHandler;
    private CreateServerValidator createServerValidator;

    void Awake() {
        mainManuHandler = transform.root.GetComponentInChildren<MainMenuHandler>();
        createServerValidator = GetComponent<CreateServerValidator>();
    }

    public void onEnter() {
        createServerValidator.validate();
        if (createServerValidator.isValid) {
            play();
        }
    }

    public void play() {
        PhotonCharacterInitializer.init(playerNameInput.text);
        mainManuHandler.showLoadingScreen();
        PhotonManager.Instance.createRoom(serverNameInput.text);
    }
}
