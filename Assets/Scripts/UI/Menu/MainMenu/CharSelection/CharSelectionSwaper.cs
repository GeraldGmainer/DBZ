using UnityEngine;

public class CharSelectionSwaper : MonoBehaviour {
    private Transform respawn;
    private GameObject characterInScene;

    void Awake() {
        respawn = GameObject.FindGameObjectWithTag("Respawn").transform;
    }

    public void swap(GameObject character) {
        removeCharacter();
        spawnCharacter(character);
    }

    private void removeCharacter() {
        if (characterInScene != null) {
            Destroy(characterInScene);
        }
    }

    private void spawnCharacter(GameObject character) {
        characterInScene = (GameObject)Instantiate(character, respawn.position, respawn.rotation);
    }
}
