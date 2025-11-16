public struct KeybindingsLine {
    public string label;
    public string value;
    public Character character;

    public KeybindingsLine(string newLabel, string newValue, Character newCharacter) {
        label = newLabel;
        value = newValue;
        character = newCharacter;
    }
}