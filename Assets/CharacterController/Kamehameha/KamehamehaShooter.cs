using UnityEngine;
using System.Collections;

public class KamehamehaShooter : MonoBehaviour {
    private MovementHandler movementHandler;
    private KamehamehaSpawner kamehamehaSpawner;
    private KamehamehaHandler kamehamehaHandler;
    private ThunderboltSpawner thunderboltSpawner;
    private KamehamehaCameraShaker kamehamehaCameraShaker;
    private KamehamehaSoundHandler kamehamehaSoundHandler;
    private KamehamehaCooldownTimer kamehamehaCooldownTimer;
    private KamehamehaTargetValidator kamehamehaTargetValidator;
    private KamehamehaTargetDeterminer kamehamehaTargetDeterminer;
    private HandBallErruptionIncreaser handBallErruptionIncreaser;

    void Awake() {
        movementHandler = GetComponent<MovementHandler>();
        kamehamehaSpawner = GetComponent<KamehamehaSpawner>();
        kamehamehaHandler = GetComponent<KamehamehaHandler>();
        thunderboltSpawner = GetComponent<ThunderboltSpawner>();
        kamehamehaSoundHandler = GetComponent<KamehamehaSoundHandler>();
        kamehamehaCameraShaker = GetComponent<KamehamehaCameraShaker>();
        kamehamehaCooldownTimer = GetComponent<KamehamehaCooldownTimer>();
        kamehamehaTargetValidator = GetComponent<KamehamehaTargetValidator>();
        kamehamehaTargetDeterminer = GetComponent<KamehamehaTargetDeterminer>();
        handBallErruptionIncreaser = GetComponent<HandBallErruptionIncreaser>();
    }

    public void shoot() {
        handBallErruptionIncreaser.stop();
        kamehamehaCameraShaker.stop();
        thunderboltSpawner.stop();
        kamehamehaSoundHandler.stop();

        if (!kamehamehaHandler.isCastingKamehameha) {
            return;
        }
        if (kamehamehaHandler.isKamehamehaTooShort()) {
            PlayerLog.addErrorMessage("Kamehameha failed - too short");
            kamehamehaSpawner.cancel();
            kamehamehaSoundHandler.stop();
        }
        else {
            tryShootKamehameha();
        }
        kamehamehaHandler.isCastingKamehameha = false;
        movementHandler.canRotate = true;
    }

    private void tryShootKamehameha() {
        Vector3 target = kamehamehaTargetDeterminer.determine();
        if (!kamehamehaTargetValidator.isValid(target)) {
            kamehamehaSpawner.cancel();
            kamehamehaSoundHandler.stop();
            return;
        }
        rotateCharacter(target);
        kamehamehaSpawner.shoot(target, kamehamehaHandler.size);
        kamehamehaCooldownTimer.startCooldown();
        StartCoroutine("shootingCoroutine");
        kamehamehaSoundHandler.playKamehamehaShoot();
    }

    private void rotateCharacter(Vector3 target) {
        Vector3 lookAt = target - transform.position;

        if (movementHandler.isGrounded) {
            lookAt.y = 0;
        }
        transform.rotation = Quaternion.LookRotation(lookAt);
    }

    IEnumerator shootingCoroutine() {
        movementHandler.canMove = false;
        kamehamehaHandler.isShootingKamehameha = true;
        yield return new WaitForSeconds(KamehamehaHandler.SHOOTING_TIME);
        movementHandler.canMove = true;
        kamehamehaHandler.isShootingKamehameha = false;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
