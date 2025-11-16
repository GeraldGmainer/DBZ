using UnityEngine;
using System.Collections;

public class PunchHandler : MonoBehaviour {
    private PunchTimer punchTimer;
    private PunchAnimator punchAnimator;

    public int combo { get; private set; }

    void Awake() {
        punchTimer = GetComponent<PunchTimer>();
        punchAnimator = GetComponent<PunchAnimator>();
    }

    public void increase() {
        if (punchTimer.isPrePunch) {
            StopCoroutine("prePunchComboIncrease");
            StartCoroutine("prePunchComboIncrease");
            return;
        }
        if (punchTimer.isCooldown) {
            return;
        }
        punchTimer.start();
        increaseCombo();
        punchAnimator.updateAnimator(combo);
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
        yield return new WaitForSeconds(punchTimer.calculateTimeUntilCooldownReady());
        increase();
    }
}
