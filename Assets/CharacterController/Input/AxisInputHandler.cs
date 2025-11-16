using UnityEngine;

public class AxisInputHandler : MonoBehaviour {
    private MovementHandler movementHandler;

    private float verticalAxis;
    private float horizontalAxis;
    private float strafeAxis;
    private float fire2Axis;
    private float verticalFlyAxis;

    void Awake() {
        movementHandler = GetComponent<MovementHandler>();
    }

    public void update() {
        updateVerticalAxis();
        updateHorizontalAxis();
        updateHorizontalFire2Axis();
        updateStrafeAxis();
        updateMovingBackwards();
        updateVerticalFlyAxis();
    }

    private void updateVerticalAxis() {
        verticalAxis = Input.GetAxisRaw("Vertical");
        if (Input.GetButton("Fire1") && true) {
            verticalAxis = 1.0f;
        }
    }

    private void updateHorizontalAxis() {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
    }

    private void updateHorizontalFire2Axis() {
        fire2Axis = 0;
        if (true) {
            fire2Axis = Input.GetAxis("MouseX");
        }
    }

    private void updateStrafeAxis() {
        strafeAxis = Input.GetAxisRaw("HorizontalStrafe");
        if (true && horizontalAxis != 0) {
            strafeAxis = horizontalAxis;
            horizontalAxis = 0f;
        }
        if (verticalAxis != 0) {
            strafeAxis = 0;
        }
    }

    private void updateMovingBackwards() {
        movementHandler.isMovingBackwards = verticalAxis < 0;
    }

    private void updateVerticalFlyAxis() {
        resetVerticalFlyAxis();
        if (Input.GetButton("Jump") && !Input.GetButton("FlyDown")) {
            verticalFlyAxis = 1;
        }
        if (Input.GetButton("FlyDown") && !Input.GetButton("Jump")) {
            verticalFlyAxis = -1;
        }
    }

    public bool isMovementAxisPressed() {
        Vector3 direction = new Vector3(strafeAxis, 0, verticalAxis);
        return direction.z != 0 || direction.x != 0;
    }

    public void resetVerticalFlyAxis() {
        verticalFlyAxis = 0;
    }


    /************/

    public float getHorizontalAxis() {
        return horizontalAxis;
    }

    public float getFire2Axis() {
        return fire2Axis;
    }

    public float getStrafeAxis() {
        return strafeAxis;
    }

    public float getVerticalAxis() {
        return verticalAxis;
    }

    public float getVerticalFlyAxis() {
        return verticalFlyAxis;
    }
}
