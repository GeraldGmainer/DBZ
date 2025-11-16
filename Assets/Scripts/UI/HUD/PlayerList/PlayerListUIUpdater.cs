using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerListUIUpdater : MonoBehaviour {
    private const string PLAYER_LIST_ENTRY_PREFAB_NAME = "PlayerListEntry";
    private static Color highlightColor = new Color(1, 1, 1, 0.35f);

    public void update(RectTransform contentPanel, List<PlayerListEntry> playerListEntries) {
        foreach (PlayerListEntry playerListEntry in playerListEntries) {
            GameObject go = createEntry(contentPanel);
            fillEntry(go, playerListEntry);
            hightLocalPlayer(go, playerListEntry);
        }
    }

    private GameObject createEntry(RectTransform contentPanel) {
        GameObject go = Instantiate(Resources.Load(PLAYER_LIST_ENTRY_PREFAB_NAME), Vector3.zero, Quaternion.identity) as GameObject;
        go.GetComponent<RectTransform>().SetParent(contentPanel);
        return go;
    }

    private void fillEntry(GameObject go, PlayerListEntry playerListEntry) {
        go.GetComponentsInChildren<Text>()[0].text = playerListEntry.ping.ToString();
        go.GetComponentsInChildren<Text>()[1].text = playerListEntry.player;
        go.GetComponentsInChildren<Text>()[2].text = playerListEntry.kills.ToString();
        go.GetComponentsInChildren<Text>()[3].text = playerListEntry.deaths.ToString();
    }

    private void hightLocalPlayer(GameObject go, PlayerListEntry playerListEntry) {
        if (playerListEntry.isLocal) {
            foreach (Image img in go.GetComponentsInChildren<Image>()) {
                img.color = highlightColor;

            }
        }
    }
}
