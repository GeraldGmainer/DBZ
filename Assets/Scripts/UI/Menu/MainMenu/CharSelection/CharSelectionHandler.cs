using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharSelectionHandler : MonoBehaviour {
    [SerializeField]
    private GameObject gokuPrefab;
    [SerializeField]
    private GameObject trunksPrefab;

    private List<Image> iconImages = new List<Image>();

    private CharSelectionSwaper charSelectionSwaper;

    void Awake() {
        charSelectionSwaper = GetComponent<CharSelectionSwaper>();
        foreach (Image img in GetComponentsInChildren<Image>()) {
            if (img.gameObject.name == "Image") {
                iconImages.Add(img);
            }
        }
    }

    void Start() {
        chooseGoku();
    }

    public void chooseGoku() {
        SceneModel.selectedCharacter = Character.GOKU;
        charSelectionSwaper.swap(gokuPrefab);
        focusButton(0);
    }

    public void chooseTrunks() {
        SceneModel.selectedCharacter = Character.TRUNKS;
        charSelectionSwaper.swap(trunksPrefab);
        focusButton(1);
    }

    private void focusButton(int button) {
        foreach (Image img in iconImages) {
            img.enabled = false;
        }
        iconImages[button].enabled = true;
    }
}
