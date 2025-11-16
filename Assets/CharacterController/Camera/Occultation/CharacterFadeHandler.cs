using UnityEngine;
using System.Collections.Generic;

/*
 * 
 *  requires shader with transparent
 * 
 */

public class CharacterFadeHandler : MonoBehaviour {
    private CameraDeterminer cameraDeterminer;
    private CameraPivotPositionDeterminer cameraPivotPositionDeterminer;

    private Renderer[] characterRenderersToFade;

    void Awake() {
        cameraDeterminer = GetComponent<CameraDeterminer>();
        cameraPivotPositionDeterminer = GetComponent<CameraPivotPositionDeterminer>();
        updateCharacterRenderersToFade();
    }

    public void handleCharacterFading() {
        float actualDistance = Vector3.Distance(cameraPivotPositionDeterminer.getCameraPivotPosition(), cameraDeterminer.getCamera().transform.position);
        float newAlpha = Mathf.Floor(Mathf.Clamp((actualDistance - CharacterViewFrustumHandler.CHAR_FADE_END_DISTANCE) / (CharacterViewFrustumHandler.CHAR_FADE_START_DISTANCE - CharacterViewFrustumHandler.CHAR_FADE_END_DISTANCE), 0, 1) * 100) / 100;
        float maximumAlpha = CharacterViewFrustumHandler.FADE_OUT_ALPHA;

        foreach (Renderer r in characterRenderersToFade) {
            Color color = r.material.color;
            color.a = Mathf.SmoothStep(CharacterViewFrustumHandler.CHAR_FADE_OUT_ALPHA, maximumAlpha, newAlpha);
            r.material.color = color;
        }
    }

    private void updateCharacterRenderersToFade() {
        List<Renderer> renderers = new List<Renderer>();
        Renderer[] temp = GetComponentsInChildren<Renderer>();

        foreach (Renderer r in temp) {
            if (r.material.HasProperty("_Color")) {
                // Add only if the color property is available
                renderers.Add(r);
            }
        }
        characterRenderersToFade = renderers.ToArray();
    }
}
