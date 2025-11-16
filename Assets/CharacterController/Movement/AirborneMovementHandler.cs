using UnityEngine;

public class AirborneMovementHandler : MonoBehaviour {
    private MovementHandler movementHandler;
    private AxisInputHandler axisInputHandler;

    private Rigidbody rigidBody;
    private Vector3 direction;
    private Vector3 directionWorld;
    private Vector3 smoothedVelocity;

    void Awake() {
        movementHandler = GetComponent<MovementHandler>();
        rigidBody = GetComponent<Rigidbody>();
        axisInputHandler = GetComponent<AxisInputHandler>();
    }

    public void update() {
        applyJump();
        applyExtraGravity();
        calculateDirection();
        calculateSmoothedVelocity();
        applySmoothedVelocity();
    }

    private void applyJump() {
        if (movementHandler.shouldJump) {
            movementHandler.shouldJump = false;
            float jumpHeight = MovementHandler.JUMP_HEIGHT * (movementHandler.isSprinting ? 1.13f : 1f);
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpHeight, rigidBody.velocity.z);
        }
    }

    private void calculateDirection() {
        direction = new Vector3(0, 0, axisInputHandler.getVerticalAxis());
        directionWorld = transform.TransformDirection(direction);
        if (directionWorld.magnitude > 1) {
            directionWorld = Vector3.Normalize(directionWorld);
        }
    }

    private void calculateSmoothedVelocity() {
        smoothedVelocity = directionWorld * MovementHandler.AIRBORNE_MOVEMENT_FORCE * 20;
        smoothedVelocity.y = 0;
    }

    private void applySmoothedVelocity() {
        rigidBody.AddForce(smoothedVelocity);
    }

    private void applyExtraGravity() {
        Vector3 extraGravityForce = (Physics.gravity * MovementHandler.EXTRA_GRAVITY) - Physics.gravity;
        rigidBody.AddForce(extraGravityForce);
    }
}
