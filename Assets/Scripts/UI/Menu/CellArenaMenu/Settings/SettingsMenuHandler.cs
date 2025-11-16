using UnityEngine;

[RequireComponent(typeof(SettingsMenuInitializer))]

public class SettingsMenuHandler : MonoBehaviour {

    public void updateCameraMode(int index) {
        if (index == 0) {
            Settings.Instance.cameraMode = CameraMode.WOW;
        }
        else {
            Settings.Instance.cameraMode = CameraMode.THIRD_PERSON;
        }
        SettingsPlayerPrefs.Instance.updateCameraModePref();
    }

    public void updateKillingSpreeType(int index) {
        if (index == 0) {
            Settings.Instance.killingSpreeType = KillingSpreeType.NORMAL;
        }
        else if (index == 1) {
            Settings.Instance.killingSpreeType = KillingSpreeType.FEMALE;
        }
        else {
            Settings.Instance.killingSpreeType = KillingSpreeType.DMC;
        }
        SettingsPlayerPrefs.Instance.updateKillingSpreeTypePref();
        KillingSpreeHandler.Instance.initTables();
    }

    public void updateMouseSensitivity(float value) {
        Settings.Instance.mouseSensitivity = value;
        SettingsPlayerPrefs.Instance.updateMouseSensitivityPref();
    }

    public void toggleInvertX(bool value) {
        Settings.Instance.invertMouseX = value;
        SettingsPlayerPrefs.Instance.updateInvertXPref();
    }

    public void toggleInvertY(bool value) {
        Settings.Instance.invertMouseY = value;
        SettingsPlayerPrefs.Instance.updateInvertYPref();
    }

    public void toogleMusic(bool value) {
        Settings.Instance.isMusicOn = value;
        SettingsPlayerPrefs.Instance.updateMusicPref();
        AudioVolumeHandler.Instance.updateMusic();
    }

    public void toogleEffects(bool value) {
        Settings.Instance.isEffectsOn = value;
        SettingsPlayerPrefs.Instance.updateEffectsPref();
        AudioVolumeHandler.Instance.updateEffect();
    }

    public void toogleVocals(bool value) {
        Settings.Instance.isVocalsOn = value;
        SettingsPlayerPrefs.Instance.updateVocalsPref();
        AudioVolumeHandler.Instance.updateVocals();
    }

    public void toogleKillingSpree(bool value) {
        Settings.Instance.isKillingSpreeOn = value;
        SettingsPlayerPrefs.Instance.updateKillingSpreePref();
        AudioVolumeHandler.Instance.updateKillingSpree();
    }

    public void toggleWearJacket(bool value) {
        Settings.Instance.wearJacket = value;
        JacketToggler.Instance.activiteJacket(value);
        SettingsPlayerPrefs.Instance.updateWearJacketPref();
    }
}
