using UnityEngine;

public class CharacterCameraHandler : MonoBehaviour {
    public static Vector3 CAMERA_PIVIOT_LOCATION = new Vector3(0, 1.5f, 0);
    public const float MOUSE_SCROLL_SENSITIVITY = 8.0f;
    public const float MIN_SCROLL_DISTANCE = 0.75f;
    public const float MAX_SCROLL_DISTANCE = 40f;
    public const float SCROLL_SMOOTH_TIME = 0.15f;
    public const float MOUSE_SMOOTH_TIME = 0.08f;
    public const float ALIGN_CAMERA_WHEN_MOVING_SMOOTH_TIME = 0.2f;
    public const float START_MOUSE_X_POSITION = 0;
    public const float START_MOUSE_Y_POSITION = 15.0f;
    public const float START_SCROLL = 2.0f;
    public const float MOUSE_X_MIN = -90.0f;
    public const float MOUSE_X_MAX = 90.0f;
    public const float MOUSE_Y_MIN = -89.5f;
    public const float MOUSE_Y_MAX = 89.5f;

    private CameraDeterminer cameraDeterminer;
    private AxisInputHandler axisInputHandler;
    private CharacterFadeHandler characterFadeHandler;
    private CameraPositionDeterminer cameraPositionDeterminer;
    private MouseYConstrainDeterminer mouseYConstrainDeterminer;
    private CameraMouseClickInputHandler cameraMouseClickInputHandler;
    private CameraPivotPositionDeterminer cameraPivotPositionDeterminer;

    public bool canMoveCamera { get; set; }
    public bool alignCameraWhenMoving { get; private set; }
    public bool isAlreadyAlignedWithCamera { get; private set; }

    void Awake() {
        cameraDeterminer = GetComponent<CameraDeterminer>();
        axisInputHandler = GetComponent<AxisInputHandler>();
        characterFadeHandler = GetComponent<CharacterFadeHandler>();
        cameraPositionDeterminer = GetComponent<CameraPositionDeterminer>();
        mouseYConstrainDeterminer = GetComponent<MouseYConstrainDeterminer>();
        cameraMouseClickInputHandler = GetComponent<CameraMouseClickInputHandler>();
        cameraPivotPositionDeterminer = GetComponent<CameraPivotPositionDeterminer>();
    }

    void Start() {
        resetCamera();
        canMoveCamera = true;
    }

    void LateUpdate() {
        updateAlignCameraWhenMoving();
        updateAlignCameraWithCharacter();
        cameraPivotPositionDeterminer.setCameraPivotInWorldSpace();
        mouseYConstrainDeterminer.calculateMouseYConstrainDeterminer();
        updateCameraPosition();
        characterFadeHandler.handleCharacterFading();
        alignCameraViewAngle();
        mouseYConstrainDeterminer.handleCameraLyingOnGround();
    }

    private void updateAlignCameraWhenMoving() {
        if (Settings.Instance.cameraMode == CameraMode.THIRD_PERSON) {
            alignCameraWhenMoving = false;
        }
        else {
            alignCameraWhenMoving = true;
        }
    }

    private void updateAlignCameraWithCharacter() {
        if (canMoveCamera) {
            isAlreadyAlignedWithCamera = axisInputHandler.isMovementAxisPressed() && !Input.GetButton("Fire1") && !true;
        }
        else {
            isAlreadyAlignedWithCamera = axisInputHandler.isMovementAxisPressed();
        }
    }

    private void updateCameraPosition() {
        cameraDeterminer.getCamera().transform.position = cameraPositionDeterminer.getCameraPostion();
    }

    private void alignCameraViewAngle() {
        if (cameraPositionDeterminer.isInThirdPerson()) {
            cameraDeterminer.getCamera().transform.LookAt(cameraPivotPositionDeterminer.getCameraPivotPosition());
        }
        else {
            cameraDeterminer.getCamera().transform.eulerAngles = cameraPositionDeterminer.getSmoothedMouseXY();
            cameraDeterminer.getCamera().transform.Rotate(Vector3.up);
        }
    }

    private void resetCamera() {
        cameraPositionDeterminer.reset();
        cameraMouseClickInputHandler.reset();
    }

    void OnDrawGizmos() {
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawSphere(character.transform.position + character.transform.TransformVector(characterCamera.cameraPivotLocalPosition), 0.1f);
    }
}
