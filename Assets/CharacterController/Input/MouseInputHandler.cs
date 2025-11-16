using UnityEngine;

public class MouseInputHandler : MonoBehaviour {
    public float mouseXInput { get; private set; }
    public float mouseYInput { get; private set; }
    public float mouseWheelInput { get; private set; }

    public void update() {
        updateMouseXInput();
        updateMouseYInput();
        updateMouseWheelInput();
    }

    private void updateMouseXInput() {
        mouseXInput = Input.GetAxis("MouseX");
        if (Settings.Instance.invertMouseX) {
            mouseXInput *= -1;
        }
    }

    private void updateMouseYInput() {
        mouseYInput = Input.GetAxis("MouseY") * Settings.Instance.mouseSensitivity;
        if (!Settings.Instance.invertMouseY) {
            mouseYInput *= -1;
        }
    }

    private void updateMouseWheelInput() {
        mouseWheelInput = Input.GetAxis("MouseScrollWheel") * CharacterCameraHandler.MOUSE_SCROLL_SENSITIVITY;
    }
}
