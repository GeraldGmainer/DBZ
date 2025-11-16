using UnityEngine;

public class KamehamehaHandler : MonoBehaviour {
    public const float MIN_SIZE = 1f;
    public const float MAX_SIZE = 15f;
    public const float SIZE_TIME_MULTIPLIER = 1.5f;
    public const float COOLDOWN = 5;
    public const float MIN_RANGE = 3f;
    public const float MAX_RANGE = 1000f;
    public const float MAX_ANGLE = 45f;
    public const float SHOOTING_TIME = 3f;
    public const float COMET_SPEED = 100f;
    public const float COMET_SPAWN_DELAY = 0.2f;
    public const float COMET_MOVE_DELAY = 0.1f;
    public const float COMET_EXPLOSION_DELAY = 0.1f;
    public static Vector3 COMET_OFFSET = new Vector3(0, 1f, 1.5f);
    public const float EXPLOSION_MIN_DESTROY_TIME = 4f;
    public const float EXPLOSION_MAX_DESTROY_TIME = 12f;
    public const float EXPLOSION_MIN_DAMAGE = 15f;
    public const float EXPLOSION_MAX_DAMAGE = 100f;

    private KamehamehaCaster kamehamehaCaster;
    private KamehamehaShooter kamehamehaShooter;

    public float size { get; set; }
    public bool isCastingKamehameha { get; set; }
    public bool isShootingKamehameha { get; set; }

    void Start() {
        kamehamehaCaster = GetComponent<KamehamehaCaster>();
        kamehamehaShooter = GetComponent<KamehamehaShooter>();
    }

    public void onButtonDown() {
        kamehamehaCaster.cast();
    }

    public void onButton() {
        if (!isCastingKamehameha) {
            return;
        }
        kamehamehaCaster.updateCast();
        KamehamehaCastStatDisplayer.Instance.setCastPercent(getSizePercent(), isKamehamehaTooShort());
    }

    public void onButtonUp() {
        kamehamehaShooter.shoot();
        size = 0;
        KamehamehaCastStatDisplayer.Instance.removeText();
    }

    public float getSizePercent() {
        return size / KamehamehaHandler.MAX_SIZE * 100;
    }

    public bool isKamehamehaTooShort() {
        return size < KamehamehaHandler.MIN_SIZE;
    }
}
