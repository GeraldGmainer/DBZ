using UnityEngine;
using UnityEngine.Audio;

public class AudioVolumeHandler : MonoBehaviour {
    [SerializeField]
    private AudioMixerGroup musicMixer;
    [SerializeField]
    private AudioMixerGroup effectMixer;
    [SerializeField]
    private AudioMixerGroup vocalesMixer;
    [SerializeField]
    private AudioMixerGroup killingSpreeMixer;

    public static AudioVolumeHandler Instance;

    void Awake() {
        Instance = this;
    }

    void Start() {
        updateMusic();
        updateEffect();
        updateVocals();
        updateKillingSpree();
    }

    public void updateMusic() {
        if (Settings.Instance.isMusicOn) {
            musicMixer.audioMixer.SetFloat("MusicVolume", 0f);

        }
        else {
            musicMixer.audioMixer.SetFloat("MusicVolume", -80f);
        }
    }

    public void updateEffect() {
        if (Settings.Instance.isEffectsOn) {
            effectMixer.audioMixer.SetFloat("EffectsVolume", 0f);

        }
        else {
            effectMixer.audioMixer.SetFloat("EffectsVolume", -80f);
        }
    }

    public void updateVocals() {
        if (Settings.Instance.isVocalsOn) {
            vocalesMixer.audioMixer.SetFloat("VocalsVolume", 0f);

        }
        else {
            vocalesMixer.audioMixer.SetFloat("VocalsVolume", -80f);
        }
    }

    public void updateKillingSpree() {
        if (Settings.Instance.isKillingSpreeOn) {
            killingSpreeMixer.audioMixer.SetFloat("KillingSpreeVolume", 0f);

        }
        else {
            killingSpreeMixer.audioMixer.SetFloat("KillingSpreeVolume", -80f);
        }
    }
}
