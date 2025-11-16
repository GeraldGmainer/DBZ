using UnityEngine;
using System.Collections;

public class KamehamehaCometExplosionDeterminer : Photon.MonoBehaviour {
    private const string TERRAIN_TAG = "Terrain";
    private const string EXPLOSION_PARTICLE_NAME = "KamehamehaExplosion";

    private KamehamehaComet kamehamehaComet;
    private KamehamehaExplosion kamehamehaExplosion;
    private KamehamehaCometMover kamehamehaCometMover;

    private bool canExplode;
    private bool ignoreGround;
    private bool hasHitSomething;

    void Awake() {
        kamehamehaComet = GetComponent<KamehamehaComet>();
        kamehamehaCometMover = GetComponent<KamehamehaCometMover>();
    }

    void Start() {
        StartCoroutine("canExplodeCoroutine");
    }

    IEnumerator canExplodeCoroutine() {
        yield return new WaitForSeconds(KamehamehaHandler.COMET_EXPLOSION_DELAY);
        canExplode = true;
    }

    void OnTriggerEnter(Collider col) {
        if (!photonView.isMine) {
            return;
        }
        if (col.gameObject.layer == Layers.IGNORE_RAYCAST) {
            return;
        }
        if (isSpawnedInTerrain(col)) {
            ignoreGround = true;
        }
        if (hasHitSomething || !canExplode) {
            return;
        }
        if (ignoreGround && col.gameObject.tag == TERRAIN_TAG) {
            return;
        }
        hasHitSomething = true;
        createExplosion(col);
        PhotonNetwork.Destroy(gameObject);
    }

    private bool isSpawnedInTerrain(Collider col) {
        return !kamehamehaCometMover.getShouldMove() && col.gameObject.tag == TERRAIN_TAG;
    }

    private void createExplosion(Collider hitCollider) {
        Vector3 pos = transform.position;

        /*RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit)) {
            pos = hit.point;
        }*/

        GameObject go = PhotonNetwork.Instantiate(EXPLOSION_PARTICLE_NAME, pos, transform.rotation, 0);
        kamehamehaExplosion = go.GetComponent<KamehamehaExplosion>();
        kamehamehaExplosion.updateSize(kamehamehaComet.getSize());
    }
}
