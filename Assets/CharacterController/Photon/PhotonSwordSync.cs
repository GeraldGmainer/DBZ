using UnityEngine;
using System.Collections;

public class PhotonSwordSync : Photon.MonoBehaviour {

    private bool syncIsAttachedToSheath;
    private DeathHandler deathHandler;
    private DrawSwordAttacher drawSwordAttacher;

    void Awake() {
        deathHandler = GetComponent<DeathHandler>();
        drawSwordAttacher = GetComponent<DrawSwordAttacher>();
    }

    void Start() {
        if (photonView == null || !photonView.ObservedComponents.Contains(this)) {
            Debug.LogError(this + " is not observed");
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(drawSwordAttacher.isAttachedToSheath());
        }
        else {
            syncIsAttachedToSheath = (bool)stream.ReceiveNext();
        }
    }

    public void Update() {
        if (deathHandler.isDead) {
            return;
        }
        if (photonView.isMine) {
            return;
        }
        if (syncIsAttachedToSheath) {
            drawSwordAttacher.attachToSheath();
        }
        else {
            drawSwordAttacher.attachToGrip();
        }
    }
}
