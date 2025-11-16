using UnityEngine;
using System.Collections;

public class CharacterServerStats : MonoBehaviour {
    private const float PING_UPDATE_TIME = 2f;

    public static CharacterServerStats Instance;

    void Awake() {
        Instance = this;
    }

    void Start() {
        if (Application.isEditor) {
            resetStats();
        }
        StartCoroutine("updatePing");
    }

    IEnumerator updatePing() {
        while (true) {
            updatePlayerCustomProp("ping", PhotonNetwork.GetPing());
            yield return new WaitForSeconds(PING_UPDATE_TIME);
        }
    }

    public void increaseDeaths() {
        int deaths = (int)PhotonNetwork.player.customProperties["deaths"] + 1;
        updatePlayerCustomProp("deaths", deaths);
    }

    public void increaseKills() {
        int kills = (int)PhotonNetwork.player.customProperties["kills"] + 1;
        updatePlayerCustomProp("kills", kills);
    }

    public void decreaseKills() {
        int kills = (int)PhotonNetwork.player.customProperties["kills"] - 1;
        updatePlayerCustomProp("kills", kills);
    }

    private void resetStats() {
        updatePlayerCustomProp("ping", 0);
        updatePlayerCustomProp("deaths", 0);
        updatePlayerCustomProp("kills", 0);
    }

    private static void updatePlayerCustomProp(string propName, int value) {
        ExitGames.Client.Photon.Hashtable PlayerCustomProps = new ExitGames.Client.Photon.Hashtable();
        PlayerCustomProps[propName] = value;
        PhotonNetwork.player.SetCustomProperties(PlayerCustomProps);
    }

}
