using UnityEngine;

public class CameraMouseClickInputHandler : MonoBehaviour {
    private CameraDeterminer cameraDeterminer;
    private MouseInputHandler mouseInputHandler;
    private KamehamehaHandler kamehamehaHandler;
    private CameraMouseXSmoother cameraMouseXSmoother;
    private CameraMouseYSmoother cameraMouseYSmoother;
    private MouseYConstrainDeterminer mouseYConstrainDeterminer;

    private float desiredMouseY;
    private bool isCharacterAligned;

    void Awake() {

        cameraDeterminer = GetComponent<CameraDeterminer>();
        mouseInputHandler = GetComponent<MouseInputHandler>();
        cameraMouseXSmoother = GetComponent<CameraMouseXSmoother>();
        cameraMouseYSmoother = GetComponent<CameraMouseYSmoother>();
        mouseYConstrainDeterminer = GetComponent<MouseYConstrainDeterminer>();
    }

    public void update() {
        Debug.Log(Settings.Instance.cameraMode);
        if (!(Input.GetButton("Fire1") || true || Settings.Instance.cameraMode != CameraMode.WOW)) {
            return;
        }
        updateDesiredMouseY();
        handleMouseX();
        handleMouseY();
    }

    private void updateDesiredMouseY() {
        desiredMouseY += mouseInputHandler.mouseYInput;
    }

    private void handleMouseX() {
        if (true) {
            alignCharacterOnFirstFrame();
        }
        else {
            isCharacterAligned = false;
            cameraMouseXSmoother.rotateMouseX(mouseInputHandler.mouseXInput);
        }
    }

    private void alignCharacterOnFirstFrame() {
        if (!isCharacterAligned) {
            isCharacterAligned = true;

            float cameraYrotation = cameraDeterminer.getCamera().transform.eulerAngles.y;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, cameraYrotation, transform.eulerAngles.z);
            cameraMouseXSmoother.resetMouseX();
        }
    }

    private void handleMouseY() {
        if (mouseYConstrainDeterminer.isMouseYConstrained()) {
            clampConstrainedMoveYRotation();
        }
        else {
            clampMoveYRotation();
        }

        desiredMouseY = Mathf.Min(desiredMouseY, CharacterCameraHandler.MOUSE_Y_MAX);
    }

    private void clampConstrainedMoveYRotation() {
        cameraMouseYSmoother.setMoveYRotation(Mathf.Clamp(desiredMouseY, Mathf.Max(cameraMouseYSmoother.getMoveYRotation(), CharacterCameraHandler.MOUSE_Y_MIN), CharacterCameraHandler.MOUSE_Y_MAX));
        desiredMouseY = Mathf.Max(desiredMouseY, cameraMouseYSmoother.getMoveYRotation() - 90.0f);
    }

    private void clampMoveYRotation() {
        cameraMouseYSmoother.clampMouseY(desiredMouseY);
    }

    public void reset() {
        desiredMouseY = CharacterCameraHandler.START_MOUSE_Y_POSITION;
    }

    public float getDesiredMouseY() {
        return desiredMouseY;
    }
}
