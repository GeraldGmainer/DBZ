using UnityEngine;

[RequireComponent(typeof(KamehamehaCometExplosionDeterminer))]
[RequireComponent(typeof(KamehamehaCometMover))]

public class KamehamehaComet : Photon.MonoBehaviour {
    private const string LIGHTNING_NAME = "lightning";
    private const string SPHERES_NAME = "spheres";
    private const string COMET_BALL_NAME = "cometBall";
    private const string FOG_NAME = "fog";
    private const float COMET_TRAIL_TIME = 3f;

    private ParticleSystem lightningParticle;
    private ParticleSystem spheresParticle;
    private ParticleSystem cometBallParticle;
    private ParticleSystem fogParticle;
    private float size;
    private GameObject owner;
    private TrailRenderer trail;
    private Vector3 targetPosition;
    //private bool hitNothing;

    void Awake() {
        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>()) {
            if (ps.name.Contains(LIGHTNING_NAME)) {
                lightningParticle = ps;
            }
            else if (ps.name.Contains(SPHERES_NAME)) {
                spheresParticle = ps;
            }
            else if (ps.name.Contains(COMET_BALL_NAME)) {
                cometBallParticle = ps;
            }
            else if (ps.name.Contains(FOG_NAME)) {
                fogParticle = ps;
            }
        }
        if (lightningParticle == null || spheresParticle == null || cometBallParticle == null || fogParticle == null) {
            Debug.Log("kamehamehaComet: particles not found");
            this.enabled = false;
            return;
        }
        trail = gameObject.GetComponentInChildren<TrailRenderer>();
    }

    void Update() {
        if (!photonView.isMine) {
            return;
        }
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f) {
            //hitNothing = true;
            PlayerLog.addWarningMessage("Kamehameha hit nothing  ;-(");
            PhotonNetwork.Destroy(gameObject);
        }
    }

    public void updateSize(float newSize) {
        photonView.RPC("RPC_updateSize", PhotonTargets.All, newSize);
    }

    [PunRPC]
    private void RPC_updateSize(float newSize) {
        size = newSize;
        updateSphereParticleLifetime();
        updateCometSize();
        updateSphereSize();
        updateTrailSize();
        updateLightSize();
        updateLightningSize();
        updateFogSize();
        updateColliderSize();
    }

    private void updateSphereParticleLifetime() {
        spheresParticle.startLifetime = COMET_TRAIL_TIME + 1f;
    }

    private void updateCometSize() {
        cometBallParticle.startSize *= size;
    }

    private void updateSphereSize() {
        spheresParticle.startSize = spheresParticle.startSize + spheresParticle.startSize * size * 0.2f;
        float scale = 1 + size * 0.5f;
        spheresParticle.gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void updateTrailSize() {
        trail.startWidth = trail.startWidth + trail.startWidth * size * 1f;
        trail.endWidth = trail.endWidth + trail.endWidth * size * 0.4f;
    }

    private void updateLightSize() {
        foreach (Light l in GetComponentsInChildren<Light>()) {
            l.range *= size;
        }
    }

    private void updateLightningSize() {
        lightningParticle.startSize = lightningParticle.startSize + lightningParticle.startSize * size * 0.5f;
        float scale = 1 + size * 0.65f;
        lightningParticle.gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void updateFogSize() {
        fogParticle.startSize = fogParticle.startSize + fogParticle.startSize * size * 0.15f;
        float scale = 1 + size * 0.5f;
        fogParticle.gameObject.transform.localScale = new Vector3(scale, scale, scale);
        fogParticle.startLifetime += size / 20;
    }

    private void updateColliderSize() {
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        collider.height *= size;
        collider.radius *= size;
    }

    void OnDestroy() {
        /*
        if (!hitNothing) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit)) {
                transform.position = hit.point;
            }
        }
        */
        trail.transform.parent = null;
        trail.autodestruct = true;
    }

    /******/


    public void setTargetPosition(Vector3 pos) {
        targetPosition = pos;
    }

    public Vector3 getTargetPosition() {
        return targetPosition;
    }

    public float getSize() {
        return size;
    }
}
