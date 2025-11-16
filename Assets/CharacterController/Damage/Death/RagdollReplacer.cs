using UnityEngine;
using System.Collections;

public class RagdollReplacer : Photon.MonoBehaviour {
    private CharController charController;
    private RagdollMatcher ragdollMatcher;
    private TrunksRagdollReplacer trunksRagdollReplacer;

    private GameObject ragdoll;

    void Awake() {
        charController = GetComponent<CharController>();
        ragdollMatcher = GetComponent<RagdollMatcher>();
        trunksRagdollReplacer = GetComponent<TrunksRagdollReplacer>();
    }

    public void replace() {
        int networkId = PhotonNetwork.AllocateViewID();
        photonView.RPC("RPC_replace", PhotonTargets.AllBuffered, networkId);
    }

    [PunRPC]
    private void RPC_replace(int networkId) {
        disableCharacter();
        ragdoll = createRagdoll(networkId);
        ragdollMatcher.applyCharacterBoneTransformsToRagdoll(ragdoll.transform);

        if (charController.getCharacter() == Character.TRUNKS) {
            trunksRagdollReplacer.replace(ragdoll);
        }
    }

    private void disableCharacter() {
        Rigidbody rigidBody = transform.GetComponent<Rigidbody>();
        rigidBody.isKinematic = true;
        rigidBody.detectCollisions = false;
        transform.GetComponent<CapsuleCollider>().enabled = false;
        transform.FindChild(charController.getGeoGroupName()).gameObject.SetActive(false);
        transform.FindChild(charController.getSpine1Name()).gameObject.SetActive(false);
    }

    private GameObject createRagdoll(int networkId) {
        GameObject go = Instantiate(Resources.Load(charController.getRagdollPrefabName()), transform.position, transform.rotation) as GameObject;
        go.GetComponent<PhotonView>().viewID = networkId;
        return go;
    }

    public Transform getSpine1() {
        return ragdoll.transform.FindChild(charController.getSpine1Name());
    }

}
