using UnityEngine;

public class KamehamehaCursor : MonoBehaviour {
    private KamehamehaHandler kamehamehaHandler;
    private KamehamehaAngleValidator kamehamehaAngleValidator;
    private KamehamehaRangeValidator kamehamehaRangeValidator;
    private KamehamehaTargetDeterminer kamehamehaTargetDeterminer;

    void Awake() {
        kamehamehaHandler = GetComponent<KamehamehaHandler>();
        kamehamehaAngleValidator = GetComponent<KamehamehaAngleValidator>();
        kamehamehaRangeValidator = GetComponent<KamehamehaRangeValidator>();
        kamehamehaTargetDeterminer = GetComponent<KamehamehaTargetDeterminer>();
    }

    void Update() {
        if (!kamehamehaHandler.isCastingKamehameha) {
            CursorController.Instance.showDefaultCursor();
            return;
        }
        showKamehamehaCursor();
    }

    private void showKamehamehaCursor() {
        if (isKamehamehaValid(kamehamehaTargetDeterminer.determine())) {
            CursorController.Instance.showOrangeKamehamehaCursor();
        }
        else {
            CursorController.Instance.showRedKamehamehaCuror();
        }
    }

    private bool isKamehamehaValid(Vector3 target) {
        return !kamehamehaAngleValidator.isMaxAngleReached(target) && !kamehamehaRangeValidator.isTooClose(target);
    }

}
