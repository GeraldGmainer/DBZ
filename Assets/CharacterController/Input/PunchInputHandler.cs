using UnityEngine;

public class PunchInputHandler : MonoBehaviour {
    private MovementHandler movementHandler;
    private AxisInputHandler axisInputHandler;
    private PunchHandler punchHandler;
    private DrawSwordHandler drawSwordHandler;

    void Awake() {
        movementHandler = GetComponent<MovementHandler>();
        axisInputHandler = GetComponent<AxisInputHandler>();
        punchHandler = GetComponent<PunchHandler>();
        drawSwordHandler = GetComponent<DrawSwordHandler>();
    }

    public void update() {
        if (movementHandler.isSprinting || axisInputHandler.getVerticalFlyAxis() != 0) {
            return;
        }
        if (drawSwordHandler != null && drawSwordHandler.isAttachedToGrip()) {
            return;
        }
        handlePunchCombo();
    }

    private void handlePunchCombo() {
        if (Input.GetButtonDown("Action1")) {
            punchHandler.increase();
        }
    }
}
