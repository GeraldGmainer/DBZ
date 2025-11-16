using UnityEngine;

public class CameraMouseYSmoother : MonoBehaviour {
    private float mouseYRotation;
    private float mouseYCurrentVelocity;
    private float mouseYSmoothed;

    public float getSmoothedMouseY() {
        smoothMouseY();

        return mouseYSmoothed;
    }

    private void smoothMouseY() {
        mouseYSmoothed = Mathf.SmoothDamp(mouseYSmoothed, mouseYRotation, ref mouseYCurrentVelocity, CharacterCameraHandler.MOUSE_SMOOTH_TIME);
    }

    public void reset(float startMouseY) {
        mouseYRotation = startMouseY;
    }

    public float getMoveYRotation() {
        return mouseYRotation;
    }

    public void setMoveYRotation(float rotation) {
        mouseYRotation = rotation;
    }

    public void clampMouseY(float desired) {
        mouseYRotation = Mathf.Clamp(desired, CharacterCameraHandler.MOUSE_Y_MIN, CharacterCameraHandler.MOUSE_Y_MAX);
    }


}
