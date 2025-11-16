using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class KeybindingsHandler : MonoBehaviour {
    private const string KEYBINDINGSLINE_NAME = "KeybindingLine";

    private List<KeybindingsLine> keybindings = new List<KeybindingsLine>();
    private CharController player;

    void Awake() {
        initializeKeybindings();
        determinePlayer();
        addKeybindings();
    }

    private void initializeKeybindings() {
        keybindings.Add(new KeybindingsLine("Sprint", "SHIFT", Character.NONE));
        keybindings.Add(new KeybindingsLine("Fly Up", "SPACE", Character.NONE));
        keybindings.Add(new KeybindingsLine("Fly Down", "CTRL", Character.NONE));
        keybindings.Add(new KeybindingsLine("Cancel Fly", "Y", Character.NONE));
        keybindings.Add(new KeybindingsLine("Music ON/OFF", "CTRL M", Character.NONE));
        keybindings.Add(new KeybindingsLine("Sound Effects ON/OFF", "CTRL N", Character.NONE));
        keybindings.Add(new KeybindingsLine("Vocals ON/OFF", "CTRL B", Character.NONE));
        keybindings.Add(new KeybindingsLine("Punch", "1", Character.NONE));

        keybindings.Add(new KeybindingsLine("Kamehameha", "2", Character.GOKU));

        keybindings.Add(new KeybindingsLine("Draw Sword", "2", Character.TRUNKS));
    }

    private void determinePlayer() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharController>();
        if (player == null) {
            Debug.LogError("KeybindingsHandler: player not found");
        }
    }

    private void addKeybindings() {
        foreach (KeybindingsLine keybinding in keybindings) {
            if (keybinding.character == Character.NONE || keybinding.character == player.getCharacter()) {
                addKeybinding(keybinding);
            }
        }
    }

    private void addKeybinding(KeybindingsLine keybinding) {
        GameObject rect = Instantiate(Resources.Load(KEYBINDINGSLINE_NAME), Vector3.zero, Quaternion.identity) as GameObject;
        rect.GetComponent<RectTransform>().SetParent(gameObject.GetComponent<RectTransform>());
        rect.GetComponentsInChildren<Text>()[0].text = keybinding.label;
        rect.GetComponentsInChildren<Text>()[1].text = keybinding.value;
    }
}


