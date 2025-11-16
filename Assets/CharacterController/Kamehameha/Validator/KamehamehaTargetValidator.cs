using UnityEngine;

public class KamehamehaTargetValidator : MonoBehaviour {
    private KamehamehaAngleValidator kamehamehaAngleValidator;
    private KamehamehaRangeValidator kamehamehaRangeValidator;

    void Awake() {
        kamehamehaRangeValidator = GetComponent<KamehamehaRangeValidator>();
        kamehamehaAngleValidator = GetComponent<KamehamehaAngleValidator>();
    }

    public bool isValid(Vector3 target) {
        if (kamehamehaRangeValidator.isTooClose(target)) {
            PlayerLog.addErrorMessage("The target is too close");
            return false;
        }
        if (kamehamehaAngleValidator.isMaxAngleReached(target)) {
            PlayerLog.addErrorMessage("The target is not within range");
            return false;
        }
        return true;
    }
}
