using UnityEngine;
using System.Collections;

public class MainMenuCursorController : MonoBehaviour {

	void Awake () {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true;
    }
	
}
