using UnityEngine;
using UnityEngine.UI;

public class PhotonConnectingInfo : MonoBehaviour {

    [SerializeField]
    private Text loadingText;
    [SerializeField]
    private Text statusText;

    void Awake() {
        enabled = false;
    }

    void Update() {
        loadingText.text = "Connecting" + GetConnectingDots();
        statusText.text = "Status: " + PhotonNetwork.connectionStateDetailed;

        if (PhotonNetwork.connectionStateDetailed == PeerState.Joined) {
            statusText.text = "Loading Map";
            enabled = false;
        }
    }

    private string GetConnectingDots() {
        string str = "";
        int numberOfDots = Mathf.FloorToInt(Time.timeSinceLevelLoad * 3f % 4);

        for (int i = 0; i < numberOfDots; ++i) {
            str += " .";
        }

        return str;
    }
}
