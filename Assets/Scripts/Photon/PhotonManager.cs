using UnityEngine;
using Photon;

public class PhotonManager : PunBehaviour {
    public static PhotonManager Instance;

    private string roomName;
    private bool creatingServer;
    private PhotonConnectingInfo photonConnectingInfo;

    void Awake() {
        Instance = this;
        photonConnectingInfo = GetComponent<PhotonConnectingInfo>();
    }

    void Start() {
        disconnectLobby();
        PhotonNetwork.automaticallySyncScene = true;
    }

    public void disconnectLobby() {
        if (PhotonNetwork.connected || PhotonNetwork.connecting) {
            PhotonNetwork.Disconnect();
        }
    }

    public void createRoom(string roomName) {
        this.roomName = roomName;
        PhotonNetwork.ConnectUsingSettings("v1.0");
        photonConnectingInfo.enabled = true;
        creatingServer = true;
    }

    public void enterFindServer() {
        creatingServer = false;
        PhotonNetwork.ConnectUsingSettings("v1.0");
    }

    public void joinRoom(string roomName) {
        PhotonNetwork.JoinRoom(roomName);
        photonConnectingInfo.enabled = true;
    }

    public override void OnJoinedLobby() {
        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 60;
        PhotonNetwork.automaticallySyncScene = true;

        if (creatingServer) {
            RoomOptions roomOptions = new RoomOptions() {
                isVisible = true,
                maxPlayers = 10
            };
            PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
        }
    }

    public override void OnJoinedRoom() {
        if (creatingServer) {
            MapLoader.goToCellArena();
        }
    }

}
