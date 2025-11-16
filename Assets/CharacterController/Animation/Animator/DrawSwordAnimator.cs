using UnityEngine;

public class DrawSwordAnimator : MonoBehaviour {
    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        animator.SetLayerWeight(AnimatorLayers.drawSwordLayer, 1);
    }

    public void update(int value) {
        animator.SetInteger(AnimatorHashIDs.drawSword, value);
    }
}
