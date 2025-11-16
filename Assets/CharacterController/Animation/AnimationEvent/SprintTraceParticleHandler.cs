using UnityEngine;

public class SprintTraceParticleHandler : MonoBehaviour {
    private const float DELAY_TIME = 0.5f;
    private const string SPRINT_PARTICLE_NAME = "SprintTraceParticle";
    private static Vector3 RIGHT_HAND_POSITION = new Vector3(0.4f, 1f, 0.5f);
    private static Vector3 LEFT_HAND_POSITION = new Vector3(-0.4f, 1f, 0.7f);
    private static Vector3 LEFT_FOOT_POSITION = new Vector3(0.3f, 0.2f, -0.2f);
    private static Vector3 RIGHT_FOOT_POSITION = new Vector3(-0.3f, 0.15f, -0.5f);

    private float delayCounter;
    private MovementHandler movementHandler;
    private AxisInputHandler axisInputHandler;

    void Awake() {
        movementHandler = GetComponent<MovementHandler>();
        axisInputHandler = GetComponent<AxisInputHandler>();
    }

    void Update() {
        delayCounter = movementHandler.isSprinting ? delayCounter + Time.deltaTime : 0;
    }

    public void spawnRightHandWindTrace() {
        spawnParticle(RIGHT_HAND_POSITION);
    }

    public void spawnLeftHandWindTrace() {
        spawnParticle(LEFT_HAND_POSITION);
    }

    public void spawnLeftFootWindTrace() {
        spawnParticle(LEFT_FOOT_POSITION);
    }

    public void spawnRightFootWindTrace() {
        spawnParticle(RIGHT_FOOT_POSITION);
    }

    private void spawnParticle(Vector3 pos) {
        if (axisInputHandler.getHorizontalAxis() != 0) {
            return;
        }
        if (!movementHandler.isSprinting) {
            return;
        }
        if (delayCounter < DELAY_TIME) {
            return;
        }
        if (Random.Range(0, 2) == 0) {
            return;
        }
        Instantiate(Resources.Load(SPRINT_PARTICLE_NAME), transform.position + transform.TransformVector(pos), transform.rotation);
    }
}
