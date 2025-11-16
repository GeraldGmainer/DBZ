using UnityEngine;

public class SettingsPlayerPrefs : MonoBehaviour {
    private const string CAMERA_MODE = "CameraModePlayerPref";
    private const string KILLING_SPREE_TYPE = "KillingSpreeTypePlayerPref";
    private const string MOUSE_SENSITIVITY = "MouseSensitivityPlayerPref";
    private const string INVERT_X = "InvertXPlayerPref";
    private const string INVERT_Y = "InvertYPlayerPref";
    private const string MUSIC = "MusicPlayerPref";
    private const string EFFECTS = "EffectsPlayerPref";
    private const string VOCALS = "VocalsPlayerPref";
    private const string KILLING_SPREE = "KillingSpreePlayerPref";
    private const string WEAR_JACKET = "wearJacketPref";

    private static SettingsPlayerPrefs instance;
    public static SettingsPlayerPrefs Instance {
        get {
            return instance;
        }
    }

    void Awake() {
        instance = this;
        loadPlayerPrefs();
    }

    private void loadPlayerPrefs() {
        Settings.Instance.cameraMode = (CameraMode)PlayerPrefs.GetInt(CAMERA_MODE, (int)Settings.Instance.cameraMode);
        Settings.Instance.killingSpreeType = (KillingSpreeType)PlayerPrefs.GetInt(KILLING_SPREE_TYPE, (int)Settings.Instance.killingSpreeType);

        Settings.Instance.mouseSensitivity = PlayerPrefs.GetFloat(MOUSE_SENSITIVITY, Settings.Instance.mouseSensitivity);
        Settings.Instance.invertMouseX = convertToBool(PlayerPrefs.GetInt(INVERT_X, convertToInt(Settings.Instance.invertMouseX)));
        Settings.Instance.invertMouseY = convertToBool(PlayerPrefs.GetInt(INVERT_Y, convertToInt(Settings.Instance.invertMouseY)));

        Settings.Instance.isMusicOn = convertToBool(PlayerPrefs.GetInt(MUSIC, convertToInt(Settings.Instance.isMusicOn)));
        Settings.Instance.isEffectsOn = convertToBool(PlayerPrefs.GetInt(EFFECTS, convertToInt(Settings.Instance.isEffectsOn)));
        Settings.Instance.isVocalsOn = convertToBool(PlayerPrefs.GetInt(VOCALS, convertToInt(Settings.Instance.isVocalsOn)));
        Settings.Instance.isKillingSpreeOn = convertToBool(PlayerPrefs.GetInt(KILLING_SPREE, convertToInt(Settings.Instance.isKillingSpreeOn)));

        Settings.Instance.wearJacket = convertToBool(PlayerPrefs.GetInt(WEAR_JACKET, convertToInt(Settings.Instance.wearJacket)));
    }

    public void updateCameraModePref() {
        PlayerPrefs.SetInt(CAMERA_MODE, (int)Settings.Instance.cameraMode);
    }

    public void updateKillingSpreeTypePref() {
        PlayerPrefs.SetInt(KILLING_SPREE_TYPE, (int)Settings.Instance.killingSpreeType);
    }

    public void updateMouseSensitivityPref() {
        PlayerPrefs.SetFloat(MOUSE_SENSITIVITY, Settings.Instance.mouseSensitivity);
    }

    public void updateInvertXPref() {
        PlayerPrefs.SetInt(INVERT_X, convertToInt(Settings.Instance.invertMouseX));
    }

    public void updateInvertYPref() {
        PlayerPrefs.SetInt(INVERT_Y, convertToInt(Settings.Instance.invertMouseY));
    }

    public void updateMusicPref() {
        PlayerPrefs.SetInt(MUSIC, convertToInt(Settings.Instance.isMusicOn));
    }

    public void updateEffectsPref() {
        PlayerPrefs.SetInt(EFFECTS, convertToInt(Settings.Instance.isEffectsOn));
    }

    public void updateVocalsPref() {
        PlayerPrefs.SetInt(VOCALS, convertToInt(Settings.Instance.isVocalsOn));
    }

    public void updateKillingSpreePref() {
        PlayerPrefs.SetInt(KILLING_SPREE, convertToInt(Settings.Instance.isKillingSpreeOn));
    }

    public void updateWearJacketPref() {
        PlayerPrefs.SetInt(WEAR_JACKET, convertToInt(Settings.Instance.wearJacket));
    }

    /*****/

    private int convertToInt(bool value) {
        return value ? 1 : 0;
    }

    private bool convertToBool(int value) {
        return value == 1 ? true : false;
    }

}
