using UnityEngine;

[RequireComponent(typeof(TrunksEyeBlinkBlendShape))]

[RequireComponent(typeof(DrawSwordHandler))]
[RequireComponent(typeof(DrawSwordAnimator))]
[RequireComponent(typeof(DrawSwordAttacher))]
[RequireComponent(typeof(DrawSwordTimer))]

[RequireComponent(typeof(HoldSwordAnimator))]
[RequireComponent(typeof(SwordIKHandler))]

[RequireComponent(typeof(SwordTrail))]

[RequireComponent(typeof(SwordHitBoxController))]

[RequireComponent(typeof(PunchInputHandler))]
[RequireComponent(typeof(PunchHandler))]
[RequireComponent(typeof(PunchTimer))]
[RequireComponent(typeof(PunchAnimator))]

[RequireComponent(typeof(JacketToggler))]

[RequireComponent(typeof(TrunksRagdollReplacer))]


public class TrunksController : CharController {
    public override Character getCharacter() {
        return TrunksCharacterModel.character;
    }

    public override string getRagdollPrefabName() {
        return TrunksCharacterModel.ragdollPrefabName;
    }

    public override string getSpine1Name() {
        return TrunksCharacterModel.spine1Name;
    }

    public override string getGeoGroupName() {
        return TrunksCharacterModel.geoGroupName;
    }

}
