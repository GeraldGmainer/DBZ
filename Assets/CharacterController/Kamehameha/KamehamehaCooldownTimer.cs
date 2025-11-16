using UnityEngine;
using System.Collections;

public class KamehamehaCooldownTimer : MonoBehaviour {
    public bool isOnCooldown { get; private set; }

    public void startCooldown() {
        StartCoroutine("cooldownCoroutine");
    }

    IEnumerator cooldownCoroutine() {
        isOnCooldown = true;
        yield return new WaitForSeconds(KamehamehaHandler.COOLDOWN);
        isOnCooldown = false;
    }
}
