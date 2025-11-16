using UnityEngine;

public class CameraDeterminer : MonoBehaviour {
    private Camera activeCamera;

    void Awake() {
        if (GameObject.FindGameObjectWithTag("MainCamera") == null) {
            Debug.LogError("no camera with tag 'MainCamera' found!");
            enabled = false;
            return;
        }
        activeCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public Camera getCamera() {
        return activeCamera;
    }
}
