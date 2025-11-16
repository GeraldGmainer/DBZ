using UnityEngine;
using System.Collections;

public class KamehamehaFrontBall : MonoBehaviour {
    private static string OUTTER_BALL_NAME = "outterBall";
    private static string LIGHTNING_NAME = "lightning";
    private static string LIGHT_PEAK_NAME = "lightPeak";

    private ParticleSystem frontBallParticle;
    private ParticleSystem outterBallParticle;
    private ParticleSystem lightningParticle;
    private ParticleSystem lightPeakParticle;
    private float size;

    void Awake() {
        frontBallParticle = GetComponent<ParticleSystem>();
        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>()) {
            if (ps.name.Contains(OUTTER_BALL_NAME)) {
                outterBallParticle = ps;
            }
            else if (ps.name.Contains(LIGHTNING_NAME)) {
                lightningParticle = ps;
            }
            else if (ps.name.Contains(LIGHT_PEAK_NAME)) {
                lightPeakParticle = ps;
            }
        }

        if (frontBallParticle == null || outterBallParticle == null || lightningParticle == null || lightPeakParticle == null) {
            Debug.LogError("KamehamehaFrontBall: not all particles found");
        }
    }

    public void updateSize(float newSize) {
        size = newSize;
        updateFrontBallSize();
    }

    private void updateFrontBallSize() {
        frontBallParticle.startSize = frontBallParticle.startSize + frontBallParticle.startSize * size * 0.05f;
    }

    public void destroy(float time) {
        Destroy(gameObject, time);
    }
}
