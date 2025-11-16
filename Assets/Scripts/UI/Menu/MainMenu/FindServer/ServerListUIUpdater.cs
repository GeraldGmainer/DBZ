using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ServerListUIUpdater : MonoBehaviour {
    private const string PLAYER_STATUS_FORMAT = "{0} / {1}";

    [SerializeField]
    private Text infoMessage;
    [SerializeField]
    private Button serverEntryPrefab;
    [SerializeField]
    private RectTransform serverListPanel;

    private ServerListSelectionHandler serverListSelectionHandler;

    void Awake() {
        serverListSelectionHandler = GetComponent<ServerListSelectionHandler>();
    }

    public void updateLoadingMessage() {
        clearServerListPanel();
        infoMessage.gameObject.SetActive(true);
        StartCoroutine("loadingMessage");
    }

    IEnumerator loadingMessage() {
        while (true) {
            infoMessage.text = "Searching Servers" + getConnectionDots();
            yield return null;
        }
    }

    public void updateServerList(List<ServerEntryModel> serverList) {
        StopCoroutine("loadingMessage");
        clearServerListPanel();
        fillServerListPanel(serverList);
    }

    private void clearServerListPanel() {
        var children = new List<GameObject>();
        foreach (Transform child in serverListPanel) {
            if (child != infoMessage.transform) {
                children.Add(child.gameObject);
            }
        }
        children.ForEach(child => Destroy(child));
    }

    private void fillServerListPanel(List<ServerEntryModel> serverList) {
        updateInfoMessage(serverList);
        foreach (ServerEntryModel serverEntry in serverList) {
            insertServerEntry(serverEntry);
        }
    }

    private void insertServerEntry(ServerEntryModel serverEntry) {
        Button go = (Button)Instantiate(serverEntryPrefab, Vector3.zero, Quaternion.identity);
        go.transform.SetParent(serverListPanel);
        updateTexts(go, serverEntry);
        addListener(go, serverEntry);
    }

    private void updateTexts(Button go, ServerEntryModel serverEntry) {
        Text serverName = go.GetComponentsInChildren<Text>()[0];
        Text playerStatus = go.GetComponentsInChildren<Text>()[1];

        serverName.text = serverEntry.name;
        playerStatus.text = string.Format(PLAYER_STATUS_FORMAT, serverEntry.playerCount, serverEntry.maxPlayers);
    }

    private void addListener(Button go, ServerEntryModel serverEntryModel) {
        go.onClick.AddListener(() => { serverListSelectionHandler.select(go, serverEntryModel); });
    }

    private void updateInfoMessage(List<ServerEntryModel> serverList) {
        if (serverList.Count == 0) {
            infoMessage.gameObject.SetActive(true);
            infoMessage.text = "No Servers found";
        }
        else {
            infoMessage.gameObject.SetActive(false);
        }
    }

    private string getConnectionDots() {
        string str = "";
        int numberOfDots = Mathf.FloorToInt(Time.timeSinceLevelLoad * 3f % 4);

        for (int i = 0; i < numberOfDots; ++i) {
            str += " .";
        }
        return str;
    }
}
