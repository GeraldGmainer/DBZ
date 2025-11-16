
public struct KillingSpree {
    public KillingSpreeType type;
    public string path;
    public int spreeCounter;

    public KillingSpree(KillingSpreeType type, string path, int spreeCounter) {
        this.type = type;
        this.path = path;
        this.spreeCounter = spreeCounter;
    }
}
