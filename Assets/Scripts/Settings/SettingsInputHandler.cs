using UnityEngine;
using System.Collections;

public class SettingsInputHandler : MonoBehaviour {

    void Update() {
        if (CellArenaMenuHandler.Instance != null && CellArenaMenuHandler.Instance.isMenuOpen) {
            return;
        }
        handleToggleMusic();
        handleToggleEffects();
        handleToggleVocals();
    }

    private void handleToggleMusic() {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetButtonDown("Music")) {
            Settings.Instance.isMusicOn = !Settings.Instance.isMusicOn;
            SettingsPlayerPrefs.Instance.updateMusicPref();
            AudioVolumeHandler.Instance.updateMusic();
            addMusicToast();
        }
    }

    private void handleToggleEffects() {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetButtonDown("Effects")) {
            Settings.Instance.isEffectsOn = !Settings.Instance.isEffectsOn;
            SettingsPlayerPrefs.Instance.updateEffectsPref();
            AudioVolumeHandler.Instance.updateEffect();
            addEffectsToast();
        }
    }

    private void handleToggleVocals() {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetButtonDown("Vocals")) {
            Settings.Instance.isVocalsOn = !Settings.Instance.isVocalsOn;
            SettingsPlayerPrefs.Instance.updateVocalsPref();
            AudioVolumeHandler.Instance.updateVocals();
            addVocalsToast();
        }
    }

    private void addMusicToast() {
        if(Settings.Instance.isMusicOn) {
            Toast.add("Music enabled");
        }
        else {
            Toast.add("Music disabled");
        }
    }

    private void addEffectsToast() {
        if (Settings.Instance.isEffectsOn) {
            Toast.add("Sound Effects enabled");
        }
        else {
            Toast.add("Sound Effects disabled");
        }
    }

    private void addVocalsToast() {
        if (Settings.Instance.isEffectsOn) {
            Toast.add("Vocals enabled");
        }
        else {
            Toast.add("Vocals disabled");
        }
    }
}
