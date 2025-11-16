using UnityEngine;

public class CameraPivotPositionDeterminer : MonoBehaviour {
    private Vector3 cameraPivotPosition;
    private DeathHandler deathHandler;
    private RagdollReplacer ragdollReplacer;

    void Awake() {
        deathHandler = GetComponent<DeathHandler>();
        ragdollReplacer = GetComponent<RagdollReplacer>();
    }

    public void setCameraPivotInWorldSpace() {
        if (deathHandler.isDead) {
            cameraPivotPosition = ragdollReplacer.getSpine1().position;
        }
        else {
            cameraPivotPosition = transform.position + transform.TransformVector(CharacterCameraHandler.CAMERA_PIVIOT_LOCATION);
        }
    }

    public Vector3 getCameraPivotPosition() {
        return cameraPivotPosition;
    }
}
