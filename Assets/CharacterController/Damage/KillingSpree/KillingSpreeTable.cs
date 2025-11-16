using System.Collections.Generic;

public class KillingSpreeTable {
    private const string NORMAL_FOLDER_PREFIX = "normal/";
    private const string FEMALE_FOLDER_PREFIX = "female/";
    private const string DMC_FOLDER_PREFIX = "dmc/";
    private const string SUICIDE_FOLDER_PREFIX = "suicide/";

    private List<KillingSpree> normalTable = new List<KillingSpree>();
    private List<KillingSpree> femaleTable = new List<KillingSpree>();
    private List<KillingSpree> dmcTable = new List<KillingSpree>();
    private List<KillingSpree> suicideTable = new List<KillingSpree>();

    public KillingSpreeTable() {
        initNormal();
        initFemale();
        initDmc();
        initSuicide();
    }

    private void initNormal() {
        addNormal("firstblood", 1);
        addNormal("killingspree", 2);
        addNormal("rampage", 3);
        addNormal("dominating", 4);
        addNormal("unstoppable", 5);
        addNormal("wickedsick", 6);
        addNormal("ultrakill", 7);
        addNormal("holyshit", 8);
        addNormal("ludicrouskill", 9);
        addNormal("godlike", 10);
        addNormal("monsterkill", 11);
    }

    private void initFemale() {
        addFemale("firstblood", 1);
        addFemale("killingspree", 2);
        addFemale("rampage", 3);
        addFemale("dominating", 4);
        addFemale("unstoppable", 5);
        addFemale("wickedsick", 6);
        addFemale("ultrakill", 7);
        addFemale("holyshit", 8);
        addFemale("godlike", 9);
        addFemale("monsterkill", 10);
    }

    private void initDmc() {
        addDmc("dirty", 1);
        addDmc("cruel", 2);
        addDmc("brutal", 3);
        addDmc("anarchic", 4);
        addDmc("savage", 5);
        addDmc("sadistic", 6);
        addDmc("sentational", 7);
        addDmc("savage", 8);
        addDmc("sadistic", 9);
        addDmc("sentational", 10);
        addDmc("savage", 11);
        addDmc("sadistic", 12);
        addDmc("sentational", 13);
    }

    private void initSuicide() {
        addSuicide(KillingSpreeType.NORMAL, "humiliation");
        addSuicide(KillingSpreeType.FEMALE, "humiliationfemale");
        addSuicide(KillingSpreeType.DMC, "killyourself");
    }

    private void addNormal(string path, int spreeCounter) {
        normalTable.Add(new KillingSpree(KillingSpreeType.NORMAL, NORMAL_FOLDER_PREFIX + path, spreeCounter));
    }

    private void addFemale(string path, int spreeCounter) {
        femaleTable.Add(new KillingSpree(KillingSpreeType.FEMALE, FEMALE_FOLDER_PREFIX + path, spreeCounter));
    }

    private void addDmc(string path, int spreeCounter) {
        dmcTable.Add(new KillingSpree(KillingSpreeType.DMC, DMC_FOLDER_PREFIX + path, spreeCounter));
    }

    private void addSuicide(KillingSpreeType type, string path) {
        suicideTable.Add(new KillingSpree(type, SUICIDE_FOLDER_PREFIX + path, 0));
    }

    public List<KillingSpree> determine(KillingSpreeType killingSpreeType) {
        switch (killingSpreeType) {
            case KillingSpreeType.NORMAL:
            return normalTable;

            case KillingSpreeType.FEMALE:
            return femaleTable;

            case KillingSpreeType.DMC:
            return dmcTable;

            default:
            return null;
        }
    }

    public string getSuicide(KillingSpreeType killingSpreeType) {
        return suicideTable.Find(suicide => suicide.type == killingSpreeType).path;
    }
}
