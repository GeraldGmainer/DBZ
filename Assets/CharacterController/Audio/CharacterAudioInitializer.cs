using UnityEngine;

public class CharacterAudioInitializer : MonoBehaviour {
    private const string AUDIO_SOURCES_PREFAB_NAME = "CharacterAudioSources";

    private AudioSource[] audioSources;

    public void init() {
        GameObject go = Instantiate(Resources.Load(AUDIO_SOURCES_PREFAB_NAME), Vector3.zero, Quaternion.identity) as GameObject;
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;

        audioSources = go.GetComponents<AudioSource>();
    }

    public AudioSource getEffectsSource() {
        return audioSources[0];
    }

    public AudioSource getVocalSource() {
        return audioSources[1];
    }

    public AudioSource getKillingSpreeSource() {
        return audioSources[2];
    }
}
