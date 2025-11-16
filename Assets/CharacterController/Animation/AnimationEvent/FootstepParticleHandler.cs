using UnityEngine;

public class FootstepParticleHandler : MonoBehaviour {
    private const string FOOTSTEP_PARTICLE_NAME = "FootstepParticle";
    private static Vector3 LEFT_FOOT_POSITION = new Vector3(-0.15f, 0f, 0);
    private static Vector3 RIGHT_FOOT_POSITION = new Vector3(0.15f, 0f, 0);

    private TerrainDeterminer terrainDeterminer;
    private MovementHandler movementHandler;

    void Awake() {
        terrainDeterminer = GetComponent<TerrainDeterminer>();
        movementHandler = GetComponent<MovementHandler>();
    }

    public void spawnLeftFootstep(int runAnimation) {
        if (isValidMovement(runAnimation)) {
            spawnParticle(LEFT_FOOT_POSITION);
        }
    }

    public void spawnRightFootstep(int sprintAnimation) {
        if (isValidMovement(sprintAnimation)) {
            spawnParticle(RIGHT_FOOT_POSITION);
        }
    }

    private void spawnParticle(Vector3 offset) {
        Instantiate(Resources.Load(FOOTSTEP_PARTICLE_NAME), transform.position + transform.TransformVector(offset), transform.rotation);
    }

    private bool isValidMovement(int sprintAnimation) {
        if (isFromRunAnimation(sprintAnimation)) {
            return !movementHandler.isSprinting && terrainDeterminer.isTerrain() && movementHandler.isGrounded;
        }
        else if (isFromSprintAnimation(sprintAnimation)) {
            return movementHandler.isSprinting && terrainDeterminer.isTerrain() && movementHandler.isGrounded;
        }
        else {
            return false;
        }
    }

    private bool isFromRunAnimation(int sprintAnimation) {
        return sprintAnimation == 0;
    }

    private bool isFromSprintAnimation(int sprintAnimation) {
        return sprintAnimation == 1;
    }
}
