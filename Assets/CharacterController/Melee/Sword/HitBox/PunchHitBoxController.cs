using UnityEngine;

public class PunchHitBoxController : MonoBehaviour {
    private PunchHitBox leftPunchHitBox;
    private PunchHitBox rightPunchHitBox;

    void Start() {
        leftPunchHitBox = GameObjectFinder.inChildByName(transform, "leftHandBase_skin_JNT").GetComponentInChildren<PunchHitBox>();
        rightPunchHitBox = GameObjectFinder.inChildByName(transform, "rightHandBase_skin_JNT").GetComponentInChildren<PunchHitBox>();
        leftPunchHitBox.init(5f, GetComponent<PunchSoundHandler>());
        rightPunchHitBox.init(5f, GetComponent<PunchSoundHandler>());
    }

    public void enableLeftPunchHitBox() {
        leftPunchHitBox.setBoxColliderEnabled(true);
    }

    public void disableLeftPunchHitBox() {
        leftPunchHitBox.setBoxColliderEnabled(false);
    }

    public void enableRightPunchHitBox() {
        rightPunchHitBox.setBoxColliderEnabled(true);
    }

    public void disableRightPunchHitBox() {
        rightPunchHitBox.setBoxColliderEnabled(false);
    }
}
