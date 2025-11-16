using UnityEngine;

public class ObjectDeactivator : MonoBehaviour {
    void Awake() {
        gameObject.SetActive(false);
    }
}
