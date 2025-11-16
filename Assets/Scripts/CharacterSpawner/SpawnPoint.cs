using UnityEngine;

public class SpawnPoint : MonoBehaviour {
    private int layerMask;
    private SphereCollider sphereCollider;

    void Awake() {
        sphereCollider = GetComponent<SphereCollider>();
        determineLayerMask();
    }

    public bool isEmpty() {
        return Physics.OverlapSphere(transform.position, sphereCollider.radius, layerMask).Length == 0;
    }

    private void determineLayerMask() {
        layerMask = 1 << Layers.PLAYER_HITBOX;
    }

}
