using UnityEngine;

public class CameraShaker : MonoBehaviour {
    private static CameraShaker instance;

    private Thinksquirrel.CShake.CameraShake cameraShake;

    public static CameraShaker Instance {
        get {
            return instance;
        }
    }

    void Awake() {
        instance = this;
        cameraShake = GetComponent<Thinksquirrel.CShake.CameraShake>();
    }

    public void shake(int numberOfShakes, float speed, float decay) {
        Thinksquirrel.CShake.CameraShake.ShakeType type = Thinksquirrel.CShake.CameraShake.ShakeType.CameraMatrix;
        Vector3 shakeAmount = new Vector3(1, 1, 1);
        Vector3 rotationAmount = new Vector3(1, 1, 1);
        float distance = 0.05f;
        float uiShakeModifier = 1f;
        bool multiplyByTimeScale = true;
        cameraShake.Shake(type, numberOfShakes, shakeAmount, rotationAmount, distance, speed, decay, uiShakeModifier, multiplyByTimeScale);
    }
	
}
