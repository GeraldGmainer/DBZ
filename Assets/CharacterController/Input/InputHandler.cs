using UnityEngine;

public class InputHandler : MonoBehaviour {
    private Character character;

    private ActionInputHandler actionInputHandler;
    private AxisInputHandler axisInputHandler;
    private KamehamehaInputHandler kamehamehaInputHandler;
    private MouseInputHandler mouseInputHandler;
    private PunchInputHandler punchInputHandler;
    private SwordInputHandler swordInputHandler;
    private CameraMouseClickInputHandler cameraMouseClickInputHandler;

    private MovementHandler movementHandler;
    private CharacterCameraHandler characterCameraHandler;
    private DeathHandler deathHandler;

    void Start() {
        character = GetComponent<CharController>().getCharacter();

        actionInputHandler = GetComponent<ActionInputHandler>();
        axisInputHandler = GetComponent<AxisInputHandler>();
        kamehamehaInputHandler = GetComponent<KamehamehaInputHandler>();
        mouseInputHandler = GetComponent<MouseInputHandler>();
        punchInputHandler = GetComponent<PunchInputHandler>();
        swordInputHandler = GetComponent<SwordInputHandler>();
        cameraMouseClickInputHandler = GetComponent<CameraMouseClickInputHandler>();

        movementHandler = GetComponent<MovementHandler>();
        characterCameraHandler = GetComponent<CharacterCameraHandler>();
    }

    void Update() {
        if (CellArenaMenuHandler.Instance.isMenuOpen) {
            axisInputHandler.resetVerticalFlyAxis();
            return;
        }

        mouseInputHandler.update();
        axisInputHandler.update();

        if (movementHandler.canMove) {
            actionInputHandler.update();
        }

        punchInputHandler.update();

        if (character == Character.TRUNKS) {
            swordInputHandler.update();
        }

        if (movementHandler.canMove && character == Character.GOKU) {
            kamehamehaInputHandler.update();
        }

        if (characterCameraHandler.canMoveCamera) {
            cameraMouseClickInputHandler.update();
        }
    }
}
