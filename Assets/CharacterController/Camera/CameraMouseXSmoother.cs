using UnityEngine;

public class CameraMouseXSmoother : MonoBehaviour {
    private CharacterCameraHandler characterCameraHandler;

    private float mouseXRotation;
    private float mouseXCurrentVelocity;
    private float mouseXSmoothed;
    private KamehamehaHandler kamehamehaHandler;

    void Awake() {
        characterCameraHandler = GetComponent<CharacterCameraHandler>();
    }

    public float getSmoothedMouseX() {
        alignCameraWithCharacter();
        smoothMouseX();
        return mouseXSmoothed;
    }

    private void alignCameraWithCharacter() {
        if (!characterCameraHandler.isAlreadyAlignedWithCamera || !characterCameraHandler.alignCameraWhenMoving) {
            return;
        }

        float offsetToCameraRotation = CustomModulo(mouseXRotation, 360.0f);

        if (offsetToCameraRotation == 0) {
            return;
        }

        int numberOfFullRotations = (int)(mouseXRotation) / 360;

        if (mouseXRotation < 0) {
            if (offsetToCameraRotation < -180) {
                numberOfFullRotations--;
            }
        }
        else {
            if (offsetToCameraRotation > 180) {
                numberOfFullRotations++;
            }
        }

        mouseXRotation = numberOfFullRotations * 360.0f;
    }

    private float CustomModulo(float dividend, float divisor) {
        if (dividend < 0) {
            return dividend - divisor * Mathf.Ceil(dividend / divisor);
        }
        else {
            return dividend - divisor * Mathf.Floor(dividend / divisor);
        }
    }

    private void smoothMouseX() {
        float smoothTime = CharacterCameraHandler.MOUSE_SMOOTH_TIME;
        if (characterCameraHandler.isAlreadyAlignedWithCamera && characterCameraHandler.alignCameraWhenMoving) {
            smoothTime = CharacterCameraHandler.ALIGN_CAMERA_WHEN_MOVING_SMOOTH_TIME;
        }
        mouseXSmoothed = Mathf.SmoothDamp(mouseXSmoothed, mouseXRotation, ref mouseXCurrentVelocity, smoothTime);
    }

    public void reset(float startPosition) {
        mouseXRotation = startPosition;
    }

    public void resetMouseX() {
        mouseXRotation = 0;
        mouseXSmoothed = 0;
        mouseXCurrentVelocity = 0;
    }

    public void rotateMouseX(float xInput) {
        mouseXRotation += xInput * Settings.Instance.mouseSensitivity;
    }

    public void addRotation(float rot) {
        mouseXRotation += rot;
        mouseXSmoothed += rot;
    }
}
