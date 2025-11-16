using UnityEngine;
using System.Collections;

public class CharacterInvicibleTimer : MonoBehaviour {
    private const float INVICIBLE_TIME = 3f;

    public bool isInvicible { get; private set; }

    public void start() {
        StartCoroutine("startTimer");
    }

    IEnumerator startTimer() {
        isInvicible = true;
        yield return new WaitForSeconds(INVICIBLE_TIME);
        isInvicible = false;
    }
}
