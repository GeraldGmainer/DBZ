using UnityEngine;

[RequireComponent(typeof(KamehamehaCursorDrawer))]

public class CursorController : MonoBehaviour {
    public static CursorController Instance;

    [SerializeField]
    private Texture2D defaultCursor;
    [SerializeField]
    private Vector2 hotspot = new Vector3(22, 25);

    private KamehamehaCursorDrawer kamehamehaCursorDrawer;

    private bool isOrangeKamehamehaCursor;
    private bool isRedKamehamehaCursor;

    void Awake() {
        Instance = this;
        kamehamehaCursorDrawer = GetComponent<KamehamehaCursorDrawer>();
    }

    void Start() {
        showHardwareCursor();
    }

    void Update() {
        if (isRedKamehamehaCursor || isOrangeKamehamehaCursor) {
            hideHardwareCursor();
            kamehamehaCursorDrawer.calulateKamehamehaCursorRotation();
        }
        else {
            showHardwareCursor();
            handleCursorLock();
        }
    }

    private void hideHardwareCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.visible = false;
    }

    private void showHardwareCursor() {
        if (CellArenaMenuHandler.Instance.isMenuOpen) {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            Cursor.visible = true;
        }
        else {
            Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
            Cursor.visible = true;
        }
    }

    private void handleCursorLock() {
        if (Settings.Instance.cameraMode == CameraMode.THIRD_PERSON && !CellArenaMenuHandler.Instance.isMenuOpen) {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void OnGUI() {
        if (isOrangeKamehamehaCursor) {
            kamehamehaCursorDrawer.drawOrangeKamehamehaCursor();
        }
        if (isRedKamehamehaCursor) {
            kamehamehaCursorDrawer.drawRedKamehamehaCursor();
        }
    }

    public void showOrangeKamehamehaCursor() {
        isOrangeKamehamehaCursor = true;
        isRedKamehamehaCursor = false;
    }

    public void showRedKamehamehaCuror() {
        isOrangeKamehamehaCursor = false;
        isRedKamehamehaCursor = true;
    }

    public void showDefaultCursor() {
        isOrangeKamehamehaCursor = false;
        isRedKamehamehaCursor = false;
    }
}
