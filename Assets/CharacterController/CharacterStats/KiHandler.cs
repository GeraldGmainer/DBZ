using UnityEngine;
using System.Collections;

public class KiHandler : Photon.MonoBehaviour {
    public const float REFRESH_TIME = 0.02f;
    public const float START_KI = 50;
    public const float MAX_KI = 100;
    public const float KI_REG = 3;

    private float ki;

    void Start() {
        if (!photonView.isMine) {
            return;
        }
        ki = START_KI;
        StartCoroutine("updateKi");
    }

    IEnumerator updateKi() {
        while (true) {
            ki += KI_REG * REFRESH_TIME;
            ki = Mathf.Clamp(ki, 0, MAX_KI);
            updateUI();
            yield return new WaitForSeconds(REFRESH_TIME);
        }
    }

    public void kill() {
        StopCoroutine("updateKi");
        ki = 0;
        updateUI();
    }

    private void updateUI() {
        CharacterStatsDisplayer.Instance.setKi(getKiPercent());
    }

    private float getKiPercent() {
        return ki / MAX_KI * 100;
    }
}
