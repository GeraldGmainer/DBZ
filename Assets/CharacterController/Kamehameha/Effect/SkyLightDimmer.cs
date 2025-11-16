using UnityEngine;

public class SkyLightDimmer : MonoBehaviour {
    private float minSunLightIntensity = 0f;
    private float sunLightIntensitySmoothValue = 0.075f;
    private Color skyboxStartColor = new Color(0.6f, 0.6f, 0.6f);
    private Color skyboxDarkColor = new Color(0.2f, 0.2f, 0.2f);
    private float skyboxSmoothValue = 0.75f;

    private Light sunLight;
    private float skyboxInreaseBlend;
    private float startLightIntensity;
    private KamehamehaHandler kamehamehaHandler;

    void Start() {
        sunLight = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>();
        if (sunLight == null) {
            Debug.Log("LightDimmer: no light found");
            this.enabled = false;
            return;
        }
        kamehamehaHandler = GetComponent<KamehamehaHandler>();

        startLightIntensity = sunLight.intensity;
        RenderSettings.skybox.SetColor("_Tint", skyboxStartColor);
    }

    void Update() {
        handleSunIntensity();
        handleSkybox();
    }

    void OnApplicationQuit() {
        if (sunLight != null) {
            startLightIntensity = sunLight.intensity;
            RenderSettings.skybox.SetColor("_Tint", skyboxStartColor);
        }
    }


    private void handleSunIntensity() {
        if (kamehamehaHandler.isCastingKamehameha) {
            decreaseSunIntensity();
        }
        if (!kamehamehaHandler.isCastingKamehameha && sunLight.intensity < startLightIntensity) {
            increaseSunIntensity();
        }
    }

    private void handleSkybox() {
        if (kamehamehaHandler.isCastingKamehameha && kamehamehaHandler.size > 1f) {
            decreaseSkyboxColor();
        }
        if (!kamehamehaHandler.isCastingKamehameha && RenderSettings.skybox.GetColor("_Tint") != skyboxStartColor) {
            increaseSkyboxColor();
        }
        else {
            skyboxInreaseBlend = 0;
        }
    }

    private void decreaseSunIntensity() {
        sunLight.intensity -= Time.deltaTime * sunLightIntensitySmoothValue;
        sunLight.intensity = Mathf.Clamp(sunLight.intensity, minSunLightIntensity, startLightIntensity);
    }

    private void increaseSunIntensity() {
        sunLight.intensity += Time.deltaTime * sunLightIntensitySmoothValue * 10f;
        sunLight.intensity = Mathf.Clamp(sunLight.intensity, minSunLightIntensity, startLightIntensity);
    }

    private void decreaseSkyboxColor() {
        Color color = Color.Lerp(skyboxStartColor, skyboxDarkColor, kamehamehaHandler.getSizePercent() / 100);
        RenderSettings.skybox.SetColor("_Tint", color);
    }

    private void increaseSkyboxColor() {
        skyboxInreaseBlend += Time.deltaTime * skyboxSmoothValue;
        Color color = Color.Lerp(skyboxDarkColor, skyboxStartColor, skyboxInreaseBlend);
        RenderSettings.skybox.SetColor("_Tint", color);
    }
}
