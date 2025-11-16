using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*
 * 
 * TODO: refactoring + cleanup
 * 
 */

public class TagDependedOcculationHandler : MonoBehaviour {

    private SortedDictionary<int, GameObject> _previousObjectsToFade = new SortedDictionary<int, GameObject>();
    private Dictionary<int, IEnumerator> _fadeOutCoroutines = new Dictionary<int, IEnumerator>();
    // Contains all currently active fade in coroutines
    private Dictionary<int, IEnumerator> _fadeInCoroutines = new Dictionary<int, IEnumerator>();

    public void fadeObjects(SortedDictionary<int, GameObject> objectsToFade) {
        List<GameObject> fadeOut = new List<GameObject>();
        List<GameObject> fadeIn = new List<GameObject>();

        // The following lines do the following: 
        // - Compare the objects to fade of the last frame and the objects hit in this frame
        // - If an object is in "_previousObjectsToFade" but not in "objectsToFade", fade it back in (as it is no longer inside the view frustum
        // - If an object is not in "_previousObjectsToFade" but in "objectsToFade", fade it out (as it is enters the view frustum this frame)
        // - If an object is in both lists, do nothing and continue (as the object was already inside the view frustum and still is)
        SortedDictionary<int, GameObject>.Enumerator i = _previousObjectsToFade.GetEnumerator();
        SortedDictionary<int, GameObject>.Enumerator j = objectsToFade.GetEnumerator();

        bool iFinished = !i.MoveNext();
        bool jFinished = !j.MoveNext();
        bool aListFinished = iFinished || jFinished;

        while (!aListFinished) {
            int iKey = i.Current.Key;
            int jKey = j.Current.Key;

            if (iKey == jKey) {
                iFinished = !i.MoveNext();
                jFinished = !j.MoveNext();
                aListFinished = iFinished || jFinished;
            }
            else if (iKey < jKey) {
                fadeIn.Add(i.Current.Value);
                aListFinished = !i.MoveNext();
                iFinished = true;
                jFinished = false;
            }
            else {
                fadeOut.Add(j.Current.Value);
                aListFinished = !j.MoveNext();
                iFinished = false;
                jFinished = true;
            }
        }

        if (iFinished && !jFinished) {
            do {
                fadeOut.Add(j.Current.Value);
            } while (j.MoveNext());
        }
        else if (!iFinished && jFinished) {
            do {
                fadeIn.Add(i.Current.Value);
            } while (i.MoveNext());
        }

        foreach (GameObject o in fadeOut) {
            int objectID = o.transform.GetInstanceID();
            // Create a new coroutine for fading out the object
            IEnumerator coroutine = FadeObjectCoroutine(CharacterViewFrustumHandler.FADE_OUT_ALPHA, CharacterViewFrustumHandler.FADE_OUT_DURATION, o);

            // Check if there is a running fade in coroutine for this object
            IEnumerator runningCoroutine;
            if (_fadeInCoroutines.TryGetValue(objectID, out runningCoroutine)) {
                // Stop the already running coroutine
                StopCoroutine(runningCoroutine);
                // Remove it from the fade in coroutines
                _fadeInCoroutines.Remove(objectID);
            }
            // Add the new fade out coroutine to the list of fade out coroutines
            _fadeOutCoroutines.Add(objectID, coroutine);
            // Start the coroutine
            StartCoroutine(coroutine);
        }

        foreach (GameObject o in fadeIn) {
            if (o == null) {
                continue;
            }
            int objectID = o.transform.GetInstanceID();
            // Create a new coroutine for fading in the object
            IEnumerator coroutine = FadeObjectCoroutine(CharacterViewFrustumHandler.FADE_IN_ALPHA, CharacterViewFrustumHandler.FADE_IN_DURATION, o);

            // Check if there is a running fade out coroutine for this object
            IEnumerator runningCoroutine;
            if (_fadeOutCoroutines.TryGetValue(objectID, out runningCoroutine)) {
                // Stop the already running coroutine
                StopCoroutine(runningCoroutine);
                // Remove it from the fade out coroutines
                _fadeOutCoroutines.Remove(objectID);
            }
            // Add the new fade in coroutine to the list of fade in coroutines
            _fadeInCoroutines.Add(objectID, coroutine);
            // Start the coroutine
            StartCoroutine(coroutine);
        }

        // Set the "_previousObjectsToFade" for the next frame occultation computations
        _previousObjectsToFade = objectsToFade;
    }

    private IEnumerator FadeObjectCoroutine(float to, float duration, GameObject o) {
        bool continueFading = true;
        // Get all renderers of object "o"
        Renderer[] objectRenderers = o.transform.GetComponentsInChildren<Renderer>();

        if (objectRenderers.Length > 0) {
            // There are renderers to fade, create a current velocity array for each renderer fade out
            float[] currentVelocity = new float[objectRenderers.Length];

            while (continueFading) {
                for (int i = 0; i < objectRenderers.Length; i++) {
                    Renderer r = objectRenderers[i];
                    if (r == null) {
                        continue;
                    }
                    Material[] mats = r.materials;
                    float alpha = -1.0f;

                    foreach (Material m in mats) {
                        if (m.HasProperty("_Color")) {
                            // Material has a color property
                            if (alpha == -1.0f) {
                                // Compute the alpha for every material only once
                                alpha = Mathf.SmoothDamp(m.color.a, to, ref currentVelocity[i], duration);
                            }

                            // Apply the modified alpha value
                            Color color = m.color;
                            color.a = alpha;
                            m.color = color;
                        }
                    }

                    r.materials = mats;

                    if (IsAlmostEqual(alpha, to, 0.01f)) {
                        // The current alpha is almost equal to the target alpha value "to" => stop fading
                        continueFading = false;
                    }
                }

                // Continue computation in the next frame
                yield return null;
            }
        }

        int objectID = o.transform.GetInstanceID();
        // Coroutine done => remove the coroutine from both coroutine lists
        _fadeOutCoroutines.Remove(objectID);
        _fadeInCoroutines.Remove(objectID);
    }

    /* Equal method for float equality. Returns true if the distance between "a" and "b" is smaller than "epsilon" */
    private bool IsAlmostEqual(float a, float b, float epsilon) {
        return Mathf.Abs(a - b) < epsilon;
    }

}
