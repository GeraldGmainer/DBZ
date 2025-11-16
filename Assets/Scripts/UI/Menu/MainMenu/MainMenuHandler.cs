using UnityEngine;
using System.Collections;

public class MainMenuHandler : MonoBehaviour {
    [SerializeField]
    private RectTransform mainMenuPanel;
    [SerializeField]
    private RectTransform setupPanel;
    [SerializeField]
    private RectTransform loadingScreenPanel;
    [SerializeField]
    private RectTransform createServerPanel;
    [SerializeField]
    private RectTransform findServerPanel;

    public MainMenuPage currentMenu { get; private set; }

    void Start() {
        showMainMenu();
    }

    public void showMainMenu() {
        showMenu(mainMenuPanel);
        currentMenu = MainMenuPage.MAIN_MENU;
    }

    public void showCreateServer() {
        hideMenu(mainMenuPanel);
        showMenu(setupPanel);
        hideMenu(findServerPanel);
        showMenu(createServerPanel);
        setMenuPage(MainMenuPage.CREATE_SERVER);
    }

    public void showFindServer() {
        hideMenu(mainMenuPanel);
        showMenu(setupPanel);
        hideMenu(createServerPanel);
        showMenu(findServerPanel);
        GetComponentInChildren<FindServer>().enterFindServer();
        setMenuPage(MainMenuPage.FIND_SERVER);
    }

    public void showLoadingScreen() {
        hideSetupPanel();
        showMenu(loadingScreenPanel);
        setMenuPage(MainMenuPage.LOADING_SCREEN);
    }

    public void hideSetupPanel() {
        hideMenu(setupPanel);
    }

    public void hideLoadingScreen() {
        hideMenu(loadingScreenPanel);
        showMenu(mainMenuPanel);
        setMenuPage(MainMenuPage.MAIN_MENU);
    }

    public void hideEverything() {
        hideMenu(loadingScreenPanel);
        hideSetupPanel();
        showMainMenu();
    }

    public void quit() {
        MapLoader.quit();
    }

    private void hideMenu(RectTransform menu) {
        menu.gameObject.SetActive(false);
    }

    private void showMenu(RectTransform menu) {
        menu.offsetMin = Vector2.zero;
        menu.offsetMax = Vector2.zero;
        menu.gameObject.SetActive(true);
    }

    private void setMenuPage(MainMenuPage mainMenuPage) {
        StartCoroutine(setMenuPageDelay(mainMenuPage));
    }

    IEnumerator setMenuPageDelay(MainMenuPage mainMenuPage) {
        yield return null;
        currentMenu = mainMenuPage;
    }
}
