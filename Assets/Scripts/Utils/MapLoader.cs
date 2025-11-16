using UnityEngine;

public class MapLoader {
    private const string MAIN_MAIN_NAME = "MainMenu";
    private static string CELL_ARENA_MAP_NAME = "CellArenaMap";

    public static void goToMainMenu() {
        Application.LoadLevel(MAIN_MAIN_NAME);
    }

    public static void goToCellArena() {
        PhotonNetwork.LoadLevel(CELL_ARENA_MAP_NAME);
    }

    public static void quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
