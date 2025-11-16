using UnityEngine;

public class KamehamehaRangeValidator : MonoBehaviour {
    public bool isTooClose(Vector3 target) {
        return Vector3.Distance(transform.position, target) < KamehamehaHandler.MIN_RANGE;
    }
}
