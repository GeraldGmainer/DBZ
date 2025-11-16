using UnityEngine;

public class MoveSpeedDeterminer : MonoBehaviour {
    private MovementHandler movementHandler;
    private AxisInputHandler axisInputHandler;

    private Vector3 direction;
    private Vector3 directionWorld;

    void Awake() {
        movementHandler = GetComponent<MovementHandler>();
        axisInputHandler = GetComponent<AxisInputHandler>();
    }

    public void update() {
        calulateDirection();
    }

    private void calulateDirection() {
        direction = new Vector3(axisInputHandler.getStrafeAxis(), 0, axisInputHandler.getVerticalAxis());
        directionWorld = transform.TransformDirection(direction);
        if (directionWorld.magnitude > 1) {
            directionWorld = Vector3.Normalize(directionWorld);
        }
    }

    public float getHorizontalSpeed() {
        float speed = 0f;

        if (axisInputHandler.isMovementAxisPressed()) {
            speed = (MovementHandler.RUN_SPEED * MovementHandler.STRAFE_MULTIPLIER * Mathf.Abs(direction.x) + MovementHandler.RUN_SPEED * Mathf.Abs(direction.z)) / (Mathf.Abs(direction.x) + Mathf.Abs(direction.z));
        }
        if (movementHandler.isSprinting && axisInputHandler.getStrafeAxis() == 0) {
            speed = MovementHandler.RUN_SPEED * MovementHandler.SPRINT_MULTIPLIER;
        }
        if (movementHandler.isMovingBackwards) {
            speed = MovementHandler.RUN_SPEED * MovementHandler.BACKWARD_MULTIPLIER;
        }
        return speed;
    }

    public float getVerticalSpeed() {
        return axisInputHandler.getVerticalFlyAxis() * MovementHandler.RUN_SPEED;
    }
}
