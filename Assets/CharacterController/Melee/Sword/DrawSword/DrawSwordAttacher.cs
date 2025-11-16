using UnityEngine;

public class DrawSwordAttacher : Photon.MonoBehaviour {
    private const string SWORD_NAME = "sword";

    private GameObject sword;
    private Transform gripJoint;
    private Transform sheathJoint;
    private bool attachedToSheath;

    void Awake() {
        sheathJoint = GameObjectFinder.inChildByName(transform, "sheath_JNT").transform;
        gripJoint = GameObjectFinder.inChildByName(transform, "grip_JNT").transform;
        if (sheathJoint == null || gripJoint == null) {
            Debug.LogError("DrawSwordAttacher: bones not found");
            enabled = false;
        }
    }

    public void init() {
        sword = Instantiate(Resources.Load(SWORD_NAME), Vector3.zero, Quaternion.identity) as GameObject;
        attachToSheath();
        //attachToGrip();
    }

    //From Animation event
    public void attachToSheath() {
        if (sheathJoint.childCount == 0) {
            reparentSword(sheathJoint);
            attachedToSheath = true;
        }
    }

    //From Animation event
    public void attachToGrip() {
        if (gripJoint.childCount == 0) {
            reparentSword(gripJoint);
            attachedToSheath = false;
            sword.transform.localPosition = new Vector3(0, 0, -0.02f);
        }
    }

    private void reparentSword(Transform joint) {
        sword.transform.parent = joint;
        sword.transform.localPosition = Vector3.zero;
        sword.transform.localRotation = Quaternion.identity;
    }

    public bool isAttachedToSheath() {
        return attachedToSheath;
    }

    public void destroySword() {
        Destroy(sword);
    }

    public GameObject getSword() {
        return sword;
    }

    void OnDestroy() {
        if (sword != null) {
            destroySword();
        }
    }

}
