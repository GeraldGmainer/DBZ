using UnityEngine;

public class HoldSwordAnimator : MonoBehaviour {
    private Animator animator;
    private DrawSwordHandler drawSwordHandler;

    void Awake() {
        animator = GetComponent<Animator>();
        drawSwordHandler = GetComponent<DrawSwordHandler>();
    }

    public void update() {
        if (drawSwordHandler.isAttachedToSheath()) {
            animator.SetLayerWeight(AnimatorLayers.holdSwordLayer, 0);
        }
        else {
            animator.SetLayerWeight(AnimatorLayers.holdSwordLayer, 1);
        }
    }
}
