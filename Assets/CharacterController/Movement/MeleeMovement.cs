using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeMovement : MonoBehaviour {
    private const float MOVE_DISTANCE = 1f;
    private const float MOVE_TIME = 0.2f;
    private const float MIN_CHARACTER_DISTANCE = 2f;

    private MovementHandler movementHandler;

    private bool lockNextEnable;

    void Awake() {
        movementHandler = GetComponent<MovementHandler>();
    }

    public void moveForwardMelee() {
        if (isValidDistance()) {
            StopCoroutine("moveForward");
            StartCoroutine("moveForward");
        }
    }

    public void disableMovementMelee() {
        lockNextEnable = false;
        if (!movementHandler.canMove) {
            lockNextEnable = true;
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        movementHandler.canMove = false;
    }

    public void enableMovementMelee() {
        if (!lockNextEnable) {
            movementHandler.canMove = true;
        }
        lockNextEnable = false;
    }

    private bool isValidDistance() {
        List<GameObject> otherPlayers = GameObjectFinder.otherPlayers(gameObject);
        if (otherPlayers.Count == 0) {
            return true;
        }
        foreach (GameObject go in otherPlayers) {
            if (Vector3.Distance(transform.position, go.transform.position) < MIN_CHARACTER_DISTANCE) {
                return false;
            }
        }
        return true;
    }

    IEnumerator moveForward() {
        float startTime = Time.time;
        Vector3 targetPosition = transform.position + transform.TransformDirection(new Vector3(0, 0, MOVE_DISTANCE));

        while (startTime + MOVE_TIME > Time.time) {
            float step = MOVE_DISTANCE / MOVE_TIME * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            yield return null;
        }
    }
}
