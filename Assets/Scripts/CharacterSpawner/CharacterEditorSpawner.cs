using UnityEngine;
using System.Collections;

public class CharacterEditorSpawner : MonoBehaviour {
    private const string GOKU_PREFAB_NAME = "goku";
    private const string TRUNKS_PREFAB_NAME = "trunks";

    private CharacterSpawner characterSpawner;

    void Awake() {
        characterSpawner = GetComponent<CharacterSpawner>();
    }

    public void spawn() {
        PhotonNetwork.ConnectUsingSettings("v1.0");
    }

    void OnJoinedLobby() {
        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 60;
        PhotonNetwork.automaticallySyncScene = true;

        if (SceneModel.selectedCharacter == Character.NONE && Application.isEditor) {
            RoomOptions roomOptions = new RoomOptions() { isVisible = true, maxPlayers = 10 };
            PhotonNetwork.player.name = "Test";
            PhotonNetwork.JoinOrCreateRoom("Twerkfest", roomOptions, TypedLobby.Default);
        }
    }

    void OnJoinedRoom() {
        if (SceneModel.selectedCharacter == Character.NONE && Application.isEditor) {
            characterSpawner.spawnDefaultCharacter();
        }
    }
}
