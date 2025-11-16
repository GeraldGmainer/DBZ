using UnityEngine;

public abstract class EyeBlinkBlendShape : MonoBehaviour {
    private static int BLENDSHAPE_INDEX = 0;

    [SerializeField]
    private float blinkSpeed = 0.2f;
    [SerializeField]
    private float blinkBreak = 2f;
    [SerializeField]
    private AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public abstract string getMeshName();

    private float blendValue = 0f;
    private float breakTimer = 0f;
    private float direction = 1;
    private SkinnedMeshRenderer skinnedMeshRenderer;

    void Start() {
        if (isPlayerDead()) {
            enabled = false;
            return;
        }
        GameObject mesh = getChildGameObject(gameObject, getMeshName());

        if (mesh == null) {
            Debug.LogError("EyeBlinkHandler: mesh not found");
            enabled = false;
            return;
        }
        skinnedMeshRenderer = mesh.GetComponent<SkinnedMeshRenderer>();

        foreach (Keyframe key in curve.keys) {
            if (key.time > 1 || key.value > 1) {
                Debug.LogWarning("EyeBlinkHandler: curve key time and weight values should be between 0 to 1");
            }
        }
    }

    private bool isPlayerDead() {
        return GameObjectFinder.inChildByName(transform, GetComponent<CharController>().getGeoGroupName()) == null;
    }

    void FixedUpdate() {
        if (isInBreak()) {
            increaseBreak();
        }
        else {
            blinkEyes();
        }
    }

    private bool isInBreak() {
        return breakTimer <= blinkBreak;
    }

    private void increaseBreak() {
        breakTimer += Time.fixedDeltaTime;
    }

    private void blinkEyes() {
        if (isBlinkHalfFinished()) {
            direction = -1;
        }
        if (isBlinkFinished()) {
            restartBreak();
            return;
        }

        blendValue += getSecondsConsideredBlickSpeed() * direction;
        updateEyes();
    }

    private bool isBlinkHalfFinished() {
        return blendValue >= 100;
    }

    private bool isBlinkFinished() {
        return blendValue <= 0 && direction == -1;
    }

    private void restartBreak() {
        breakTimer = 0;
        blendValue = 0;
        direction = 1;
        updateEyes();
    }

    private void updateEyes() {
        skinnedMeshRenderer.SetBlendShapeWeight(BLENDSHAPE_INDEX, curve.Evaluate(blendValue / 100) * 100);
    }

    private float getSecondsConsideredBlickSpeed() {
        //multiply by 2 cuz blickSpeed is for up + down movement
        return 1 / blinkSpeed * 2 * 2;
    }

    private GameObject getChildGameObject(GameObject fromGameObject, string withName) {
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) if (t.gameObject.name.Contains(withName)) return t.gameObject;
        return null;
    }
}
