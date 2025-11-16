using UnityEngine;

public class KamehamehaHandBallErruption : MonoBehaviour {
	private const string ATTACH_NAME = "leftHandBase_skin_JNT";

    private Vector3 handBallOffset = new Vector3(-0.092f, 0.141f, 0.086f);

    public void start(Transform owner) {
		gameObject.transform.parent = GameObjectFinder.inChildByName(owner, ATTACH_NAME).transform;
		gameObject.transform.localPosition = handBallOffset;
	}
}
