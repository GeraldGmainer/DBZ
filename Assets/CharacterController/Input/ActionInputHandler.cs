using UnityEngine;
using System.Collections;

public class ActionInputHandler : MonoBehaviour {
    private Animator animator;
    private MovementHandler movementHandler;
    private MovementAnimator movementAnimator;

    void Awake() {
        animator = GetComponent<Animator>();
        movementHandler = GetComponent<MovementHandler>();
        movementAnimator = GetComponent<MovementAnimator>();
    }

    public void update() {
        handleSprinting();
        handleJumping();
        handleFlying();
        handleCancelFlying();
    }

    private void handleSprinting() {
        movementHandler.isSprinting = Input.GetButton("Sprint");
    }

    private void handleJumping() {
        if (movementHandler.isGrounded && Input.GetButtonDown("Jump") && !isJumpIdleEndState()) {
            movementHandler.shouldJump = true;
            movementAnimator.setJump(true);
            StartCoroutine("resetJump");
        }
    }

    private bool isJumpIdleEndState() {
        return animator.GetCurrentAnimatorStateInfo(AnimatorLayers.baseLayer).IsName(AnimatorStates.JUMP_IDLE_END);
    }

    IEnumerator resetJump() {
        yield return new WaitForSeconds(0.3f);
        movementAnimator.setJump(false);
    }

    private void handleFlying() {
        if (!movementHandler.isGrounded && Input.GetButtonDown("Jump")) {
            movementHandler.fly();
        }
    }

    private void handleCancelFlying() {
        if (Input.GetButtonDown("CancelFlying")) {
            movementHandler.stopFly();
        }
    }
}
