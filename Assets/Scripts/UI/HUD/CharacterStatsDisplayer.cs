using UnityEngine;
using UnityEngine.UI;

public class CharacterStatsDisplayer : MonoBehaviour {
    private static string PERCENT_TEMPLATE = "{0} %";

    private static CharacterStatsDisplayer instance;

    private Text healthText;
    private Text kiText;
    private Text staminaText;

    public static CharacterStatsDisplayer Instance {
        get {
            return instance;
        }
    }

    void Awake() {
        instance = this;

        foreach (Text guiText in GetComponentsInChildren<Text>()) {
            if (guiText.name.Equals("healthText")) {
                healthText = guiText;
            }
            else if (guiText.name.Equals("kiText")) {
                kiText = guiText;
            }
            else if (guiText.name.Equals("staminaText")) {
                staminaText = guiText;
            }
        }
    }

    public void setHealth(float health) {
        healthText.text = string.Format(PERCENT_TEMPLATE, health.ToString("0"));
    }

    public void setKi(float ki) {
        kiText.text = string.Format(PERCENT_TEMPLATE, ki.ToString("0"));
    }

    public void setStamina(float stamina) {
        staminaText.text = string.Format(PERCENT_TEMPLATE, stamina.ToString("0"));
    }
}
