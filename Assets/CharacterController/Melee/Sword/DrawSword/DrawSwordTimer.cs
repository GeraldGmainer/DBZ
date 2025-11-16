using UnityEngine;
using System.Collections;

public class DrawSwordTimer : MonoBehaviour {
    private const float COOLDOWN = 0.95f;

    public bool isCooldown { get; private set; }

    public void cooldown() {
        StopAllCoroutines();
        StartCoroutine(updateCooldown());
    }

    IEnumerator updateCooldown() {
        isCooldown = true;
        yield return new WaitForSeconds(COOLDOWN);
        isCooldown = false;
    }
}
