using UnityEngine;

public class MainMenuRandomAnimatorController : MonoBehaviour {

    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        animator.applyRootMotion = false;
        animator.SetInteger("randomAnimation", Random.Range(0, 2));
    }
}
