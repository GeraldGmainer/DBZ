using UnityEngine;
using System;
using System.Collections.Generic;

public class CameraOcculationChecker : MonoBehaviour {
    private TagDependedOcculationHandler tagDependedOcculationHandler;
    private CameraPivotPositionDeterminer cameraPivotPositionDeterminer;

    private Vector3 targetPosition;

    void Awake() {
        tagDependedOcculationHandler = GetComponent<TagDependedOcculationHandler>();
        cameraPivotPositionDeterminer = GetComponent<CameraPivotPositionDeterminer>();
    }

    public List<RaycastHit> getClippingPlaneRaycastHits(ClipPlanePoints clipPlanePoints, Vector3 startPosition) {
        targetPosition = cameraPivotPositionDeterminer.getCameraPivotPosition();
        Vector3 rayDirection = startPosition - targetPosition;
        float rayDistance = rayDirection.magnitude;

        return raycastAllClippingPlane(clipPlanePoints, rayDirection, rayDistance);
    }

    private List<RaycastHit> raycastAllClippingPlane(ClipPlanePoints clipPlanePoints, Vector3 rayDirection, float rayDistance) {
        SortedDictionary<int, GameObject> objectsToFade = new SortedDictionary<int, GameObject>();
        List<RaycastHit> closestRaycastHits = new List<RaycastHit>();

        raycastAndSaveHit(targetPosition, rayDirection, rayDistance, ref objectsToFade, ref closestRaycastHits);
        raycastAndSaveHit(targetPosition + clipPlanePoints.upperLeft, rayDirection, rayDistance, ref objectsToFade, ref closestRaycastHits);
        raycastAndSaveHit(targetPosition + clipPlanePoints.upperRight, rayDirection, rayDistance, ref objectsToFade, ref closestRaycastHits);
        raycastAndSaveHit(targetPosition + clipPlanePoints.lowerLeft, rayDirection, rayDistance, ref objectsToFade, ref closestRaycastHits);
        raycastAndSaveHit(targetPosition + clipPlanePoints.lowerRight, rayDirection, rayDistance, ref objectsToFade, ref closestRaycastHits);

        tagDependedOcculationHandler.fadeObjects(objectsToFade);

        return closestRaycastHits;
    }

    private void raycastAndSaveHit(Vector3 start, Vector3 direction, float distance, ref SortedDictionary<int, GameObject> objectsToFade, ref List<RaycastHit> closestRaycastHits) {
        RaycastHit[] hitArray = Physics.RaycastAll(start, direction, distance);
        if (hitArray.Length > 0) {
            Array.Sort(hitArray, RaycastHitComparator);

            for (int i = 0; i < hitArray.Length; i++) {
                RaycastHit hit = hitArray[i];
                int hitObjectID = hit.transform.GetInstanceID();

                if (CharacterViewFrustumHandler.CAMERA_ZOOM_TAG.Equals(hit.transform.tag)) {
                    closestRaycastHits.Add(hitArray[i]);
                }
                else if (!objectsToFade.ContainsKey(hitObjectID) && CharacterViewFrustumHandler.CAMERA_FADE_TAG.Equals(hit.transform.tag)) {
                    objectsToFade.Add(hitObjectID, hit.transform.gameObject);
                }
            }
        }
    }

    private int RaycastHitComparator(RaycastHit a, RaycastHit b) {
        return a.distance.CompareTo(b.distance);
    }
}
