using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuInitializer : MonoBehaviour {

    [SerializeField]
    private Dropdown cameraModeDropdown;
    [SerializeField]
    private Dropdown killingSpreeTypeDropdown;
    [SerializeField]
    private Slider mouseSensitivitySlider;
    [SerializeField]
    private Toggle invertXCheckbox;
    [SerializeField]
    private Toggle invertYCheckbox;
    [SerializeField]
    private Toggle musicCheckbox;
    [SerializeField]
    private Toggle effectsCheckbox;
    [SerializeField]
    private Toggle vocalsCheckbox;
    [SerializeField]
    private Toggle killingSpreeCheckbox;
    [SerializeField]
    private Toggle wearJacket;

    void OnEnable() {
        refreshSettings();
    }

    private void refreshSettings() {
        cameraModeDropdown.value = (int)Settings.Instance.cameraMode;
        cameraModeDropdown.value = (int)Settings.Instance.cameraMode;
        killingSpreeTypeDropdown.value = (int)Settings.Instance.killingSpreeType;
        killingSpreeTypeDropdown.value = (int)Settings.Instance.killingSpreeType;

        mouseSensitivitySlider.value = Settings.Instance.mouseSensitivity;
        invertXCheckbox.isOn = Settings.Instance.invertMouseX;
        invertYCheckbox.isOn = Settings.Instance.invertMouseY;

        musicCheckbox.isOn = Settings.Instance.isMusicOn;
        effectsCheckbox.isOn = Settings.Instance.isEffectsOn;
        vocalsCheckbox.isOn = Settings.Instance.isVocalsOn;
        killingSpreeCheckbox.isOn = Settings.Instance.isKillingSpreeOn;

        wearJacket.isOn = Settings.Instance.wearJacket;
    }
}
