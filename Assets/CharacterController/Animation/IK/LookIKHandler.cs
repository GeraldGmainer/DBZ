using UnityEngine;

public class LookIKHandler : MonoBehaviour {
    private const float SMOOTH_VALUE = 10f;
    private const float MAX_IK_BLEND = 0.5f;

    private Animator animator;
    private MovementHandler movementHandler;
    private CameraDeterminer cameraDeterminer;
    private RotationMovementHandler rotationMovementHandler;

    private float IKweight = 0f;
    private Vector3 lookPosition;

    void Awake() {
        animator = GetComponent<Animator>();
        movementHandler = GetComponent<MovementHandler>();
        cameraDeterminer = GetComponent<CameraDeterminer>();
        rotationMovementHandler = GetComponent<RotationMovementHandler>();
    }

    void Update() {
        if (!isValidLookIK()) {
            IKweight = Mathf.SmoothStep(IKweight, 0, Time.deltaTime * SMOOTH_VALUE);
            return;
        }
        IKweight = Mathf.SmoothStep(IKweight, MAX_IK_BLEND, Time.deltaTime * SMOOTH_VALUE);
        determineLookPosition();
    }

    private bool isValidLookIK() {
        if (rotationMovementHandler.rotation != 0) {
            return false;
        }
        if (!animator.GetCurrentAnimatorStateInfo(AnimatorLayers.baseLayer).IsName(AnimatorStates.GROUND_MOVEMENT)) {
            return false;
        }
        return Mathf.Abs(movementHandler.getCurrentSpeed()) < 0.5f;
    }

    private void determineLookPosition() {
        Ray ray = new Ray(cameraDeterminer.getCamera().transform.position, cameraDeterminer.getCamera().transform.forward * 50);
        lookPosition = ray.GetPoint(50);

        //Debug.DrawRay(cam.transform.position, lookPosition, Color.blue);
    }

    void OnAnimatorIK() {
        animator.SetLookAtWeight(IKweight, IKweight, IKweight, IKweight, 1f);
        animator.SetLookAtPosition(lookPosition);
    }
}
