using UnityEngine;

public class KamehamehaCaster : MonoBehaviour {
    private MovementHandler movementHandler;
    private KamehamehaSpawner kamehamehaSpawner;
    private KamehamehaHandler kamehamehaHandler;
    private ThunderboltSpawner thunderboltSpawner;
    private KamehamehaSoundHandler kamehamehaSoundHandler;
    private KamehamehaCameraShaker kamehamehaCameraShaker;
    private KamehamehaCooldownTimer kamehamehaCooldownTimer;
    private HandBallErruptionIncreaser handBallErruptionIncreaser;

    void Awake() {
        movementHandler = GetComponent<MovementHandler>();
        kamehamehaSpawner = GetComponent<KamehamehaSpawner>();
        kamehamehaHandler = GetComponent<KamehamehaHandler>();
        thunderboltSpawner = GetComponent<ThunderboltSpawner>();
        kamehamehaCameraShaker = GetComponent<KamehamehaCameraShaker>();
        kamehamehaCooldownTimer = GetComponent<KamehamehaCooldownTimer>();
        handBallErruptionIncreaser = GetComponent<HandBallErruptionIncreaser>();
        kamehamehaSoundHandler = GetComponent<KamehamehaSoundHandler>();
    }

    public void cast() {
        if (movementHandler.isMoving()) {
            PlayerLog.addErrorMessage("Cannot cast Kamehameha while moving");
            return;
        }
        if (kamehamehaCooldownTimer.isOnCooldown) {
            PlayerLog.addErrorMessage("Kamehamaha is still on cooldown");
            return;
        }
        kamehamehaSoundHandler.playKamehamhehaCast();
        kamehamehaHandler.isCastingKamehameha = true;
        movementHandler.canRotate = false;
        kamehamehaSpawner.cast();
        kamehamehaCameraShaker.shake();
        handBallErruptionIncreaser.start();
        thunderboltSpawner.start();
    }

    public void updateCast() {
        if (isMovingWhileCasting()) {
            kamehamehaSoundHandler.stop();
            handBallErruptionIncreaser.stop();
            kamehamehaCameraShaker.stop();
            thunderboltSpawner.stop();
            kamehamehaSpawner.cancel();
            kamehamehaHandler.isCastingKamehameha = false;
            movementHandler.canRotate = true;
            kamehamehaHandler.size = 0;
            return;
        }
        increaseSize();
    }

    private bool isMovingWhileCasting() {
        return movementHandler.isMoving() && kamehamehaHandler.isCastingKamehameha;
    }

    private void increaseSize() {
        kamehamehaHandler.size += Time.deltaTime * KamehamehaHandler.SIZE_TIME_MULTIPLIER;
        kamehamehaHandler.size = Mathf.Clamp(kamehamehaHandler.size, 0, KamehamehaHandler.MAX_SIZE);
    }
}
