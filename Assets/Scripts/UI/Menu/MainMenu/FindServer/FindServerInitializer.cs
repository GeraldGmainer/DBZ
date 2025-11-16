using UnityEngine;

public class FindServerInitializer : MonoBehaviour {
    private FindServer findServer;

    void Awake() {
        findServer = GetComponent<FindServer>();
    }

    void OnEnable() {
        randomPlayerName();
    }

    private void randomPlayerName() {
        int random = Random.Range(0, RandomPlayerNames.names.Count);
        findServer.playerNameInput.text = RandomPlayerNames.names[random];
    }
}
