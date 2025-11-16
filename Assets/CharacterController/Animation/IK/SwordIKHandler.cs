using UnityEngine;

public class SwordIKHandler : MonoBehaviour {
    private Animator animator;
    private MovementHandler movementHandler;
    private DrawSwordHandler drawSwordHandler;

    private float weight;

    void Awake() {
        animator = GetComponent<Animator>();
        movementHandler = GetComponent<MovementHandler>();
        drawSwordHandler = GetComponent<DrawSwordHandler>();
    }

    void Update() {
        if (isRunningWithSword()) {
            weight = Mathf.Lerp(weight, 1.0f, Time.deltaTime * 10f);
        }
        else {
            weight = Mathf.Lerp(weight, 0.0f, Time.deltaTime * 10f);
        }
    }

    private bool isRunningWithSword() {
        if (!animator.GetCurrentAnimatorStateInfo(AnimatorLayers.baseLayer).IsName(AnimatorStates.GROUND_MOVEMENT)) {
            return false;
        }
        return movementHandler.isMoving() && !movementHandler.isSprinting && movementHandler.isGrounded && drawSwordHandler.isAttachedToGrip();
    }

    void OnAnimatorIK() {
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, weight);
        Quaternion rotation = animator.GetIKRotation(AvatarIKGoal.RightHand);
        rotation.eulerAngles += new Vector3(10f, 15f, -20f);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rotation);
    }
}
