using UnityEngine;

public class CameraPositionDeterminer : MonoBehaviour {
    private CameraPivotPositionDeterminer cameraPivotPositionDeterminer;
    private CameraMouseXSmoother cameraMouseXSmoother;
    private CameraMouseYSmoother cameraMouseYSmoother;
    private CameraMouseWheelSmoother cameraMouseWheelSmoother;
    private CharacterViewFrustumHandler characterViewFrustumHandler;

    private float mouseXSmoothed;
    private float mouseYSmoothed;
    private float distanceSmooth;
    private float desiredDistance;

    void Awake() {
        cameraPivotPositionDeterminer = GetComponent<CameraPivotPositionDeterminer>();
        characterViewFrustumHandler = GetComponent<CharacterViewFrustumHandler>();
        cameraMouseXSmoother = GetComponent<CameraMouseXSmoother>();
        cameraMouseYSmoother = GetComponent<CameraMouseYSmoother>();
        cameraMouseWheelSmoother = GetComponent<CameraMouseWheelSmoother>();
    }

    public Vector3 getCameraPostion() {
        distanceSmooth = cameraMouseWheelSmoother.getSmoothedDistance(getClosestPossibleCameraDistance());
        mouseXSmoothed = cameraMouseXSmoother.getSmoothedMouseX();
        mouseYSmoothed = cameraMouseYSmoother.getSmoothedMouseY();

        return recalculateCameraPosition(mouseYSmoothed, mouseXSmoothed, distanceSmooth);
    }

    private float getClosestPossibleCameraDistance() {
        float desiredDistance = cameraMouseWheelSmoother.getDesiredDistance();
        Vector3 desiredPosition = recalculateCameraPosition(mouseYSmoothed, mouseXSmoothed, desiredDistance);
        return characterViewFrustumHandler.getClosestPossibleCameraDistance(desiredPosition);
    }

    private Vector3 recalculateCameraPosition(float xAxisDegrees, float yAxisDegrees, float distance) {
        Vector3 offset = -transform.forward;
        offset.y = 0.0f;
        offset *= distance;

        Quaternion rotXaxis = Quaternion.AngleAxis(xAxisDegrees, transform.right);
        Quaternion rotYaxis = Quaternion.AngleAxis(yAxisDegrees, Vector3.up);
        Quaternion rotation = rotYaxis * rotXaxis;

        return cameraPivotPositionDeterminer.getCameraPivotPosition() + rotation * offset;
    }

    public bool isInThirdPerson() {
        return distanceSmooth > 0.1f;
    }

    public Vector3 getSmoothedMouseXY() {
        return new Vector3(-mouseYSmoothed, mouseXSmoothed, 0);
    }

    public void reset() {
        cameraMouseXSmoother.reset(CharacterCameraHandler.START_MOUSE_X_POSITION);
        cameraMouseYSmoother.reset(CharacterCameraHandler.START_MOUSE_Y_POSITION);
        cameraMouseWheelSmoother.reset(CharacterCameraHandler.START_SCROLL);
    }
}
