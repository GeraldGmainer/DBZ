using UnityEngine;
using System.Collections;

public class SwordAttackHandler : MonoBehaviour {
    private SwordAttackTimer swordAttackTimer;
    private SwordAttackAnimator swordAttackAnimator;

    public int combo { get; private set; }

    void Awake() {
        swordAttackTimer = GetComponent<SwordAttackTimer>();
        swordAttackAnimator = GetComponent<SwordAttackAnimator>();
    }

    public void increase() {
        if (swordAttackTimer.isPrePunch) {
            StopCoroutine("prePunchComboIncrease");
            StartCoroutine("prePunchComboIncrease");
            return;
        }
        if (swordAttackTimer.isCooldown) {
            return;
        }
        swordAttackTimer.start();
        increaseCombo();
        swordAttackAnimator.updateAnimator(combo);
    }

    private void increaseCombo() {
        combo++;
        if (combo > 3) {
            combo = 1;
        }
    }

    public void resetCombo() {
        combo = 0;
    }

    IEnumerator prePunchComboIncrease() {
        yield return new WaitForSeconds(swordAttackTimer.calculateTimeUntilCooldownReady());
        increase();
    }
}
