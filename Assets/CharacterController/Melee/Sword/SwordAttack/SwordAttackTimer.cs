using UnityEngine;
using System.Collections;

public class SwordAttackTimer : MonoBehaviour {
    private const float COOLDOWN_TIME = 0.6f;
    private const float RESET_TIME = 1f;
    private const float PRE_PUNCH_TIME = 0.35f;
    private const float CAN_ROTATE_TIME = 0.5f;

    private float prePunchStart;

    public bool isCooldown { get; private set; }
    public bool isPrePunch { get; private set; }

    private MovementHandler movementHandler;
    private SwordAttackHandler swordAttackHandler;

    void Awake() {
        movementHandler = GetComponent<MovementHandler>();
        swordAttackHandler = GetComponent<SwordAttackHandler>();
    }

    public void start() {
        StopCoroutine("cooldownTimer");
        StartCoroutine("cooldownTimer");

        StopCoroutine("resetCombo");
        StartCoroutine("resetCombo");

        StopCoroutine("prePunchTimer");
        StartCoroutine("prePunchTimer");

        StopCoroutine("canMoveTimer");
        StartCoroutine("canMoveTimer");
    }

    public float calculateTimeUntilCooldownReady() {
        float spazi = 0.05f;
        return -(Time.time - prePunchStart - PRE_PUNCH_TIME) + spazi;
    }

    IEnumerator cooldownTimer() {
        isCooldown = true;
        yield return new WaitForSeconds(COOLDOWN_TIME);
        isCooldown = false;
    }

    IEnumerator resetCombo() {
        yield return new WaitForSeconds(RESET_TIME);
        swordAttackHandler.resetCombo();
    }

    IEnumerator prePunchTimer() {
        isPrePunch = false;
        yield return new WaitForSeconds(COOLDOWN_TIME - PRE_PUNCH_TIME);
        prePunchStart = Time.time;
        isPrePunch = true;
        yield return new WaitForSeconds(PRE_PUNCH_TIME);
        isPrePunch = false;
    }

    IEnumerator canMoveTimer() {
        movementHandler.canMove = false;
        movementHandler.canRotate = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(CAN_ROTATE_TIME);
        movementHandler.canRotate = true;
        yield return new WaitForSeconds(COOLDOWN_TIME - CAN_ROTATE_TIME + 0.05f);
        movementHandler.canMove = true;
    }
}
