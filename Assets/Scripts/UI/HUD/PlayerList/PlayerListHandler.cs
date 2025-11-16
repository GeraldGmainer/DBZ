using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerListHandler : MonoBehaviour {
    public RectTransform contentPanel;
    public RectTransform playerListPanel;
    public Text serverName;

    private PlayerListUIUpdater playerListUIUpdater;
    private PlayerListDeterminer playerListDeterminer;

    void Awake() {
        playerListUIUpdater = GetComponent<PlayerListUIUpdater>();
        playerListDeterminer = GetComponent<PlayerListDeterminer>();
    }

    void OnJoinedRoom() {
        setServerName();
    }

    public void enable() {
        setServerName();
        showPanel();
    }

    public void update() {
        clearPanel();
        playerListUIUpdater.update(contentPanel, playerListDeterminer.determine());
    }

    public void disable() {
        playerListPanel.gameObject.SetActive(false);
    }

    private void showPanel() {
        playerListPanel.gameObject.SetActive(true);
        playerListPanel.offsetMax = Vector2.zero;
        playerListPanel.offsetMin = Vector2.zero;
    }

    private void clearPanel() {
        var children = new List<GameObject>();
        foreach (Transform child in contentPanel) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }

    private void setServerName() {
        if (serverName != null && PhotonNetwork.room != null) {
            serverName.text = PhotonNetwork.room.name;
        }
    }

}
