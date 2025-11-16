using UnityEngine;

public class KamehamehaInputHandler : MonoBehaviour {
    private KamehamehaHandler kamehamehaHandler;

    private bool kamehamehaPressed;

    void Awake() {
        kamehamehaHandler = GetComponent<KamehamehaHandler>();
    }

    public void update() {
        handleKamehameha();
    }

    private void handleKamehameha() {
        if (Input.GetButton("Action2") && !kamehamehaPressed) {
            kamehamehaHandler.onButtonDown();
            kamehamehaPressed = true;
        }
        if (Input.GetButton("Action2")) {
            kamehamehaHandler.onButton();
        }
        if (!Input.GetButton("Action2") && kamehamehaPressed) {
            kamehamehaHandler.onButtonUp();
            kamehamehaPressed = false;
        }
    }
}
