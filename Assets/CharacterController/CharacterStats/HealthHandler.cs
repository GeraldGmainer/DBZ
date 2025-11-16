using UnityEngine;
using System.Collections;

public class HealthHandler : Photon.MonoBehaviour {
    public const float REFRESH_TIME = 0.02f;
    public const float START_HEALTH = 75;
    public const float MAX_HEALTH = 100;
    public const float HEALTH_REG = 0.5f;

    public float health { get; private set; }

    void Start() {
        if (!photonView.isMine) {
            return;
        }
        health = START_HEALTH;
        StartCoroutine("updateHealthCoroutine");
    }

    IEnumerator updateHealthCoroutine() {
        while (true) {
            health += HEALTH_REG * REFRESH_TIME;
            health = Mathf.Clamp(health, 0, MAX_HEALTH);
            updateUI();
            yield return new WaitForSeconds(REFRESH_TIME);
        }
    }

    public void reduceHealth(float dmg) {
        health -= dmg;
        health = Mathf.Clamp(health, 0, MAX_HEALTH);
        updateUI();
    }

    public void kill() {
        StopCoroutine("updateHealthCoroutine");
        health = 0;
        updateUI();
    }

    private void updateUI() {
        CharacterStatsDisplayer.Instance.setHealth(health / MAX_HEALTH * 100);
    }
}
