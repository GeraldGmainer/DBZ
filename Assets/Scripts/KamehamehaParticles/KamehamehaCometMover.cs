using UnityEngine;
using System.Collections;

public class KamehamehaCometMover : Photon.MonoBehaviour {
    private KamehamehaComet kamehamehaComet;

    private bool shouldMove;
    private Vector3 previous;

    void Awake() {
        kamehamehaComet = GetComponent<KamehamehaComet>();
    }

    void Start() {
        if (!photonView.isMine) {
            return;
        }
        StartCoroutine("moveDelayCoroutine");
    }

    void Update() {
        if (!photonView.isMine) {
            return;
        }
        if (shouldMove) {
            moveComet();
        }
    }

    IEnumerator moveDelayCoroutine() {
        yield return new WaitForSeconds(KamehamehaHandler.COMET_MOVE_DELAY);
        addOffset();
        shouldMove = true;
        yield return new WaitForSeconds(KamehamehaHandler.COMET_MOVE_DELAY);
        photonView.RPC("RPC_turnOnTrail", PhotonTargets.All);
    }

    [PunRPC]
    void RPC_turnOnTrail() {
        GetComponentInChildren<TrailRenderer>().enabled = true;
    }

    private void addOffset() {
        transform.position = Vector3.MoveTowards(transform.position, kamehamehaComet.getTargetPosition(), kamehamehaComet.getSize());
    }

    private void moveComet() {
        float step = KamehamehaHandler.COMET_SPEED * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, kamehamehaComet.getTargetPosition(), step);
        Debug.DrawLine(transform.position, kamehamehaComet.getTargetPosition(), Color.red);
    }

    public bool getShouldMove() {
        return shouldMove;
    }
}
