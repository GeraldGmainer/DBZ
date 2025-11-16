using UnityEngine;
using System.Collections;

public class KamehamehaExplosionLightHandler : MonoBehaviour {
    private static string LIGHT_NAME = "pointLight";
    private static string HALO_NAME = "halo";

    private KamehamehaExplosion kamehamehaExplosion;
    private Light pointLight;
    private Light halo;
    private float startTime;
    private float startPointLightIntensity;
    private float startHaloIntensity;

    void Awake() {
        kamehamehaExplosion = GetComponent<KamehamehaExplosion>();
        startTime = Time.time;

        foreach (Light l in GetComponentsInChildren<Light>()) {
            if (LIGHT_NAME.Equals(l.name)) {
                pointLight = l;
                startPointLightIntensity = pointLight.intensity;
            }
            else if (HALO_NAME.Equals(l.name)) {
                halo = l;
                startHaloIntensity = halo.intensity;
            }
        }
    }

    public void updateLights(float size) {
        foreach (Light l in GetComponentsInChildren<Light>()) {
            if (l.name.Equals(HALO_NAME)) {
                l.range *= size;
            }
            else if (l.name.Equals(LIGHT_NAME)) {
                l.range = l.range + l.range * 0.2f * size;
            }
        }
    }

    void FixedUpdate() {
        if (isAtHalfLifeTime()) {
            decreasePointLightIntensity();
            decreaseHaloIntensity();
        }
    }

    private bool isAtHalfLifeTime() {
        return (Time.time - startTime) > kamehamehaExplosion.getDestroyTime() / 2;
    }

    private void decreasePointLightIntensity() {
        pointLight.intensity -= Time.fixedDeltaTime / kamehamehaExplosion.getDestroyTime() * 2 * startPointLightIntensity;
        pointLight.intensity = Mathf.Clamp(pointLight.intensity, 0, 100000f);
    }

    private void decreaseHaloIntensity() {
        halo.intensity -= Time.fixedDeltaTime / kamehamehaExplosion.getDestroyTime() * 2 * startHaloIntensity;
        halo.intensity = Mathf.Clamp(halo.intensity, 0, 100000f);
    }

}
