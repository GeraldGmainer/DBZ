using UnityEngine;

public class CharacterAudioPlayer : MonoBehaviour {
    private static AudioSource effectsAudioSource;
    private static AudioSource vocalAudioSource;
    private static AudioSource killingSpreeAudioSource;
    private CharacterAudioInitializer characterAudioInitializer;

    /*** Effects ***/

    public static void playEffect(AudioClip clip) {
        playAudioClip(clip, effectsAudioSource, false);
    }

    public static void playEffectLoop(AudioClip clip) {
        playAudioClip(clip, effectsAudioSource, true);
    }

    public static void stopEffect() {
        stop(effectsAudioSource);
    }

    public static void stopEffectClip(AudioClip clip) {
        if (effectsAudioSource.clip == clip) {
            stopEffect();
        }
    }

    public static float getEffectClipLength() {
        return effectsAudioSource.clip.length;
    }

    /*** Vocals ***/

    public static void playVocal(AudioClip clip) {
        playAudioClip(clip, vocalAudioSource, false);
    }

    public static void stopVocal() {
        stop(vocalAudioSource);
    }

    /*** Killing Spree ***/

    public static void playerKillingSpree(AudioClip clip) {
        playAudioClip(clip, killingSpreeAudioSource, false);
    }

    /****/

    private static void playAudioClip(AudioClip clip, AudioSource audioSource, bool loop) {
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    private static void stop(AudioSource audioSource) {
        audioSource.clip = null;
        audioSource.Stop();
    }

    /****/

    void Awake() {
        characterAudioInitializer = GetComponent<CharacterAudioInitializer>();
        characterAudioInitializer.init();
        effectsAudioSource = characterAudioInitializer.getEffectsSource();
        vocalAudioSource = characterAudioInitializer.getVocalSource();
        killingSpreeAudioSource = characterAudioInitializer.getKillingSpreeSource();
    }
}
