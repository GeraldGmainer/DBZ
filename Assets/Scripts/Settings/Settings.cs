using UnityEngine;

public class Settings : MonoBehaviour {
    private static Settings instance;

    public static Settings Instance {
        get {
            return instance;
        }
    }

    void Awake() {
        instance = this;
    }

    public CameraMode cameraMode = CameraMode.THIRD_PERSON;
    public KillingSpreeType killingSpreeType = KillingSpreeType.NORMAL;

    public float mouseSensitivity = 8.0f;
    public bool invertMouseX = false;
    public bool invertMouseY = false;

    public bool isMusicOn = true;
    public bool isEffectsOn = true;
    public bool isVocalsOn = true;
    public bool isKillingSpreeOn = true;

    public bool wearJacket = false;
}
