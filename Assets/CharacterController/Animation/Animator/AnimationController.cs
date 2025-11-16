using UnityEngine;

public class AnimationController : MonoBehaviour {
    private Animator animator;
    private Character character;
    private MovementAnimator movementAnimator;
    private KamehamehaAnimator kamehamehaAnimator;
    private HoldSwordAnimator holdSwordAnimator;

    void Awake() {
        animator = GetComponent<Animator>();
        character = GetComponent<CharController>().getCharacter();
        movementAnimator = GetComponent<MovementAnimator>();
        holdSwordAnimator = GetComponent<HoldSwordAnimator>();
        kamehamehaAnimator = GetComponent<KamehamehaAnimator>();
    }

    void Start() {
        disableRootMotion();
    }

    void Update() {
        if (animator == null || animator.runtimeAnimatorController == null) {
            return;
        }
        movementAnimator.update();

        if(character == Character.GOKU) {
            kamehamehaAnimator.update();
        }
        if (character == Character.TRUNKS) {
            holdSwordAnimator.update();
        }
    }

    public void disableRootMotion() {
        animator.applyRootMotion = false;
    }

    public void enableRootMotion() {
        animator.applyRootMotion = true;
    }
}







