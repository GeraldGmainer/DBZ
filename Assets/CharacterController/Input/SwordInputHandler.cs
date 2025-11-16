using UnityEngine;
using System.Collections.Generic;

public class SwordInputHandler : MonoBehaviour {
    private List<string> validAnimatorStates = new List<string> { AnimatorStates.GROUND_MOVEMENT, AnimatorStates.FLY_MOVEMENT, AnimatorStates.SWORD_ATTACK1, AnimatorStates.SWORD_ATTACK2, AnimatorStates.SWORD_ATTACK3,
    AnimatorStates.SWORD_ATTACK1END, AnimatorStates.SWORD_ATTACK2END, AnimatorStates.SWORD_ATTACK3END};

    private Animator animator;
    private MovementHandler movementHandler;
    private DrawSwordHandler drawSwordHandler;
    private SwordAttackHandler swordAttack;

    void Awake() {
        animator = GetComponent<Animator>();
        movementHandler = GetComponent<MovementHandler>();
        drawSwordHandler = GetComponent<DrawSwordHandler>();
        swordAttack = GetComponent<SwordAttackHandler>();
    }

    public void update() {
        if (GetComponent<DrawSwordHandler>().isDrawing) {
            return;
        }
        handleSwordAttachment();
        handleSwordAttack();
    }

    private void handleSwordAttachment() {
        if (Input.GetButtonDown("Action2")) {
            drawSwordHandler.updateAttachment();
        }
    }

    private void handleSwordAttack() {
        if (Input.GetButtonDown("Action1") && drawSwordHandler.isAttachedToGrip() && isValidMovement()) {
            swordAttack.increase();
        }
    }

    private bool isValidMovement() {
        return !movementHandler.isSprinting && isValidAnimatorState();
    }

    private bool isValidAnimatorState() {
        foreach (string state in validAnimatorStates) {
            if (animator.GetCurrentAnimatorStateInfo(AnimatorLayers.baseLayer).IsName(state)) {
                return true;
            }
        }
        return false;
    }
}
