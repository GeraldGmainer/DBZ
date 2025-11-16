using UnityEngine;

public class JumpRunStartStateBehaviour : StateMachineBehaviour {
    [SerializeField]
    private GameObject jumpParticle;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (animator.GetComponent<TerrainDeterminer>().isTerrain()) {
            Instantiate(jumpParticle, animator.rootPosition, Quaternion.Euler(90, 0, 0));
        }
    }
}
