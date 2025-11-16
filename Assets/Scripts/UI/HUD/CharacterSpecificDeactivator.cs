using UnityEngine;

public class CharacterSpecificDeactivator : MonoBehaviour {
    [SerializeField]
    private Character deactivateWhenPlayingCharacter;

    void Start() {
        if (SceneModel.selectedCharacter == deactivateWhenPlayingCharacter) {
            gameObject.SetActive(false);
        }
    }
}
