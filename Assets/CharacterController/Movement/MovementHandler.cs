using UnityEngine;

public class MovementHandler : Photon.MonoBehaviour {
    public const float RUN_SPEED = 10.0f;
    public const float STRAFE_MULTIPLIER = 0.8f;
    public const float AIRBORNE_MOVEMENT_FORCE = 12f;
    public const float SPRINT_MULTIPLIER = 2.0f;
    public const float BACKWARD_MULTIPLIER = 0.2f;
    public const float HORIZONTAL_ROTATING_SPEED = 1.5f;
    public const float MOVE_ACCERLATION = 1f;
    public const float JUMP_HEIGHT = 9.0f;
    public const float EXTRA_GRAVITY = 50f;

    private Rigidbody rigidBody;

    public bool isSprinting { get; set; }
    public bool isFlying { get; set; }
    public bool isMovingBackwards { get; set; }
    public bool shouldJump { get; set; }
    public bool isGrounded { get; set; }
    public bool canMove { get; set; }
    public bool canRotate { get; set; }

    void Awake() {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Start() {
        if (!photonView.isMine) {
            return;
        }
        canMove = true;
        canRotate = true;
        isGrounded = true;
    }

    void Update() {
        if (!photonView.isMine) {
            return;
        }
        stopMovementOnMenuOpen();
        stopFlyingWhenGrounded();
    }

    private void stopMovementOnMenuOpen() {
        if (CellArenaMenuHandler.Instance.isMenuOpen && (isFlying || isGrounded)) {
            rigidBody.velocity = Vector3.zero;
        }
    }

    private void stopFlyingWhenGrounded() {
        if (isGrounded) {
            stopFly();
        }
    }

    public void fly() {
        rigidBody.useGravity = false;
        isFlying = true;
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
    }

    public void stopFly() {
        rigidBody.useGravity = true;
        isFlying = false;
    }

    public bool isMoving() {
        return Mathf.Abs(getCurrentHorzizontalSpeed()) > 0.5f;
    }

    public float getCurrentSpeed() {
        if (isMovingBackwards) {
            return rigidBody.velocity.magnitude * -1;
        }
        return rigidBody.velocity.magnitude;
    }

    public float getCurrentHorzizontalSpeed() {
        if (isMovingBackwards) {
            return new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z).magnitude * -1;
        }
        return new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z).magnitude;
    }

    public float getCurrentVerticalSpeed() {
        return rigidBody.velocity.y;
    }
}
