using UnityEngine;
using System.Collections.Generic;

public class CreateServerInitializer : MonoBehaviour {
    private CreateServer createServer;

    void Awake() {
        createServer = GetComponent<CreateServer>();
    }

    void OnEnable() {
        randomServerName();
        randomPlayerName();
    }

    private void randomServerName() {
        int random = Random.Range(0, RandomServerNames.names.Count);
        createServer.serverNameInput.text = RandomServerNames.names[random];
    }

    private void randomPlayerName() {
        int random = Random.Range(0, RandomPlayerNames.names.Count);
        createServer.playerNameInput.text = RandomPlayerNames.names[random];
    }
}
