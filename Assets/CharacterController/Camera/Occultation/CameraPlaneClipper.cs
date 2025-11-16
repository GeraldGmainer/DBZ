using UnityEngine;

public class CameraPlaneClipper : MonoBehaviour {
    private CameraDeterminer cameraDeterminer;
    private CameraPivotPositionDeterminer cameraPivotPositionDeterminer;

    void Awake() {
        cameraDeterminer = GetComponent<CameraDeterminer>();
        cameraPivotPositionDeterminer = GetComponent<CameraPivotPositionDeterminer>();
    }

    public ClipPlanePoints getClippingPlanesAtCameraNearClipPlane(Vector3 atPosition) {
        ClipPlanePoints clipPlanePoints = new ClipPlanePoints();

        float halfFieldOfView = cameraDeterminer.getCamera().fieldOfView * 0.5f * Mathf.Deg2Rad;
        float halfHeight = cameraDeterminer.getCamera().nearClipPlane * Mathf.Tan(halfFieldOfView);
        float halfWidth = halfHeight * cameraDeterminer.getCamera().aspect;

        Vector3 targetDirection = cameraPivotPositionDeterminer.getCameraPivotPosition() - atPosition;
        targetDirection.Normalize();

        Vector3 localRight = cameraDeterminer.getCamera().transform.right;
        Vector3 localUp = Vector3.Cross(targetDirection, localRight);
        localUp.Normalize();

        float offset = cameraDeterminer.getCamera().nearClipPlane;

        clipPlanePoints.upperLeft = -localRight * halfWidth;
        clipPlanePoints.upperLeft += localUp * halfHeight;
        clipPlanePoints.upperLeft += targetDirection * offset;

        clipPlanePoints.upperRight = localRight * halfWidth;
        clipPlanePoints.upperRight += localUp * halfHeight;
        clipPlanePoints.upperRight += targetDirection * offset;

        clipPlanePoints.lowerLeft = -localRight * halfWidth;
        clipPlanePoints.lowerLeft -= localUp * halfHeight;
        clipPlanePoints.lowerLeft += targetDirection * offset;

        clipPlanePoints.lowerRight = localRight * halfWidth;
        clipPlanePoints.lowerRight -= localUp * halfHeight;
        clipPlanePoints.lowerRight += targetDirection * offset;

        return clipPlanePoints;
    }


}
