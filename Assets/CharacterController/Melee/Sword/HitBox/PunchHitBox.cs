using UnityEngine;

public class PunchHitBox : MonoBehaviour {
    private SphereCollider sphereCollider;
    private PunchSoundHandler punchSoundHandler;

    private float punchDamage;

    void Awake() {
        sphereCollider = GetComponent<SphereCollider>();
    }

    void Start() {
        setBoxColliderEnabled(false);
    }

    public void init(float dmg, PunchSoundHandler psh) {
        punchDamage = dmg;
        punchSoundHandler = psh;
    }

    void OnTriggerEnter(Collider col) {
        if (!transform.root.GetComponent<PhotonView>().isMine) {
            return;
        }
        if (col.gameObject.tag != "Player") {
            return;
        }
        applyDamage(col);
        setBoxColliderEnabled(false);
        punchSoundHandler.playSound();
    }

    public void setBoxColliderEnabled(bool value) {
        sphereCollider.enabled = value;
    }

    private void applyDamage(Collider col) {
        PhotonView enemyView = col.GetComponent<PhotonView>();
        enemyView.RPC("RPC_applyDamage", PhotonPlayer.Find(enemyView.ownerId), punchDamage, transform.root.GetComponent<PhotonView>().ownerId);
    }
}
