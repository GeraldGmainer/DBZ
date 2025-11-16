using UnityEngine;

public class KamehamehaAngleValidator : MonoBehaviour {

    public bool isMaxAngleReached(Vector3 target) {
        float angle = Vector3.Angle(target - transform.position, transform.forward);
        return angle > KamehamehaHandler.MAX_ANGLE;
    }
}
