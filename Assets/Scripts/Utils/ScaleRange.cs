using UnityEngine;
using System.Collections;

public static class ScaleRange {

    /*
        oldMin = 1, oldMax = 5
        newMin = 10, newMax = 100

        if you send in 1, you'll get 10; if you send in 3, you'll get 60; if you send in 5, you'll get 100.
     */
    public static float scale(float oldMin, float oldMax, float newMin, float newMax, float oldValue) {
        float oldRange = (oldMax - oldMin);
        float newRange = (newMax - newMin);
        float newValue = (((oldValue - oldMin) * newRange) / oldRange) + newMin;

        return (newValue);
    }
}
