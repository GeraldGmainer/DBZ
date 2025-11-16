using UnityEngine;

public class SwordHitBoxController : MonoBehaviour {
    private SwordHitBox swordHitBox;

    public void init() {
        swordHitBox = GetComponentInChildren<SwordHitBox>();
        swordHitBox.init(10f, GetComponent<SwordAttackSoundHandler>());
    }

    public void enableSwordHitBox() {
        swordHitBox.setBoxColliderEnabled(true);
    }

    public void disableSwordHitBox() {
        swordHitBox.setBoxColliderEnabled(false);
    }
}
