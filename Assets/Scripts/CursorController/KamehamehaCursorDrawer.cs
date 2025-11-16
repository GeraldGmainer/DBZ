using UnityEngine;

public class KamehamehaCursorDrawer : MonoBehaviour {
    [SerializeField]
    private float kamehamehaCursor2Speed = 70f;
    [SerializeField]
    private float kamehamehaCursor3Speed = 40f;
    [SerializeField]
    private Texture2D kamehamehaCursor1;
    [SerializeField]
    private Texture2D kamehamehaCursor2;
    [SerializeField]
    private Texture2D kamehamehaCursor3;
    [SerializeField]
    private Texture2D kamehamehaCursor4;
    [SerializeField]
    private Texture2D kamehamehaCursor5;
    [SerializeField]
    private Texture2D kamehamehaCursor6;
    [SerializeField]
    private float kamehamehaCursorSize = 50f;

    private float angle1 = 0.0f;
    private float angle2 = 0.0f;

    public void drawRedKamehamehaCursor() {
        Matrix4x4 matx = GUI.matrix;
        float x = (float)(Event.current.mousePosition.x - kamehamehaCursorSize / 2.0);
        float y = (float)(Event.current.mousePosition.y - kamehamehaCursorSize / 2.0);
        Vector2 pivot = new Vector2((float)(x + kamehamehaCursorSize / 2.0), (float)(y + kamehamehaCursorSize / 2.0));
        Rect rect = new Rect(x, y, kamehamehaCursorSize, kamehamehaCursorSize);

        GUI.DrawTexture(rect, kamehamehaCursor1);

        GUI.matrix = matx;
        GUIUtility.RotateAroundPivot(angle1, pivot);
        GUI.DrawTexture(rect, kamehamehaCursor2);

        GUI.matrix = matx;
        GUIUtility.RotateAroundPivot(angle2, pivot);
        GUI.DrawTexture(rect, kamehamehaCursor3);
    }

    public void drawOrangeKamehamehaCursor() {
        Matrix4x4 matrix = GUI.matrix;
        float x = (float)(Event.current.mousePosition.x - kamehamehaCursorSize / 2.0);
        float y = (float)(Event.current.mousePosition.y - kamehamehaCursorSize / 2.0);
        Vector2 pivot = new Vector2((float)(x + kamehamehaCursorSize / 2.0), (float)(y + kamehamehaCursorSize / 2.0));
        Rect rect = new Rect(x, y, kamehamehaCursorSize, kamehamehaCursorSize);

        GUI.DrawTexture(rect, kamehamehaCursor4);

        GUI.matrix = matrix;
        GUIUtility.RotateAroundPivot(angle1, pivot);
        GUI.DrawTexture(rect, kamehamehaCursor5);

        GUI.matrix = matrix;
        GUIUtility.RotateAroundPivot(angle2, pivot);
        GUI.DrawTexture(rect, kamehamehaCursor6);
    }

    public void calulateKamehamehaCursorRotation() {
        angle1 += Time.deltaTime * kamehamehaCursor2Speed;
        angle1 = angle1 % 360.0f;

        angle2 -= Time.deltaTime * kamehamehaCursor3Speed;
        angle2 = angle2 % 360.0f;
    }
}
