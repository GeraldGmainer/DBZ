using UnityEngine;

public class TrunksRagdollReplacer : Photon.MonoBehaviour {

    private DrawSwordAttacher drawSwordAttacher;

    void Awake() {
        drawSwordAttacher = GetComponent<DrawSwordAttacher>();
    }

    public void replace(GameObject ragdoll) {
        handleSword();
        handleJacket(ragdoll);
    }

    private void handleSword() {
        GameObject sword = drawSwordAttacher.getSword();
        if (sword == null) {
            return;
        }
        drawSwordAttacher.destroySword();
        GameObject go = Instantiate(Resources.Load("swordRagdoll"), sword.transform.position, sword.transform.rotation) as GameObject;
        go.transform.parent = transform;
    }

    private void handleJacket(GameObject ragdoll) {
        if (Settings.Instance.wearJacket == true) {
            GameObjectFinder.inChildByName(ragdoll.transform, "jacket").SetActive(true);
        }
        else {
            GameObjectFinder.inChildByName(ragdoll.transform, "jacket").SetActive(false);
        }
    }
}
