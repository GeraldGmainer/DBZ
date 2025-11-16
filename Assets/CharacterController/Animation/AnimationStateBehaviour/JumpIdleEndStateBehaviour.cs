using UnityEngine;

public class JumpIdleEndStateBehaviour : StateMachineBehaviour {
    [SerializeField]
    private GameObject landingParticle;
    [SerializeField]
    public AudioClip[] landingSounds;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!animator.GetCurrentAnimatorStateInfo(AnimatorLayers.baseLayer).IsName(AnimatorStates.FLY_DOWN)) {
            return;
        }
        handleLandingParticle(animator);

        if (!animator.GetComponent<PhotonView>().isMine) {
            return;
        }
        handleLandingSound(animator);
    }

    private void handleLandingParticle(Animator animator) {
        Instantiate(landingParticle, animator.rootPosition + new Vector3(0, 0.1f, 0), Quaternion.identity);
    }

    private void handleLandingSound(Animator animator) {
        AudioClip clip = landingSounds[Random.Range(0, landingSounds.Length)];
        CharacterAudioPlayer.playEffect(clip);
    }
}
