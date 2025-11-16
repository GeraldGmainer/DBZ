using UnityEngine;
using System.Collections;

public class PunchAnimator : MonoBehaviour {
    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    public void updateAnimator(int combo) {
        animator.SetInteger(AnimatorHashIDs.punch, combo);
        StopCoroutine("resetAnimator");
        StartCoroutine("resetAnimator");
    }

    IEnumerator resetAnimator() {
        yield return new WaitForSeconds(0.1f);
        animator.SetInteger(AnimatorHashIDs.punch, 0);
    }
}
