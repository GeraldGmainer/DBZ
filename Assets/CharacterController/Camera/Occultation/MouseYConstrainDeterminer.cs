using UnityEngine;

public class MouseYConstrainDeterminer : MonoBehaviour {
    private CameraDeterminer cameraDeterminer;
    private CameraMouseYSmoother cameraMouseYSmoother;
    private CameraPivotPositionDeterminer cameraPivotPositionDeterminer;
    private CameraMouseClickInputHandler cameraMouseClickHandler;

    private bool mouseYConstrained;

    void Awake() {
        cameraDeterminer = GetComponent<CameraDeterminer>();
        cameraPivotPositionDeterminer = GetComponent<CameraPivotPositionDeterminer>();
        cameraMouseYSmoother = GetComponent<CameraMouseYSmoother>();
        cameraMouseClickHandler = GetComponent<CameraMouseClickInputHandler>();
    }

    public void calculateMouseYConstrainDeterminer() {
        mouseYConstrained = false;
        recalculateMouseYConstrain();
    }

    private void recalculateMouseYConstrain() {
        RaycastHit hitInfo;
        mouseYConstrained = Physics.Raycast(cameraDeterminer.getCamera().transform.position, Vector3.down, out hitInfo, 1.0f);
        mouseYConstrained = mouseYConstrained && isTerrainHit(hitInfo) && isCameraYUnderPivot();
    }

    private bool isTerrainHit(RaycastHit hitInfo) {
        return hitInfo.transform.GetComponent<Terrain>();
    }

    private bool isCameraYUnderPivot() {
        return cameraDeterminer.getCamera().transform.position.y < cameraPivotPositionDeterminer.getCameraPivotPosition().y;
    }

    public void handleCameraLyingOnGround() {
        float lookUpDegrees = cameraMouseClickHandler.getDesiredMouseY() - cameraMouseYSmoother.getMoveYRotation();
        cameraDeterminer.getCamera().transform.Rotate(Vector3.right, lookUpDegrees);
    }

    /********************/

    public bool isMouseYConstrained() {
        return mouseYConstrained;
    }
}
