using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerListDeterminer : MonoBehaviour {

    public List<PlayerListEntry> determine() {
        if (!PhotonNetwork.connectedAndReady || PhotonNetwork.connecting) {
            return new List<PlayerListEntry>();
        }
        List<PlayerListEntry> playerListEntries = determineOtherPlayers();
        playerListEntries.Add(determineLocalPlayer());
        return sortList(playerListEntries);
    }

    private List<PlayerListEntry> determineOtherPlayers() {
        List<PlayerListEntry> otherPlayers = new List<PlayerListEntry>();
        foreach (PhotonPlayer photonPlayer in PhotonNetwork.otherPlayers) {
            otherPlayers.Add(convert(photonPlayer));
        }
        return otherPlayers;
    }

    private PlayerListEntry determineLocalPlayer() {
        PlayerListEntry localPlayer = convert(PhotonNetwork.player);
        localPlayer.ping = PhotonNetwork.GetPing();
        localPlayer.isLocal = true;
        return localPlayer;
    }

    private PlayerListEntry convert(PhotonPlayer photonPlayer) {
        PlayerListEntry playerListEntry = new PlayerListEntry();
        playerListEntry.ping = (int)photonPlayer.customProperties["ping"];
        playerListEntry.player = (string)photonPlayer.name;
        playerListEntry.kills = (int)photonPlayer.customProperties["kills"];
        playerListEntry.deaths = (int)photonPlayer.customProperties["deaths"];
        playerListEntry.isLocal = false;
        return playerListEntry;
    }

    private List<PlayerListEntry> sortList(List<PlayerListEntry> playerListEntries) {
        return playerListEntries.OrderByDescending(x => x.kills).ThenBy(y => y.deaths).ToList();
    }
}
