using UnityEngine;
using System.Collections;

public class KamehamehaSpawner : Photon.MonoBehaviour {
    private const string COMET_PARTICLE_NAME = "KamehamehaComet";
    private const string HAND_BALL_PARTICLE_NAME = "KamehamehaHandBall";
    private const string HAND_BALL_ERRUPTION_PARTICLE_NAME = "KamehamehaHandBallErruption";
    private const string FRONT_BALL_PARTICLE_NAME = "KamehamehaFrontBall";
    private const string GROUND_EFFECTS_PARTICLE_NAME = "KamehamehaGroundEffects";
    private const string GROUND_EFFECTS_FLYING_PARTICLE_NAME = "KamehamehaGroundEffectsFlying";
    private const float COMET_TRAIL_TIME = 3f;

    private KamehamehaGroundEffects groundEffects;
    private KamehamehaComet kamehamehaComet;
    private MovementHandler movementHandler;
    private KamehamehaHandBall kamehamehaHandBall;
    private KamehamehaFrontBall kamehamehaFrontBall;

    void Awake() {
        movementHandler = GetComponent<MovementHandler>();
    }

    public void cast() {
        photonView.RPC("createHandBall", PhotonTargets.All);
        photonView.RPC("createGroundAura", PhotonTargets.All);
    }

    public void shoot(Vector3 targetPosition, float size) {
        photonView.RPC("createFrontBall", PhotonTargets.All, targetPosition, size);
        StartCoroutine(shootEffectsWithDelayCoroutine(targetPosition, size));
    }

    public void increaseHandBallSize() {
        photonView.RPC("RPC_increaseHandBallSize", PhotonTargets.All);
    }

    public void cancel() {
        photonView.RPC("destroyHandBall", PhotonTargets.All);
        photonView.RPC("destroyGroundAura", PhotonTargets.All);
    }

    public void createHandBallErruption() {
        photonView.RPC("RPC_createHandBallErruption", PhotonTargets.All);
    }

    IEnumerator shootEffectsWithDelayCoroutine(Vector3 targetPosition, float size) {
        yield return new WaitForSeconds(KamehamehaHandler.COMET_SPAWN_DELAY);

        createComet(targetPosition, size);
        photonView.RPC("destroyHandBall", PhotonTargets.All);
        photonView.RPC("destroyGroundAuraWithDelay", PhotonTargets.All);
        photonView.RPC("destroyFrontBallAfterCometTrail", PhotonTargets.All);
    }

    [PunRPC]
    private void RPC_createHandBallErruption() {
        GameObject go = Instantiate(Resources.Load(HAND_BALL_ERRUPTION_PARTICLE_NAME), transform.position, transform.rotation) as GameObject;
        go.GetComponent<KamehamehaHandBallErruption>().start(transform);
    }

    [PunRPC]
    private void RPC_increaseHandBallSize() {
        kamehamehaHandBall.increaseHandBallSize();
    }

    [PunRPC]
    private void createHandBall() {
        GameObject go = Instantiate(Resources.Load(HAND_BALL_PARTICLE_NAME), transform.position, transform.rotation) as GameObject;
        kamehamehaHandBall = go.GetComponent<KamehamehaHandBall>();
        kamehamehaHandBall.start(transform);
    }

    [PunRPC]
    private void createGroundAura() {
        string particleObject = GROUND_EFFECTS_PARTICLE_NAME;
        if (movementHandler.isFlying) {
            particleObject = GROUND_EFFECTS_FLYING_PARTICLE_NAME;
        }
        GameObject go = Instantiate(Resources.Load(particleObject), transform.position, transform.rotation) as GameObject;
        groundEffects = go.GetComponent<KamehamehaGroundEffects>();
    }

    [PunRPC]
    private void createFrontBall(Vector3 targetPosition, float size) {
        Vector3 sizeOffset = KamehamehaHandler.COMET_OFFSET * size / 30;
        sizeOffset.x = sizeOffset.y = 0;
        Vector3 pos = transform.position + transform.rotation * (KamehamehaHandler.COMET_OFFSET + sizeOffset);
        GameObject go = Instantiate(Resources.Load(FRONT_BALL_PARTICLE_NAME), pos, transform.rotation) as GameObject;
        kamehamehaFrontBall = go.GetComponent<KamehamehaFrontBall>();
        kamehamehaFrontBall.updateSize(size);
    }

    private void createComet(Vector3 targetPosition, float size) {
        Vector3 pos = transform.position + transform.rotation * (KamehamehaHandler.COMET_OFFSET - new Vector3(0, 0, -0.5f));
        Quaternion rot = Quaternion.Euler(90, transform.rotation.y, transform.rotation.z);
        GameObject go = PhotonNetwork.Instantiate(COMET_PARTICLE_NAME, pos, rot, 0);
        kamehamehaComet = go.GetComponent<KamehamehaComet>();
        kamehamehaComet.updateSize(size);
        kamehamehaComet.setTargetPosition(targetPosition);
    }

    [PunRPC]
    private void destroyFrontBallAfterCometTrail() {
        kamehamehaFrontBall.destroy(COMET_TRAIL_TIME);
    }

    [PunRPC]
    private void destroyHandBall() {
        kamehamehaHandBall.destroy();
    }

    [PunRPC]
    private void destroyGroundAura() {
        groundEffects.destroy();
    }

    [PunRPC]
    private void destroyGroundAuraWithDelay() {
        groundEffects.destroy(KamehamehaHandler.COMET_SPAWN_DELAY);
    }
}
