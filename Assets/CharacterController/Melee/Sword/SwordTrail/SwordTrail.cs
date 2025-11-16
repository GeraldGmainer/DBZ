using UnityEngine;
using Xft;

public class SwordTrail : MonoBehaviour {

    public void enableSwordTrail() {
        GetComponentInChildren<XWeaponTrail>().Activate();
    }

    public void disableSwordTrail() {
        GetComponentInChildren<XWeaponTrail>().StopSmoothly(0.35f);
    }
}
