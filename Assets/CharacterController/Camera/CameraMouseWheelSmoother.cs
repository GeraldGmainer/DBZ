using UnityEngine;

public class CameraMouseWheelSmoother : MonoBehaviour {
    private CameraDeterminer cameraDeterminer;
    private MouseInputHandler mouseInputHandler;

    private float desiredDistance;
    private float distanceCurrentVelocity;
    private float distanceSmooth;

    void Awake() {
        cameraDeterminer = GetComponent<CameraDeterminer>();
        mouseInputHandler = GetComponent<MouseInputHandler>();
    }

    public float getDesiredDistance() {
        getScrollWheelInput();
        return desiredDistance;
    }

    public float getSmoothedDistance(float closestDistance) {
        calculateSmoothDesiredDistance(closestDistance);
        return distanceSmooth;
    }

    private void getScrollWheelInput() {
        desiredDistance = desiredDistance - mouseInputHandler.mouseWheelInput;
        desiredDistance = Mathf.Clamp(desiredDistance, CharacterCameraHandler.MIN_SCROLL_DISTANCE, CharacterCameraHandler.MAX_SCROLL_DISTANCE);
    }

    private float calculateSmoothDesiredDistance(float closestDistance) {
        if (closestDistance != -1) {
            closestDistance -= cameraDeterminer.getCamera().nearClipPlane;
            if (distanceSmooth < closestDistance) {
                // Smooth the distance if we move from a smaller constrained distance to a bigger constrained distance
                distanceSmooth = Mathf.SmoothDamp(distanceSmooth, closestDistance, ref distanceCurrentVelocity, CharacterCameraHandler.SCROLL_SMOOTH_TIME);
            }
            else {
                // Do not smooth if the new closest distance is smaller than the current distance
                distanceSmooth = closestDistance;
            }
        }
        else {
            distanceSmooth = Mathf.SmoothDamp(distanceSmooth, desiredDistance, ref distanceCurrentVelocity, CharacterCameraHandler.SCROLL_SMOOTH_TIME);
        }
        return distanceSmooth;
    }

    public void reset(float startDistance) {
        desiredDistance = startDistance;
    }

}
