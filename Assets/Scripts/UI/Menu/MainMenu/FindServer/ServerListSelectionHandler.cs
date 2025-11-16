using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ServerListSelectionHandler : MonoBehaviour {
    private const float DOUBLE_CLICK_TIME = 0.5f;

    private Color selectionColor = new Color(230, 230, 230, 0.4f);
    private Color normalColor = new Color(230, 230, 230, 0f);

    private Button oldButton;
    private bool isInDoubleClickRange;
    private ServerEntryModel selectedServerEntry;

    private FindServer findServer;
    private FindServerValidator findServerValidator;

    void Awake() {
        findServer = GetComponent<FindServer>();
        findServerValidator = GetComponent<FindServerValidator>();
    }

    public void select(Button button, ServerEntryModel serverEntryModel) {
        if (oldButton != null) {
            oldButton.GetComponent<Image>().color = normalColor;
        }

        selectedServerEntry = serverEntryModel;
        button.GetComponent<Image>().color = selectionColor;
        findServerValidator.validate();

        if (isInDoubleClickRange && oldButton == button && findServerValidator.isValid) {
            findServer.play();
            return;
        }
        StopCoroutine("doubleClickTimer");
        StartCoroutine("doubleClickTimer");

        oldButton = button;
    }

    IEnumerator doubleClickTimer() {
        isInDoubleClickRange = true;
        yield return new WaitForSeconds(DOUBLE_CLICK_TIME);
        isInDoubleClickRange = false;
    }

    void OnEnable() {
        selectedServerEntry = null;
    }

    public ServerEntryModel getSelectedServerEntry() {
        return selectedServerEntry;
    }

}
