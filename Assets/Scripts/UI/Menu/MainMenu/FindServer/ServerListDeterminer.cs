using UnityEngine;
using System.Collections.Generic;

public class ServerListDeterminer : MonoBehaviour {
    private List<ServerEntryModel> serverList = new List<ServerEntryModel>();

    private ServerListUIUpdater serverListUIUpdater;

    void Awake() {
        serverListUIUpdater = GetComponent<ServerListUIUpdater>();
    }

    void OnReceivedRoomListUpdate() {
        updateServerList();
        serverListUIUpdater.updateServerList(serverList);
    }

    private void updateServerList() {
        serverList = new List<ServerEntryModel>();
        foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList()) {
            if (roomInfo.visible && roomInfo.open) {
                addServerEntry(roomInfo);
            }
        }
    }

    private void addServerEntry(RoomInfo roomInfo) {
        ServerEntryModel serverEntry = new ServerEntryModel();
        serverEntry.name = roomInfo.name;
        serverEntry.maxPlayers = roomInfo.maxPlayers;
        serverEntry.playerCount = roomInfo.playerCount;
        serverList.Add(serverEntry);
    }
}
