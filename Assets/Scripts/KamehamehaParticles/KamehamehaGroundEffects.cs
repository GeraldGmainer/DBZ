using UnityEngine;
using System.Collections;

public class KamehamehaGroundEffects : MonoBehaviour {
    private static string LIGHTNING_NAME = "lightning";

    private Vector3 offset = new Vector3(0, 0.01f, 0);

    private ParticleSystem lightningParticle;
    private float lightningCount;
    private float lightningIncreaseSpeed = 2f;
    private float maxLightnings = 40f;

    void Awake() {
        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>()) {
            if (ps.name.Contains(LIGHTNING_NAME)) {
                lightningParticle = ps;
            }
        }
        if (lightningParticle == null) {
            Debug.LogError("GroundEffects: particle not found");
            enabled = false;
        }
    }

    void Start() {
        transform.position += offset;
        lightningCount = lightningParticle.emissionRate;
    }

    void Update() {
        increaseLightningCount();
    }

    private void increaseLightningCount() {
        lightningCount += Time.deltaTime * lightningIncreaseSpeed;
        lightningCount = Mathf.Clamp(lightningCount, 0, maxLightnings);
        lightningParticle.emissionRate = lightningCount;
    }

    public void destroy() {
        Destroy(gameObject);
    }

    public void destroy(float delay) {
        Destroy(gameObject, delay);
    }

}
