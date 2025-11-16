using UnityEngine;
using Photon;

public class PhotonCharacterComponentDeactivator : Photon.MonoBehaviour {
    void Awake() {
        if (PhotonNetwork.offlineMode) {
            return;
        }
        if (photonView.isMine) {
            return;
        }
        Character character = GetComponent<CharController>().getCharacter();

        GetComponent<InputHandler>().enabled = false;
        GetComponent<CharacterCameraHandler>().enabled = false;
        GetComponent<AnimationController>().enabled = false;
        GetComponent<LookIKHandler>().enabled = false;
        GetComponentInChildren<PunchTimer>().enabled = false;

        if (character == Character.GOKU) {
            GetComponentInChildren<KamehamehaCursor>().enabled = false;
            GetComponentInChildren<SkyLightDimmer>().enabled = false;
        }

        if (character == Character.TRUNKS) {
            if (GetComponentInChildren<SwordHitBox>() != null) {
                GetComponentInChildren<SwordHitBox>().enabled = false;
            }
            if (GameObjectFinder.inChildByName(transform, "_geo") == null) {
                GetComponent<DrawSwordHandler>().enabled = false;
            }
            GetComponent<SwordAttackTimer>().enabled = false;
        }
    }
}
