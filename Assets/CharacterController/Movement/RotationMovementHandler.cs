using UnityEngine;

public class RotationMovementHandler : MonoBehaviour {
    private MovementHandler movementHandler;
    private AxisInputHandler axisInputHandler;

    public float rotation { get; private set; }
    public float animatorDirection { get; private set; }

    void Awake() {
        movementHandler = GetComponent<MovementHandler>();
        axisInputHandler = GetComponent<AxisInputHandler>();
    }

    public void update() {
        calculateRoation();
        updateAnimatorDirection();
        applyRotation();
    }

    private void calculateRoation() {
        float horizontalRotation = axisInputHandler.getHorizontalAxis() * MovementHandler.HORIZONTAL_ROTATING_SPEED;
        float fire2Rotation = axisInputHandler.getFire2Axis() * Settings.Instance.mouseSensitivity;
        rotation = horizontalRotation + fire2Rotation;
    }

    private void updateAnimatorDirection() {
        animatorDirection = Mathf.Clamp(rotation, -1, 1);

        if (movementHandler.isFlying) {
            if (animatorDirection > 0.1f) {
                animatorDirection = 1f;
            }
            else if (animatorDirection < -0.1f) {
                animatorDirection = -1f;
            }
        }
    }

    private void applyRotation() {
        transform.Rotate(Vector3.up * rotation * Time.fixedDeltaTime * 100f);
    }
}
