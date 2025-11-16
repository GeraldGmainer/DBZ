using UnityEngine;
using System.Collections;

public class ThunderboltSpawner : MonoBehaviour {
    private const string THUNDERBOLT_PARTICLE_NAME = "KamehamehaThunderBolt";
    private static Vector2 RANDOM_LOCATION = new Vector2(50f, 50f);
    private static Vector2 SPAWN_DELAY_RANGE = new Vector2(1, 2f);

    public void start() {
        StartCoroutine("thunderboltCoroutine");
    }

    public void stop() {
        StopCoroutine("thunderboltCoroutine");
    }

    IEnumerator thunderboltCoroutine() {
        yield return new WaitForSeconds(4);

        while (true) {
            yield return new WaitForSeconds(Random.Range(SPAWN_DELAY_RANGE.x, SPAWN_DELAY_RANGE.y));
            spawn();
        }
    }

    private void spawn() {
        float randomX = Random.Range(transform.position.x - RANDOM_LOCATION.x / 2, transform.position.x + RANDOM_LOCATION.x / 2);
        float randomZ = Random.Range(transform.position.z, RANDOM_LOCATION.y) + 2;
        Vector3 pos = transform.position + transform.rotation * new Vector3(randomX, 10, randomZ);
        PhotonNetwork.Instantiate(THUNDERBOLT_PARTICLE_NAME, determineGroundPosition(pos), Quaternion.Euler(90, 0, 0),0);
    }

    private Vector3 determineGroundPosition(Vector3 pos) {
        RaycastHit hit;

        if (Physics.Raycast(pos, Vector3.down * 1000f, out hit)) {
            return hit.point;
        }
        return pos;
    }
}
