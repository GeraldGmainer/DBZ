using UnityEngine;
using System.Collections;

public class KamehamehaExplosionDamageDealer : Photon.MonoBehaviour {
    private float size;

    void OnTriggerEnter(Collider col) {
        if (!photonView.isMine) {
            return;
        }
        if (col.tag != "Player") {
            return;
        }
        applyDamage(col);
    }

    private void applyDamage(Collider col) {
        float dmg = ScaleRange.scale(0, KamehamehaHandler.MAX_SIZE, KamehamehaHandler.EXPLOSION_MIN_DAMAGE, KamehamehaHandler.EXPLOSION_MAX_DAMAGE, size);
        PhotonView enemyView = col.GetComponent<PhotonView>();
        enemyView.RPC("RPC_applyDamage", PhotonPlayer.Find(enemyView.ownerId), dmg, photonView.ownerId);
    }

    public void setSize(float value) {
        size = value;
    }
}
