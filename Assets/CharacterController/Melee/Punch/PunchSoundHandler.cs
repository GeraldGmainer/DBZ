using UnityEngine;

public class PunchSoundHandler : MonoBehaviour {
    private const string PUNCH1 = "punch1";
    private const string PUNCH3 = "punch3";

    private PunchHandler punchHandler;

    void Awake() {
        punchHandler = GetComponent<PunchHandler>();
    }

    public void playSound() {
        AudioClip clip = determineAudioClip();
        CharacterAudioPlayer.playEffect(clip);
    }

    private AudioClip determineAudioClip() {
        if (punchHandler.combo == 1 || punchHandler.combo == 2) {
            return createAudioClip(PUNCH1);
        }
        else if (punchHandler.combo == 3) {
            return createAudioClip(PUNCH3);
        }
        return null;
    }

    private AudioClip createAudioClip(string name) {
        return (AudioClip)Resources.Load(name);
    }
}
