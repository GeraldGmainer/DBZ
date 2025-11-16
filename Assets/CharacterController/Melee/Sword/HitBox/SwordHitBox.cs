using UnityEngine;

public class SwordHitBox : MonoBehaviour {
    private BoxCollider boxCollider;
    private SwordAttackSoundHandler swordAttackSoundHandler;

    private float swordDamage;

    void Awake() {
        boxCollider = GetComponent<BoxCollider>();
    }

    void Start() {
        setBoxColliderEnabled(false);
    }

    public void init(float dmg, SwordAttackSoundHandler sash) {
        swordDamage = dmg;
        swordAttackSoundHandler = sash;
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
        swordAttackSoundHandler.playSound();
    }

    public void setBoxColliderEnabled(bool value) {
        boxCollider.enabled = value;
    }

    private void applyDamage(Collider col) {
        PhotonView enemyView = col.GetComponent<PhotonView>();
        enemyView.RPC("RPC_applyDamage", PhotonPlayer.Find(enemyView.ownerId), swordDamage, transform.root.GetComponent<PhotonView>().ownerId);
    }
}
