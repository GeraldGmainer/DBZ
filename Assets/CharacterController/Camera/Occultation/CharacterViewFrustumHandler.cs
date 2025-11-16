using UnityEngine;
using System;
using System.Collections.Generic;

public class CharacterViewFrustumHandler : MonoBehaviour {
    public const string CAMERA_ZOOM_TAG = "AffectCameraZoom";
    public const string CAMERA_FADE_TAG = "AffectCameraFade";
    public const float FADE_OUT_ALPHA = 0.2f;
    public const float FADE_IN_ALPHA = 1.0f;
    public const float FADE_OUT_DURATION = 0.2f;
    public const float FADE_IN_DURATION = 0.2f;
    public const float CHAR_FADE_OUT_ALPHA = 0;
    public const float CHAR_FADE_START_DISTANCE = 1.2f;
    public const float CHAR_FADE_END_DISTANCE = 0.8f;

    private CameraPlaneClipper cameraPlaneClipper;
    private CameraOcculationChecker cameraOcculationChecker;
    private CameraPivotPositionDeterminer cameraPivotPositionDeterminer;

    private float closestDistance;
    private Vector3 startPosition;
    private ClipPlanePoints clipPlanePoints;

    void Awake() {
        cameraPlaneClipper = GetComponent<CameraPlaneClipper>();
        cameraOcculationChecker = GetComponent<CameraOcculationChecker>();
        cameraPivotPositionDeterminer = GetComponent<CameraPivotPositionDeterminer>();
    }

    public float getClosestPossibleCameraDistance(Vector3 atPosition) {
        startPosition = atPosition;

        clipPlanePoints = cameraPlaneClipper.getClippingPlanesAtCameraNearClipPlane(startPosition);
        List<RaycastHit> closestRaycastHits = cameraOcculationChecker.getClippingPlaneRaycastHits(clipPlanePoints, startPosition);
        return getClosestRaycastHitDistance(closestRaycastHits);
    }

    private float getClosestRaycastHitDistance(List<RaycastHit> closestRaycastHits) {
        RaycastHit[] closestRaycastHitsArray = convertToArray(closestRaycastHits);

        if (closestRaycastHitsArray.Length > 0) {
            return closestRaycastHitsArray[0].distance;
        }
        return -1;
    }

    private RaycastHit[] convertToArray(List<RaycastHit> closestRaycastHits) {
        RaycastHit[] closestRaycastHitsArray = closestRaycastHits.ToArray();
        Array.Sort(closestRaycastHitsArray, RaycastHitComparator);
        return closestRaycastHitsArray;
    }


    void OnDrawGizmos() {
        drawStartClippingPlaneLines();
        drawTargetClippingPlanesLines();
        drawLinesBetweenStartAndTarget();
        drawCenterLine();
    }

    private void drawStartClippingPlaneLines() {
        Gizmos.color = Color.red;

        Vector3 UpperLeft = startPosition + clipPlanePoints.upperLeft;
        Vector3 UpperRight = startPosition + clipPlanePoints.upperRight;
        Vector3 LowerLeft = startPosition + clipPlanePoints.lowerLeft;
        Vector3 LowerRight = startPosition + clipPlanePoints.lowerRight;

        Gizmos.DrawLine(UpperLeft, UpperRight);
        Gizmos.DrawLine(UpperLeft, LowerLeft);
        Gizmos.DrawLine(UpperRight, LowerRight);
        Gizmos.DrawLine(LowerLeft, LowerRight);
    }

    private void drawTargetClippingPlanesLines() {
        Gizmos.color = Color.red;

        Vector3 upperLeft = cameraPivotPositionDeterminer.getCameraPivotPosition() + clipPlanePoints.upperLeft;
        Vector3 upperRight = cameraPivotPositionDeterminer.getCameraPivotPosition() + clipPlanePoints.upperRight;
        Vector3 lowerLeft = cameraPivotPositionDeterminer.getCameraPivotPosition() + clipPlanePoints.lowerLeft;
        Vector3 lowerRight = cameraPivotPositionDeterminer.getCameraPivotPosition() + clipPlanePoints.lowerRight;

        Gizmos.DrawLine(upperLeft, upperRight);
        Gizmos.DrawLine(upperLeft, lowerLeft);
        Gizmos.DrawLine(upperRight, lowerRight);
        Gizmos.DrawLine(lowerLeft, lowerRight);
    }

    private void drawLinesBetweenStartAndTarget() {
        Gizmos.color = Color.white;

        Vector3 upperLeft = cameraPivotPositionDeterminer.getCameraPivotPosition() + clipPlanePoints.upperLeft;
        Vector3 upperRight = cameraPivotPositionDeterminer.getCameraPivotPosition() + clipPlanePoints.upperRight;
        Vector3 lowerLeft = cameraPivotPositionDeterminer.getCameraPivotPosition() + clipPlanePoints.lowerLeft;
        Vector3 lowerRight = cameraPivotPositionDeterminer.getCameraPivotPosition() + clipPlanePoints.lowerRight;
        Vector3 viewFrustumDirection = startPosition - cameraPivotPositionDeterminer.getCameraPivotPosition();

        Gizmos.DrawRay(upperLeft, viewFrustumDirection);
        Gizmos.DrawRay(upperRight, viewFrustumDirection);
        Gizmos.DrawRay(lowerLeft, viewFrustumDirection);
        Gizmos.DrawRay(lowerRight, viewFrustumDirection);

    }

    private void drawCenterLine() {
        Gizmos.color = Color.cyan;

        Vector3 viewFrustumDirection = startPosition - cameraPivotPositionDeterminer.getCameraPivotPosition();
        Gizmos.DrawRay(cameraPivotPositionDeterminer.getCameraPivotPosition(), viewFrustumDirection);
    }

    private int RaycastHitComparator(RaycastHit a, RaycastHit b) {
        return a.distance.CompareTo(b.distance);
    }
}

public struct ClipPlanePoints {
    public Vector3 upperLeft;
    public Vector3 upperRight;
    public Vector3 lowerLeft;
    public Vector3 lowerRight;
}
