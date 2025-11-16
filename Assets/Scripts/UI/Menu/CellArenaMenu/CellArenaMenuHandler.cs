using UnityEngine;

public class CellArenaMenuHandler : MonoBehaviour {
    private static CellArenaMenuHandler instance;

    public static CellArenaMenuHandler Instance {
        get {
            return instance;
        }
    }

    [SerializeField]
    private RectTransform ingameMenu;
    [SerializeField]
    private RectTransform settingsMenu;
    [SerializeField]
    private RectTransform keybindingsMenu;

    public bool isMenuOpen { get; private set; }

    private CellArenaMenuPage currentMenu = CellArenaMenuPage.NOTHING;

    void Awake() {
        instance = this;
        hideIngameMenu();
    }

    void Update() {
        if (Input.GetButtonDown("Cancel")) {
            handleCancel();
        }
    }

    public void hideIngameMenu() {
        currentMenu = CellArenaMenuPage.NOTHING;
        hideMenu(ingameMenu);
        isMenuOpen = false;
    }

    public void showSettingsMenu() {
        hideMenu(ingameMenu);
        currentMenu = CellArenaMenuPage.SETTINGS;
        showMenu(settingsMenu);
    }

    public void hideSettingsMenu() {
        hideMenu(settingsMenu);
        showIngameMenu();
    }

    public void showKeybindindsMenu() {
        hideMenu(ingameMenu);
        currentMenu = CellArenaMenuPage.KEYBINDINGS;
        showMenu(keybindingsMenu);
    }

    public void hideKeybindindsMenu() {
        hideMenu(keybindingsMenu);
        showIngameMenu();
    }

    public void goToMainMenu() {
        PhotonNetwork.Disconnect();
        MapLoader.goToMainMenu();
    }

    public void quit() {
        PhotonNetwork.Disconnect();
        MapLoader.quit();
    }

    private void handleCancel() {
        switch (currentMenu) {
            case CellArenaMenuPage.NOTHING:
            showIngameMenu();
            break;

            case CellArenaMenuPage.INGAME_MENU:
            hideIngameMenu();
            break;

            case CellArenaMenuPage.SETTINGS:
            hideSettingsMenu();
            break;

            case CellArenaMenuPage.KEYBINDINGS:
            hideKeybindindsMenu();
            break;

            default:
            Debug.Log("CanvasHandler: undefined menu");
            break;
        }
    }

    private void showIngameMenu() {
        currentMenu = CellArenaMenuPage.INGAME_MENU;
        showMenu(ingameMenu);
        isMenuOpen = true;
    }

    private void hideMenu(RectTransform menu) {
        menu.gameObject.SetActive(false);
    }

    private void showMenu(RectTransform menu) {
        menu.offsetMax = Vector2.zero;
        menu.offsetMin = Vector2.zero;
        menu.gameObject.SetActive(true);
    }
}
