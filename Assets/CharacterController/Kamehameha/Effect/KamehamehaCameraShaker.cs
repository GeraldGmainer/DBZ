using UnityEngine;
using System.Collections;

public class KamehamehaCameraShaker : MonoBehaviour {
    private KamehamehaHandler kamehamehaHandler;
    private CameraShaker cameraShaker;

    void Awake() {
        kamehamehaHandler = GetComponent<KamehamehaHandler>();
    }

    public void shake() {
        StartCoroutine("cameraShakeCoroutine");
    }

    public void stop() {
        StopCoroutine("cameraShakeCoroutine");
    }

    IEnumerator cameraShakeCoroutine() {
        yield return new WaitForSeconds(1f);
        while (true) {
            shakeCamera(1, Mathf.Clamp(kamehamehaHandler.size * 10, 10, 40), 0f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void shakeCamera(int numberOfShakes, float speed, float decay) {
        CameraShaker.Instance.shake(numberOfShakes, speed, decay);
    }
}
