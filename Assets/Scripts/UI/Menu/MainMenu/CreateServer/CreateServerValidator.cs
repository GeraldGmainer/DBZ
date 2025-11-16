using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreateServerValidator : MonoBehaviour {
    private CreateServer createServer;
    private EventSystem system;

    public bool isValid { get; private set; }

    void Awake() {
        createServer = GetComponent<CreateServer>();
        system = EventSystem.current;
    }

    void Update() {
        handleTab();
    }

    public void onServerNameChange(string value) {
        if (createServer == null) {
            return;
        }
        validate();
    }

    public void onPlayerNameChange(string value) {
        if (createServer == null) {
            return;
        }
        validate();
    }

    public void validate() {
        if (string.IsNullOrEmpty(createServer.serverNameInput.text) || string.IsNullOrEmpty(createServer.playerNameInput.text)) {
            isValid = false;
        }
        else {
            isValid = true;
        }
        createServer.createServerButton.interactable = isValid;
    }

    private void handleTab() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            }
            if (next != null) {
                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null)
                    inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret

                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
            //else Debug.Log("next nagivation element not found");
        }
    }
}
