using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MenuActiveHandler : MonoBehaviour {
    private HorizontalLayoutGroup[] panels;
    private List<Image> iconImages = new List<Image>();
    private GameObject selected;

    void Awake() {
        panels = GetComponentsInChildren<HorizontalLayoutGroup>();
        foreach (Image img in GetComponentsInChildren<Image>()) {
            if (img.gameObject.name == "Image") {
                iconImages.Add(img);
            }
        }
    }

    void OnEnable() {
        setActive(panels[0].GetComponentInChildren<Button>().gameObject);
    }

    void Update() {
        if (!hasLostSelectedElement()) {
            selected = EventSystem.current.currentSelectedGameObject;
        }
        setActive(selected);
    }

    private bool hasLostSelectedElement() {
        return EventSystem.current.currentSelectedGameObject == null;
    }

    private void setActive(GameObject go) {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(go);
        handleIconImages();
    }

    private void handleIconImages() {
        foreach (Image img in iconImages) {
            if (determineAssociatedButton(img) == selected) {
                img.enabled = true;
            }
            else {
                img.enabled = false;
            }
        }
    }

    private GameObject determineAssociatedButton(Image img) {
        return img.gameObject.transform.parent.GetComponentInChildren<Button>().gameObject;
    }

    public GameObject getSelectedButton() {
        return selected;
    }
}
