using UnityEngine;

[RequireComponent(typeof(KamehamehaExplosionLightHandler))]
[RequireComponent(typeof(KamehamehaExplosionSoundHandler))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CapsuleCollider))]

public class KamehamehaExplosion : Photon.MonoBehaviour {
    private static string OUTTER_CORE_NAME = "outterCore";
    private static string LINES_NAME = "lines";
    private static string LIGHTNING_NAME = "lightning";
    private static string SPHERE_SHELL_NAME = "sphereShell";
    private static string FIREBALL_NAME = "fireball";
    private static string SHOCKWAVE_NAME = "shockwave";
    private static string FOG_NAME = "fog";

    private KamehamehaExplosionLightHandler kamehamehaExplosionLightHandler;
    private KamehamehaExplosionSoundHandler kamehamehaExplosionSoundHandler;

    private float size;

    private float destroyTime;
    private ParticleSystem innerCore;
    private ParticleSystem outterCore;
    private ParticleSystem lines;
    private ParticleSystem lightning;
    private ParticleSystem sphereShell;
    private ParticleSystem fireball;
    private ParticleSystem shockwave;
    private ParticleSystem fog;
    private SphereCollider hitbox;

    void Awake() {
        kamehamehaExplosionLightHandler = GetComponent<KamehamehaExplosionLightHandler>();
        kamehamehaExplosionSoundHandler = GetComponent<KamehamehaExplosionSoundHandler>();
        initializeParticleSystems();
        hitbox = GetComponent<SphereCollider>();
    }

    public void updateSize(float newSize) {
        photonView.RPC("RPC_updateSize", PhotonTargets.All, newSize);
        GetComponent<KamehamehaExplosionDamageDealer>().setSize(newSize);
    }

    [PunRPC]
    private void RPC_updateSize(float newSize) {
        size = newSize;
        calculateDestroyTime();
        Invoke("destroy", destroyTime);

        updateInnerCore();
        updateOutterCore();
        updateLines();
        updateLightning();
        updateSphereShell();
        updateFireball();
        updateShockwave();
        updateFog();
        updateHitbox();

        kamehamehaExplosionLightHandler.updateLights(size);
        kamehamehaExplosionSoundHandler.playSound(size);
    }

    private void calculateDestroyTime() {
        destroyTime = ScaleRange.scale(0, KamehamehaHandler.MAX_SIZE, KamehamehaHandler.EXPLOSION_MIN_DESTROY_TIME, KamehamehaHandler.EXPLOSION_MAX_DESTROY_TIME, size);
    }

    private void destroy() {
        Destroy(gameObject);
    }

    private void updateInnerCore() {
        innerCore.startSize *= size;
        innerCore.startLifetime = destroyTime;
    }
    private void updateOutterCore() {
        outterCore.startSize *= size;
        outterCore.startLifetime = destroyTime;
    }

    private void updateLines() {
        lines.startSize *= size;
        lines.startLifetime = destroyTime;
    }

    private void updateLightning() {
        lightning.startSize = lightning.startSize + lightning.startSize * size * 0.25f;
        float scale = 1 + size * 0.65f;
        lightning.gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void updateSphereShell() {
        sphereShell.startSize *= size;
    }

    private void updateFireball() {
        fireball.startSize *= size;
    }

    private void updateShockwave() {
        shockwave.startSize *= size;
    }

    private void updateFog() {
        fog.startSize *= size;
    }

    private void updateHitbox() {
        float radius = ScaleRange.scale(KamehamehaHandler.MIN_SIZE, KamehamehaHandler.MAX_SIZE, 3f, 26f, size);
        hitbox.radius = radius;
    }

    private void initializeParticleSystems() {
        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>()) {
            if (OUTTER_CORE_NAME.Equals(ps.name)) {
                outterCore = ps;
            }
            else if (LINES_NAME.Equals(ps.name)) {
                lines = ps;
            }
            else if (LIGHTNING_NAME.Equals(ps.name)) {
                lightning = ps;
            }
            else if (SPHERE_SHELL_NAME.Equals(ps.name)) {
                sphereShell = ps;
            }
            else if (FIREBALL_NAME.Equals(ps.name)) {
                fireball = ps;
            }
            else if (SHOCKWAVE_NAME.Equals(ps.name)) {
                shockwave = ps;
            }
            else if (FOG_NAME.Equals(ps.name)) {
                fog = ps;
            }
        }
        innerCore = GetComponent<ParticleSystem>();
        if (outterCore == null || lines == null || lightning == null || sphereShell == null || fireball == null || shockwave == null || fog == null) {
            Debug.LogError("KamehamehaExplosion: particles not found");
        }
    }

    public float getDestroyTime() {
        return destroyTime;
    }
}
