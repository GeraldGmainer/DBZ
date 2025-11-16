using UnityEngine;

public class FlyingMovementHandler : MonoBehaviour {
    private Rigidbody rigidBody;
    private AxisInputHandler axisInputHandler;
    private MoveSpeedDeterminer moveSpeedDeterminer;

    private Vector3 direction;
    private Vector3 directionWorld;
    private Vector3 smoothedVelocity;
    private Vector3 velocityRef = Vector3.zero;

    void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        axisInputHandler = GetComponent<AxisInputHandler>();
        moveSpeedDeterminer = GetComponent<MoveSpeedDeterminer>();
    }

    public void update() {
        calculateDirection();
        calculateSmoothedVelocity();
        applySmoothedVelocity();
    }

    private void calculateDirection() {
        direction = new Vector3(axisInputHandler.getStrafeAxis(), 0, axisInputHandler.getVerticalAxis());
        directionWorld = transform.TransformDirection(direction);
        if (directionWorld.magnitude > 1) {
            directionWorld = Vector3.Normalize(directionWorld);
        }
    }

    private void calculateSmoothedVelocity() {
        directionWorld *= moveSpeedDeterminer.getHorizontalSpeed();
        directionWorld.y = moveSpeedDeterminer.getVerticalSpeed();
        clampVelocity();

        Vector3 targetVelocity = directionWorld;
        float smoothTime = Time.fixedDeltaTime * MovementHandler.MOVE_ACCERLATION;
        smoothTime = Time.fixedDeltaTime * MovementHandler.MOVE_ACCERLATION * (Mathf.Abs(targetVelocity.magnitude - rigidBody.velocity.magnitude));
        smoothedVelocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocityRef, smoothTime);
    }

    private void asdf() {
        Debug.Log(smoothedVelocity);
    }

    private void clampVelocity() {
        directionWorld = Vector3.ClampMagnitude(directionWorld, getMaxVelocity());
    }

    private float getMaxVelocity() {
        if (direction.x == 0 && direction.z == 0) {
            return moveSpeedDeterminer.getVerticalSpeed();
        }
        return moveSpeedDeterminer.getHorizontalSpeed();
    }

    private void applySmoothedVelocity() {
        if (float.IsNaN(directionWorld.x)) {
            Debug.LogError("FlyingMovementHandler: x not a number error");
            return;
        }
        if (float.IsNaN(directionWorld.y)) {
            Debug.LogError("FlyingMovementHandler: y not a number error");
            return;
        }
        if (float.IsNaN(directionWorld.z)) {
            Debug.LogError("FlyingMovementHandler: z not a number error");
            return;
        }

        rigidBody.velocity = directionWorld;
    }
}
