using System;
using UnityEngine;

[RequireComponent(typeof(Thinksquirrel.CShake.CameraShake))]
[RequireComponent(typeof(CameraShaker))]

[RequireComponent(typeof(GokuEyeBlinkBlendShape))]

[RequireComponent(typeof(KamehamehaAnimator))]
[RequireComponent(typeof(KamehamehaCursor))]
[RequireComponent(typeof(KamehamehaHandler))]
[RequireComponent(typeof(KamehamehaSpawner))]
[RequireComponent(typeof(KamehamehaCaster))]
[RequireComponent(typeof(KamehamehaShooter))]
[RequireComponent(typeof(KamehamehaTargetDeterminer))]
[RequireComponent(typeof(KamehamehaTargetValidator))]
[RequireComponent(typeof(KamehamehaCooldownTimer))]
[RequireComponent(typeof(KamehamehaCameraShaker))]
[RequireComponent(typeof(HandBallErruptionIncreaser))]
[RequireComponent(typeof(SkyLightDimmer))]
[RequireComponent(typeof(ThunderboltSpawner))]
[RequireComponent(typeof(KamehamehaAngleValidator))]
[RequireComponent(typeof(KamehamehaRangeValidator))]
[RequireComponent(typeof(KamehamehaCursor))]
[RequireComponent(typeof(KamehamehaSoundHandler))]
[RequireComponent(typeof(KamehamehaInputHandler))]

[RequireComponent(typeof(PunchInputHandler))]
[RequireComponent(typeof(PunchHandler))]
[RequireComponent(typeof(PunchTimer))]
[RequireComponent(typeof(PunchAnimator))]

public class GokuController : CharController {
    public override Character getCharacter() {
        return GokuCharacterModel.character;
    }

    public override string getRagdollPrefabName() {
        return GokuCharacterModel.ragdollPrefabName;
    }

    public override string getSpine1Name() {
        return GokuCharacterModel.spine1Name;
    }

    public override string getGeoGroupName() {
        return GokuCharacterModel.geoGroupName;
    }
}

