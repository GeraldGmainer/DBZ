using UnityEngine;
using System.Collections;

public class CharacterSpawner : MonoBehaviour {
    [SerializeField]
    private Character defaultCharacter = Character.GOKU;

    private SpawnPointDeterminer spawnPointDeterminer;
    private CharacterEditorSpawner characterEditorSpawner;

    public static CharacterSpawner Instance;

    void Awake() {
        Instance = this;
        spawnPointDeterminer = GetComponent<SpawnPointDeterminer>();
        characterEditorSpawner = GetComponent<CharacterEditorSpawner>();
    }

    void Start() {
        if (SceneModel.selectedCharacter != Character.NONE) {
            spawnCharacter();
        }
        else if (Application.isEditor) {
            characterEditorSpawner.spawn();
        }
        else {
            Debug.LogError("CharacterSpawner: no character cannot be respawned");
            PlayerLog.addErrorMessage("no character cannot be respawned. WTF");
        }
    }

    public void respawn() {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
        spawnCharacter();
    }

    private void spawnCharacter() {
        StartCoroutine(spawnWithDelay());
    }

    IEnumerator spawnWithDelay() {
        yield return new WaitForSeconds(0.5f);
        Transform spawnPoint = spawnPointDeterminer.determine();
        GameObject go = PhotonNetwork.Instantiate(determineCharacter(SceneModel.selectedCharacter), spawnPoint.position, spawnPoint.rotation, 0);
        go.name = PhotonNetwork.player.name;
    }

    public void spawnDefaultCharacter() {
        SceneModel.selectedCharacter = defaultCharacter;
        spawnCharacter();
    }

    private string determineCharacter(Character character) {
        switch (character) {
            case Character.GOKU:
            return GokuCharacterModel.prefabName;

            case Character.TRUNKS:
            return TrunksCharacterModel.prefabName;

            default:
            Debug.LogError("CharacterSpawner: WTF");
            return "";
        }
    }

}
