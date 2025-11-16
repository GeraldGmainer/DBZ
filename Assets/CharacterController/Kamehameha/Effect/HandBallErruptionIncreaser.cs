using UnityEngine;
using System.Collections;

public class HandBallErruptionIncreaser : MonoBehaviour {
    private KamehamehaSpawner kamehamahaSpawner;
    private KamehamehaHandler kamehamehaHandler;

    void Start() {
        kamehamahaSpawner = GetComponent<KamehamehaSpawner>();
        kamehamehaHandler = GetComponent<KamehamehaHandler>();
    }

    public void start() {
        StartCoroutine("handBallErruptionCoroutine");
    }

    public void stop() {
        StopCoroutine("handBallErruptionCoroutine");
    }

    IEnumerator handBallErruptionCoroutine() {
        while (kamehamehaHandler.isCastingKamehameha && kamehamehaHandler.getSizePercent() < 100) {
            yield return new WaitForSeconds(KamehamehaHandler.MAX_SIZE / KamehamehaHandler.SIZE_TIME_MULTIPLIER / 4);
            kamehamahaSpawner.createHandBallErruption();
            kamehamahaSpawner.increaseHandBallSize();
        }
    }
}
