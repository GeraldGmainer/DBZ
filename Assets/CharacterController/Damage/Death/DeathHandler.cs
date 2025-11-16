using UnityEngine;

public class DeathHandler : Photon.MonoBehaviour {
    public bool isDead { get; set; }

    private KiHandler kiHandler;
    private HealthHandler healthHandler;
    private StaminaHandler staminaHandler;
    private RagdollReplacer ragdollReplacer;
    private MovementHandler movementHandler;
    private KillingSpreeHandler killingSpreeHandler;

    void Awake() {
        kiHandler = GetComponent<KiHandler>();
        healthHandler = GetComponent<HealthHandler>();
        staminaHandler = GetComponent<StaminaHandler>();
        ragdollReplacer = GetComponent<RagdollReplacer>();
        movementHandler = GetComponent<MovementHandler>();
        killingSpreeHandler = GetComponent<KillingSpreeHandler>();
    }

    public void kill(int damageCauserOwnerId) {
        string playerName = PhotonPlayer.Find(damageCauserOwnerId).name;

        handleMovement();
        handleCharacterStats();
        handleSuicide(damageCauserOwnerId);
        handleKillCallback(damageCauserOwnerId);
        handleKillFromPlayer(damageCauserOwnerId, playerName);

        ragdollReplacer.replace();
        killingSpreeHandler.reset();
        RespawnMenu.Instance.showMenu();
        isDead = true;
    }

    private void handleMovement() {
        movementHandler.canMove = false;
        movementHandler.canRotate = false;
    }

    private void handleSuicide(int damageCauserOwnerId) {
        if (damageCauserOwnerId == photonView.ownerId) {
            CharacterServerStats.Instance.decreaseKills();
            killingSpreeHandler.suicide();
        }
    }

    private void handleKillFromPlayer(int damageCauserOwnerId, string playerName) {
        if (damageCauserOwnerId != photonView.ownerId) {
            PlayerLog.addWarningMessage(playerName + " killed you");
        }
    }

    private void handleKillCallback(int damageCauserOwnerId) {
        if (damageCauserOwnerId != photonView.ownerId) {
            PhotonView ownerView = GameObjectFinder.playerByOwnerId(damageCauserOwnerId).GetComponent<PhotonView>();
            ownerView.RPC("RPC_killCallback", PhotonPlayer.Find(ownerView.ownerId));
        }
    }

    private void handleCharacterStats() {
        kiHandler.kill();
        healthHandler.kill();
        staminaHandler.kill();
        CharacterServerStats.Instance.increaseDeaths();
    }
}
