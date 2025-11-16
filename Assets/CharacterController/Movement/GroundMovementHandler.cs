using UnityEngine;

public class GroundMovementHandler : MonoBehaviour {
    private Rigidbody rigidBody;
    private AxisInputHandler axisInputHandler;
    private MoveSpeedDeterminer moveSpeedDeterminer;

    private Vector3 direction;
    private Vector3 directionWorld;
    private Vector3 smoothedVelocity;
    //private Vector3 velocityRef = Vector3.zero;
    private float maxVelocityChange = 2f;

    void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        axisInputHandler = GetComponent<AxisInputHandler>();
        moveSpeedDeterminer = GetComponent<MoveSpeedDeterminer>();
    }

    public void update() {
        calculateDirection();
        //calculateSmoothedVelocity();
        //applySmoothedVelocity();

        Vector3 velocityChange = CalculateVelocityChange(directionWorld);
        if (float.IsNaN(velocityChange.x)) {
            Debug.LogError("GroundMovementHandler: x not a number error");
            return;
        }
        if (float.IsNaN(velocityChange.z)) {
            Debug.LogError("GroundMovementHandler: z not a number error");
            return;
        }
        rigidBody.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private Vector3 CalculateVelocityChange(Vector3 inputVector) {
        Vector3 relativeVelocity = inputVector * moveSpeedDeterminer.getHorizontalSpeed();
        Vector3 velocityChange = relativeVelocity - rigidBody.velocity;
        float maxChange = maxVelocityChange;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxChange, maxChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxChange, maxChange);
        velocityChange.y = 0;
        return velocityChange;
    }

    private void calculateDirection() {
        direction = new Vector3(axisInputHandler.getStrafeAxis(), 0, axisInputHandler.getVerticalAxis());
        directionWorld = transform.TransformDirection(direction);
        if (directionWorld.magnitude > 1) {
            directionWorld = Vector3.Normalize(directionWorld);
        }
    }

    private void calculateSmoothedVelocity() {
        /* Vector3 targetVelocity = directionWorld * getMoveSpeedMultiplier();
        float smoothTime = Time.deltaTime * characterMovement.getMoveAccerlation() ;
        smoothedVelocity = Vector3.Lerp(rigidBody.velocity, targetVelocity, smoothTime);
        smoothedVelocity.y = rigidBody.velocity.y; */
        /*
        Vector3 targetVelocity = directionWorld * moveSpeedDeterminer.getHorizontalSpeed();
        float smoothTime = Time.deltaTime * characterMovement.getMoveAccerlation() ;
        smoothTime = Time.deltaTime * characterMovement.getMoveAccerlation() * (Mathf.Abs(targetVelocity.magnitude - rigidBody.velocity.magnitude ));
        smoothedVelocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocityRef, smoothTime);
        */
        smoothedVelocity = directionWorld * moveSpeedDeterminer.getHorizontalSpeed();
        smoothedVelocity.y = rigidBody.velocity.y;
    }

    private void applySmoothedVelocity() {
        rigidBody.velocity = smoothedVelocity;
    }

}
