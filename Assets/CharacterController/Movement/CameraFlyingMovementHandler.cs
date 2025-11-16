using UnityEngine;

public class CameraFlyingMovementHandler : MonoBehaviour {
    private MovementHandler movementHandler;
    private MoveSpeedDeterminer moveSpeedDeterminer;

    private Camera cam;
    private Rigidbody rigidBody;
    private Vector3 lookPosition;
    private Vector3 moveDirectionWorld;

    void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        movementHandler = GetComponent<MovementHandler>();
        moveSpeedDeterminer = GetComponent<MoveSpeedDeterminer>();
        cam = GetComponent<CameraDeterminer>().getCamera();
    }

    public void update() {
        determineLookPosition();
        determineDirection();
        applyMoveSpeed();
        clampVelocity();

        rigidBody.velocity = moveDirectionWorld;
    }

    private void determineLookPosition() {
        Ray ray;
        if (movementHandler.isMovingBackwards) {
            ray = new Ray(cam.transform.position, cam.transform.forward * -1 * 100);
        }
        else {
            ray = new Ray(cam.transform.position, cam.transform.forward * 100);
        }
        lookPosition = ray.GetPoint(100);
    }

    private void determineDirection() {
        moveDirectionWorld = (lookPosition - transform.position).normalized;
    }

    private void applyMoveSpeed() {
        moveDirectionWorld *= moveSpeedDeterminer.getHorizontalSpeed();
        moveDirectionWorld.y += moveSpeedDeterminer.getVerticalSpeed();
    }

    private void clampVelocity() {
        moveDirectionWorld = Vector3.ClampMagnitude(moveDirectionWorld, getMaxVelocity());
    }

    private float getMaxVelocity() {
        if (moveDirectionWorld.x == 0 && moveDirectionWorld.z == 0) {
            return moveSpeedDeterminer.getVerticalSpeed();
        }
        return moveSpeedDeterminer.getHorizontalSpeed();
    }
}
