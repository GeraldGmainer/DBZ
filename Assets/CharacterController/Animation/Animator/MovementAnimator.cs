using UnityEngine;

public class MovementAnimator : MonoBehaviour {
    private const float DAMP_TIME = 1f;
    private const float DELTA_TIME_MUL = 100f;

    private Animator animator;
    private MovementHandler movementHandler;
    private AxisInputHandler axisInputHandler;
    private RotationMovementHandler rotationMovementHandler;

    void Awake() {
        animator = GetComponent<Animator>();
        movementHandler = GetComponent<MovementHandler>();
        axisInputHandler = GetComponent<AxisInputHandler>();
        rotationMovementHandler = GetComponent<RotationMovementHandler>();
    }

    public void update() {
        updateBools();
        updateFloats();
    }

    public void setJump(bool value) {
        animator.SetBool(AnimatorHashIDs.jump, value);
    }

    private void updateBools() {
        animator.SetBool(AnimatorHashIDs.isFlying, movementHandler.isFlying);
        animator.SetBool(AnimatorHashIDs.inAir, !movementHandler.isGrounded);
        animator.SetBool(AnimatorHashIDs.isMoving, movementHandler.isMoving());
    }

    private void updateFloats() {
        float deltaTime = DELTA_TIME_MUL * Time.deltaTime;
        float horzizontalDampTime = DAMP_TIME;
        if (movementHandler.isFlying && axisInputHandler.getVerticalFlyAxis() == 0) {
            horzizontalDampTime = 15;
        }

        animator.SetFloat(AnimatorHashIDs.direction, rotationMovementHandler.animatorDirection, 15, deltaTime);
        animator.SetFloat(AnimatorHashIDs.verticalSpeed, movementHandler.getCurrentVerticalSpeed(), DAMP_TIME, deltaTime);
        animator.SetFloat(AnimatorHashIDs.strafe, axisInputHandler.getStrafeAxis(), DAMP_TIME, deltaTime);
        animator.SetFloat(AnimatorHashIDs.flyUpDown, axisInputHandler.getVerticalFlyAxis(), DAMP_TIME, deltaTime);
        animator.SetFloat(AnimatorHashIDs.horizontalSpeed, movementHandler.getCurrentHorzizontalSpeed(), horzizontalDampTime, deltaTime);
    }
}
