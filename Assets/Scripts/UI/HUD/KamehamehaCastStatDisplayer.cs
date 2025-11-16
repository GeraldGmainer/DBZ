using UnityEngine;
using UnityEngine.UI;

public class KamehamehaCastStatDisplayer : MonoBehaviour {
    private const string KAMEHAMEHA_TEMPLATE = "Kamehameha: {0} %";

    private static KamehamehaCastStatDisplayer instance;

    private Text kamehamehaText;
    private Color red = new Color(1f, 0, 0);
    private Color orange = new Color(1f, 0.6f, 0);

    public static KamehamehaCastStatDisplayer Instance {
        get {
            return instance;
        }
    }

    void Awake() {
        instance = this;
        kamehamehaText = GetComponent<Text>();
        removeText();
    }

    public void setCastPercent(float castPercent, bool isRed) {
        kamehamehaText.text = string.Format(KAMEHAMEHA_TEMPLATE, castPercent.ToString("0"));
        kamehamehaText.enabled = true;

        if (isRed) {
            kamehamehaText.color = red;
        }
        else {
            kamehamehaText.color = orange;
        }
    }

    public void removeText() {
        kamehamehaText.enabled = false;
    }

}
