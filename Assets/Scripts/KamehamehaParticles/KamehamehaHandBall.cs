using UnityEngine;
using System.Collections;

public class KamehamehaHandBall : MonoBehaviour {
    private static string ATTACH_NAME = "goku_leftHandBase_skin_JNT";
    private static string HAND_BALL_OUTTER_NAME = "handBallOutter";

    [SerializeField]
    private float flareStrength = 5f;
    private Vector3 handBallOffset = new Vector3(-0.092f, 0.141f, 0.086f);
    [SerializeField]
    private float lightIntensityDelay = 1f;
    [SerializeField]
    private float lightIntensity = 3.7f;

    private Camera activeCamera;
    private ParticleSystem handBallParticle;
    private ParticleSystem handBallOutterParticle;

    public void start(Transform owner) {
        gameObject.transform.parent = GameObjectFinder.inChildByName(owner, ATTACH_NAME).transform;
        gameObject.transform.localPosition = handBallOffset;
        activeCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        handBallParticle = GetComponent<ParticleSystem>();

        if (handBallParticle == null) {
            Debug.LogError("KamehamehaHandBall: particle not found");
        }
        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>()) {
            if (ps.name.Contains(HAND_BALL_OUTTER_NAME)) {
                handBallOutterParticle = ps;
            }
        }

        StartCoroutine("lightCoroutine");
    }

    IEnumerator lightCoroutine() {
        yield return new WaitForSeconds(lightIntensityDelay);
        GetComponentInChildren<Light>().intensity = lightIntensity;
    }

    void Update() {
        GetComponentInChildren<LensFlare>().brightness = flareStrength / caluclateCameraDistance();
    }

    private float caluclateCameraDistance() {
        return Vector3.Distance(gameObject.transform.position, activeCamera.transform.position);
    }

    public void increaseHandBallSize() {
        increaseHandBall();
        increaseHandBallOutter();
    }

    private void increaseHandBall() {
        handBallParticle.startSize *= 1.25f;
        handBallParticle.Clear();
        handBallParticle.Stop();
        handBallParticle.Play();
    }

    private void increaseHandBallOutter() {
        handBallOutterParticle.startSize *= 1.15f;
        handBallOutterParticle.Clear();
        handBallOutterParticle.Stop();
        handBallOutterParticle.Play();
    }

    public void destroy() {
        Destroy(gameObject);
    }

}
