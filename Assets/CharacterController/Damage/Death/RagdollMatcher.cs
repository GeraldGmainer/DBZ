using UnityEngine;

public class RagdollMatcher : MonoBehaviour {
    private CharController charController;

    void Awake() {
        charController = GetComponent<CharController>();
    }

    public void applyCharacterBoneTransformsToRagdoll(Transform ragDoll) {
        Transform rootBone = transform.FindChild(charController.getSpine1Name());
        Transform ragdollRootBone = GameObjectFinder.inChildByName(ragDoll, "spine1").transform;
        matchChildren(rootBone, ragdollRootBone);
    }

    private void matchChildren(Transform source, Transform target) {
        if (source.childCount <= 0) {
            return;
        }

        foreach (Transform sourceTransform in source.transform) {
            Transform targetTransform = target.Find(sourceTransform.name);

            if (targetTransform != null) {
                matchChildren(sourceTransform, targetTransform);
                targetTransform.localPosition = sourceTransform.localPosition;
                targetTransform.localRotation = sourceTransform.localRotation;
            }
        }
    }
}
