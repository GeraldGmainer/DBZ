using UnityEngine;

public class FlyUpDownParticleHandler : MonoBehaviour {
    private const string FLY_UP_PARTICLE_NAME = "FlyUpTraceParticle";
    private const string FLY_DOWN_PARTICLE_NAME = "FlyDownTraceParticle";
    private static Vector3 FLY_UP_OFFSET = new Vector3(0, -0.5f, 0);
    private static Vector3 FLY_DOWN_OFFSET = new Vector3(0, 2f, 0);

    private AxisInputHandler axisInputHandler;

    void Awake() {
        axisInputHandler = GetComponent<AxisInputHandler>();
    }

    public void spawnFlyUpWindTrace() {
        if (axisInputHandler.getVerticalFlyAxis() != 1f) {
            return;
        }
        spawnFlyUpParticle();
    }

    public void spawnFlyDownWindTrace() {
        if (axisInputHandler.getVerticalFlyAxis() != -1f) {
            return;
        }
        spawnFlyDownParticle();
    }

    private void spawnFlyUpParticle() {
        Instantiate(Resources.Load(FLY_UP_PARTICLE_NAME), transform.position + FLY_UP_OFFSET, transform.rotation);
    }

    private void spawnFlyDownParticle() {
        Instantiate(Resources.Load(FLY_DOWN_PARTICLE_NAME), transform.position + FLY_DOWN_OFFSET, transform.rotation);
    }
}
