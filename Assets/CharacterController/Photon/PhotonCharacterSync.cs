using UnityEngine;

public class PhotonCharacterSync : Photon.MonoBehaviour {
    private float smoothingDelay = 10f;

    private Rigidbody rigidBody;
    private DeathHandler deathHandler;
    private MovementHandler movementHandler;
    private PhotonTransformView photonTransformView;

    private Vector3 correctVelocity;
    private Vector3 correctAngularVelocity;
    private bool correctIsDead;
    private bool correctIsFlying;

    void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        deathHandler = GetComponent<DeathHandler>();
        movementHandler = GetComponent<MovementHandler>();
        photonTransformView = GetComponent<PhotonTransformView>();
    }

    void Start() {
        if (photonView == null || !photonView.ObservedComponents.Contains(this)) {
            Debug.LogError(this + " is not observed");
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(rigidBody.velocity);
            stream.SendNext(rigidBody.angularVelocity);
            stream.SendNext(deathHandler.isDead);
            stream.SendNext(movementHandler.isFlying);
        }
        else {
            correctVelocity = (Vector3)stream.ReceiveNext();
            correctAngularVelocity = (Vector3)stream.ReceiveNext();
            correctIsDead = (bool)stream.ReceiveNext();
            correctIsFlying = (bool)stream.ReceiveNext();
        }
    }

    void Update() {
        if (photonView.isMine) {
            photonTransformView.SetSynchronizedValues(rigidBody.velocity, 0);
        }
        else {
            rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, correctVelocity, Time.deltaTime * smoothingDelay);
            rigidBody.angularVelocity = Vector3.Lerp(rigidBody.angularVelocity, correctAngularVelocity, Time.deltaTime * smoothingDelay);
            deathHandler.isDead = correctIsDead;
            movementHandler.isFlying = correctIsFlying;
        }
    }
}
