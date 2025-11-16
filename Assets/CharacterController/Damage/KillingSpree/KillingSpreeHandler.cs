using UnityEngine;
using System.Collections.Generic;

public class KillingSpreeHandler : MonoBehaviour {
    public static KillingSpreeHandler Instance;

    private int spreeCounter;
    private string suicidePath;
    private List<KillingSpree> killingSprees = new List<KillingSpree>();

    private KillingSpreeTable killingSpreeTable;

    void Awake() {
        Instance = this;
    }

    void Start() {
        initTables();
    }

    public void initTables() {
        killingSpreeTable = new KillingSpreeTable();
        killingSprees = killingSpreeTable.determine(Settings.Instance.killingSpreeType);
        suicidePath = killingSpreeTable.getSuicide(Settings.Instance.killingSpreeType);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.J)) {
            increase();
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            reset();
        }
    }

    public void increase() {
        spreeCounter++;
        AudioClip clip = (AudioClip)Resources.Load(determineKillingSpree().path);
        CharacterAudioPlayer.playerKillingSpree(clip);
    }

    private KillingSpree determineKillingSpree() {
        KillingSpree killingSpree;
        if (spreeCounter >= killingSprees.Count) {
            killingSpree = killingSprees[killingSprees.Count - 1];
        }
        else {
            killingSpree = killingSprees.Find(spree => spree.spreeCounter == spreeCounter);
        }

        return killingSpree;
    }

    public void suicide() {
        AudioClip clip = (AudioClip)Resources.Load(suicidePath);
        CharacterAudioPlayer.playerKillingSpree(clip);
    }

    public void reset() {
        spreeCounter = 0;
    }
}
