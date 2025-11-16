using UnityEngine;

public class JacketToggler : MonoBehaviour {
    private const string JACKET_NAME = "jacket";

    private GameObject jacket;

    private static JacketToggler instance;
    public static JacketToggler Instance {
        get {
            return instance;
        }
    }

    void Awake() {
        instance = this;
        jacket = GameObjectFinder.inChildByName(transform, JACKET_NAME);
        if (jacket == null) {
            Debug.LogError("JacketToggler: jacket not found");
            enabled = false;
        }
    }

    void Start() {
        activiteJacket(Settings.Instance.wearJacket);
    }

    public void activiteJacket(bool value) {
        jacket.SetActive(value);
    }

    private GameObject getChildGameObject(string withName) {
        Transform[] ts = transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name.Contains(withName)) return t.gameObject;
        return null;
    }
}
