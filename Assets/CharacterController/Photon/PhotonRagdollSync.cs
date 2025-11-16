using UnityEngine;

public class PhotonRagdollSync : Photon.MonoBehaviour {
    private Vector3 pos;
    private Quaternion rot;

    private Transform spine1;

    void Awake() {
        spine1 = GameObjectFinder.inChildByName(transform, "spine1").transform;
    }

    void Start() {
        if (photonView == null || !photonView.ObservedComponents.Contains(this)) {
            Debug.LogError(this + " is not observed");
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(spine1.position);
            stream.SendNext(spine1.rotation);
        }
        else {
            pos = (Vector3)stream.ReceiveNext();
            rot = (Quaternion)stream.ReceiveNext();
        }
    }

    void Update() {
        if (photonView.isMine) {
            return;
        }
        if (Vector3.Distance(spine1.position, pos) > 0.5f) {
            spine1.position = pos;
            spine1.rotation = rot;
        }
    }
}
