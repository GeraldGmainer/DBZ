using UnityEngine;

public class DamageHandler : Photon.MonoBehaviour {
    private DeathHandler deathHandler;
    private HealthHandler healthHandler;
    private KillingSpreeHandler killingSpreeHandler;
    private CharacterInvicibleTimer characterInvicibleTimer;

    void Awake() {
        deathHandler = GetComponent<DeathHandler>();
        healthHandler = GetComponent<HealthHandler>();
        killingSpreeHandler = GetComponent<KillingSpreeHandler>();
        characterInvicibleTimer = GetComponent<CharacterInvicibleTimer>();
    }

    void Start() {
        characterInvicibleTimer.start();
    }

    [PunRPC]
    public void RPC_applyDamage(float dmg, int damageCauserOwnerId) {
        if (characterInvicibleTimer.isInvicible) {
            return;
        }

        healthHandler.reduceHealth(dmg);

        if (healthHandler.health < 1 && !deathHandler.isDead) {
            deathHandler.kill(damageCauserOwnerId);
        }
    }

    [PunRPC]
    private void RPC_killCallback() {
        killingSpreeHandler.increase();
        CharacterServerStats.Instance.increaseKills();
    }

}
