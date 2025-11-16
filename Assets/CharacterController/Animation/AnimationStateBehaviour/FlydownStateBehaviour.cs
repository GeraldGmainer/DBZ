using UnityEngine;

public class FlydownStateBehaviour : StateMachineBehaviour {
    [SerializeField]
    private float distanceToPlay = 17f;
    [SerializeField]
    private bool showDebugLine;

    [SerializeField]
    public AudioClip flyDownSound;

    private bool isPlaying;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!animator.GetComponent<PhotonView>().isMine) {
            return;
        }
        bool hitSomething = Physics.Raycast(animator.transform.position, animator.transform.TransformDirection(Vector3.down), distanceToPlay);
        if (!isPlaying && hitSomething) {
            playSound(animator);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!animator.GetComponent<PhotonView>().isMine) {
            return;
        }
        stopSound(animator);
    }

    private void playSound(Animator animator) {
        isPlaying = true;
        CharacterAudioPlayer.playEffect(flyDownSound);
    }

    private void stopSound(Animator animator) {
        isPlaying = false;
        CharacterAudioPlayer.stopEffectClip(flyDownSound);
    }

}
