using UnityEngine;
using System.Collections;

public class KamehamehaExplosionSoundHandler : MonoBehaviour {
    private const string SMALL_EXPLOSION = "kamehamehaExplosionSmall";
    private const string BIG_EXPLOSION = "kamehamehaExplosionBig";
    private const string EXPLOSION_AFTER = "kamehamehaExplosionAfter";

    private AudioSource audioSource1;
    private AudioSource audioSource2;


    void Awake() {
        audioSource1 = GetComponents<AudioSource>()[0];
        audioSource2 = GetComponents<AudioSource>()[1];
    }

    public void playSound(float size) {
        if (size / KamehamehaHandler.MAX_SIZE * 100 < 50) {
            audioSource1.PlayOneShot(getAudioClip(SMALL_EXPLOSION));
        }
        else {
            audioSource1.PlayOneShot(getAudioClip(BIG_EXPLOSION));
            StartCoroutine(playExplosionAfter(3f));
        }
    }

    IEnumerator playExplosionAfter(float delay) {
        yield return new WaitForSeconds(delay);
        audioSource2.PlayOneShot(getAudioClip(EXPLOSION_AFTER));
    }

    private AudioClip getAudioClip(string clipName) {
        return (AudioClip)Resources.Load(clipName);
    }
}
