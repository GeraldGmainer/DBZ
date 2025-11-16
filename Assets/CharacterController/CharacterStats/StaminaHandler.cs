using UnityEngine;
using System.Collections;

public class StaminaHandler : Photon.MonoBehaviour {
    public const float REFRESH_TIME = 0.02f;
    public const float START_STAMINA = 25;
    public const float MAX_STAMINA = 100;
    public const float STAMINA_REG = 5;

    private float stamina;

    void Start() {
        if (!photonView.isMine) {
            return;
        }
        stamina = START_STAMINA;
        StartCoroutine("updateStamina");
    }

    IEnumerator updateStamina() {
        while (true) {
            stamina += STAMINA_REG * REFRESH_TIME;
            stamina = Mathf.Clamp(stamina, 0, MAX_STAMINA);
            updateUI();
            yield return new WaitForSeconds(REFRESH_TIME);
        }
    }

    public void kill() {
        StopCoroutine("updateStamina");
        stamina = 0;
        updateUI();
    }

    private float getStaminaPercent() {
        return stamina / MAX_STAMINA * 100;
    }

    private void updateUI() {
        CharacterStatsDisplayer.Instance.setStamina(getStaminaPercent());
    }
}
