using UnityEngine;

public class IsGroundedDeterminer : MonoBehaviour {
    int layerMask;
    private MovementHandler movementHandler;

    void Awake() {
        movementHandler = GetComponent<MovementHandler>();
    }

    void Start() {
        layerMask = ~((1 << Layers.PLAYER_HITBOX) | (1 << Layers.DAMAGE_HITBOX) | (1 << Layers.IGNORE_RAYCAST));
    }

    void Update() {
        RaycastHit hitInfo;
        Vector3 startPosition = transform.position + (Vector3.up * 0.1f);
        movementHandler.isGrounded = Physics.Raycast(startPosition, Vector3.down, out hitInfo, 0.3f, layerMask);
        //Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * 0.3f), Color.red);
    }
}
