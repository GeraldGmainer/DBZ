using UnityEngine;
using System.Collections;

public class KamehamehaSoundHandler : Photon.MonoBehaviour {
    private const string CAST_VOCAL = "kamehamehaCastVocal";
    private const string CAST_EFFECT = "kamehamehaCastEffect";
    private const string CAST_LOOP_EFFECT = "kamehamehaCastLoopEffect";
    private const string SHOOT_VOCAL = "kamehamehaShootVocal";
    private const string SHOOT_EFFECT = "kamehamehaShootEffect";

    public void playKamehamhehaCast() {
        stop();
        CharacterAudioPlayer.playVocal(getAudioClip(CAST_VOCAL));
        photonView.RPC("RPC_playKamehamehaCastEffect", PhotonTargets.All);
    }

    [PunRPC]
    private void RPC_playKamehamehaCastEffect() {
        StartCoroutine("playCastEffect");
    }

    public void playKamehamehaShoot() {
        CharacterAudioPlayer.playVocal(getAudioClip(SHOOT_VOCAL));
        photonView.RPC("RPC_playKamehamehaShootEffect", PhotonTargets.All);
    }

    [PunRPC]
    private void RPC_playKamehamehaShootEffect() {
        CharacterAudioPlayer.playEffect(getAudioClip(SHOOT_EFFECT));
    }

    public void stop() {
        CharacterAudioPlayer.stopVocal();
        photonView.RPC("RPC_stopEffects", PhotonTargets.All);
    }

    [PunRPC]
    private void RPC_stopEffects() {
        StopCoroutine("playCastEffect");
        CharacterAudioPlayer.stopEffect();
    }

    IEnumerator playCastEffect() {
        CharacterAudioPlayer.playEffect(getAudioClip(CAST_EFFECT));
        yield return new WaitForSeconds(CharacterAudioPlayer.getEffectClipLength());
        CharacterAudioPlayer.playEffectLoop(getAudioClip(CAST_LOOP_EFFECT));
    }

    private AudioClip getAudioClip(string clipName) {
        return (AudioClip)Resources.Load(clipName);
    }
}
