using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToastMessage : MonoBehaviour {

    public void setMessage(string message, float messageLiveTime, float fadeOutTime) {
        GetComponent<Text>().text = message;
        StartCoroutine(fadeCoroutine(messageLiveTime - fadeOutTime, fadeOutTime));
        Destroy(gameObject, messageLiveTime);
    }

    IEnumerator fadeCoroutine(float startFadeOutTime, float fadeOutTime) {
        yield return new WaitForSeconds(startFadeOutTime);
        gameObject.GetComponent<Text>().CrossFadeAlpha(0f, fadeOutTime, false);
    }
}
