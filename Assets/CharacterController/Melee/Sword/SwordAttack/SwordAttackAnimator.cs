using UnityEngine;
using System.Collections;

public class SwordAttackAnimator : MonoBehaviour {
    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    public void updateAnimator(int combo) {
        animator.SetInteger(AnimatorHashIDs.swordAttack, combo);
        StopCoroutine("resetAnimator");
        StartCoroutine("resetAnimator");
    }

    IEnumerator resetAnimator() {
        yield return new WaitForSeconds(0.4f);
        animator.SetInteger(AnimatorHashIDs.swordAttack, 0);
    }
}
