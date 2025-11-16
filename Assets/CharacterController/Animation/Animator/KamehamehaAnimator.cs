using UnityEngine;

public class KamehamehaAnimator : MonoBehaviour {
    private Animator animator;
    private KamehamehaHandler kamehamehaHandler;

    void Awake() {
        kamehamehaHandler = GetComponent<KamehamehaHandler>();
        animator = GetComponent<Animator>();
    }

    public void update() {
        animator.SetBool(AnimatorHashIDs.castingKamehameha, kamehamehaHandler.isCastingKamehameha);
        animator.SetBool(AnimatorHashIDs.shootingKamehameha, kamehamehaHandler.isShootingKamehameha);
    }
}