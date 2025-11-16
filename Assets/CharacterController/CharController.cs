using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]

[RequireComponent(typeof(FlyUpDownParticleHandler))]
[RequireComponent(typeof(FootstepParticleHandler))]
[RequireComponent(typeof(SprintTraceParticleHandler))]
[RequireComponent(typeof(LookIKHandler))]

[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(MovementAnimator))]

[RequireComponent(typeof(CameraDeterminer))]
[RequireComponent(typeof(CameraMouseClickInputHandler))]
[RequireComponent(typeof(CameraMouseWheelSmoother))]
[RequireComponent(typeof(CameraMouseXSmoother))]
[RequireComponent(typeof(CameraMouseYSmoother))]
[RequireComponent(typeof(CameraMouseYSmoother))]
[RequireComponent(typeof(CameraPivotPositionDeterminer))]
[RequireComponent(typeof(CameraPositionDeterminer))]
[RequireComponent(typeof(CharacterCameraHandler))]

[RequireComponent(typeof(CameraOcculationChecker))]
[RequireComponent(typeof(CameraPlaneClipper))]
[RequireComponent(typeof(CharacterFadeHandler))]
[RequireComponent(typeof(CharacterViewFrustumHandler))]
[RequireComponent(typeof(MouseYConstrainDeterminer))]
[RequireComponent(typeof(TagDependedOcculationHandler))]

[RequireComponent(typeof(HealthHandler))]
[RequireComponent(typeof(KiHandler))]
[RequireComponent(typeof(StaminaHandler))]

[RequireComponent(typeof(DamageHandler))]
[RequireComponent(typeof(CharacterInvicibleTimer))]

[RequireComponent(typeof(DeathHandler))]
[RequireComponent(typeof(RagdollMatcher))]
[RequireComponent(typeof(RagdollReplacer))]

[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(AxisInputHandler))]
[RequireComponent(typeof(ActionInputHandler))]
[RequireComponent(typeof(MouseInputHandler))]

[RequireComponent(typeof(MovementUpdater))]
[RequireComponent(typeof(MovementHandler))]
[RequireComponent(typeof(AirborneMovementHandler))]
[RequireComponent(typeof(FlyingMovementHandler))]
[RequireComponent(typeof(GroundMovementHandler))]
[RequireComponent(typeof(MoveSpeedDeterminer))]
[RequireComponent(typeof(RotationMovementHandler))]
[RequireComponent(typeof(IsGroundedDeterminer))]
[RequireComponent(typeof(CameraFlyingMovementHandler))]
[RequireComponent(typeof(TerrainDeterminer))]

[RequireComponent(typeof(CharacterAudioPlayer))]

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PhotonCharacterComponentDeactivator))]
[RequireComponent(typeof(PhotonCharacterSync))]
[RequireComponent(typeof(PhotonTransformView))]
[RequireComponent(typeof(PhotonAnimatorView))]


public abstract class CharController : MonoBehaviour {
    public abstract Character getCharacter();
    public abstract string getRagdollPrefabName();

    public abstract string getSpine1Name();
    public abstract string getGeoGroupName();

    void Awake() {
        checkTag();
        checkLayer();
    }

    private void checkTag() {
        if (gameObject.tag != "Player") {
            Debug.LogError("Character must have Tag 'Player'");
        }
    }

    private void checkLayer() {
        if (gameObject.layer != Layers.PLAYER_HITBOX) {
            Debug.LogError("Character must have Layer 'PlayerHitbox'");
        }
    }
}
