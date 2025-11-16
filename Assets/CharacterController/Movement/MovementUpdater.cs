using UnityEngine;

public class MovementUpdater : Photon.MonoBehaviour {
    private MovementHandler movementHandler;
    private MoveSpeedDeterminer moveSpeedDeterminer;
    private FlyingMovementHandler flyingMovementHandler;
    private GroundMovementHandler groundMovementHandler;
    private AirborneMovementHandler airborneMovementHandler;
    private RotationMovementHandler rotationMovementHandler;
    private CameraFlyingMovementHandler cameraFlyingMovementHandler;

    void Awake() {
        movementHandler = GetComponent<MovementHandler>();
        moveSpeedDeterminer = GetComponent<MoveSpeedDeterminer>();
        flyingMovementHandler = GetComponent<FlyingMovementHandler>();
        groundMovementHandler = GetComponent<GroundMovementHandler>();
        airborneMovementHandler = GetComponent<AirborneMovementHandler>();
        rotationMovementHandler = GetComponent<RotationMovementHandler>();
        cameraFlyingMovementHandler = GetComponent<CameraFlyingMovementHandler>();
    }

    void FixedUpdate() {
        if (!photonView.isMine) {
            return;
        }
        if ((!movementHandler.isFlying && !movementHandler.isGrounded) || movementHandler.shouldJump) {
            airborneMovementHandler.update();
        }
        if (movementHandler.canRotate) {
            rotationMovementHandler.update();
        }
        if (!movementHandler.canMove || CellArenaMenuHandler.Instance.isMenuOpen) {
            return;
        }

        moveSpeedDeterminer.update();

        if (movementHandler.isGrounded) {
            groundMovementHandler.update();
        }
        if (movementHandler.isFlying) {
            if (true && Settings.Instance.cameraMode != CameraMode.THIRD_PERSON) {
                cameraFlyingMovementHandler.update();
            }
            else {
                flyingMovementHandler.update();
            }
        }
    }
}
