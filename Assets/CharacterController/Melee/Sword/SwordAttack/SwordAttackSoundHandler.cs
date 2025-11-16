using UnityEngine;

public class SwordAttackSoundHandler : MonoBehaviour {
    private const string SWORD_ATTACK1 = "swordAttack1";
    private const string SWORD_ATTACK2 = "swordAttack2";
    private const string SWORD_ATTACK3 = "swordAttack3";

    private SwordAttackHandler swordAttackHandler;

    void Awake() {
        swordAttackHandler = GetComponent<SwordAttackHandler>();
    }

    public void playSound() {
        AudioClip clip = determineAudioClip();
        CharacterAudioPlayer.playEffect(clip);
    }

    private AudioClip determineAudioClip() {
        if (swordAttackHandler.combo == 1) {
            return createAudioClip(SWORD_ATTACK1);
        }
        if (swordAttackHandler.combo == 2) {
            return createAudioClip(SWORD_ATTACK2);
        }
        else if (swordAttackHandler.combo == 3) {
            return createAudioClip(SWORD_ATTACK3);
        }
        return null;
    }

    private AudioClip createAudioClip(string name) {
        return (AudioClip)Resources.Load(name);
    }
}
