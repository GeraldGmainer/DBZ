using UnityEngine;

public class KamehamehaTargetDeterminer : MonoBehaviour {
    private CameraDeterminer cameraDeterminer;

    void Awake() {
        cameraDeterminer = GetComponent<CameraDeterminer>();
    }

    public Vector3 determine() {
        Vector3 target = determineTargetToNearestCollsion();
        if (target == Vector3.zero) {
            return determineMaxRangeTarget();
        }
        return target;
    }

    private Vector3 determineTargetToNearestCollsion() {
        Ray ray = cameraDeterminer.getCamera().ScreenPointToRay(Input.mousePosition);
        RaycastHit[] allHits;
        allHits = Physics.RaycastAll(ray);
        foreach (var hit in allHits) {
            if (!hit.transform.tag.Equals("Player") && Vector3.Distance(transform.position, hit.point) <= KamehamehaHandler.MAX_RANGE && hit.transform.gameObject.layer != Layers.IGNORE_RAYCAST) {
                return hit.point;
            }
        }
        return Vector3.zero;
    }

    private Vector3 determineMaxRangeTarget() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = KamehamehaHandler.MAX_RANGE;
        return cameraDeterminer.getCamera().ScreenToWorldPoint(mousePosition);
    }
}
